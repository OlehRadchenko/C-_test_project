// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

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
    [global::Microsoft.AspNetCore.Components.RouteAttribute("/admin/products/edit/{id:long}")]
    [global::Microsoft.AspNetCore.Components.RouteAttribute("/admin/products/create")]
    public partial class Editor : OwningComponentBase<IStoreRepository>
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 43 "O:\4P\Pandora Case\TEST_VS22_LOGIN\SportsSln\SportsStore\Pages\Admin\Editor.razor"
       
    public IStoreRepository Repository => Service;
    [Inject]
    public NavigationManager NavManager { get; set; }
    [Parameter]
    public long Id { get; set; } = 0;
    public Product Product { get; set; } = new Product();
    protected override void OnParametersSet()
    {
        if (Id != 0)
        {
            Product = Repository.Products.FirstOrDefault(p => p.ProductID == Id);
        }
    }
    public void SaveProduct()
    {
        if (Id == 0)
        {
            Repository.CreateProduct(Product);
        }
        else
        {
            Repository.SaveProduct(Product);
        }
        NavManager.NavigateTo("/admin/products");
    }
    public string ThemeColor => Id == 0 ? "primary" : "warning";
    public string TitleText => Id == 0 ? "Dodawanie" : "Edytowanie";

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
