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
	public void InitializeDB ()
	{
		Program.OrdersSystemDB.Initialize();
	}

	[TearDown]
	public void CleanDB ()
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

	[Test, NonParallelizable]
	public void AddOrderAndRemove_Success_True ()
	{
		OrderCommandsResults.AddOrder(validTestOrder.GuildID, validTestOrder.ChannelID, validTestOrder.PersonName, validTestOrder.PersonName);
		bool result = OrderCommandsResults.RemoveOrder(validTestOrder.GuildID, validTestOrder.ChannelID, 1);

		Assert.IsTrue(result);
	}

	[Test, NonParallelizable]
	public void AddOrderAndRemoveWithInvalidGuildID_Success_False ()
	{
		OrderCommandsResults.AddOrder(validTestOrder.GuildID, validTestOrder.ChannelID, validTestOrder.PersonName, validTestOrder.PersonName);
		bool result = OrderCommandsResults.RemoveOrder(0, validTestOrder.ChannelID, 1);

		Assert.IsFalse(result);
	}

	[Test, NonParallelizable]
	public void AddOrderAndRemoveWithInvalidChannelID_Success_False ()
	{
		OrderCommandsResults.AddOrder(validTestOrder.GuildID, validTestOrder.ChannelID, validTestOrder.PersonName, validTestOrder.PersonName);
		bool result = OrderCommandsResults.RemoveOrder(validTestOrder.GuildID, 0, 1);

		Assert.IsFalse(result);
	}

	[Test, NonParallelizable]
	public void AddOrderAndRemoveWithInvalidOrderID_Success_False ()
	{
		OrderCommandsResults.AddOrder(validTestOrder.GuildID, validTestOrder.ChannelID, validTestOrder.PersonName, validTestOrder.PersonName);
		bool result = OrderCommandsResults.RemoveOrder(validTestOrder.GuildID, validTestOrder.ChannelID, 0);

		Assert.IsFalse(result);
	}

	[Test, NonParallelizable]
	public void AddOrderAndUpdate_Success_True ()
	{
		OrderCommandsResults.AddOrder(validTestOrder.GuildID, validTestOrder.ChannelID, validTestOrder.PersonName, validTestOrder.PersonName);
		bool result = OrderCommandsResults.UpdateOrder(validTestOrder.GuildID, validTestOrder.ChannelID, 1, "New Text");

		Assert.IsTrue(result);
	}

	[Test, NonParallelizable]
	public void AddOrderAndUpdateWithInvalidGuildID_Success_False ()
	{
		OrderCommandsResults.AddOrder(validTestOrder.GuildID, validTestOrder.ChannelID, validTestOrder.PersonName, validTestOrder.PersonName);
		bool result = OrderCommandsResults.UpdateOrder(0, validTestOrder.ChannelID, 1, "New Text");

		Assert.IsFalse(result);
	}

	[Test, NonParallelizable]
	public void AddOrderAndUpdateWithInvalidChannelID_Success_False ()
	{
		OrderCommandsResults.AddOrder(validTestOrder.GuildID, validTestOrder.ChannelID, validTestOrder.PersonName, validTestOrder.PersonName);
		bool result = OrderCommandsResults.UpdateOrder(validTestOrder.GuildID, 0, 1, "New Text");

		Assert.IsFalse(result);
	}

	[Test, NonParallelizable]
	public void AddOrderAndUpdateWithInvalidOrderID_Success_False ()
	{
		OrderCommandsResults.AddOrder(validTestOrder.GuildID, validTestOrder.ChannelID, validTestOrder.PersonName, validTestOrder.PersonName);
		bool result = OrderCommandsResults.UpdateOrder(validTestOrder.GuildID, validTestOrder.ChannelID, 0, "New Text");

		Assert.IsFalse(result);
	}

	[Test, NonParallelizable]
	public void AddOrderAndUpdateWithInvalidText_Success_False ()
	{
		OrderCommandsResults.AddOrder(validTestOrder.GuildID, validTestOrder.ChannelID, validTestOrder.PersonName, validTestOrder.PersonName);
		bool result = OrderCommandsResults.UpdateOrder(validTestOrder.GuildID, validTestOrder.ChannelID, 1, "");

		Assert.IsFalse(result);
	}
}