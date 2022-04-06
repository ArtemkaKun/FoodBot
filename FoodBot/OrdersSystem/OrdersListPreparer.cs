namespace FoodBot.OrdersSystem;

public static class OrdersListPreparer
{
	public static List<Order> SortOrders (IEnumerable<Order> unsortedOrders)
	{
		return unsortedOrders.OrderBy(order => order.Text).ToList();
	}
	
	public static Dictionary<Order, int> CountOrders (IEnumerable<Order> orders)
	{
		List<Order> sortedOrders = SortOrders(orders);
		
		Dictionary<Order, int> countedOrders = new();
		
		foreach (Order order in sortedOrders)
		{
			if (countedOrders.ContainsKey(order))
			{
				countedOrders[order]++;
			}
			else
			{
				countedOrders.Add(order, 1);
			}
		}
		
		return countedOrders;
	}
}