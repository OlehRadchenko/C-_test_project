using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Models;
using SportsStore.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using System.Text.Json;
using Microsoft.AspNetCore.Routing;

namespace SportsStore.Tests
{
    public class CartTests
    {
        [Fact]
        public void Can_Add_New_Lines()
        {
            // Przygotowanie — utworzenie produktów testowych.
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };

            // Przygotowanie — utworzenie nowego koszyka.
            Cart target = new Cart();
            // Działanie.
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            CartLine[] results = target.Lines.ToArray();
            // Asercje.
            Assert.Equal(2, results.Length);
            Assert.Equal(p1, results[0].Product);
            Assert.Equal(p2, results[1].Product);
        }

        [Fact]
        public void Correct_Sum_Lines()
        {
            // Przygotowanie — utworzenie produktów testowych.
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100.59M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 200.99M };

            // Przygotowanie — utworzenie nowego koszyka.
            Cart target = new Cart();
            // Działanie.
            target.AddItem(p1, 1);
            target.AddItem(p2, 2);
            decimal wynik = 502.57M;
            // Asercje.
            Assert.Equal(wynik, target.ComputeTotalValue());
        }

        [Fact]
        public void Can_Add_Quantity_For_Existing_Products()
        {
            // Przygotowanie — utworzenie produktów testowych.
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Product p3 = new Product { ProductID = 3, Name = "P3" };
            // Przygotowanie — utworzenie nowego koszyka.
            Cart target = new Cart();
            // Działanie.
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 11);
            target.AddItem(p3, 6);
            CartLine[] results = target.Lines.ToArray();
            // Asercje.
            Assert.Equal(3, results.Length);
            Assert.Equal(p1, results[0].Product);
            Assert.Equal(p2, results[1].Product);
            Assert.Equal(p3, results[2].Product);
            Assert.Equal(12, results[0].Quantity);
            Assert.Equal(1, results[1].Quantity);
            Assert.Equal(6, results[2].Quantity);
        }

        [Fact]
        public void Can_Remove_Line()
        {
            // Przygotowanie — tworzenie produktów testowych.
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Product p3 = new Product { ProductID = 3, Name = "P3" };
            // Przygotowanie — utworzenie nowego koszyka.
            Cart target = new Cart();
            // Przygotowanie — dodanie kilku produktów do koszyka.
            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);
            // Działanie.
            target.RemoveLine(p2);
            // Asercje.
            Assert.Empty(target.Lines.Where(c => c.Product == p2));
            Assert.Equal(2, target.Lines.Count());
        }

        [Fact]
        public void Can_Clear_Contents()
        {
            // Przygotowanie — tworzenie produktów testowych.
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };
            // Przygotowanie — utworzenie nowego koszyka.
            Cart target = new Cart();
            // Przygotowanie — dodanie kilku produktów do koszyka.
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            // Działanie — czyszczenie koszyka.
            target.Clear();
            // Asercje.
            Assert.Empty(target.Lines);
        }

        [Fact]
        public void Can_Load_Cart()
        {
            // Przygotowanie
            // — utworzenie imitacji repozytorium.
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Mock<IStoreRepository> mockRepo = new Mock<IStoreRepository>();
            mockRepo.Setup(m => m.Products).Returns((new Product[] {p1, p2}).AsQueryable<Product>());
            // Utworzenie koszyka na zakupy.
            Cart testCart = new Cart();
            testCart.AddItem(p1, 2);
            testCart.AddItem(p2, 1);
            // Utworzenie imitacji kontekstu strony i sesji.
            Mock<ISession> mockSession = new Mock<ISession>();
            byte[] data = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(testCart));
            mockSession.Setup(c => c.TryGetValue(It.IsAny<string>(), out data));
            Mock<HttpContext> mockContext = new Mock<HttpContext>();
            mockContext.SetupGet(c => c.Session).Returns(mockSession.Object);
            // Działanie.
            CartModel cartModel = new CartModel(mockRepo.Object, testCart);
            cartModel.OnGet("myUrl");
            // Asercje.
            Assert.Equal(2, cartModel.Cart.Lines.Count());
            Assert.Equal("myUrl", cartModel.ReturnUrl);
        }

        [Fact]
        public void Can_Update_Cart()
        {
            // Przygotowanie
            // — utworzenie imitacji repozytorium.
            Mock<IStoreRepository> mockRepo = new Mock<IStoreRepository>();
            mockRepo.Setup(m => m.Products).Returns((new Product[] {
                new Product { ProductID = 1, Name = "P1" }
            }).AsQueryable<Product>());
            Cart testCart = new Cart();
            Mock<ISession> mockSession = new Mock<ISession>();
            mockSession.Setup(s => s.Set(It.IsAny<string>(), It.IsAny<byte[]>()))
            .Callback<string, byte[]>((key, val) => {
                testCart = JsonSerializer.Deserialize<Cart>(Encoding.UTF8.GetString(val));
            });
            Mock<HttpContext> mockContext = new Mock<HttpContext>();
            mockContext.SetupGet(c => c.Session).Returns(mockSession.Object);
            // Działanie.
            CartModel cartModel = new CartModel(mockRepo.Object, testCart);
            cartModel.OnPost(1, "myUrl");
            // Asercje.
            Assert.Single(testCart.Lines);
            Assert.Equal("P1", testCart.Lines.First().Product.Name);
            Assert.Equal(1, testCart.Lines.First().Quantity);
        }
    }
}
