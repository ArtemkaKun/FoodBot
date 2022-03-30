using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodBot.DBSystem;

public abstract record EntityWithChatIdentifier
{
	[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public uint ID { get; init; }
	public ulong GuildID { get; init; }
	public ulong ChannelID { get; init; }
}