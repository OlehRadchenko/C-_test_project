#pragma checksum "O:\4P\Pandora Case\TEST_VS22_LOGIN\SportsSln\SportsStore\Pages\Admin\Routed.razor" "{8829d00f-11b8-4213-878b-770e8597ac16}" "6fde80763a6665ec7c4cfff5499ee0c921da0876afadade982897f5baa2ded99"
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
    public partial class Routed : global::Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenComponent<global::Microsoft.AspNetCore.Components.Routing.Router>(0);
            __builder.AddAttribute(1, "AppAssembly", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Reflection.Assembly>(
#nullable restore
#line 1 "O:\4P\Pandora Case\TEST_VS22_LOGIN\SportsSln\SportsStore\Pages\Admin\Routed.razor"
                     typeof(Startup).Assembly

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(2, "Found", (global::Microsoft.AspNetCore.Components.RenderFragment<Microsoft.AspNetCore.Components.RouteData>)((context) => (__builder2) => {
                __builder2.AddMarkupContent(3, "\r\n    ");
                __builder2.OpenComponent<global::Microsoft.AspNetCore.Components.RouteView>(4);
                __builder2.AddAttribute(5, "RouteData", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::Microsoft.AspNetCore.Components.RouteData>(
#nullable restore
#line 3 "O:\4P\Pandora Case\TEST_VS22_LOGIN\SportsSln\SportsStore\Pages\Admin\Routed.razor"
                           context

#line default
#line hidden
#nullable disable
                )));
                __builder2.AddAttribute(6, "DefaultLayout", (object)(global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Type>(
#nullable restore
#line 3 "O:\4P\Pandora Case\TEST_VS22_LOGIN\SportsSln\SportsStore\Pages\Admin\Routed.razor"
                                                   typeof(AdminLayout)

#line default
#line hidden
#nullable disable
                )));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(7, "\r\n ");
            }
            ));
            __builder.AddAttribute(8, "NotFound", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.AddMarkupContent(9, "\r\n    ");
                __builder2.AddMarkupContent(10, "<h4 class=\"bg-danger text-white text-center p-2\">\r\n        Nie znaleziono dopasowanej trasy\r\n    </h4>\r\n ");
            }
            ));
            __builder.CloseComponent();
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
