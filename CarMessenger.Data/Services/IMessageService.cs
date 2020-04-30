using CarMessenger.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMessenger.Data.Services
{
	public interface IMessageService
	{
		public Task<bool> Store(Message message);
		public Task<List<Message>> Get(string countryCode, string licensePlate);
	}
}
