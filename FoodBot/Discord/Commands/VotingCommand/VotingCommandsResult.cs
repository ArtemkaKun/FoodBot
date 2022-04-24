using FoodBot.VotingSystem;

namespace FoodBot.Discord.Commands.VotingCommand;

public static class VotingCommandsResult
{
	public static string? SetVotingStartParameters (ulong guildID, ulong channelID, string startTime, string message)
	{
		if (guildID == 0 || channelID == 0 || string.IsNullOrEmpty(startTime) == true || string.IsNullOrEmpty(message) == true || TimeSpan.TryParse(startTime, out TimeSpan startTimeParsed) == false) // TODO code duplication. 24.04.2022. Artem Yurchenko
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

	public static string? SetVotingEndParameters (ulong guildID, ulong channelID, string message)
	{
		if (guildID == 0 || channelID == 0 || string.IsNullOrEmpty(message) == true) // TODO code duplication. 24.04.2022. Artem Yurchenko
		{
			return "Wrong input parameters";
		}

		return Program.VotingSystemDB.AddVotingEndParameters(new VotingEndParameters
		{
			GuildID = guildID,
			ChannelID = channelID,
			Message = message
		});
	}

	public static (string? message, string? errorMessage) GetVotingParameters (ulong guildID, ulong channelID)
	{
		if (guildID == 0 || channelID == 0) // TODO code duplication. 24.04.2022. Artem Yurchenko
		{
			return (null, "Wrong input parameters");
		}

		bool startParametersGetResult = Program.VotingSystemDB.TryGetVotingStartParametersByChatIdentifier(guildID, channelID, out VotingStartParameters? startParameters);
		bool mainParametersGetResult = Program.VotingSystemDB.TryGetVotingMainParametersByChatIdentifier(guildID, channelID, out VotingMainParameters? mainParameters);
		bool endParametersGetResult = Program.VotingSystemDB.TryGetVotingEndParametersByChatIdentifier(guildID, channelID, out VotingEndParameters? endParameters);

		if (startParametersGetResult == false || mainParametersGetResult == false || endParametersGetResult == false)
		{
			return (null, "No voting parameters found");
		}

		return ($"Start voting at {startParameters?.StartTime} with a message \"{startParameters?.Message}\", voting duration is {mainParameters?.DurationInMinutes} minutes, end message is \"{endParameters?.Message}\"", null);
	}

	public static string? RemoveVotingStartParameters (ulong guildID, ulong channelID)
	{
		if (guildID == 0 || channelID == 0) // TODO code duplication. 24.04.2022. Artem Yurchenko
		{
			return "Wrong input parameters";
		}

		return Program.VotingSystemDB.RemoveVotingStartParametersByChatIdentifier(guildID, channelID);
	}

	public static string? RemoveVotingMainParameters (ulong guildID, ulong channelID)
	{
		if (guildID == 0 || channelID == 0) // TODO code duplication. 24.04.2022. Artem Yurchenko
		{
			return "Wrong input parameters";
		}

		return Program.VotingSystemDB.RemoveVotingMainParametersByChatIdentifier(guildID, channelID);
	}

	public static string? RemoveVotingEndParameters (ulong guildID, ulong channelID)
	{
		if (guildID == 0 || channelID == 0) // TODO code duplication. 24.04.2022. Artem Yurchenko
		{
			return "Wrong input parameters";
		}

		return Program.VotingSystemDB.RemoveVotingEndParametersByChatIdentifier(guildID, channelID);
	}
}