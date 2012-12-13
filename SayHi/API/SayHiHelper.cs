using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Utilities;
using System.IO;
using System.Text;
using SayHi.API.Models;
using System.Net;
using GeneralClasses;

namespace SayHi.API
{
	public class SayHiHelper
	{
		private object m_registerUserState;

		public event Action<UserModel> OnRegisterUserCompleted;
		public event Action<Event> OnGetEventInfoCompleted;

		public const string APIBaseURL = "http://say-hi.herokuapp.com/api/";
		public const string APIVersionPath = "v1/";
		public const string RegisterUserPath = "registerUser/";
		public const string GetEventInfoPath = "event/getEvent/";
		public const string LoginUserPath = "getUserInfo/login/";
		private const string JSONBeginObject = "_[_";
		private const string JSONEndObject = "_]_";

		public SayHiHelper ()
		{
		}

		string CreateEndpointURL (string endpointPath)
		{
			return string.Format ("{0}{1}{2}", APIBaseURL, APIVersionPath, endpointPath);
		}

		static bool CompareStrings (JsonTextReader jtr, string value)
		{
			return string.Compare (jtr.Value as string, value) == 0;
		}

		public void RegisterUser (string username, string password, string firstName, string lastName, string emailAddress, string dob = "", 
		                         string summary = "", string interest1 = "", string interest2 = "", string interest3 = "")
		{
			string json = ParamsToJSON ("dob", dob, "summary", summary, "user", SayHiHelper.JSONBeginObject, "email", emailAddress,
			                           "first_name", firstName, "last_name", lastName, "username", username, "password", password,
			                           SayHiHelper.JSONEndObject);
			SayHiRestClient syrc = new SayHiRestClient (SayHiRestClient.HTTPPOSTMETHOD, CreateEndpointURL (RegisterUserPath), json);
			syrc.OnRestCallCompleted += (RestResult obj) => 
			{
				UserModel ret = null;
				if (!obj.IsSuccess)
				{
					ret = new UserModel (obj.IsSuccess, obj.Result);
				}
				else
				{
					try
					{
						ret = new UserModel (true);

						using (JsonTextReader jtr = new JsonTextReader(new StringReader(obj.Result)))
						{
							JsonIndexer idx = new JsonIndexer (jtr);
							ret.ID = idx ["id"];
//							while (jtr.Read())
//							{
//								if (jtr.TokenType == JsonToken.PropertyName && CompareStrings (jtr, "id"))
//								{
//									ret.ID = jtr.ReadAsString ();
//									break;
//								}
//							}						
						}
						//TODO:parse other fields

						ret.FirstName = firstName;
						ret.LastName = lastName;
						ret.EmailAddress = emailAddress;
						ret.DOBRaw = dob;
						ret.Summary = summary;
						ret.InterestOne = interest1;
						ret.InterestTwo = interest2;
						ret.InterestThree = interest3;
					}
					catch (Exception e)
					{
						ret = new UserModel (false, GenerateParseErrorMessage (e));
					}
				}

				SafeRaiseEvent (OnRegisterUserCompleted, ret);
			};

			syrc.SendRestRequest ();
		}

		public void LoginUser (string username, string password)
		{
			string json = ParamsToJSON ("username", username, "password", password);
			SayHiRestClient syrc = new SayHiRestClient (SayHiRestClient.HTTPPOSTMETHOD, CreateEndpointURL (LoginUserPath), json);
			syrc.OnRestCallCompleted += (RestResult obj) => 
			{
				UserModel ret = null;
				
				if (!obj.IsSuccess)
				{
					ret = new UserModel (obj.IsSuccess, obj.Result);
				}
				else
				{
					try
					{
						ret = new UserModel (true);
						
						using (JsonTextReader jtr = new JsonTextReader(new StringReader(obj.Result)))
						{
							JsonIndexer idx = new JsonIndexer (jtr);
							ret.DOBRaw = idx ["dob"];
							ret.EmailAddress = idx ["email"];
							ret.ID = idx ["id"];
							ret.LastName = idx ["lastName"];
						}
					}
					catch (Exception e)
					{
						ret = new UserModel (false, GenerateParseErrorMessage (e));
					}
				}
				
				SafeRaiseEvent (OnRegisterUserCompleted, ret);
				
			};
			syrc.SendRestRequest ();
		}

		static string GenerateParseErrorMessage (Exception e)
		{
			string msg = string.Format ("Falied to parse json. Error: {0}\nMessage: {1}", e.GetType (), e.Message);
			return msg;
		}

		public void GetEventInfo (string eventCode)
		{
			string json = ParamsToJSON ("event_code", eventCode ?? string.Empty);
			SayHiRestClient syrc = new SayHiRestClient (SayHiRestClient.HTTPPOSTMETHOD, CreateEndpointURL (GetEventInfoPath),
			                                           json);
			syrc.OnRestCallCompleted += (RestResult obj) => 
			{
				Event ret = null;

				if (!obj.IsSuccess)
				{
					ret = new Event (obj.IsSuccess, obj.Result);
				}
				else
				{
					try
					{
						ret = new Event (true);

						using (JsonTextReader jtr = new JsonTextReader(new StringReader(obj.Result)))
						{

							while (jtr.Read())
							{

							}
						}
					}
					catch (Exception e)
					{
						ret = new Event (false, GenerateParseErrorMessage (e));
					}
				}

				SafeRaiseEvent (OnGetEventInfoCompleted, ret);

			};
			syrc.SendRestRequest ();
		}

		//public void GetUserInfo(string id)

		void SafeRaiseEvent (Delegate eventToRaise, params object[] args)
		{
			var shadow = eventToRaise;
			if (shadow != null)
			{
				shadow.DynamicInvoke (args);
			}
		}

		string ParamsToJSON (params string[] keysAndValues)
		{
			if (keysAndValues == null || keysAndValues.Length == 0)
			{
				return "{}";
			}
			StringBuilder sb = new StringBuilder ();
			using (StringWriter sw = new StringWriter(sb))
			{
				using (JsonTextWriter wr = new JsonTextWriter(sw))
				{
					wr.WriteStartObject ();
					for (int i = 0; i < keysAndValues.Length; i+=2)
					{
						string key = keysAndValues [i];
						string prop = null;

						if (i < keysAndValues.Length - 1)
						{
							prop = keysAndValues [i + 1];
						}

						if (!WriteStartOrEndObject (key, wr))
						{
							wr.WritePropertyName (key);
						}
						if (prop != null)
						{
							if (!WriteStartOrEndObject (prop, wr))
							{
								wr.WriteValue (prop);
							}
						}
					}
					wr.WriteEndObject ();
					wr.Close ();
				}
			}

			return sb.ToString ();
		}

		private bool WriteStartOrEndObject (string value, JsonTextWriter wr)
		{
			if (value == JSONBeginObject)
			{
				wr.WriteStartObject ();
				return true;
			}
			else
			if (value == JSONEndObject)
			{
				wr.WriteEndObject ();

				return true;
			}

			return false;
		}

		bool IsValueToken (JsonToken token)
		{
			return token != JsonToken.Comment && token != JsonToken.EndArray
				&& token != JsonToken.EndConstructor && token != JsonToken.EndObject
				&& token != JsonToken.None && token != JsonToken.PropertyName 
				&& token != JsonToken.StartArray && token != JsonToken.StartConstructor
				&& token != JsonToken.StartObject;
		}

		/* Request Skeleton
		 * 			string json = ParamsToJSON (...);
			SayHiRestClient syrc = new SayHiRestClient (SayHiRestClient, CreateEndpointURL(), json);
			syrc.OnRestCallCompleted += (RestResult obj) => 
			{
				ResponseType ret = null;

				if (!obj.IsSuccess)
				{
					ret = new ResponseType (obj.IsSuccess, obj.Result);
				}
				else
				{
					try
					{
						ret = new ResponseType (true);

						using (JsonTextReader jtr = new JsonTextReader(new StringReader(obj.Result)))
						{

							while (jtr.Read())
							{

							}
						}
					}
					catch (Exception e)
					{
						ret = new ResponseType (false, GenerateParseErrorMessage (e));
					}
				}

				SafeRaiseEvent (OnRegisterUserCompleted, ret);

			};
			syrc.SendRestRequest ();
			*/
	}
}