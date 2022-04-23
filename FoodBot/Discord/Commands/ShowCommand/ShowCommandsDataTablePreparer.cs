using System.Data;
using FoodBot.OrdersSystem;

namespace FoodBot.Discord.Commands.ShowCommand;

public static class ShowCommandsDataTablePreparer
{
	// TODO same values as in the test class. 23.04.2022. Artem Yurchenko
	private const string ID_COLUMN_NAME = "Commands";
	private const string PERSON_COLUMN_NAME = "Description";
	private const string ORDER_COLUMN_NAME = "Order";
	
	public static DataTable GetOrdersDataTable (List<Order> orders)
	{
		DataTable ordersTable = new();
		ordersTable.Columns.Add(ID_COLUMN_NAME, typeof(uint));
		ordersTable.Columns.Add(PERSON_COLUMN_NAME, typeof(string));
		ordersTable.Columns.Add(ORDER_COLUMN_NAME, typeof(string));
		
		foreach (Order order in orders)
		{
			ordersTable.Rows.Add(order.ID, order.PersonName, order.Text);
		}
		
		return ordersTable;
	}
}