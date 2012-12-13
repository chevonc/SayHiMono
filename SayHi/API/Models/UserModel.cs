using System;
using System.Collections.ObjectModel;

namespace SayHi.API.Models
{
	public class Event : ResponseBase
	{
		public Event ()
		{

		}
		public Event (bool isSuccess, string msg = ""):
			base(isSuccess, msg)
		{

		}
		public string EventCode {
			get;
			set;

		}

		public string EventId {
			get;
			set;
		}

		public string EventName {
			get;
			set;
		}

		public string EventOrganizer {
			get;
			set;
		}

		public string EventSummary {
			get;
			set;
		}

	}

	public class UserModel : ResponseBase
	{
		public UserModel ()
		{
		}

		public UserModel (bool isSucess, string msg = ""):
			base(isSucess, msg)
		{

		}

		public string FirstName {
			get;
			set;
		}

		public string LastName {
			get;
			set;
		}

		public string EmailAddress {
			get;
			set;
		}

		public string ID {
			get;
			set;
		}

		public DateTime DOB {
			get;
			private set;
		}

		public string m_dobRaw;
		public string DOBRaw {
			get
			{
				return m_dobRaw;
			}
			set
			{
				m_dobRaw = value;
				DOB = DateTime.Parse (m_dobRaw);
			}
		}

		public string Summary {
			get;
			set;
		}

		public string InterestOne {
			get;
			set;
		}
		public string InterestTwo {
			get;
			set;
		}
		public string InterestThree {
			get;
			set;
		}


		public ObservableCollection<Event> Events {
			get;
			set;
		}


	}
}

