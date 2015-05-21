using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UICanvas
{
	public class Action : INotifyPropertyChanged
	{
		public Screen Target
		{
			get { return _Target; }
			set { _Target = value; NotifyPropertyChanged("Target"); }
		}
		private Screen _Target;

		public Screen Origin
		{
			get { return _Origin; }
			set { _Origin = value; NotifyPropertyChanged("Origin"); }
		}
		private Screen _Origin;

		public Point Position
		{
			get { return _Position; }
			set { _Position = value; NotifyPropertyChanged("Position"); }
		}
		private Point _Position;

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
