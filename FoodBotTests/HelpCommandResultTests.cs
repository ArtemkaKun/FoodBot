using FoodBot.Discord.Commands.HelpCommand;
using NUnit.Framework;

namespace FoodBotTests;

public class HelpCommandResultTests
{
	[Test]
	public void GetHelpCommandResultString_EqualsToExpectedString_True ()
	{
		string expectedHelpCommandAnswer = "```Commands  | Description          | \n----------------------------------\n!help, !h | Shows bot's commands | \n```";
		string actualHelpCommandAnswer = HelpCommandResult.GetHelpCommandAnswer();
		Assert.AreEqual(expectedHelpCommandAnswer, actualHelpCommandAnswer);
	}
}