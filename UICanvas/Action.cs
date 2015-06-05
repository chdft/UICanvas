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
			set
			{
				if (_Origin != null)
				{
					_Origin.PropertyChanged -= _Origin_PropertyChanged;
				}
				_Origin = value;
				if (_Origin != null)
				{
					_Origin.PropertyChanged += _Origin_PropertyChanged;
				}
				NotifyPropertyChanged("Origin");
				NotifyPropertyChanged("AbsolutePosition");
			}
		}
		private Screen _Origin;

		/// <summary>
		/// position relative to the top-left corner of the canvas
		/// this value is calculated using the Origin screens position
		/// </summary>
		public Point AbsolutePosition
		{
			get
			{
				if (Origin == null) { throw new InvalidOperationException("Origin must not be null to calculate an absolute position"); }
				return new Point(Origin.Position.X + Position.X, Origin.Position.Y + Position.Y);
			}
			set
			{
				if (Origin == null) { throw new InvalidOperationException("Origin must not be null to calculate a realative position"); }
				Position = new Point(value.X - Origin.Position.X, value.Y - Origin.Position.Y);
			}
		}

		/// <summary>
		/// position relative to the top-left corner of the Origin screen
		/// </summary>
		public Point Position
		{
			get { return _Position; }
			set { _Position = value; NotifyPropertyChanged("Position"); NotifyPropertyChanged("AbsolutePosition"); }
		}
		private Point _Position;

		public ActionType Type
		{
			get { return _Type; }
			set { _Type = value; NotifyPropertyChanged("Type"); }
		}
		private ActionType _Type;

		public event PropertyChangedEventHandler PropertyChanged;
		protected void NotifyPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		void _Origin_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (string.IsNullOrEmpty(e.PropertyName) || e.PropertyName == "Position")
			{
				NotifyPropertyChanged("AbsolutePosition");
			}
		}

		public enum ActionType
		{
			ClickOrTap,
			Click,
			Tap,
			DoubleClick,
			RightClick,
			LongTap,
			Drop
		}
	}
}
