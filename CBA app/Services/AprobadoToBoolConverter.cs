using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace CBA_app.Services
{
    public class AprobadoToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is string str && str == "S";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is bool b && b) ? "S" : "N";
        }

        /// <summary>
        /// Convierte un valor string "S" o "N" a "Si" o "No" para mostrar en un Entry.
        /// </summary>
        /// <param name="value">Valor de entrada ("S" o "No").</param>
        /// <returns>"Si" si el valor es "S", "N" si el valor es "No", caso contrario el valor original.</returns>
        public static string ConvertAprobadoToEntryText(object value)
        {
            if (value is string str)
            {
                if (str == Definiciones.SiNo.Si)
                    return "Si";
                if (str == Definiciones.SiNo.No)
                    return "N";
            }
            return value?.ToString() ?? string.Empty;
        }
    }

    public class AlternatingColorConverter : IValueConverter
    {
        public Color EvenColor { get; set; } = Colors.Red;
        public Color OddColor { get; set; } = Color.FromArgb("#F5F5F5");

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int index)
                return index % 2 == 0 ? EvenColor : OddColor;
            return EvenColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    /// <summary>
    /// Convierte el valor "S" a "Si" y "N" a "N" para mostrar en un Entry o Label.
    /// </summary>
    public class AprobadoToSiNoConverter : IValueConverter
    {
        /// <summary>
        /// Convierte "S" a "Si", "N" a "N", cualquier otro valor se retorna como string.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                if (str == "S")
                    return "Si";
                if (str == "N")
                    return "No";
            }
            return value?.ToString() ?? string.Empty;
        }

        /// <summary>
        /// Convierte "Si" a "S", "N" a "N", cualquier otro valor se retorna como string.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                if (str.Equals("Si", StringComparison.OrdinalIgnoreCase))
                    return "S";
                if (str.Equals("No", StringComparison.OrdinalIgnoreCase))
                    return "N";
            }
            return value?.ToString() ?? string.Empty;
        }
    }
}
