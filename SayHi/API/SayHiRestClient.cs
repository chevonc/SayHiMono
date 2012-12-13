using System;
using System.Net;
using System.IO;
using System.Text;

namespace SayHi
{
	public struct RestResult
	{
		public string Result
		{
			get;
			internal set;
		}

		public bool IsSuccess
		{
			get;
			internal set;
		}
	}

	public class SayHiRestClient
	{
		public event Action<RestResult> OnRestCallCompleted;

		public const string HTTPPOSTMETHOD = "POST";
		public const string HTTPGETMETHOD = "GET";
	
		public SayHiRestClient (string method, string url, string data)
		{
			Url = url;
			RestData = data;
			Method = method;
		}

		public void SendRestRequest()
		{
			HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create(new Uri(Url));
			hwr.ContentType = Method == HTTPPOSTMETHOD ? "application/json" : "application/x-www-form-urlencoded";
			hwr.Method = Method;
			try
			{
				hwr.BeginGetRequestStream(OnBeginRequestStream, hwr);
			}
			catch
			{
				RestResult ret = new RestResult();
				ret.IsSuccess = false;
				ret.Result = "Could not initiate and send rest request";
				var shadow = OnRestCallCompleted;
				if(shadow != null)
				{
					shadow(ret);
				}
			}
		}

		private void OnBeginRequestStream(IAsyncResult res)
		{
			try
			{
				HttpWebRequest hwr = (HttpWebRequest)res.AsyncState;
				using(Stream sr = hwr.EndGetRequestStream(res))
				{
					using(StreamWriter sw =  new StreamWriter(sr))
					{
						sw.Write(RestData ?? string.Empty);
						sw.Flush();
						sw.Close();
					}
				}

				hwr.BeginGetResponse(OnBeginGetResponse, hwr);
			}
			catch(WebException e)
			{
				RestResult ret = new RestResult();
				ret.IsSuccess = false;
				var response = ((HttpWebResponse)e.Response);
				string.Format("Status Code : {0}\nStatus Description : {1}", response.StatusCode, response.StatusDescription);
				
				try
				{
					using(var stream = response.GetResponseStream())
					{
						using(var reader = new StreamReader(stream))
						{
							ret.Result = string.Format("{0}\nError: {1}", ret.Result, reader.ReadToEnd());
						}
					}
				}
				catch(WebException ex)
				{
					
				}
				var shadow = OnRestCallCompleted;
				if(shadow != null)
				{
					shadow(ret);
				}
			}
		}

		private void OnBeginGetResponse(IAsyncResult res)
		{
			RestResult ret = new RestResult();

			try
			{
				HttpWebRequest hwr = (HttpWebRequest)res.AsyncState;
				WebResponse wr = hwr.EndGetResponse(res);
				if(wr == null)
				{
					throw new WebException("null", null, WebExceptionStatus.UnknownError, wr);
				}

				using(StreamReader strmrdr = new StreamReader(wr.GetResponseStream()))
				{
					ret.Result = strmrdr.ReadToEnd();
					strmrdr.Close();
					//hwr.Close();
				} 

				ret.IsSuccess = true;
			}
			catch(WebException e)
			{
				var response = ((HttpWebResponse)e.Response);
				ret.IsSuccess = false;
				ret.Result = string.Format("Status Code : {0}\n\nStatus Description : {1}", response.StatusCode, response.StatusDescription);				
				try
				{
					using(var stream = response.GetResponseStream())
					{
						using(var reader = new StreamReader(stream))
						{
							ret.Result = string.Format("{0}\nError: {1}", ret.Result, reader.ReadToEnd());
						}
					}
				}
				catch
				{
					
				}
			}
			var shadow = OnRestCallCompleted;
			if(shadow != null)
			{
				shadow(ret);
			}
		}

		public string Method
		{
			get;
			private set;
		}

		public string RestData
		{
			get;
			private set;
		}

		public string Url
		{
			get;
			private set;
		}

		public bool IsBusy
		{
			get;
			private set;
		}
	}
}

