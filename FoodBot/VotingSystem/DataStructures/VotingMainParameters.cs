using FoodBot.DBSystem;

namespace FoodBot.VotingSystem;

public record VotingMainParameters : EntityWithChatIdentifier
{
	public uint DurationInMinutes { get; init; }
}