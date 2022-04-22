using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FoodBot;
using FoodBot.OrdersSystem;
using NUnit.Framework;

namespace FoodBotTests;

[NonParallelizable]
public class OrdersSystemDBTests
{
	// TODO same code as in VotingSystemDBTests. 30.03.2022. Artem Yurchenko
	private const ulong TEST_GUILD_ID = 0;
	private const ulong TEST_CHANNEL_ID = 0;
	
	private readonly Order testOrder = new()
	{
		GuildID = TEST_GUILD_ID,
		ChannelID = TEST_CHANNEL_ID,
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
	public void AddTestOrder_PresentAndValid_True ()
	{
		Program.OrdersSystemDB.AddOrder(testOrder);
		List<Order> foundOrders = Program.OrdersSystemDB.GetTodayOrdersByChatIdentifier(testOrder.GuildID, testOrder.ChannelID);
		
		if (foundOrders.Count == 0 || foundOrders.Contains(testOrder) == false)
		{
			Assert.Fail("No orders found");
			return;
		}

		Program.OrdersSystemDB.RemoveOrder(testOrder);
		Assert.Pass();
	}

	[Test, NonParallelizable]
	public void AddTestOrderAndRemove_PresentInDB_False ()
	{
		Program.OrdersSystemDB.AddOrder(testOrder);
		Program.OrdersSystemDB.RemoveOrder(testOrder);
		
		List<Order> foundOrders = Program.OrdersSystemDB.GetTodayOrdersByChatIdentifier(testOrder.GuildID, testOrder.ChannelID);
		
		if (foundOrders.Count != 0)
		{
			Assert.Fail("Order found");
			return;
		}
		
		Assert.Pass();
	}

	[Test, NonParallelizable]
	public void AddTwoOrders_BothPresentAndValid_True ()
	{
		Order secondTestOrder = new()
		{
			GuildID = TEST_GUILD_ID,
			ChannelID = TEST_CHANNEL_ID,
			Date = DateTime.Today,
			PersonName = "Test",
			Text = "Test2"
		};
		
		Program.OrdersSystemDB.AddOrder(testOrder);
		Program.OrdersSystemDB.AddOrder(secondTestOrder);
		
		List<Order> foundOrders = Program.OrdersSystemDB.GetTodayOrdersByChatIdentifier(testOrder.GuildID, testOrder.ChannelID);
		
		if (foundOrders.Count == 0 || foundOrders.Contains(testOrder) == false || foundOrders.Contains(secondTestOrder) == false)
		{
			Assert.Fail("No orders found");
			return;
		}
		
		Program.OrdersSystemDB.RemoveOrder(testOrder);
		Program.OrdersSystemDB.RemoveOrder(secondTestOrder);
		Assert.Pass();
	}
	
	[Test, NonParallelizable]
	public void AddTestOrderAndUpdateIt_PresentAndValid_True ()
	{
		Program.OrdersSystemDB.AddOrder(testOrder);

		if (Program.OrdersSystemDB.TryUpdateOrderTextByChatIdentifierAndID(testOrder.GuildID, testOrder.ChannelID, testOrder.ID, "Test2") == false)
		{
			Assert.Fail("Order update was failed");
			return;
		}
		
		List<Order> foundOrders = Program.OrdersSystemDB.GetTodayOrdersByChatIdentifier(testOrder.GuildID, testOrder.ChannelID);
		
		if (foundOrders.Count == 0 || foundOrders.Find(order => order.Text == "Test2") == null)
		{
			Assert.Fail("No order found");
			return;
		}

		Program.OrdersSystemDB.RemoveOrder(testOrder);
		Assert.Pass();
	}

	[Test, NonParallelizable]
	public void AddTestOrderAndRemoveItById_NotPresentInDB_True ()
	{
		Program.OrdersSystemDB.AddOrder(testOrder);

		if (Program.OrdersSystemDB.TryRemoveOrderByChatIdentifierAndID(testOrder.GuildID, testOrder.ChannelID, testOrder.ID) == false)
		{
			Assert.Fail("Order removing was failed");
			return;
		}
		
		List<Order> foundOrders = Program.OrdersSystemDB.GetTodayOrdersByChatIdentifier(testOrder.GuildID, testOrder.ChannelID);
		
		if (foundOrders.Count != 0)
		{
			Assert.Fail("Order found");
			return;
		}
		
		Assert.Pass();
	}

	[Test, NonParallelizable]
	public void AddTwoTestOrdersAndGetOnlyForOneUser_ReturnedOnlyOneValidOrder_True ()
	{
		Order secondTestOrder = new()
		{
			GuildID = TEST_GUILD_ID,
			ChannelID = TEST_CHANNEL_ID,
			Date = DateTime.Today,
			PersonName = "Test2",
			Text = "Test"
		};
		
		Program.OrdersSystemDB.AddOrder(testOrder);
		Program.OrdersSystemDB.AddOrder(secondTestOrder);
		
		List<Order> foundOrders = Program.OrdersSystemDB.GetTodayOrdersByChatIdentifierAndPersonName(testOrder.GuildID, testOrder.ChannelID, testOrder.PersonName);
		
		if (foundOrders.Count is 0 or not 1 || foundOrders.Contains(testOrder) == false)
		{
			Assert.Fail("No orders found");
			return;
		}
		
		Program.OrdersSystemDB.RemoveOrder(testOrder);
		Program.OrdersSystemDB.RemoveOrder(secondTestOrder);
		Assert.Pass();
	}
}