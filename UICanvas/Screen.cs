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

		public int Height
		{
			get { return _Height; }
			set { _Height = value; NotifyPropertyChanged("Height"); }
		}
		private int _Height;

		public int Width
		{
			get { return _Width; }
			set { _Width = value; NotifyPropertyChanged("Width"); }
		}
		private int _Width;

		/// <summary>
		/// position relative to the top-left corner of the canvas
		/// </summary>
		public Point Position
		{
			get { return _Position; }
			set { _Position = value; NotifyPropertyChanged("Position"); }
		}
		private Point _Position;

		/// <summary>
		/// position of the center of this screen relative to the top-left corner of the canvas
		/// </summary>
		public Point CenterPosition
		{
			get
			{
				return new Point(Position.X + Width / 2, Position.Y + Height / 2);
			}
		}

		/// <summary>
		/// Collection of Strokes forming the content of this screen
		/// </summary>
		public System.Windows.Ink.StrokeCollection ContentStrokes
		{
			get { return _ContentStrokes; }
			set { _ContentStrokes = value; NotifyPropertyChanged("ContentStrokes"); }
		}
		private System.Windows.Ink.StrokeCollection _ContentStrokes;

		/// <summary>
		/// user defined title
		/// </summary>
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
