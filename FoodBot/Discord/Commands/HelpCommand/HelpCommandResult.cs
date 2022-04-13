using System.Data;
using System.Text;
using AsciiTableGenerators;
using FoodBot.Discord.Commands.Helpers;

namespace FoodBot.Discord.Commands.HelpCommand;

public static class HelpCommandResult
{
	public static string GetHelpCommandAnswer ()
	{
		Dictionary<string, string> actualCommandsDescriptionMap = BotCommandsCollector.GetCommandDescriptionMap();
		DataTable commandsDataTable = BotCommandsDataTablePreparer.GetCommandsDataTable(actualCommandsDescriptionMap);
		StringBuilder commandsTableBuilder = AsciiTableGenerator.CreateAsciiTableFromDataTable(commandsDataTable);
		StringBuilder preparedCommandsTableBuilder = DiscordAsciiTablePreparer.PrepareAsciiTable(commandsTableBuilder);

		return preparedCommandsTableBuilder.ToString();
	}
}