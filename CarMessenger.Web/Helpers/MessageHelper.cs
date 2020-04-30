using CarMessenger.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CarMessenger.Web.Helpers
{
	public static class MessageHelper
	{
		public readonly static char[] separators = { ',', ';', ':', '!', '/', '#' };

		public static Message ParseMessage(string body)
		{
			var pattern = @"^([A-Za-z]{2})[,;:!\/#](.+?)[,;:!\/#](.+)$";
			var regex = new Regex(pattern);
			var match = regex.Match(body);

			if (!match.Success || match.Groups.Count != 4)
			{
				return null;
			}

			var message = new Message
			{
				CountryCode = match.Groups[1].Value,
				LicensePlate = match.Groups[2].Value,
				Content = match.Groups[3].Value
			};

			return SanitizeInput(message);
		}

		public static Message SanitizeInput(Message message)
		{
			message.CountryCode = message.CountryCode.Trim().ToLowerInvariant();
			message.LicensePlate = Regex.Replace(message.LicensePlate.Trim().ToLowerInvariant(), @"\s+", "");
			message.Content = message.Content.Trim();

			return message;
		}
	}
}
