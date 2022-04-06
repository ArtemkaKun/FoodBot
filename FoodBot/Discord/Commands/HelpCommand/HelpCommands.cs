using Discord.Commands;

namespace FoodBot.Discord.Commands.HelpCommand;

public class HelpCommands : ModuleBase<SocketCommandContext>
{
	private const string HELP_COMMAND_NAME = "help";
	private const string HELP_COMMAND_ALIAS_NAME = "h";
	private const string HELP_COMMAND_DESCRIPTION = "Shows bot's commands";

	[Command(HELP_COMMAND_NAME)]
	[Alias(HELP_COMMAND_ALIAS_NAME)]
	[Summary(HELP_COMMAND_DESCRIPTION)]
	public Task ShowHelpMessage ()
	{
		return null;
	}
}