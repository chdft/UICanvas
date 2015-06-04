using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
	/// Interaktionslogik für CanvasControl.xaml
	/// </summary>
	public partial class CanvasControl : UserControl, INotifyPropertyChanged
	{
		public CanvasControl()
		{
			InitializeComponent();
		}

		public ObservableCollection<Screen> Screens
		{
			get { return (ObservableCollection<Screen>)GetValue(ScreensProperty); }
			set { SetValue(ScreensProperty, value); }
		}
		public static readonly DependencyProperty ScreensProperty =
			DependencyProperty.Register("Screens", typeof(ObservableCollection<Screen>), typeof(CanvasControl), new PropertyMetadata());

		public ObservableCollection<Action> Actions
		{
			get { return (ObservableCollection<Action>)GetValue(ActionsProperty); }
			set { SetValue(ActionsProperty, value); }
		}
		public static readonly DependencyProperty ActionsProperty =
			DependencyProperty.Register("Actions", typeof(ObservableCollection<Action>), typeof(CanvasControl), new PropertyMetadata());
		
		public double ActionOpacity
		{
			get { return (double)GetValue(ActionOpacityProperty); }
			set { SetValue(ActionOpacityProperty, value); }
		}
		public static readonly DependencyProperty ActionOpacityProperty =
			DependencyProperty.Register("ActionOpacity", typeof(double), typeof(CanvasControl), new PropertyMetadata(0.35));

		public Action PendingAction
		{
			get { return _PendingAction; }
			set { _PendingAction = value; NotifyPropertyChanged("PendingAction"); }
		}
		private Action _PendingAction;

		public event PropertyChangedEventHandler PropertyChanged;
		protected void NotifyPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		private void UIAddAction(Screen screen, Point position)
		{
			if (PendingAction == null)
			{
				PendingAction = new Action()
				{
					Origin = screen,
					Position = position
				};
			}
			else
			{
				if (screen != PendingAction.Origin)
				{
					PendingAction.Target = screen;
					if (Actions == null)
					{
						Actions = new ObservableCollection<Action>();
					}
					Actions.Add(PendingAction);
					PendingAction = null;
				}
				else
				{
					PendingAction = null;
				}
			}
		}

		private void ScreenControl_AddAction(object sender, ScreenControl.AddActionEventArgs e)
		{
			if (e.Source != null)
			{
				UIAddAction(e.Source, e.Position);
			}
		}

		private void MenuItemRemoveAction_Click(object sender, RoutedEventArgs e)
		{
			MenuItem menuItem = sender as MenuItem;
			if (menuItem != null)
			{
				Action action = menuItem.DataContext as Action;
				if (action != null && Actions.Contains(action))
				{
					Actions.Remove(action);
				}
			}
		}

		private void MenuItemRemoveScreen_Click(object sender, RoutedEventArgs e)
		{
			MenuItem menuItem = sender as MenuItem;
			if (menuItem != null)
			{
				Screen screen = menuItem.DataContext as Screen;
				if (screen != null && Screens.Contains(screen))
				{
					Screens.Remove(screen);
				}
			}
		}

		/*private void ScreenControl_StylusDown(object sender, StylusDownEventArgs e)
		{
			if (e.StylusDevice.StylusButtons.Count > 0 && e.StylusDevice.StylusButtons[0].StylusButtonState == StylusButtonState.Down)
			{
				ScreenControl originControl = sender as ScreenControl;
				if (originControl != null && originControl.Screen != null)
				{
					UIAddAction(originControl.Screen, e.GetPosition(originControl));
				}
			}
		}

		private void ScreenControl_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
		{
			ScreenControl originControl = sender as ScreenControl;
			if (originControl != null && originControl.Screen != null)
			{
				UIAddAction(originControl.Screen, e.GetPosition(originControl));
			}
		}*/
	}
}
