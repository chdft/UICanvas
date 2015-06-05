using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace UICanvas
{
	class ActionTypeConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value is Action.ActionType)
			{
				Action.ActionType type = (Action.ActionType)value;
				switch (type)
				{
					default:
					case Action.ActionType.ClickOrTap:
						return Brushes.Red;
					case Action.ActionType.Click:
						return Brushes.Blue;
					case Action.ActionType.Tap:
						return Brushes.Green;
					case Action.ActionType.DoubleClick:
						return Brushes.Violet;
					case Action.ActionType.RightClick:
						return Brushes.Yellow;
					case Action.ActionType.LongTap:
						return Brushes.GreenYellow;
					case Action.ActionType.Drop:
						return Brushes.Beige;
				}
			}
			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
