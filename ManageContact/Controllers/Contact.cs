using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageContact.Controllers
{
	//"type": string["home" | "work" | "mobile"]

	public class Contact
	{
		public int Id { get; set; }
		public Name name { get; set; }
		public Address address { get; set; }
		public Phone[] phone { get; set; }
		public string email { get; set; }

	}

	public class Name
	{
		public string first { get; set; }
		public string middle { get; set; }
		public string last { get; set; }
	}

	public class Address
	{
		public string street { get; set; }
		public string city { get; set; }
		public string state { get; set; }
		public string zip { get; set; }
	}

	public class Phone
	{
		public string number { get; set; }
		public string type { get; set; }

	}

	public class CallContact
	{
		public Name name { get; set; }
		public string phone { get; set; }
	}
}
