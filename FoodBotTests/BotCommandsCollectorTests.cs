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
			{"!help, !h", "Shows bot's commands"} // TODO same values as in the BotCommandsDataTablePreparerTests class. 13.04.2022. Artem Yurchenko.
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