using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CarMessenger.Data.Models;
using CarMessenger.Data.Services;
using CarMessenger.Web.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CarMessenger.DataAccess.Controllers
{
	[ApiController]
	public class WebController : ControllerBase
	{
		private IMessageService _messageService;
		public WebController(IMessageService messageService)
		{
			_messageService = messageService;
		}

		[HttpGet]
		[Route("/message/get")]
		public async Task<ActionResult<IEnumerable<Message>>> Get(string licensePlate, string countryCode)
		{
			var data = await _messageService.Get(countryCode, licensePlate);
			return Ok(data);
		}

		[HttpPost]
		[Route("/message/store")]
		public async Task<ActionResult> Store(Message message)
		{
			// sanitize input
			message = MessageHelper.SanitizeInput(message);

			// check country
			if (!PredefinedConstants.Countries.Contains(message.CountryCode))
			{
				return ValidationProblem($"Country code {message.CountryCode} is either misformated or unsupported");
			}

			// check licensePlate
			if (PredefinedConstants.PlateRegex.ContainsKey(message.CountryCode) && !Regex.IsMatch(message.LicensePlate, PredefinedConstants.PlateRegex[message.CountryCode]))
			{
				return ValidationProblem($"License plate {message.LicensePlate} is not a valid {message.CountryCode} plate.");
			}

			var response = await _messageService.Store(message);

			if (response)
			{
				return Ok();
			}
			else
			{
				return Problem();
			}

		}
	}
}
