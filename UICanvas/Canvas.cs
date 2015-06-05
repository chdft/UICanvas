using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UICanvas
{
	public class Canvas : INotifyPropertyChanged
	{
		public string Title
		{
			get { return _Title; }
			set { _Title = value; NotifyPropertyChanged("Title"); }
		}
		private string _Title;

		public ObservableCollection<Screen> Screens
		{
			get { return _Screens; }
			set { _Screens = value; NotifyPropertyChanged("Screens"); }
		}
		private ObservableCollection<Screen> _Screens;

		public ObservableCollection<Action> Actions
		{
			get { return _Actions; }
			set { _Actions = value; NotifyPropertyChanged("Actions"); }
		}
		private ObservableCollection<Action> _Actions;

		private static JsonSerializerSettings SerializationSettings = new JsonSerializerSettings()
			{
				Converters = new[] { new InkStrokeConverter() },
				PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All,
#if DEBUG
				Formatting = Formatting.Indented
#endif
			};

		public void SaveToFile(string fileName)
		{
			if (string.IsNullOrEmpty(fileName)) { throw new ArgumentException("fileName should be a valid path"); }
			System.IO.File.WriteAllText(fileName, JsonConvert.SerializeObject(this, SerializationSettings));
		}
		public static Canvas FromFile(string fileName)
		{
			if (string.IsNullOrEmpty(fileName) || !System.IO.File.Exists(fileName)) { throw new ArgumentException("fileName should be a valid path"); }
			return JsonConvert.DeserializeObject<Canvas>(System.IO.File.ReadAllText(fileName), SerializationSettings);
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
