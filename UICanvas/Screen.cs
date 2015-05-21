using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UICanvas
{
	public class Screen : INotifyPropertyChanged
	{
		public Screen(Screen baseData = null)
		{
			if (baseData != null)
			{
				Height = baseData.Height;
				Width = baseData.Width;
				ContentStrokes = baseData.ContentStrokes;
				Title = baseData.Title;
			}
			if (ContentStrokes == null)
			{
				ContentStrokes = new System.Windows.Ink.StrokeCollection();
			}
		}

		public ObservableCollection<Action> Actions
		{
			get { return _Actions; }
			set { _Actions = value; NotifyPropertyChanged("Actions"); }
		}
		private ObservableCollection<Action> _Actions;

		public int Height
		{
			get { return _Height; }
			set { _Height = value; NotifyPropertyChanged("Height"); NotifyPropertyChanged("TopOffset"); }
		}
		private int _Height;

		public int Width
		{
			get { return _Width; }
			set { _Width = value; NotifyPropertyChanged("Width"); NotifyPropertyChanged("LeftOffset"); }
		}
		private int _Width;

		public int XOffset
		{
			get { return _XOffset; }
			set { _XOffset = value; NotifyPropertyChanged("XOffset"); NotifyPropertyChanged("LeftOffset"); }
		}
		private int _XOffset;

		public int YOffset
		{
			get { return _YOffset; }
			set { _YOffset = value; NotifyPropertyChanged("YOffset"); NotifyPropertyChanged("TopOffset"); }
		}
		private int _YOffset;

		public int TopOffset
		{
			get { return YOffset + Height / 2; }
		}

		public int LeftOffset
		{
			get { return XOffset + Width / 2; }
		}

		public System.Windows.Ink.StrokeCollection ContentStrokes
		{
			get { return _ContentStrokes; }
			set { _ContentStrokes = value; NotifyPropertyChanged("ContentStrokes"); }
		}
		private System.Windows.Ink.StrokeCollection _ContentStrokes;

		public string Title
		{
			get { return _Title; }
			set { _Title = value; NotifyPropertyChanged("Title"); }
		}
		private string _Title;

		public override string ToString()
		{
			return Title;
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
