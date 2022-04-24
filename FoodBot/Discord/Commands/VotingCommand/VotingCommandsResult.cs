using FoodBot.VotingSystem;

namespace FoodBot.Discord.Commands.VotingCommand;

public static class VotingCommandsResult
{
	public static string? SetVotingStartParameters (ulong guildID, ulong channelID, string startTime, string message)
	{
		if (guildID == 0 || channelID == 0 || string.IsNullOrEmpty(startTime) || string.IsNullOrEmpty(message) || TimeSpan.TryParse(startTime, out TimeSpan startTimeParsed) == false)
		{
			return "Wrong input parameters";
		}

		return Program.VotingSystemDB.AddVotingStartParameters(new VotingStartParameters
		{
			GuildID = guildID,
			ChannelID = channelID,
			StartTime = startTimeParsed,
			Message = message
		});
	}
}