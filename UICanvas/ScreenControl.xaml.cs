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

		public delegate void AddActionHandler(object sender, AddActionEventArgs e);
		public event AddActionHandler AddAction;

		protected void OnAddAction(Point position)
		{
			if (AddAction != null)
			{
				AddAction(this, new AddActionEventArgs(Screen, position));
			}
		}

		private void Screen_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
		{
			OnAddAction(e.GetPosition(ScreenCanvas));
			e.Handled = true;
		}

		private void Screen_StylusDown(object sender, StylusDownEventArgs e)
		{
			if (e.StylusDevice.StylusButtons.Count > 0 && e.StylusDevice.StylusButtons[0].StylusButtonState == StylusButtonState.Down)
			{
				OnAddAction(e.GetPosition(ScreenCanvas));
				e.Handled = true;
			}
		}

		public class AddActionEventArgs : EventArgs
		{
			public Screen Source
			{
				get { return _Source; }
			}
			private readonly Screen _Source;

			public Point Position
			{
				get { return _Position; }
			}
			private readonly Point _Position;
			public AddActionEventArgs(Screen source, Point position)
			{
				this._Source = source;
				this._Position = position;
			}
		}
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
				Point p = item.Screen.Position;
				p.X += e.HorizontalChange;
				p.Y += e.VerticalChange;
				if (p.X < 0) { p.X = 0; }
				if (p.Y < 0) { p.Y = 0; }
				item.Screen.Position = p;
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
