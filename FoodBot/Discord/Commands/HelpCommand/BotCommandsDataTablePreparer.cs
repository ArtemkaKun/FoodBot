using System.Data;

namespace FoodBot.Discord.Commands.HelpCommand;

public static class BotCommandsDataTablePreparer
{
	private const string COMMANDS_COLUMN_NAME = "Commands";
	private const string DESCRIPTION_COLUMN_NAME = "Description";
	
	public static DataTable GetCommandsDataTable (Dictionary<string, string> commandDescriptionMap)
	{
		DataTable commandsTable = new();
		commandsTable.Columns.Add(COMMANDS_COLUMN_NAME, typeof(string));
		commandsTable.Columns.Add(DESCRIPTION_COLUMN_NAME, typeof(string));	
		
		foreach ((string? command, string? description) in commandDescriptionMap)
		{
			commandsTable.Rows.Add(command, description);
		}
		
		return commandsTable;
	}
}