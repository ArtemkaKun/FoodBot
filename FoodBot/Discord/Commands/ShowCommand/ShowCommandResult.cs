using System.Data;
using System.Text;
using AsciiTableGenerators;
using FoodBot.Discord.Commands.Helpers;
using FoodBot.OrdersSystem;

namespace FoodBot.Discord.Commands.ShowCommand;

public static class ShowCommandResult
{
	public static string GetShowCommandAnswer (List<Order> orders)
	{
		DataTable ordersDataTable = ShowCommandsDataTablePreparer.GetOrdersDataTable(orders);
		StringBuilder ordersTableBuilder = AsciiTableGenerator.CreateAsciiTableFromDataTable(ordersDataTable);
		StringBuilder preparedOrdersTableBuilder = DiscordAsciiTablePreparer.PrepareAsciiTable(ordersTableBuilder);

		return preparedOrdersTableBuilder.ToString();
	}

	public static string GetShowSortedCommandAnswer (List<Order> orders)
	{
		List<Order> sortedOrders = OrdersListPreparer.SortOrders(orders);
		DataTable ordersDataTable = ShowCommandsDataTablePreparer.GetOrdersDataTable(sortedOrders);
		StringBuilder ordersTableBuilder = AsciiTableGenerator.CreateAsciiTableFromDataTable(ordersDataTable);
		StringBuilder preparedOrdersTableBuilder = DiscordAsciiTablePreparer.PrepareAsciiTable(ordersTableBuilder);

		return preparedOrdersTableBuilder.ToString();
	}

	public static string GetShowCountCommandAnswer (List<Order> orders)
	{
		DataTable ordersDataTable = ShowCommandsDataTablePreparer.GetCountedOrdersDataTable(orders);
		StringBuilder ordersTableBuilder = AsciiTableGenerator.CreateAsciiTableFromDataTable(ordersDataTable);
		StringBuilder preparedOrdersTableBuilder = DiscordAsciiTablePreparer.PrepareAsciiTable(ordersTableBuilder);

		return preparedOrdersTableBuilder.ToString();
	}
}