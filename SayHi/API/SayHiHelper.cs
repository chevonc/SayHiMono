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
using System.Collections.Generic;

namespace SayHi.API
{
	public class SayHiHelper
	{
		private object m_registerUserState;

		public event Action<UserModel> OnRegisterUserCompleted;
		public event Action<Event> OnGetEventInfoCompleted;
		public event Action<ResponseBase> OnCheckIntoEventCompleted;
		public event Action<List<Event>> OnGetEventsCompleted;
		public event Action<ResponseBase> OnCreateEventCompleted;
		public event Action<UserModel> OnMatchUserCompleted;
		public const string APIBaseURL = "http://say-hi.herokuapp.com/api/";
		public const string APIVersionPath = "v1/";
		public const string RegisterUserPath = "registerUser/";
		public const string GetEventInfoPath = "event/getEvent/";
		public const string LoginUserPath = "getUserInfo/login/";
		public const string CheckInUserPath = "event/addUser/";
		public const string GetEventsPath = "event/?format=json";
		public const string CreateEventPath = "event/setEvent/";
		public const string MatchUserPath = "getUserInfo/getNextMatch/"; //TODO: fill in match user path
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
	
							while (jtr.Read())
							{
								if (JsonKeyMatches (jtr, JsonToken.PropertyName, "id"))
								{
									ret.ID = jtr.ReadAsString ();
									break;//only parsing ID
								}
							}						
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
//							JsonIndexer idx = new JsonIndexer (jtr);
//							ret.DOBRaw = idx ["dob"];
//							ret.EmailAddress = idx ["email"];
//							ret.ID = idx ["id"];
//							ret.LastName = idx ["lastName"];
							while (jtr.Read())
							{
								if (JsonKeyMatches (jtr, JsonToken.PropertyName, "email"))
								{
									ret.EmailAddress = jtr.ReadAsString ();
								}
								else
								if (JsonKeyMatches (jtr, JsonToken.PropertyName, "firstName"))
								{
									ret.FirstName = jtr.ReadAsString ();
								}
								else
								if (JsonKeyMatches (jtr, JsonToken.PropertyName, "lastName"))
								{
									ret.LastName = jtr.ReadAsString ();
								}
								else
								if (JsonKeyMatches (jtr, JsonToken.PropertyName, "id"))
								{
									ret.ID = jtr.ReadAsString ();
								}
							}
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
								if (JsonKeyMatches (jtr, JsonToken.PropertyName, "address"))
								{
									ret.Address = jtr.ReadAsString ();
								}
								else
								if (JsonKeyMatches (jtr, JsonToken.PropertyName, "date"))
								{
									ret.Date = jtr.ReadAsString ();
								}
								else
								if (JsonKeyMatches (jtr, JsonToken.PropertyName, "end_time"))
								{
									ret.EndTime = jtr.ReadAsString ();
								}
								else
								if (JsonKeyMatches (jtr, JsonToken.PropertyName, "event_code"))
								{
									ret.Code = jtr.ReadAsString ();
								}
								else
								if (JsonKeyMatches (jtr, JsonToken.PropertyName, "name"))
								{
									ret.Name = jtr.ReadAsString ();
								}
								else
								if (JsonKeyMatches (jtr, JsonToken.PropertyName, "start_time"))
								{
									ret.StartTime = jtr.ReadAsString ();
								}
								else
								if (JsonKeyMatches (jtr, JsonToken.PropertyName, "summary"))
								{
									ret.Summary = jtr.ReadAsString ();
								}
								else
								if (JsonKeyMatches (jtr, JsonToken.PropertyName, "venue"))
								{
									ret.Venue = jtr.ReadAsString ();
								}
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
	
		public void CheckIntoEvent (string userId, string eventCode)
		{
			string json = ParamsToJSON ("userid", userId, "event_code");
			SayHiRestClient syrc = new SayHiRestClient (SayHiRestClient.HTTPPOSTMETHOD, CreateEndpointURL (CheckInUserPath), json);
			syrc.OnRestCallCompleted += (RestResult obj) => 
			{
				ResponseBase ret = null;

				if (!obj.IsSuccess)
				{
					ret = new ResponseBase (obj.IsSuccess, obj.Result);
				}
				else
				{
					try
					{
						bool success = false;
						string msg = "";
						using (JsonTextReader jtr = new JsonTextReader(new StringReader(obj.Result)))
						{

							while (jtr.Read())
							{
								if (JsonKeyMatches (jtr, JsonToken.PropertyName, "Success"))
								{
									success = jtr.ReadAsInt32 () == 0;
								} 
							}

							if (!success)
							{
								msg = "Unabled to parse JSON";
							}

							ret = new ResponseBase (success, msg);
						}
					}
					catch (Exception e)
					{
						ret = new ResponseBase (false, GenerateParseErrorMessage (e));
					}
				}

				SafeRaiseEvent (OnRegisterUserCompleted, ret);

			};
			syrc.SendRestRequest ();
		}

		public void GetEvents (string userId, string eventCode)
		{
			string json = string.Empty;
			SayHiRestClient syrc = new SayHiRestClient (SayHiRestClient.HTTPPOSTMETHOD, 
			                                            CreateEndpointURL (GetEventsPath + "&attendees="), json);
			syrc.OnRestCallCompleted += (RestResult obj) => 
			{
				List<Event> ret = new List<Event> ();

				if (obj.IsSuccess)
				{
					try
					{
						using (JsonTextReader jtr = new JsonTextReader(new StringReader(obj.Result)))
						{
							while (jtr.Read())
							{
								//read start objs
								ret.Add (ParseEventJson (jtr));
								//read off end objs
							}
						}
					}
					catch (Exception e)
					{
					}
				}
				
				SafeRaiseEvent (OnRegisterUserCompleted, ret);
				
			};
			syrc.SendRestRequest ();
		}

		public Event ParseEventJson (JsonTextReader jtr)
		{
			return new Event (true, "");
		}

		public void CreateEvent (string userId, string eventName, string address, string city, 
		                        string venue, string date, string startTime, string endTime, string summary, 
		                        int maxAttendants, bool recurring)
		{
			string json = ParamsToJSON ("user_id", userId, "name", eventName, "address", address, "city", 
			                            city, "venue", venue, "date", date, "start_time", startTime, "end_time",
			                            endTime, "summary", summary, "max_attendants", maxAttendants, "recurring", 
			                            recurring);
			SayHiRestClient syrc = new SayHiRestClient (SayHiRestClient.HTTPPOSTMETHOD, CreateEndpointURL (CreateEventPath), json);
			syrc.OnRestCallCompleted += (RestResult obj) => 
			{
				ResponseBase ret = null;
				
				if (!obj.IsSuccess)
				{
					ret = new ResponseBase (obj.IsSuccess, obj.Result);
				}
				else
				{
					try
					{
						bool success = false;
						string msg = string.Empty;
						
						using (JsonTextReader jtr = new JsonTextReader(new StringReader(obj.Result)))
						{
							
							while (jtr.Read())
							{
								if (JsonKeyMatches (jtr, JsonToken.PropertyName, "good"))
								{
									success = CompareStrings (jtr, "good");
								}
							}
							ret = new ResponseBase (success, msg);
						}
					}
					catch (Exception e)
					{
						ret = new ResponseBase (false, GenerateParseErrorMessage (e));
					}
				}	
				SafeRaiseEvent (OnCreateEventCompleted, ret);
			};
			syrc.SendRestRequest ();
		}
		public void MatchUser (string userId, string eventCode)
		{
			string json = ParamsToJSON ("userid", userId, "event_code", eventCode);

			SayHiRestClient syrc = new SayHiRestClient (SayHiRestClient.HTTPPOSTMETHOD, CreateEndpointURL (MatchUserPath), json);
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

							while (jtr.Read())
							{

							}
						}
					}
					catch (Exception e)
					{
						ret = new UserModel (false, GenerateParseErrorMessage (e));
					}
				}

				SafeRaiseEvent (OnMatchUserCompleted, ret);

			};
			syrc.SendRestRequest ();

		
		}
		bool JsonKeyMatches (JsonTextReader jtr, JsonToken token, string key)
		{
			bool ret = jtr.TokenType == token && CompareStrings (jtr, key);
			return ret;
		}

		void SafeRaiseEvent (Delegate eventToRaise, params object[] args)
		{
			var shadow = eventToRaise;
			if (shadow != null)
			{
				shadow.DynamicInvoke (args);
			}
		}

		string ParamsToJSON (params object[] keysAndValues)
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
						string key = keysAndValues [i].ToString ();
						string prop = null;

						if (i < keysAndValues.Length - 1)
						{
							prop = keysAndValues [i + 1].ToString ();
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
				ResponseBase ret = null;

				if (!obj.IsSuccess)
				{
					ret = new ResponseBase (obj.IsSuccess, obj.Result);
				}
				else
				{
					try
					{
						ret = new ResponseBase (true);

						using (JsonTextReader jtr = new JsonTextReader(new StringReader(obj.Result)))
						{

							while (jtr.Read())
							{

							}
						}
					}
					catch (Exception e)
					{
						ret = new ResponseBase (false, GenerateParseErrorMessage (e));
					}
				}

				SafeRaiseEvent (EventToRaise, ret);

			};
			syrc.SendRestRequest ();
			*/
	}
}