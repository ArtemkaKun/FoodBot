using Discord.Commands;
using FoodBot.OrdersSystem;

namespace FoodBot.Discord.Commands.ShowCommand;

[Group(SHOW_ALL_COMMANDS_GROUP_NAME)]
public class ShowAllCommands : BaseShowCommands
{
	private const string SHOW_ALL_COMMANDS_GROUP_NAME = "shall";
	private const string SHOW_TODAY_ORDERS_COMMAND_DESCRIPTION = "Shows all today's orders";
	private const string SHOW_TODAY_ORDERS_SORTED_COMMAND_DESCRIPTION = "Shows all today's orders (sorted)";
	private const string SHOW_TODAY_ORDERS_SUMMARY_COMMAND_DESCRIPTION = "Shows all today's orders (summary)";

	[Command(COMMON_SHOW_COMMAND_NAME)]
	[Summary(SHOW_TODAY_ORDERS_COMMAND_DESCRIPTION)]
	public Task ShowTodayOrders ()
	{
		return ShowOrdersData(COMMON_SHOW_COMMAND_NAME, GetAllTodayOrders());
	}

	[Command(SORT_SHOW_COMMAND_NAME)]
	[Summary(SHOW_TODAY_ORDERS_SORTED_COMMAND_DESCRIPTION)]
	public Task ShowTodayOrdersSorted ()
	{
		return ShowOrdersData(SORT_SHOW_COMMAND_NAME, GetAllTodayOrders());
	}

	[Command(SUM_SHOW_COMMAND_NAME)]
	[Summary(SHOW_TODAY_ORDERS_SUMMARY_COMMAND_DESCRIPTION)]
	public Task ShowTodayOrdersSummary ()
	{
		return ShowOrdersData(SUM_SHOW_COMMAND_NAME, GetAllTodayOrders());
	}

	private List<Order> GetAllTodayOrders ()
	{
		return Program.OrdersSystemDB.GetTodayOrdersByChatIdentifier(Context.Guild.Id, Context.Channel.Id);
	}
}