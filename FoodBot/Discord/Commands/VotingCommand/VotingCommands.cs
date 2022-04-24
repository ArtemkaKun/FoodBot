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
		string? errorMessage = VotingCommandsResult.SetVotingStartParameters(Context.Guild.Id, Context.Channel.Id, startTime, message);

		return ReplyAsync(errorMessage ?? "Start parameters set");
	}

	[Command(SET_MAIN_PARAMETERS_COMMAND_NAME)]
	[Summary(SET_MAIN_COMMAND_DESCRIPTION)]
	public Task SetVoteMainParameters (uint durationInMinutes)
	{
		string? errorMessage = VotingCommandsResult.SetVotingMainParameters(Context.Guild.Id, Context.Channel.Id, durationInMinutes);

		return ReplyAsync(errorMessage ?? "Main parameters set");
	}

	[Command(SET_END_PARAMETERS_COMMAND_NAME)]
	[Summary(SET_END_COMMAND_DESCRIPTION)]
	public Task SetVoteEndParameters ([Remainder] string message)
	{
		string? errorMessage = VotingCommandsResult.SetVotingEndParameters(Context.Guild.Id, Context.Channel.Id, message);

		return ReplyAsync(errorMessage ?? "End parameters set");
	}

	[Command(SHOW_COMMAND_NAME)]
	[Summary(SHOW_COMMAND_DESCRIPTION)]
	public Task ShowChannelVoteParameters ()
	{
		(string? message, string? errorMessage) = VotingCommandsResult.GetVotingParameters(Context.Guild.Id, Context.Channel.Id);

		return ReplyAsync(errorMessage ?? message);
	}

	[Command(DELETE_START_PARAMETERS_COMMAND_NAME)]
	[Summary(DELETE_START_COMMAND_DESCRIPTION)]
	public Task DeleteVoteStartParameters ()
	{
		string? errorMessage = VotingCommandsResult.RemoveVotingStartParameters(Context.Guild.Id, Context.Channel.Id);

		return ReplyAsync(errorMessage ?? "Start parameters removed");
	}

	[Command(DELETE_MAIN_PARAMETERS_COMMAND_NAME)]
	[Summary(DELETE_MAIN_COMMAND_DESCRIPTION)]
	public Task DeleteVoteMainParameters ()
	{
		string? errorMessage = VotingCommandsResult.RemoveVotingMainParameters(Context.Guild.Id, Context.Channel.Id);

		return ReplyAsync(errorMessage ?? "Main parameters removed");
	}

	[Command(DELETE_END_PARAMETERS_COMMAND_NAME)]
	[Summary(DELETE_END_COMMAND_DESCRIPTION)]
	public Task DeleteVoteEndParameters ()
	{
		string? errorMessage = VotingCommandsResult.RemoveVotingEndParameters(Context.Guild.Id, Context.Channel.Id);

		return ReplyAsync(errorMessage ?? "End parameters removed");
	}
}