using CarMessenger.Data.Models;
using FaunaDB.Client;
using FaunaDB.Types;
using FaunaDB.Query;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarMessenger.Data.Converters;
using System.Linq;

namespace CarMessenger.Data.Services
{
	public class FaunaDbMessageService : IMessageService
	{
		private FaunaClient _client;

		public FaunaDbMessageService(IConfiguration config)
		{
			var faunaConfig = config.GetSection("FaunaDB");
			_client = new FaunaClient(endpoint: faunaConfig["Endpoint"], secret: faunaConfig["Secret"]);
		}


		public async Task<List<Message>> Get(string countryCode, string licensePlate)
		{
			var index = Language.Index("MessageIndex");
			var match = Language.Match(index, countryCode, licensePlate);
			var result = await _client.Query(Language.Paginate(match));
			var data = result.At("data").To<Value[]>();

			var list = new List<Message>();

			data.Match(
				Success: values => list.AddRange(values.Select(v => MessageConverter.Convert(v))),
				Failure: reason =>
				{
					// TODO: do some logging here
				}
			);

			list.ForEach(m =>
			{
				m.LicensePlate = licensePlate;
				m.CountryCode = countryCode;
			});

			return list;
		}

		public async Task<bool> Store(Message message)
		{
			message.Created = DateTime.UtcNow;
			var value = await _client.Query(
				Language.Create(
					Language.Collection("message"),
					Language.Obj("data", Encoder.Encode(message))
				)
			);

			return true;
		}
	}
}
