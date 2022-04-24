using FoodBot.Discord.Commands.HelpCommand;
using NUnit.Framework;

namespace FoodBotTests;

public class HelpCommandResultTests
{
	[Test]
	public void GetHelpCommandResultString_EqualsToExpectedString_True ()
	{
		string expectedHelpCommandAnswer = "```Commands                               | Description                                 | "
		  + "\n--------------------------------------------------------------------------------------\n"
		  + "!help, !h                              | Shows bot's commands                        | \n"
		  + "!order -mk, <orderText>                | Creates a new order                         | \n"
		  + "!order -upd, <orderID> <newOrderText>  | Updates order with provided text            | \n"
		  + "!order -del, <orderID>                 | Deletes order                               | \n"
		  + "!shall                                 | Shows all today's orders                    | \n"
		  + "!shall -sort                           | Shows all today's orders (sorted)           | \n"
		  + "!shall -sum                            | Shows all today's orders (summary)          | \n"
		  + "!shmy                                  | Shows your today's orders                   | \n"
		  + "!shmy -sort                            | Shows your today's orders (sorted)          | \n"
		  + "!shmy -sum                             | Shows your today's orders (summary)         | \n"
		  + "!vote -setStart, <startTime> <message> | Sets food vote reminder start parameters    | \n"
		  + "!vote -setMain, <durationInMinutes>    | Sets food vote reminder main parameters     | \n"
		  + "!vote -setEnd, <message>               | Sets food vote reminder end parameters      | \n"
		  + "!vote -sh                              | Show channel's reminder data                | \n"
		  + "!vote -delStart                        | Deletes food vote reminder start parameters | \n"
		  + "!vote -delMain                         | Deletes food vote reminder main parameters  | \n"
		  + "!vote -delEnd                          | Deletes food vote reminder end parameters   | \n"
		  + "```";
		string actualHelpCommandAnswer = HelpCommandResult.GetHelpCommandAnswer();
		Assert.AreEqual(expectedHelpCommandAnswer, actualHelpCommandAnswer);
	}
}