using System.ComponentModel.DataAnnotations;

namespace FoodBot.Shared;

public record DiscordChatIdentifier
{
	public ulong GuildID { get; init; }
	[Key]
	public ulong ChannelID { get; init; }
}