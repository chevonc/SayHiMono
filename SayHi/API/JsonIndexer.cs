using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace SayHi
{
	public class JsonIndexer
	{
		public Dictionary<string, JsonIndexer> r_values;
		private string m_value;
		private JsonIndexer (Dictionary<string,JsonIndexer> values)
		{
			r_values = values;
		}
		private JsonIndexer (string value)
		{
			m_value = value;
		}
		public JsonIndexer (JsonTextReader reader)
		{
			r_values = new Dictionary<string, JsonIndexer> ();
			Initialize (reader, r_values);
		}

		public static implicit operator string (JsonIndexer indexer)
		{
			return indexer.m_value; 
		}

		public Dictionary<string, JsonIndexer> JsonValues {
			get
			{
				return r_values;
			}
		}

		public JsonIndexer this [string key] {
			get
			{
				return JsonValues [key];
			}
			set
			{
				JsonValues [key] = value;
			}
		}

		private void Initialize (JsonTextReader reader, Dictionary<string, JsonIndexer> dict, bool isObjectReadStart = false)
		{
			if (!isObjectReadStart)
			{
				//ignore first start object
				while (reader.TokenType == JsonToken.StartObject || reader.TokenType == JsonToken.None)
				{
					reader.Read ();
				}
			}
			//start with first value object
			do
			{
				string key = reader.Value as string;
				if (key == null)
				{
					break;
				}
				switch (reader.TokenType)
				{
				case JsonToken.EndObject:
					return;
				case JsonToken.StartObject:
					{
						JsonIndexer indexer = new JsonIndexer (reader);
						dict [key] = indexer;
						Initialize (reader, indexer.JsonValues);
					}
					break;
				case JsonToken.PropertyName:
					{
						if (key != null)
						{
							reader.Read ();//reads values
//					if (reader.TokenType == JsonToken.EndObject)
//					{
//						continue;
//					}
//					
							if (reader.TokenType == JsonToken.StartObject)
							{
								JsonIndexer indexer = new JsonIndexer (reader);
								dict [key] = indexer;
								Initialize (reader, indexer.JsonValues, true ^ isObjectReadStart);
							}
							else
							{
								dict [key] = new JsonIndexer (reader.Value as string);
							}
						}
					}
					break;
				}
			}
			while (reader.Read());
		}
	}
}

