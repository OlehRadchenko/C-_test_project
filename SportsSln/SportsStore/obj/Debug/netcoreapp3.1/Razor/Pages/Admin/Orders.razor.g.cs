#pragma checksum "O:\4P\Pandora Case\TEST_VS22_LOGIN\SportsSln\SportsStore\Pages\Admin\Orders.razor" "{8829d00f-11b8-4213-878b-770e8597ac16}" "75a11021882bb6d36dacbeab2ae6db1a556d2b84e09777b58b67ca9115088ff1"
// <auto-generated/>
#pragma warning disable 1591
namespace SportsStore.Pages.Admin
{
    #line hidden
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
#nullable restore
#line 1 "O:\4P\Pandora Case\TEST_VS22_LOGIN\SportsSln\SportsStore\Pages\Admin\_Imports.razor"
using Microsoft.AspNetCore.Components;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "O:\4P\Pandora Case\TEST_VS22_LOGIN\SportsSln\SportsStore\Pages\Admin\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "O:\4P\Pandora Case\TEST_VS22_LOGIN\SportsSln\SportsStore\Pages\Admin\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "O:\4P\Pandora Case\TEST_VS22_LOGIN\SportsSln\SportsStore\Pages\Admin\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "O:\4P\Pandora Case\TEST_VS22_LOGIN\SportsSln\SportsStore\Pages\Admin\_Imports.razor"
using Microsoft.EntityFrameworkCore;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "O:\4P\Pandora Case\TEST_VS22_LOGIN\SportsSln\SportsStore\Pages\Admin\_Imports.razor"
using SportsStore.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Components.RouteAttribute("/admin/orders")]
    public partial class Orders : OwningComponentBase<IOrderRepository>
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenComponent<global::SportsStore.Pages.Admin.OrderTable>(0);
            __builder.AddAttribute(1, "TableTitle", (object)("Zamówienia niezrealizowane"));
            __builder.AddAttribute(2, "Orders", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Collections.Generic.IEnumerable<SportsStore.Models.Order>>(
#nullable restore
#line 4 "O:\4P\Pandora Case\TEST_VS22_LOGIN\SportsSln\SportsStore\Pages\Admin\Orders.razor"
                    UnshippedOrders

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(3, "ButtonLabel", (object)("Zrealizowane"));
            __builder.AddAttribute(4, "OrderSelected", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::Microsoft.AspNetCore.Components.EventCallback<global::System.Int32>>(global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create<global::System.Int32>(this, 
#nullable restore
#line 4 "O:\4P\Pandora Case\TEST_VS22_LOGIN\SportsSln\SportsStore\Pages\Admin\Orders.razor"
                                                                               ShipOrder

#line default
#line hidden
#nullable disable
            ))));
            __builder.CloseComponent();
            __builder.AddMarkupContent(5, "\r\n");
            __builder.OpenComponent<global::SportsStore.Pages.Admin.OrderTable>(6);
            __builder.AddAttribute(7, "TableTitle", (object)("Zamówienia zrealizowane"));
            __builder.AddAttribute(8, "Orders", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Collections.Generic.IEnumerable<SportsStore.Models.Order>>(
#nullable restore
#line 6 "O:\4P\Pandora Case\TEST_VS22_LOGIN\SportsSln\SportsStore\Pages\Admin\Orders.razor"
                    ShippedOrders

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(9, "ButtonLabel", (object)("Zeruj"));
            __builder.AddAttribute(10, "OrderSelected", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::Microsoft.AspNetCore.Components.EventCallback<global::System.Int32>>(global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create<global::System.Int32>(this, 
#nullable restore
#line 6 "O:\4P\Pandora Case\TEST_VS22_LOGIN\SportsSln\SportsStore\Pages\Admin\Orders.razor"
                                                                      ResetOrder

#line default
#line hidden
#nullable disable
            ))));
            __builder.CloseComponent();
            __builder.AddMarkupContent(11, "\r\n");
            __builder.OpenElement(12, "button");
            __builder.AddAttribute(13, "class", "btn btn-info");
            __builder.AddAttribute(14, "onclick", global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create<global::Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 7 "O:\4P\Pandora Case\TEST_VS22_LOGIN\SportsSln\SportsStore\Pages\Admin\Orders.razor"
                                         e => UpdateData()

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(15, "Odśwież dane");
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 8 "O:\4P\Pandora Case\TEST_VS22_LOGIN\SportsSln\SportsStore\Pages\Admin\Orders.razor"
       
    public IOrderRepository Repository => Service;
    public IEnumerable<Order> AllOrders { get; set; }
    public IEnumerable<Order> UnshippedOrders { get; set; }
    public IEnumerable<Order> ShippedOrders { get; set; }
    protected async override Task OnInitializedAsync()
    {
        await UpdateData();
    }
    public async Task UpdateData()
    {
        AllOrders = await Repository.Orders.ToListAsync();
        UnshippedOrders = AllOrders.Where(o => !o.Shipped);
        ShippedOrders = AllOrders.Where(o => o.Shipped);
    }
    public void ShipOrder(int id) => UpdateOrder(id, true);
    public void ResetOrder(int id) => UpdateOrder(id, false);
    private void UpdateOrder(int id, bool shipValue)
    {
        Order o = Repository.Orders.FirstOrDefault(o => o.OrderID == id);
        o.Shipped = shipValue;
        Repository.SaveOrder(o);
    }

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
