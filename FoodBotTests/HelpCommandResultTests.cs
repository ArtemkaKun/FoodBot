using FoodBot.Discord.Commands.HelpCommand;
using NUnit.Framework;

namespace FoodBotTests;

public class HelpCommandResultTests
{
	[Test]
	public void GetHelpCommandResultString_EqualsToExpectedString_True ()
	{
		string expectedHelpCommandAnswer = "```Commands                              | Description                      | "
		  + "\n--------------------------------------------------------------------------\n"
		  + "!help, !h                             | Shows bot's commands             | \n"
		  + "!order -mk, <orderText>               | Creates a new order              | \n"
		  + "!order -upd, <orderID> <newOrderText> | Updates order with provided text | \n"
		  + "!order -del, <orderID>                | Deletes order                    | \n```";
		string actualHelpCommandAnswer = HelpCommandResult.GetHelpCommandAnswer();
		Assert.AreEqual(expectedHelpCommandAnswer, actualHelpCommandAnswer);
	}
}