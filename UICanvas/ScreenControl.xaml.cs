using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UICanvas
{
	/// <summary>
	/// Interaktionslogik für ScreenControl.xaml
	/// </summary>
	public partial class ScreenControl : UserControl
	{
		public ScreenControl()
		{
			InitializeComponent();
		}
		public ScreenControl(Screen Screen)
		{
			InitializeComponent();
			this.Screen = Screen;
		}

		public Screen Screen
		{
			get { return (Screen)GetValue(ScreenProperty); }
			set { SetValue(ScreenProperty, value); }
		}
		public static readonly DependencyProperty ScreenProperty =
			DependencyProperty.Register("Screen", typeof(Screen), typeof(ScreenControl), new PropertyMetadata());


	}
	public class ScreenControlMoveThumb : Thumb
	{
		public ScreenControlMoveThumb()
		{
			DragDelta += new DragDeltaEventHandler(this.MoveThumb_DragDelta);
		}

		private void MoveThumb_DragDelta(object sender, DragDeltaEventArgs e)
		{
			ScreenControl item = this.DataContext as ScreenControl;

			if (item != null && item.Screen != null)
			{
				if (item.Screen.YOffset + (int)e.VerticalChange >= 0)
				{
					item.Screen.YOffset += (int)e.VerticalChange;
				}
				else
				{
					item.Screen.YOffset = 0;
				}
				if (item.Screen.XOffset + (int)e.HorizontalChange >= 0)
				{
					item.Screen.XOffset += (int)e.HorizontalChange;
				}
				else
				{
					item.Screen.XOffset = 0;
				}
			}
		}
	}
	public class ScreenControlResizeThumb : Thumb
	{
		public ScreenControlResizeThumb()
		{
			DragDelta += new DragDeltaEventHandler(this.MoveThumb_DragDelta);
		}

		private void MoveThumb_DragDelta(object sender, DragDeltaEventArgs e)
		{
			ScreenControl item = this.DataContext as ScreenControl;

			if (item != null && item.Screen != null)
			{
				if (item.Screen.Height + (int)e.VerticalChange >= 0)
				{
					item.Screen.Height += (int)e.VerticalChange;
				}
				else
				{
					item.Screen.Height = 0;
				}
				if (item.Screen.Width + (int)e.HorizontalChange >= 0)
				{
					item.Screen.Width += (int)e.HorizontalChange;
				}
				else
				{
					item.Screen.Width = 0;
				}
			}
		}
	}
}
