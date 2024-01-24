using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using SportsStore.Components;
using SportsStore.Controllers;
using SportsStore.Infrastructure;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SportsStore.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void Can_Use_Repository()
        {
            // Przygotowanie.
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"}
            }).AsQueryable<Product>());
            HomeController controller = new HomeController(mock.Object);
            // Działanie.
            ProductsListViewModel result = controller.Index(null).ViewData.Model as ProductsListViewModel;
            // Asercja.
            Product[] prodArray = result.Products.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("P1", prodArray[0].Name);
            Assert.Equal("P2", prodArray[1].Name);
        }

        [Fact]
        public void Can_Paginate()
        {
            // Przygotowanie.
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"}
            }).AsQueryable<Product>());
            HomeController controller = new HomeController(mock.Object);
            controller.PageSize = 3;
            // Działanie.
            ProductsListViewModel result = controller.Index(null).ViewData.Model as ProductsListViewModel;
            // Asercje.
            Product[] prodArray = result.Products.ToArray();
            Assert.True(prodArray.Length == 3);
            Assert.Equal("P1", prodArray[0].Name);
            Assert.Equal("P2", prodArray[1].Name);
            Assert.Equal("P3", prodArray[2].Name);
        }

        [Fact]
        public void Can_Generate_Page_Links()
        {
            // Przygotowanie.
            var urlHelper = new Mock<IUrlHelper>();
            urlHelper.SetupSequence(x =>
            x.Action(It.IsAny<UrlActionContext>()))
            .Returns("Test/Page1")
            .Returns("Test/Page2")
            .Returns("Test/Page3")
            .Returns("Test/Page4");
            var urlHelperFactory = new Mock<IUrlHelperFactory>();
            urlHelperFactory.Setup(f =>
            f.GetUrlHelper(It.IsAny<ActionContext>()))
            .Returns(urlHelper.Object);
            PageLinkTagHelper helper =
            new PageLinkTagHelper(urlHelperFactory.Object)
            {
                PageModel = new PagingInfo
                {
                    CurrentPage = 4,
                    TotalItems = 31,
                    ItemsPerPage = 10
                },
                PageAction = "Test"
            };
            TagHelperContext ctx = new TagHelperContext(
            new TagHelperAttributeList(),
            new Dictionary<object, object>(), "");
            var content = new Mock<TagHelperContent>();
            TagHelperOutput output = new TagHelperOutput("div",
            new TagHelperAttributeList(),
            (cache, encoder) => Task.FromResult(content.Object));
            // Działanie.
            helper.Process(ctx, output);
            // Asercje.
            Assert.Equal(@"<a href=""Test/Page1"">1</a>"
            + @"<a href=""Test/Page2"">2</a>"
            + @"<a href=""Test/Page3"">3</a>"
            + @"<a href=""Test/Page4"">4</a>",
            output.Content.GetContent());
        }

        [Fact]
        public void Can_Send_Pagination_View_Model()
        {
            // Przygotowanie.
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"}
            }).AsQueryable<Product>());
            // Przygotowanie.
            HomeController controller =
            new HomeController(mock.Object) { PageSize = 3 };
            // Działanie.
            ProductsListViewModel result =
            controller.Index(null, 2).ViewData.Model as ProductsListViewModel;
            // Asercje.
            PagingInfo pageInfo = result.PagingInfo;
            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages);
        }

        [Fact]
        public void Can_Filter_Products()
        {
            // Przygotowanie.
            // Utworzenie imitacji repozytorium.
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {ProductID = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductID = 2, Name = "P2", Category = "Cat2"},
                new Product {ProductID = 3, Name = "P3", Category = "Cat1"},
                new Product {ProductID = 4, Name = "P4", Category = "Cat2"},
                new Product {ProductID = 5, Name = "P5", Category = "Cat3"}
            }).AsQueryable<Product>());
            // Przygotowanie — utworzenie kontrolera i ustawienie 3-elementowej strony.
            HomeController controller = new HomeController(mock.Object);
            controller.PageSize = 3;
            // Działanie.
            Product[] result = (controller.Index("Cat1").ViewData.Model as ProductsListViewModel).Products.ToArray();
            // Asercje.
            Assert.Equal(2, result.Length);
            Assert.True(result[0].Name == "P1" && result[0].Category == "Cat1");
            Assert.True(result[1].Name == "P3" && result[1].Category == "Cat1");
        }

        [Fact]
        public void Can_Select_Categories()
        {
            // Przygotowanie.
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {ProductID = 1, Name = "P1", Category = "Jabłka"},
                new Product {ProductID = 2, Name = "P2", Category = "Jabłka"},
                new Product {ProductID = 3, Name = "P3", Category = "Śliwki"},
                new Product {ProductID = 4, Name = "P4", Category = "Jabłka"},
            }).AsQueryable<Product>());
            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);
            // Działanie — pobranie zbioru kategorii.
            string[] results = ((IEnumerable<string>)(target.Invoke() as ViewViewComponentResult).ViewData.Model).ToArray();
            // Asercje.
            Assert.True(Enumerable.SequenceEqual(new string[] { "Jabłka", "Śliwki" }, results));
        }

        [Fact]
        public void Indicates_Selected_Category()
        {
            // Przygotowanie.
            string categoryToSelect = "Jabłka";
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {ProductID = 1, Name = "P1", Category = "Jabłka"},
                new Product {ProductID = 3, Name = "P3", Category = "Śliwki"},
                new Product {ProductID = 4, Name = "P2", Category = "Pomarańcze"},
            }).AsQueryable<Product>());
            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);
            target.ViewComponentContext = new ViewComponentContext
            {
                ViewContext = new ViewContext
                {
                    RouteData = new Microsoft.AspNetCore.Routing.RouteData()
                }
            };
            target.RouteData.Values["category"] = categoryToSelect;
            // Działanie.
            string result = (string)(target.Invoke() as ViewViewComponentResult).ViewData["SelectedCategory"];
            // Asercje.
            Assert.Equal(categoryToSelect, result);
        }

        [Fact]
        public void Generate_Category_Specific_Product_Count()
        {
            // Przygotowanie.
            Mock<IStoreRepository> mock = new Mock<IStoreRepository> ();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product {ProductID = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductID = 2, Name = "P2", Category = "Cat1"},
                new Product {ProductID = 3, Name = "P3", Category = "Cat1"},
                new Product {ProductID = 4, Name = "P4", Category = "Cat1"},
                new Product {ProductID = 5, Name = "P5", Category = "Cat3"}
            }).AsQueryable<Product>());
            HomeController target = new HomeController(mock.Object);
            target.PageSize = 3;
            Func<ViewResult, ProductsListViewModel> GetModel = result => result?.ViewData?.Model as ProductsListViewModel;
            // Działanie.
            int? res1 = GetModel(target.Index("Cat1"))?.PagingInfo.TotalPages;
            int? res2 = GetModel(target.Index("Cat2"))?.PagingInfo.TotalPages;
            int? res3 = GetModel(target.Index("Cat3"))?.PagingInfo.TotalPages;
            int? resAll = GetModel(target.Index(null))?.PagingInfo.TotalPages;
            // Asercje.
            Assert.Equal(2, res1);
            Assert.Equal(0, res2);
            Assert.Equal(1, res3);
            Assert.Equal(2, resAll);
        }
    }
}
