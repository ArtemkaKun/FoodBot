using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FoodBot.OrdersSystem;
using NUnit.Framework;

namespace FoodBotTests;

public class OrdersSystemDBTests
{
	private readonly OrdersSystemDB ordersSystemDB = new();
	
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
	
	[OneTimeSetUp]
	public void InitializeDB()
	{
		ordersSystemDB.Initialize();
	}

	[OneTimeTearDown]
	public void Terminate ()
	{
		File.Delete("./OrdersData.db");
	}

	[Test]
	public void AddTestOrder_PresentAndValid_True ()
	{
		ordersSystemDB.AddOrder(testOrder);
		List<Order> foundOrders = ordersSystemDB.GetTodayOrdersByChatIdentifier(testOrder.GuildID, testOrder.ChannelID);
		
		if (foundOrders.Count == 0 || foundOrders.Contains(testOrder) == false)
		{
			Assert.Fail("No orders found");
			return;
		}

		ordersSystemDB.RemoveOrder(testOrder);
		Assert.Pass();
	}

	[Test]
	public void AddTestOrderAndRemove_PresentInDB_False ()
	{
		ordersSystemDB.AddOrder(testOrder);
		ordersSystemDB.RemoveOrder(testOrder);
		
		List<Order> foundOrders = ordersSystemDB.GetTodayOrdersByChatIdentifier(testOrder.GuildID, testOrder.ChannelID);
		
		if (foundOrders.Count != 0)
		{
			Assert.Fail("Order found");
			return;
		}
		
		Assert.Pass();
	}

	[Test]
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
		
		ordersSystemDB.AddOrder(testOrder);
		ordersSystemDB.AddOrder(secondTestOrder);
		
		List<Order> foundOrders = ordersSystemDB.GetTodayOrdersByChatIdentifier(testOrder.GuildID, testOrder.ChannelID);
		
		if (foundOrders.Count == 0 || foundOrders.Contains(testOrder) == false || foundOrders.Contains(secondTestOrder) == false)
		{
			Assert.Fail("No orders found");
			return;
		}
		
		ordersSystemDB.RemoveOrder(testOrder);
		ordersSystemDB.RemoveOrder(secondTestOrder);
		Assert.Pass();
	}
	
	[Test]
	public void AddTestOrderAndUpdateIt_PresentAndValid_True ()
	{
		ordersSystemDB.AddOrder(testOrder);

		if (ordersSystemDB.TryUpdateOrderTextByChatIdentifierAndID(testOrder.GuildID, testOrder.ChannelID, testOrder.ID, "Test2") == false)
		{
			Assert.Fail("Order update was failed");
			return;
		}
		
		List<Order> foundOrders = ordersSystemDB.GetTodayOrdersByChatIdentifier(testOrder.GuildID, testOrder.ChannelID);
		
		if (foundOrders.Count == 0 || foundOrders.Find(order => order.Text == "Test2") == null)
		{
			Assert.Fail("No order found");
			return;
		}

		ordersSystemDB.RemoveOrder(testOrder);
		Assert.Pass();
	}

	[Test]
	public void AddTestOrderAndRemoveItById_NotPresentInDB_True ()
	{
		ordersSystemDB.AddOrder(testOrder);

		if (ordersSystemDB.TryRemoveOrderByChatIdentifierAndID(testOrder.GuildID, testOrder.ChannelID, testOrder.ID) == false)
		{
			Assert.Fail("Order removing was failed");
			return;
		}
		
		List<Order> foundOrders = ordersSystemDB.GetTodayOrdersByChatIdentifier(testOrder.GuildID, testOrder.ChannelID);
		
		if (foundOrders.Count != 0)
		{
			Assert.Fail("Order found");
			return;
		}
		
		Assert.Pass();
	}

	[Test]
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
		
		ordersSystemDB.AddOrder(testOrder);
		ordersSystemDB.AddOrder(secondTestOrder);
		
		List<Order> foundOrders = ordersSystemDB.GetTodayOrdersByChatIdentifierAndPersonName(testOrder.GuildID, testOrder.ChannelID, testOrder.PersonName);
		
		if (foundOrders.Count is 0 or not 1 || foundOrders.Contains(testOrder) == false)
		{
			Assert.Fail("No orders found");
			return;
		}
		
		ordersSystemDB.RemoveOrder(testOrder);
		ordersSystemDB.RemoveOrder(secondTestOrder);
		Assert.Pass();
	}
}