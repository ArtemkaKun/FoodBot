using FoodBot.DBSystem;

namespace FoodBot.VotingSystem;

public record VotingStartParameters : EntityWithChatIdentifier
{
	public TimeSpan StartTime { get; init; }
	public string? Message { get; init; }
}