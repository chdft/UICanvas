using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace UICanvas
{
	class ScreenPositionSubtractionConverter : DependencyObject, IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value is Point && BaseScreen != null)
			{
				Point typedValue = ((Point)value);
				return new Point(typedValue.X - BaseScreen.Position.X, typedValue.Y - BaseScreen.Position.Y);
			}
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		
		public Screen BaseScreen
		{
			get { return (Screen)GetValue(BaseScreenProperty); }
			set { SetValue(BaseScreenProperty, value); }
		}
		public static readonly DependencyProperty BaseScreenProperty =
			DependencyProperty.Register("BaseScreen", typeof(Screen), typeof(ScreenPositionSubtractionConverter), new PropertyMetadata());
	}
	/*class DoubleSubtractionConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value is double && parameter is double)
			{
				return ((double)value) - ((double)parameter);
			}
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}*/
}
