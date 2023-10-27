using CarListApp.Maui.Controls;
using CarListApp.Maui.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarListApp.Maui.Helpers
{
    public static class MenuBuilder
    {
        // role based menu building
        public static void BuildMenu()
        {
            // clear the current items in the shell
            Shell.Current.Items.Clear();

            // set flyout header
            Shell.Current.FlyoutHeader = new FlyoutHeader();

            // get user details
            var role = App.userInfo.Role;

            // add items per roles
            if (role.Equals("Administrator"))
            {
                var flyoutItem = new FlyoutItem()
                {
                    Title = "Car Management",
                    Route = nameof(MainPage),
                    FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                    Items =
                    {
                        new ShellContent
                        {
                            Icon = "dotnet_bot.svg",
                            Title = "Admin page 1",
                            ContentTemplate = new DataTemplate(typeof(MainPage))
                        },
                        new ShellContent
                        {
                            Icon = "dotnet_bot.svg",
                            Title = "Admin page 2",
                            ContentTemplate = new DataTemplate(typeof(MainPage))
                        },
                    }
                };

                if(!Shell.Current.Items.Contains(flyoutItem))
                {
                    Shell.Current.Items.Add(flyoutItem);
                }
            }
            
            if(role.Equals("User"))
            {
                var flyoutItem = new FlyoutItem()
                {
                    Title = "Car List",
                    Route = nameof(MainPage),
                    FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                    Items =
                    {
                        new ShellContent
                        {
                            Icon = "dotnet_bot.svg",
                            Title = "User page 1",
                            ContentTemplate = new DataTemplate(typeof(MainPage))
                        },
                        new ShellContent
                        {
                            Icon = "dotnet_bot.svg",
                            Title = "User page 2",
                            ContentTemplate = new DataTemplate(typeof(MainPage))
                        },
                    }
                };

                if (!Shell.Current.Items.Contains(flyoutItem))
                {
                    Shell.Current.Items.Add(flyoutItem);
                }
            }

            var logoutFlyoutItem = new FlyoutItem()
            {
                Title = "Logout",
                Route = nameof(LogoutPage),
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsSingleItem,
                Items =
                    {
                        new ShellContent
                        {
                            Icon = "dotnet_bot.svg",
                            Title = "Logout",
                            ContentTemplate = new DataTemplate(typeof(LogoutPage))
                        }
                    }
            };

            if (!Shell.Current.Items.Contains(logoutFlyoutItem))
            {
                Shell.Current.Items.Add(logoutFlyoutItem);
            }
        }
    }
}
