using System;
using System.Linq;
using System.Threading.Tasks;
using CarMessenger.Data.Models;
using Microsoft.AspNetCore.Mvc;
using CarMessenger.Data.Services;
using Twilio.AspNet.Core;
using Twilio.TwiML;
using CarMessenger.Web.Helpers;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace CarMessenger.Mobile.Controllers
{
	[ApiController]
	public class TwilioController : ControllerBase
	{
		IMessageService _service;
		IConfiguration _config;

		public TwilioController(IMessageService messageService, IConfiguration config)
		{
			_service = messageService;
			_config = config;
		}

		[HttpPost]
		[Route("/twilio/store")]
		public async Task<TwiMLResult> Store()
		{
			var twilioConfig = _config.GetSection("Twilio");
			if (Request == null
				|| Request.Form == null
				|| !Request.Form.ContainsKey("AccountSID")
				|| Request.Form["AccountSID"] != twilioConfig["AccountSID"]
				|| !Request.Form.ContainsKey("From")
				|| !Request.Form.ContainsKey("Body"))
			{
				return null;
			}


			var sender = Request.Form["From"].Single();
			var body = Request.Form["Body"].Single();


			var message = MessageHelper.ParseMessage(body);

			var messagingResponse = new MessagingResponse();

			if (message == null)
			{
				messagingResponse.Message("Message has invalid format. Use {country},{license plate},{your message}");
				return new TwiMLResult(messagingResponse);
			}

			message.SenderId = sender;

			// check country
			if (!PredefinedConstants.Countries.Contains(message.CountryCode))
			{
				messagingResponse.Message($"Country code {message.CountryCode} is either misformated or unsupported");
				return new TwiMLResult(messagingResponse);
			}

			// check licensePlate
			if (PredefinedConstants.PlateRegex.ContainsKey(message.CountryCode) && !Regex.IsMatch(message.LicensePlate, PredefinedConstants.PlateRegex[message.CountryCode]))
			{
				messagingResponse.Message($"License plate {message.LicensePlate} is not a valid {message.CountryCode} plate.");
				return new TwiMLResult(messagingResponse);
			}

			await _service.Store(message);
			return new TwiMLResult();
		}
	}
}
