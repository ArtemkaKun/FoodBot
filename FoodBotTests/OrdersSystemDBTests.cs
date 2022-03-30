using System;
using System.Collections.Generic;
using System.IO;
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
		
		Assert.Pass();
		ordersSystemDB.RemoveOrder(testOrder);
	}
}