using FaunaDB.Types;
using System;
using System.ComponentModel.DataAnnotations;

namespace CarMessenger.Data.Models
{

	public class Message
	{
		[FaunaField("licensePlate")]
		[Required]
		public string LicensePlate { get; set; }

		[FaunaField("countryCode")]
		[Required]
		public string CountryCode { get; set; }

		[FaunaField("content")]
		[Required]
		public string Content { get; set; }

		[FaunaField("senderId")]
		[Required]
		public string SenderId { get; set; }

		[FaunaField("created")]
		public DateTime Created { get; set; }
	}
}
