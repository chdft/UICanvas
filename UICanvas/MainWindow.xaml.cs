using System;
using System.Collections.Generic;
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
using Microsoft.Win32;

namespace UICanvas
{
	/// <summary>
	/// Interaktionslogik für MainWindow.xaml
	/// </summary>
	public partial class MainWindow : INotifyPropertyChanged
	{
		public MainWindow()
		{
			InitializeComponent();
			Canvas = new UICanvas.Canvas()
			{
				Screens = new System.Collections.ObjectModel.ObservableCollection<Screen>(),
				Actions = new System.Collections.ObjectModel.ObservableCollection<Action>()
			};
			Screen a = new Screen()
			{
				Width = 186,
				Height = 186,
				Position = new Point(10, 40),
				Title = "startup screen"
			};
			Canvas.Screens.Add(a);
#if DEBUG
			Screen b = new Screen()
			{
				Width = 186,
				Height = 200,
				Position = new Point(350, 32),
				Title = "screen1",
			};
			Canvas.Actions.Add(new Action()
			{
				Origin = a,
				Target = b,
				Position = new Point(20, 40)
			});
			Canvas.Screens.Add(b);
#endif
		}

		public Canvas Canvas
		{
			get { return _Canvas; }
			set { _Canvas = value; NotifyPropertyChanged("Canvas"); }
		}
		private Canvas _Canvas;

		public string PreviousFileName
		{
			get { return _PreviousFileName; }
			set { _PreviousFileName = value; NotifyPropertyChanged("PreviousFileName"); }
		}
		private string _PreviousFileName;

		private void ToolbarSave_Click(object sender, RoutedEventArgs e)
		{
			if (Canvas != null && !string.IsNullOrEmpty(PreviousFileName))
			{
				try
				{
					Canvas.SaveToFile(PreviousFileName);
				}
				catch (Exception ex)
				{
					MessageBox.Show("Couldn't save the Canvas: " + ex.Message, "IO Error");
				}
			}
		}

		private void ToolbarSaveAs_Click(object sender, RoutedEventArgs e)
		{
			if (Canvas != null)
			{
				SaveFileDialog sfd = new SaveFileDialog();
				sfd.CheckPathExists = true;
				sfd.Filter = "Canvas Files|*.cnvs|All Files|*";
				sfd.DefaultExt = ".cnvs";
				sfd.Title = "Save Canvas";
				if (sfd.ShowDialog() == true)
				{
					if (string.IsNullOrEmpty(Canvas.Title))
					{
						Canvas.Title = System.IO.Path.GetFileNameWithoutExtension(sfd.FileName);
					}
					try
					{
						Canvas.SaveToFile(sfd.FileName);
					}
					catch (Exception ex)
					{
						MessageBox.Show("Couldn't save the Canvas: " + ex.Message, "IO Error");
					}
					PreviousFileName = sfd.FileName;
				}
			}
		}

		private void ToolbarOpen_Click(object sender, RoutedEventArgs e)
		{
			if (Canvas != null)
			{
				OpenFileDialog ofd = new OpenFileDialog();
				ofd.CheckPathExists = true;
				ofd.CheckFileExists = true;
				ofd.Filter = "Canvas Files|*.cnvs|All Files|*";
				ofd.DefaultExt = ".cnvs";
				ofd.Title = "Open Canvas";
				if (ofd.ShowDialog() == true)
				{
					try
					{
						Canvas = Canvas.FromFile(ofd.FileName);
					}
					catch (Exception ex)
					{
						MessageBox.Show("Couldn't open the Canvas: " + ex.Message, "IO Error");
					}
					PreviousFileName = ofd.FileName;
				}
			}
		}

		private void ToolbarAddScreen_Click(object sender, RoutedEventArgs e)
		{
			if (Canvas != null)
			{
				if (Canvas.Screens == null)
				{
					Canvas.Screens = new System.Collections.ObjectModel.ObservableCollection<Screen>();
				}
				Canvas.Screens.Add(new Screen()
				{
					Position=new Point(10, 40),
					Width = 200,
					Height = 200
				});
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected void NotifyPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
