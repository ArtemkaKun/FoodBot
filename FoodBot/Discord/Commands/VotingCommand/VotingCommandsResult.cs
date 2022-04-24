using FoodBot.VotingSystem;

namespace FoodBot.Discord.Commands.VotingCommand;

public static class VotingCommandsResult
{
	public static string? SetVotingStartParameters (ulong guildID, ulong channelID, string startTime, string message)
	{
		if (guildID == 0 || channelID == 0 || string.IsNullOrEmpty(startTime) || string.IsNullOrEmpty(message) || TimeSpan.TryParse(startTime, out TimeSpan startTimeParsed) == false) // TODO code duplication. 24.04.2022. Artem Yurchenko
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

	public static string? SetVotingMainParameters (ulong guildID, ulong channelID, uint durationInMinutes)
	{
		if (guildID == 0 || channelID == 0 || durationInMinutes == 0) // TODO code duplication. 24.04.2022. Artem Yurchenko
		{
			return "Wrong input parameters";
		}
		
		return Program.VotingSystemDB.AddVotingMainParameters(new VotingMainParameters
		{
			GuildID = guildID,
			ChannelID = channelID,
			DurationInMinutes = durationInMinutes
		});
	}
}