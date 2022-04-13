using System.Text;
using FoodBot.Discord.Commands.Helpers;
using NUnit.Framework;

namespace FoodBotTests;

public class DiscordAsciiTablePreparerTests
{
	[Test]
	public void CreateSimpleStringBuilderAndProcessItWithDiscordAsciiTablePreparer_ResultStringContainsDiscordCodeBlockTagsInExpectedPlaces_True ()
	{
		StringBuilder testBuilder = new();
		testBuilder.Append("test");
		StringBuilder processedTestBuilder = DiscordAsciiTablePreparer.PrepareAsciiTable(testBuilder);
		Assert.AreEqual(processedTestBuilder.ToString(), "```test```");
	}
}