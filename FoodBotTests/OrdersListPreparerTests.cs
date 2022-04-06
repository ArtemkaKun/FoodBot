using System.Collections.Generic;
using FoodBot.OrdersSystem;
using NUnit.Framework;

namespace FoodBotTests;

public class OrdersListPreparerTests
{
	[Test]
	public void SortListOfOrders_SortedListEqualToExpectedSortedList_True ()
	{
		List<Order> orders = new()
		{
			new Order { PersonName = "TestPerson", Text = "orderZ" },
			new Order { PersonName = "TestPerson", Text = "orderC" },
			new Order { PersonName = "TestPerson", Text = "orderD" },
			new Order { PersonName = "TestPerson", Text = "orderB" },
			new Order { PersonName = "TestPerson", Text = "orderA" }
		};
		
		List<Order> expectedSortedList = new()
		{
			new Order { PersonName = "TestPerson", Text = "orderA" },
			new Order { PersonName = "TestPerson", Text = "orderB" },
			new Order { PersonName = "TestPerson", Text = "orderC" },
			new Order { PersonName = "TestPerson", Text = "orderD" },
			new Order { PersonName = "TestPerson", Text = "orderZ" }
		};
		
		List<Order> sortedList = OrdersListPreparer.SortOrders(orders);
		Assert.AreEqual(expectedSortedList, sortedList);
	}
	
	[Test]
	public void CountOrders_OrdersCountEqualToExpectedCount_True ()
	{
		List<Order> orders = new()
		{
			new Order { PersonName = "TestPerson", Text = "orderA" },
			new Order { PersonName = "TestPerson", Text = "orderA" },
			new Order { PersonName = "TestPerson", Text = "orderB" },
			new Order { PersonName = "TestPerson", Text = "orderB" },
			new Order { PersonName = "TestPerson", Text = "orderB" },
			new Order { PersonName = "TestPerson", Text = "orderC" },
			new Order { PersonName = "TestPerson", Text = "orderZ" }
		};
		
		Dictionary<Order, int> expectedOrderCount = new()
		{
			{ new Order { PersonName = "TestPerson", Text = "orderA" }, 2 },
			{ new Order { PersonName = "TestPerson", Text = "orderB" }, 3 },
			{ new Order { PersonName = "TestPerson", Text = "orderC" }, 1 },
			{ new Order { PersonName = "TestPerson", Text = "orderZ" }, 1 }
		};
		
		Dictionary<Order, int> orderCount = OrdersListPreparer.CountOrders(orders);
		Assert.AreEqual(expectedOrderCount, orderCount);
	}
}