using System.ComponentModel.DataAnnotations.Schema;
using FoodBot.DBSystem;

namespace FoodBot.VotingSystem;

[Table("VotingStartParameters")]
public record VotingStartParameters : EntityWithChatIdentifier
{
	public TimeSpan StartTime { get; init; }
	public string? Message { get; init; }
}