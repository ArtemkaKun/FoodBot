namespace FoodBot.OrdersSystem;

public static class OrdersListPreparer
{
	public static List<Order> SortOrders (IEnumerable<Order> unsortedOrders)
	{
		return unsortedOrders.OrderBy(order => order.Text).ToList();
	}

	public static Dictionary<string, int> CountOrders (IEnumerable<Order> orders)
	{
		List<Order> sortedOrders = SortOrders(orders);

		Dictionary<string, int> countedOrders = new();

		foreach (Order order in sortedOrders)
		{
			if (countedOrders.ContainsKey(order.Text))
			{
				countedOrders[order.Text]++;
			}
			else
			{
				countedOrders.Add(order.Text, 1);
			}
		}

		return countedOrders.OrderByDescending(orderCount => orderCount.Value).ToDictionary(orderCount => orderCount.Key, orderCount => orderCount.Value);
	}
}