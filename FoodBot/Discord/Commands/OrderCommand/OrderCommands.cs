using Discord.Commands;

namespace FoodBot.Discord.Commands.OrderCommand;

[Group(ORDER_COMMANDS_GROUP_NAME)]
public class OrderCommands : ModuleBase<SocketCommandContext>
{
	private const string ORDER_COMMANDS_GROUP_NAME = "order";
	private const string CREATE_NEW_ORDER_COMMAND_DESCRIPTION = "Creates a new order";
	private const string UPDATE_USER_ORDER_COMMAND_DESCRIPTION = "Updates order with provided text";
	private const string DELETE_ORDER_COMMAND_DESCRIPTION = "Deletes order";
	private const string ORDER_WAS_SAVED_MESSAGE = ORDER_WAS_SUCCESSFULLY_TEMPLATE_MESSAGE + "proceeded";
	private const string ORDER_ADD_FAILED = "Failed to add order";
	private const string ORDER_WAS_UPDATED_MESSAGE = ORDER_WAS_SUCCESSFULLY_TEMPLATE_MESSAGE + "updated";
	private const string ORDER_WAS_REMOVED = ORDER_WAS_SUCCESSFULLY_TEMPLATE_MESSAGE + "removed";
	private const string ORDER_UPDATE_FAILED = "Failed to update order";
	private const string ORDER_DELETE_FAILED = "Failed to remove order";
	private const string ORDER_WAS_SUCCESSFULLY_TEMPLATE_MESSAGE = "Order was successfully ";
	private const string MAKE_COMMAND_NAME = "-mk";
	private const string DELETE_COMMAND_NAME = "-del";
	private const string UPDATE_COMMAND_NAME = "-upd";

	[Command(MAKE_COMMAND_NAME)]
	[Summary(CREATE_NEW_ORDER_COMMAND_DESCRIPTION)]
	public Task CreateNewOrder ([Remainder] string orderText)
	{
		if (OrderCommandsResults.AddOrder(Context.Guild.Id, Context.Channel.Id, Context.User.Username, orderText) == true)
		{
			return ReplyAsync(ORDER_WAS_SAVED_MESSAGE);
		}

		return ReplyAsync(ORDER_ADD_FAILED);
	}

	[Command(UPDATE_COMMAND_NAME)]
	[Summary(UPDATE_USER_ORDER_COMMAND_DESCRIPTION)]
	public Task UpdateUserOrder (uint orderID, [Remainder] string newOrderText)
	{
		if (OrderCommandsResults.UpdateOrder(Context.Guild.Id, Context.Channel.Id, orderID, newOrderText) == true)
		{
			return ReplyAsync(ORDER_WAS_UPDATED_MESSAGE);
		}

		return ReplyAsync(ORDER_UPDATE_FAILED);
	}

	[Command(DELETE_COMMAND_NAME)]
	[Summary(DELETE_ORDER_COMMAND_DESCRIPTION)]
	public Task DeleteOrder (uint orderID)
	{
		if (OrderCommandsResults.RemoveOrder(Context.Guild.Id, Context.Channel.Id, orderID) == true)
		{
			return ReplyAsync(ORDER_WAS_REMOVED);
		}

		return ReplyAsync(ORDER_DELETE_FAILED);
	}
}