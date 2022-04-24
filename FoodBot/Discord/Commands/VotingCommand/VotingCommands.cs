using Discord.Commands;

namespace FoodBot.Discord.Commands.VotingCommand;

[Group(VOTE_COMMANDS_GROUP_NAME)]
public class VotingCommands : ModuleBase<SocketCommandContext>
{
	private const string VOTE_COMMANDS_GROUP_NAME = "vote";
	private const string SET_START_COMMAND_DESCRIPTION = "Sets food vote reminder start parameters";
	private const string SET_MAIN_COMMAND_DESCRIPTION = "Sets food vote reminder main parameters";
	private const string SET_END_COMMAND_DESCRIPTION = "Sets food vote reminder end parameters";
	private const string SHOW_COMMAND_DESCRIPTION = "Show channel's reminder data";
	private const string DELETE_START_COMMAND_DESCRIPTION = "Deletes food vote reminder start parameters";
	private const string DELETE_MAIN_COMMAND_DESCRIPTION = "Deletes food vote reminder main parameters";
	private const string DELETE_END_COMMAND_DESCRIPTION = "Deletes food vote reminder end parameters";
	private const string SET_START_PARAMETERS_COMMAND_NAME = "-setStart";
	private const string SET_MAIN_PARAMETERS_COMMAND_NAME = "-setMain";
	private const string SET_END_PARAMETERS_COMMAND_NAME = "-setEnd";
	private const string SHOW_COMMAND_NAME = "-sh";
	private const string DELETE_START_PARAMETERS_COMMAND_NAME = "-delStart";
	private const string DELETE_MAIN_PARAMETERS_COMMAND_NAME = "-delMain";
	private const string DELETE_END_PARAMETERS_COMMAND_NAME = "-delEnd";

	[Command(SET_START_PARAMETERS_COMMAND_NAME)]
	[Summary(SET_START_COMMAND_DESCRIPTION)]
	public Task SetVoteStartParameters (string startTime, [Remainder] string message)
	{
		return null;
	}

	[Command(SET_MAIN_PARAMETERS_COMMAND_NAME)]
	[Summary(SET_MAIN_COMMAND_DESCRIPTION)]
	public Task SetVoteMainParameters (uint durationInMinutes)
	{
		return null;
	}
	
	[Command(SET_END_PARAMETERS_COMMAND_NAME)]
	[Summary(SET_END_COMMAND_DESCRIPTION)]
	public Task SetVoteEndParameters ([Remainder] string message)
	{
		return null;
	}
	
	[Command(SHOW_COMMAND_NAME)]
	[Summary(SHOW_COMMAND_DESCRIPTION)]
	public Task ShowChannelVoteParameters ()
	{
		return null;
	}

	[Command(DELETE_START_PARAMETERS_COMMAND_NAME)]
	[Summary(DELETE_START_COMMAND_DESCRIPTION)]
	public Task DeleteVoteStartParameters ()
	{
		return null;
	}

	[Command(DELETE_MAIN_PARAMETERS_COMMAND_NAME)]
	[Summary(DELETE_MAIN_COMMAND_DESCRIPTION)]
	public Task DeleteVoteMainParameters ()
	{
		return null;
	}
	
	[Command(DELETE_END_PARAMETERS_COMMAND_NAME)]
	[Summary(DELETE_END_COMMAND_DESCRIPTION)]
	public Task DeleteVoteEndParameters ()
	{
		return null;
	}
}