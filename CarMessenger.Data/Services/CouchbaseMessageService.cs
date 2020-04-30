using CarMessenger.Data.Models;
using Couchbase.Core;
using Couchbase.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMessenger.Data.Services
{
	public class CouchbaseMessageService : IMessageService
	{
		private IBucket _bucket;

		public CouchbaseMessageService(IBucketProvider bucketProvider)
		{
			_bucket = bucketProvider.GetBucket("CarMessengerBucket");
		}

		public async Task<List<Message>> Get(string countryCode, string licensePlate)
		{
			throw new NotImplementedException();
			//			var n1ql = @"SELECT c.* FROM CarMessengerBucket c
			//WHERE c.licensePlate =$licensePlate AND c.countryCode=$countryCode";
			//			var query = QueryRequest.Create(n1ql)
			//				.AddNamedParameter("$licensePlate", licensePlate)
			//				.AddNamedParameter("$countryCode", countryCode);
			//			query.ScanConsistency(ScanConsistency.RequestPlus);
			//			var result = await _bucket.QueryAsync<Message>(query);

			//			if (!result.Success)
			//			{
			//				return BadRequest();
			//			}
		}

		public async Task<bool> Store(Message message)
		{
			message.Created = DateTime.UtcNow;
			var result = await _bucket.InsertAsync(Guid.NewGuid().ToString(), message);
			if (!result.Success)
			{
				// TODO: do some logging here
				return false;
			}

			return true;
		}
	}
}
