using System;
using System.Collections.Generic;
using System.Text;

namespace CarMessenger.Data.Models
{
	public class PredefinedConstants
	{
		public static readonly string[] Countries = new string[]
		{
			"cz",
			"sk",
			"de",
			"at"
		};

		public static Dictionary<string, string> PlateRegex = new Dictionary<string, string>
		{
			{ "cz", "^[a-z0-9]{5,8}$" }
		};
	}
}
