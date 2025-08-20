using CBA_app.Views;
using CBA_app.Views.Dashboard;
using System;
using Microsoft.Maui.Controls;

namespace CBA_app.Templates
{
    public class TabBarl : ContentView
    {
        public Command<string> TabCommand { get; set; }
        private Button btnInicio;
        private Button btnDashboard;
        private ImageButton imgButton;

        private string _activePage;
        public string ActivePage
        {
            get => _activePage;
            set
            {
                _activePage = value;
                UpdateButtonColors();
            }
        }

        public TabBarl()
        {
            TabCommand = new Command<string>(OnTabSelected);

            var grid = new Grid
            {
                ColumnSpacing = 0,
                BackgroundColor = Application.Current.Resources.TryGetValue("Primary", out var primary) ? (Color)primary : Colors.Blue,
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = GridLength.Auto },
                    new ColumnDefinition { Width = GridLength.Star }
                }
            };

            btnInicio = new Button
            {
                Text = "Inicio",
                BackgroundColor = Colors.Transparent,
                TextColor = Colors.White,
                Command = TabCommand,
                CommandParameter = "Inicio"
            };
            grid.Add(btnInicio, 0, 0);

            var frame = new Frame
            {
                CornerRadius = 50,
                Padding = 0,
                Margin = new Thickness(0, -20, 0, 0),
                HeightRequest = 50,
                WidthRequest = 50,
                BorderColor = Application.Current.Resources.TryGetValue("Primary", out primary) ? (Color)primary : Colors.Blue
            };

            imgButton = new ImageButton
            {
                Source = "more_icon.png",
                HeightRequest = 50,
                WidthRequest = 50,
                Command = TabCommand,
                CommandParameter = "Servicios"
            };
            frame.Content = imgButton;
            grid.Add(frame, 1, 0);

            btnDashboard = new Button
            {
                Text = "Dashboard",
                BackgroundColor = Colors.Transparent,
                TextColor = Colors.White,
                Command = TabCommand,
                CommandParameter = "Dashboard"
            };
            grid.Add(btnDashboard, 2, 0);

            Content = grid;

            // Inicializa el color según la página activa
            ActivePage = "Inicio";
        }

        private async void OnTabSelected(string tab)
        {
            ActivePage = tab;
            switch (tab)
            {
                case "Inicio":
                    await Shell.Current.GoToAsync($"//{nameof(Bienvenida)}");
                    break;
                case "Dashboard":
                    await Shell.Current.GoToAsync($"//{nameof(DashboardPage)}");
                    break;
                case "Servicios":
                    await Shell.Current.GoToAsync($"//{nameof(SinInternet)}");
                    break;
            }
        }

        private void UpdateButtonColors()
        {
            var accentColor = Application.Current.Resources.TryGetValue("Secondary", out var secondary) ? (Color)secondary : Colors.Orange;
            var defaultColor = Colors.White;

            btnInicio.TextColor = ActivePage == "Inicio" ? accentColor : defaultColor;
            btnDashboard.TextColor = ActivePage == "Dashboard" ? accentColor : defaultColor;
            imgButton.BackgroundColor = ActivePage == "Servicios" ? accentColor : Colors.Transparent;
        }
    }
}
