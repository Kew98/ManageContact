using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using Newtonsoft.Json;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ManageContact.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ContactsController : ControllerBase
	{
		private string DBNAME = @"..\ManageContact\Database\MyLiteData.db";

		// phone: "type": string ["home" | "work" | "mobile"]
		// GET: api/<ContactsController>
		[HttpGet]
		public IEnumerable<Contact> Get()
		{
			using (var db = new LiteDatabase(DBNAME))
			{
				var Contacts = db.GetCollection<Contact>("Contact");

				var list = Contacts.Query().ToList();

				return list;
			}

		}

		// GET api/<ContactsController>/5
		[HttpGet("{id}")]
		public Contact Get(int id)
		{
			using (var db = new LiteDatabase(DBNAME))
			{
				var Contacts = db.GetCollection<Contact>("Contact");

				var contact = Contacts.FindById(id);

				return contact;
			}
		}

		// GET api/<ContactsController>/5
		[HttpGet("call-list")]
		public string GetCallList()
		{
			using (var db = new LiteDatabase(DBNAME))
			{
				var Contacts = db.GetCollection<Contact>("Contact");

				List<CallContact> list = new List<CallContact>();

				foreach (var contact in Contacts.Query().ToList())
				{
					CallContact callContact = new CallContact();
					callContact.name = contact.name;

					var phone = contact.phone.Where(a => a.type == "home").FirstOrDefault();
					if (phone != null)
					{
						callContact.phone = phone.number;
					}

					list.Add(callContact);
				}

				string JSONresult = JsonConvert.SerializeObject(list);
				return JSONresult;
			}
		}

		// POST api/<ContactsController>
		[HttpPost]
		public void Post([FromBody] Contact contact)
		{
			using (var db = new LiteDatabase(DBNAME))
			{
				var Contacts = db.GetCollection<Contact>("Contact");

				Contacts.Insert(contact);
			}

		}

		// PUT api/<ContactsController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] Contact contactUpdate)
		{
			using (var db = new LiteDatabase(DBNAME))
			{
				var Contacts = db.GetCollection<Contact>("Contact");

				var contact = Contacts.FindById(id);

				contact.name = contactUpdate.name;
				contact.address = contactUpdate.address;
				contact.phone = contactUpdate.phone;
				contact.email = contactUpdate.email;

				Contacts.Update(contact);
			}
		}

		// DELETE api/<ContactsController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			using (var db = new LiteDatabase(DBNAME))
			{
				var Contacts = db.GetCollection<Contact>("Contact");

				Contacts.Delete(id);
			}
		}
	}
}
