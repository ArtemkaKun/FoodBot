using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoodBot.Shared;

namespace FoodBot.VotingSystem;

[Table("VotingStartParameters")]
public record VotingStartParameters
{
	[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public uint ID { get; init; }
	public DiscordChatIdentifier ChatIdentifier { get; init; }
	public TimeSpan StartTime { get; init; }
	public string? Message { get; init; }
}