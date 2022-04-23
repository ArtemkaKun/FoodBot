using System.Collections.Generic;
using FoodBot.Discord.Commands.HelpCommand;
using NUnit.Framework;

namespace FoodBotTests;

public class BotCommandsCollectorTests
{
	[Test]
	public void GetAllBotCommands_ResultIsEqualsToExpected_True ()
	{
		Dictionary<string, string> expectedCommandsDescriptionMap = new()
		{
			{"!help, !h", "Shows bot's commands"}, // TODO same values as in the BotCommandsDataTablePreparerTests class. 13.04.2022. Artem Yurchenko.
			{"!order -mk, <orderText>", "Creates a new order"}, // TODO same values as in the BotCommandsDataTablePreparerTests class. 22.04.2022. Artem Yurchenko.
			{"!order -upd, <orderID> <newOrderText>", "Updates order with provided text"}, // TODO same values as in the BotCommandsDataTablePreparerTests class. 22.04.2022. Artem Yurchenko.
			{"!order -del, <orderID>", "Deletes order"}, // TODO same values as in the BotCommandsDataTablePreparerTests class. 22.04.2022. Artem Yurchenko.
			{"!shmy", "Shows your today's orders"}, // TODO same values as in the BotCommandsDataTablePreparerTests class. 23.04.2022. Artem Yurchenko.
			{"!shmy -sort", "Shows your today's orders (sorted)"}, // TODO same values as in the BotCommandsDataTablePreparerTests class. 23.04.2022. Artem Yurchenko.
			{"!shmy -sum", "Shows your today's orders (summary)"}, // TODO same values as in the BotCommandsDataTablePreparerTests class. 23.04.2022. Artem Yurchenko.
			{"!shall", "Shows all today's orders"}, // TODO same values as in the BotCommandsDataTablePreparerTests class. 23.04.2022. Artem Yurchenko.
			{"!shall -sort", "Shows all today's orders (sorted)"}, // TODO same values as in the BotCommandsDataTablePreparerTests class. 23.04.2022. Artem Yurchenko.
			{"!shall -sum", "Shows all today's orders (summary)"} // TODO same values as in the BotCommandsDataTablePreparerTests class. 23.04.2022. Artem Yurchenko.
		};
		
		Dictionary<string, string> actualCommandsDescriptionMap = BotCommandsCollector.GetCommandDescriptionMap();

		foreach ((string? command, string? description) in actualCommandsDescriptionMap)
		{
			if (expectedCommandsDescriptionMap.ContainsKey(command) == false || expectedCommandsDescriptionMap[command] != description)
			{
				Assert.Fail($"Not expected \"{command}\" or \"{description}\"");
				return;
			}
		}
		
		Assert.AreEqual(expectedCommandsDescriptionMap, actualCommandsDescriptionMap);
	}
}