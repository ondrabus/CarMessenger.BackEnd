using CarMessenger.Data.Models;
using CarMessenger.Web.Helpers;
using NUnit.Framework;
using System;

namespace CarMessenger.Web.Tests
{
	public class MessageHelperTests
	{
		[Test]
		public void ShouldReturnMessage()
		{
			// "cz,3AB 1234,Your driving is stellar"
			// "cz;3A1 5421; Great stuff"
			var message = "Cz,3AB 1234,Your driving is stellar";

			Assert.AreEqual("cz", MessageHelper.ParseMessage(message).CountryCode);
			Assert.AreEqual("3ab1234", MessageHelper.ParseMessage(message).LicensePlate);
			Assert.AreEqual("Your driving is stellar", MessageHelper.ParseMessage(message).Content);
		}

		[Test]
		public void ShouldReturnNull()
		{
			Assert.IsNull(MessageHelper.ParseMessage("hello"));
			Assert.IsNull(MessageHelper.ParseMessage("hello;world"));
			Assert.IsNull(MessageHelper.ParseMessage("cz"));
			Assert.IsNull(MessageHelper.ParseMessage("cz;1aa2345;"));
		}

		[Test]
		public void ShouldNotReturnNull()
		{
			Assert.IsNotNull(MessageHelper.ParseMessage("cz;world;this;is;ok"));
			Assert.IsNotNull(MessageHelper.ParseMessage("en/world/this/is/ok"));
		}

	}
}
