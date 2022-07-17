using Discord.Commands;
using FoodBot.Discord.Commands.Helpers;
using FoodBot.OrdersSystem;

namespace FoodBot.Discord.Commands.ShowCommand;

public abstract class BaseShowCommands : ModuleBase<SocketCommandContext>
{
	protected const string COMMON_SHOW_COMMAND_NAME = "";
	protected const string SORT_SHOW_COMMAND_NAME = "-sort";
	protected const string SUM_SHOW_COMMAND_NAME = "-sum";

	private const string NOTHING_TO_SHOW_MESSAGE = "Nothing to show";

	private Dictionary<string, Func<List<Order>, string>> ShowAllOptionsFunctions { get; set; }

	protected BaseShowCommands ()
	{
		ShowAllOptionsFunctions = new Dictionary<string, Func<List<Order>, string>>
		{
			{COMMON_SHOW_COMMAND_NAME, ShowCommandResult.GetShowCommandAnswer},
			{SORT_SHOW_COMMAND_NAME, ShowCommandResult.GetShowSortedCommandAnswer},
			{SUM_SHOW_COMMAND_NAME, ShowCommandResult.GetShowCountCommandAnswer}
		};
	}

	protected Task ShowOrdersData (string command, List<Order> todayOrders)
	{
		string answerMessage = todayOrders.Count == 0 ? NOTHING_TO_SHOW_MESSAGE : ShowAllOptionsFunctions[command].Invoke(todayOrders);

		if (answerMessage.Length >= 2000)
		{
			string firstAnswerMessagePart = answerMessage[..1999];
			string secondAnswerMessagePart = firstAnswerMessagePart[firstAnswerMessagePart.LastIndexOf('\n')..];
			firstAnswerMessagePart = firstAnswerMessagePart[..firstAnswerMessagePart.LastIndexOf('\n')];
			secondAnswerMessagePart = secondAnswerMessagePart.Insert(secondAnswerMessagePart.Length, answerMessage[2000..]);
			firstAnswerMessagePart = firstAnswerMessagePart.Insert(firstAnswerMessagePart.Length, DiscordAsciiTablePreparer.DISCORD_CODE_TAG);
			secondAnswerMessagePart = secondAnswerMessagePart.Insert(0, DiscordAsciiTablePreparer.DISCORD_CODE_TAG);

			return ReplyAsync(firstAnswerMessagePart).ContinueWith(task => ReplyAsync(secondAnswerMessagePart));
		}
		
		return ReplyAsync(todayOrders.Count == 0 ? NOTHING_TO_SHOW_MESSAGE : ShowAllOptionsFunctions[command].Invoke(todayOrders));
	}
}