using FoodBot.Shared;

namespace FoodBot.VotingSystem;

public readonly struct VotingParameters
{
	public readonly DiscordChatIdentifier chatIdentifier;
	public readonly TimeSpan startTime;
	public readonly string? startMessage;
	public readonly uint durationInMinutes;
	public readonly string? endMessage;

	public VotingParameters (VotingStartParameters startParameters, VotingMainParameters mainParameters, VotingEndParameters endParameters)
	{
		chatIdentifier = startParameters.ChatIdentifier;
		startTime = startParameters.StartTime;
		startMessage = startParameters.Message;
		durationInMinutes = mainParameters.DurationInMinutes;
		endMessage = endParameters.Message;
	}
}