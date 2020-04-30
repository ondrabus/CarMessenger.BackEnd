using CarMessenger.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarMessenger.Data.Converters
{
	static class MessageConverter
	{
		public static Message Convert(FaunaDB.Types.Value val)
		{
			return new Message
			{
				Created = FaunaDB.Types.Decoder.Decode<DateTime>(val.At(0)),
				SenderId = FaunaDB.Types.Decoder.Decode<string>(val.At(1)),
				Content = FaunaDB.Types.Decoder.Decode<string>(val.At(2))
			};
		}
	}
}
