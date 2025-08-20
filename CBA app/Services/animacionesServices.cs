using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace CBA_app.Services
{
    public class animacionesServices
    {
        public static async void Rebote(VisualElement element)
        {
            for (int i = 0; i < 3; i++)
            {
                await element.TranslateTo(0, -30, 500, Easing.Linear);
                await element.TranslateTo(0, 0, 500, Easing.BounceOut);
            }
        }
        public static async Task Rebote(VisualElement element,int cantidad)
        {
            for (int i = 0; i < cantidad; i++)
            {
                await element.TranslateTo(0, -30, 500, Easing.Linear);
                await element.TranslateTo(0, 0, 500, Easing.BounceOut);
            }
        }
        public static async Task desplegar(VisualElement element)
        {
            if (!element.IsVisible)
            {
                element.TranslationY = -element.Height;
                element.IsVisible = true;
                await element.TranslateTo(0, 0, 500, Easing.Linear);
            }
            else
            {
                await element.TranslateTo(0, -element.Height, 500, Easing.Linear);
                element.IsVisible = false;
            }
        }

        public static async void Fade(VisualElement element)
        {
            await element.FadeTo(0, 300); // Desvanece a opacidad 0 en 1 segundo
            await element.FadeTo(1, 300); // Vuelve a opacidad 1 en 1 segundo
        }

        public static async void Scale(VisualElement element)
        {
            await element.ScaleTo(1.3
                , 500, Easing.CubicInOut); // Escala a 1.5x en 0.5 segundos
            await element.ScaleTo(1, 500, Easing.CubicInOut);   // Vuelve a escala 1 en 0.5 segundos
        }

        public static async void Rotate(VisualElement element)
        {
            await element.RotateTo(360, 1000); // Rota 360 grados en 1 segundo
            element.Rotation = 0;              // Resetea la rotación
        }

        public static async void Translate(VisualElement element)
        {
            await element.TranslateTo(100, 0, 500, Easing.CubicInOut); // Mueve 100 píxeles a la derecha en 0.5 segundos
            await element.TranslateTo(0, 0, 500, Easing.CubicInOut);   // Vuelve a la posición original en 0.5 segundos
        }

        public static async void ChangeColor(VisualElement element, Color fromColor, Color toColor)
        {
            var animation = new Animation(v =>
            {
                element.BackgroundColor = new Color(
                    fromColor.Red + (toColor.Red - fromColor.Red) * (float)v,
                    fromColor.Green + (toColor.Green - fromColor.Green) * (float)v,
                    fromColor.Blue + (toColor.Blue - fromColor.Blue) * (float)v,
                    fromColor.Alpha + (toColor.Alpha - fromColor.Alpha) * (float)v);
            }, 0, 1);
            animation.Commit(element, "ColorChange", 16, 1000, Easing.Linear);
        }
    }
}
