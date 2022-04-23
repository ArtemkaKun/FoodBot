using System;
using System.IO;
using FoodBot;
using FoodBot.Discord.Commands.OrderCommand;
using FoodBot.OrdersSystem;
using NUnit.Framework;

namespace FoodBotTests;

[NonParallelizable]
public class OrderCommandsResultsTests
{
	private readonly Order validTestOrder = new()
	{
		GuildID = 1,
		ChannelID = 1,
		Date = DateTime.Today,
		PersonName = "Test",
		Text = "Test"
	};
	
	[SetUp]
	public void InitializeDB()
	{
		Program.OrdersSystemDB.Initialize();
	}
	
	[TearDown]
	public void CleanDB()
	{
		Program.OrdersSystemDB.Terminate();
	}

	[Test, NonParallelizable]
	public void AddValidOrder_Success_True ()
	{
		bool result = OrderCommandsResults.AddOrder(validTestOrder.GuildID, validTestOrder.ChannelID, validTestOrder.PersonName, validTestOrder.PersonName);

		Assert.IsTrue(result);
	}
	
	[Test, NonParallelizable]
	public void AddOrderWithInvalidGuildID_Success_False ()
	{
		bool result = OrderCommandsResults.AddOrder(0, validTestOrder.ChannelID, validTestOrder.PersonName, validTestOrder.PersonName);

		Assert.IsFalse(result);
	}
	
	
	[Test, NonParallelizable]
	public void AddOrderWithInvalidChannelID_Success_False ()
	{
		bool result = OrderCommandsResults.AddOrder(validTestOrder.GuildID, 0, validTestOrder.PersonName, validTestOrder.PersonName);

		Assert.IsFalse(result);
	}
	
	[Test, NonParallelizable]
	public void AddOrderWithInvalidPersonName_Success_False ()
	{
		bool result = OrderCommandsResults.AddOrder(validTestOrder.GuildID, validTestOrder.ChannelID, "", validTestOrder.PersonName);

		Assert.IsFalse(result);
	}
	
	[Test, NonParallelizable]
	public void AddOrderWithInvalidText_Success_False ()
	{
		bool result = OrderCommandsResults.AddOrder(validTestOrder.GuildID, validTestOrder.ChannelID, validTestOrder.PersonName, "");

		Assert.IsFalse(result);
	}
	
}