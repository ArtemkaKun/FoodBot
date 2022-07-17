using System.Text;

namespace FoodBot.Discord.Commands.Helpers;

public static class DiscordAsciiTablePreparer
{
	public const string DISCORD_CODE_TAG = "```";

	public static StringBuilder PrepareAsciiTable (StringBuilder asciiTableBuilder)
	{
		asciiTableBuilder.Insert(0, DISCORD_CODE_TAG);
		asciiTableBuilder.Append(DISCORD_CODE_TAG);
		return asciiTableBuilder;
	}
}