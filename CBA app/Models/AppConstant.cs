

using CBA_app.Templates;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using CBA_app.Services;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using CBA_app.Views;
using CBA_app.Views.Utiles;
using CBA_app.Views.OrdenServicio;
using DevExpress.DashboardCommon;
using CBA_app.Views.Dashboard;
using CBA_app.Views.Login;

namespace CBA_app.Models
{
    public class AppConstant
    {
        public async static Task AddFlyoutMenusDetails()
        {
            AppShell.Current.FlyoutHeader = new FlyoutHeader();

            
                var adminDashboardInfo = AppShell.Current.Items.FirstOrDefault(f => f.Route == nameof(Bienvenida));
                if (adminDashboardInfo != null)
                    AppShell.Current.Items.Remove(adminDashboardInfo);
            

            var flyoutItem = new FlyoutItem
            {
                Title = "Configuración",
                Route = nameof(Bienvenida),
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                Items =
                {
                  
                    new ShellContent
                    {
                        Icon = Icons.servicio,
                        Title = "Configuración",
                        IsVisible = SesionesClass.RolTiene("GIO APP"),
                        ContentTemplate = new DataTemplate(typeof(Bienvenida)),
                    }
                }
            };
            var flyoutItemDashboard = new FlyoutItem
            {
                Title = "Dashboard",
                Route = nameof(DashboardPage),
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                Items =
                {

                    new ShellContent
                    {
                        Icon = Icons.servicio,
                        Title = "Dashboard",
                        IsVisible = SesionesClass.RolTiene("GIO APP"),
                        ContentTemplate = new DataTemplate(typeof(DashboardPage)),
                    }
                }
            };
            var flyoutFormulario = new FlyoutItem
            {
                Title = "Formulario",
                Route = nameof(DesconsolidacionPage),
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                Items =
                {

                    new ShellContent
                    {
                        Icon = Icons.servicio,
                        Title = "Formulario",
                        IsVisible = SesionesClass.RolTiene("GIO APP"),
                        ContentTemplate = new DataTemplate(typeof(DesconsolidacionPage)),
                    }
                }
            };
            // FlyoutItem: Servicios
            var flyoutItemServicios = new FlyoutItem
            {
                Title = "Sin Internet",
                Route = nameof(SinInternet),
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                Items =
        {
            new ShellContent
            {
                Icon = Icons.Home,
                Title = "Sin Internet",
                IsVisible = true,
                ContentTemplate = new DataTemplate(typeof(SinInternet)),
            }
        }
            };
            // FlyoutItem: Servicios
            var flyoutItemSinPermiso = new FlyoutItem
            {
                Title = "Sin Permiso",
                Route = nameof(SinPermisoPage),
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                Items =
        {
            new ShellContent
            {
                Icon = Icons.perfil,
                Title = "Sin Permiso",
                IsVisible = true,
                ContentTemplate = new DataTemplate(typeof(SinPermisoPage)),
            }
        }
            };
            // FlyoutItem: Servicios
            var flyoutItemPerfil = new FlyoutItem
            {
                Title = "Perfil",
                Route = nameof(PerfilPage),
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                Items =
        {
            new ShellContent
            {
                Icon = Icons.perfil,
                Title = "Perfil",
                IsVisible = true,
                ContentTemplate = new DataTemplate(typeof(PerfilPage)),
            }
        }
            };
            if (!AppShell.Current.Items.Contains(flyoutItemDashboard))
                AppShell.Current.Items.Add(flyoutItemDashboard);

            if (!AppShell.Current.Items.Contains(flyoutItem))
                AppShell.Current.Items.Add(flyoutItem);

            if (!AppShell.Current.Items.Contains(flyoutFormulario))
                AppShell.Current.Items.Add(flyoutFormulario);

            if (!AppShell.Current.Items.Contains(flyoutItemServicios))
                AppShell.Current.Items.Add(flyoutItemServicios);

            if (!AppShell.Current.Items.Contains(flyoutItemSinPermiso))
                AppShell.Current.Items.Add(flyoutItemSinPermiso);

            if (!AppShell.Current.Items.Contains(flyoutItemPerfil))
                AppShell.Current.Items.Add(flyoutItemPerfil);



            AppShell.Current.Dispatcher.Dispatch(async () =>
            {

                await Shell.Current.GoToAsync($"//{nameof(Bienvenida)}");
            });
        }
    }
}
