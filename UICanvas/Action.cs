using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
