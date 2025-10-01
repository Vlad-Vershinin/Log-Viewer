using Avalonia.Markup;
using Avalonia.Media;
using client.services.interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.ViewModels
{
    public class TextColorConverter : IValueConverter
    {
        private readonly INavigationService _navigationService;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SolidColorBrush brush)
            {
                // Определяем контрастный цвет текста в зависимости от яркости фона
                var color = brush.Color;
                var brightness = (color.R * 0.299 + color.G * 0.587 + color.B * 0.114) / 255;

                return brightness > 0.5 ? Brushes.Black : Brushes.White;
            }

            return Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }


        public TextColorConverter(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
