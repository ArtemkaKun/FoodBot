using Discord.Commands;
using FoodBot.OrdersSystem;

namespace FoodBot.Discord.Commands.ShowCommand;

[Group(SHOW_USER_COMMANDS_GROUP_NAME)]
public class ShowMyCommands : BaseShowCommands
{
	private const string SHOW_USER_COMMANDS_GROUP_NAME = "shmy";
	private const string SHOW_USER_TODAY_ORDERS_COMMAND_DESCRIPTION = "Shows your today's orders";
	private const string SHOW_USER_TODAY_ORDERS_SORTED_COMMAND_DESCRIPTION = "Shows your today's orders (sorted)";
	private const string SHOW_USER_TODAY_ORDERS_SUMMARY_COMMAND_DESCRIPTION = "Shows your today's orders (summary)";
	
	[Command(COMMON_SHOW_COMMAND_NAME)]
	[Summary(SHOW_USER_TODAY_ORDERS_COMMAND_DESCRIPTION)]
	public Task ShowUserTodayOrders ()
	{
		return ShowOrdersData(COMMON_SHOW_COMMAND_NAME, GetAllUserTodayOrders());
	}

	[Command(SORT_SHOW_COMMAND_NAME)]
	[Summary(SHOW_USER_TODAY_ORDERS_SORTED_COMMAND_DESCRIPTION)]
	public Task ShowUserTodayOrdersSorted ()
	{
		return ShowOrdersData(SORT_SHOW_COMMAND_NAME, GetAllUserTodayOrders());
	}

	[Command(SUM_SHOW_COMMAND_NAME)]
	[Summary(SHOW_USER_TODAY_ORDERS_SUMMARY_COMMAND_DESCRIPTION)]
	public Task ShowUserTodayOrdersSummary ()
	{
		return ShowOrdersData(SUM_SHOW_COMMAND_NAME, GetAllUserTodayOrders());
	}

	private List<Order> GetAllUserTodayOrders ()
	{
		return Program.OrdersSystemDB.GetTodayOrdersByChatIdentifierAndPersonName(Context.Guild.Id, Context.Channel.Id, Context.User.Username);
	}
}