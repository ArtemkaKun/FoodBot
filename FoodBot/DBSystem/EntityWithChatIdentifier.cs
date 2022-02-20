using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoodBot.Shared;

namespace FoodBot.DBSystem;

public abstract record EntityWithChatIdentifier
{
	[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public uint ID { get; init; }
	public DiscordChatIdentifier ChatIdentifier { get; init; } = null!;
}