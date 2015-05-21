using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UICanvas
{
	class InkStrokeConverter : JsonConverter
	{
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (objectType == typeof(System.Windows.Ink.StrokeCollection))
			{
				using (MemoryStream ms = new MemoryStream())
				{
					byte[] data = Convert.FromBase64String((string)reader.Value);
					ms.Write(data, 0, data.Length);
					ms.Position = 0;
					return new System.Windows.Ink.StrokeCollection(ms);
				}
			}
			return null;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			System.Windows.Ink.StrokeCollection typedValue = value as System.Windows.Ink.StrokeCollection;
			if (typedValue != null)
			{
				using (MemoryStream ms = new MemoryStream())
				{
					typedValue.Save(ms, false);
					writer.WriteValue(Convert.ToBase64String(ms.ToArray()));
				}
			}
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(System.Windows.Ink.StrokeCollection);
		}
	}
}
