using FoodBot.DBSystem;

namespace FoodBot.VotingSystem;

public record VotingEndParameters : EntityWithChatIdentifier
{
	public string? Message { get; init; }
}