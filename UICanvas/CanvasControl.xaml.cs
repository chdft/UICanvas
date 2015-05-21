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

		private void ScreenControl_StylusDown(object sender, StylusDownEventArgs e)
		{
			if (e.StylusDevice.StylusButtons.Count > 0 && e.StylusDevice.StylusButtons[0].StylusButtonState == StylusButtonState.Down)
			{
				ScreenControl originControl = sender as ScreenControl;
				if (originControl != null && originControl.Screen != null)
				{
					if (PendingAction == null)
					{
						PendingAction = new Action()
						{
							Origin = originControl.Screen,
							Position = new Point(Width / 2, Height / 2)
						};
					}
					else
					{
						if (originControl.Screen != PendingAction.Origin)
						{
							PendingAction.Target = originControl.Screen;
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
			}
		}
	}
}
