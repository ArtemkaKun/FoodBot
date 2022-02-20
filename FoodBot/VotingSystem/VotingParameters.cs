using FoodBot.DBSystem;

namespace FoodBot.VotingSystem;

public record VotingParameters : EntityWithChatIdentifier
{
	public uint DurationInMinutes { get; init; }
}