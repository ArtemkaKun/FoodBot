using System.Collections.Generic;
using System.Data;
using FoodBot.Discord.Commands.HelpCommand;
using NUnit.Framework;

namespace FoodBotTests;

public class BotCommandsDataTablePreparerTests
{
	[Test]
	public void GetAllBotCommandsAndConvertToDataTable_ReturnedDataTableEqualToExpected_True ()
	{
		DataTable expectedDataTable = new();
		// TODO same values as in the BotCommandsDataTablePreparer class. 13.04.2022. Artem Yurchenko
		expectedDataTable.Columns.Add("Command", typeof(string));
		expectedDataTable.Columns.Add("Description", typeof(string));
		expectedDataTable.Rows.Add("!help, !h", "Shows bot's commands"); // TODO same values as in the BotCommandsCollectorTests class. 13.04.2022. Artem Yurchenko.
		expectedDataTable.Rows.Add("!order -mk, <orderText>", "Creates a new order"); // TODO same values as in the BotCommandsCollectorTests class. 22.04.2022. Artem Yurchenko.
		expectedDataTable.Rows.Add("!order -upd, <orderID> <newOrderText>", "Updates order with provided text"); // TODO same values as in the BotCommandsCollectorTests class. 22.04.2022. Artem Yurchenko.
		expectedDataTable.Rows.Add("!order -del, <orderID>", "Deletes order"); // TODO same values as in the BotCommandsCollectorTests class. 22.04.2022. Artem Yurchenko.

		Dictionary<string, string> actualCommandsDescriptionMap = BotCommandsCollector.GetCommandDescriptionMap();
		DataTable commandsDataTable = BotCommandsDataTablePreparer.GetCommandsDataTable(actualCommandsDescriptionMap);

		if (expectedDataTable.Columns.Count != commandsDataTable.Columns.Count)
		{
			Assert.Fail("The number of columns in the expected data table is not equal to the number of columns in the actual data table.");
			return;
		}

		if (expectedDataTable.Rows.Count != commandsDataTable.Rows.Count)
		{
			Assert.Fail("The number of rows in the expected data table is not equal to the number of rows in the actual data table.");
			return;
		}

		for (int columnIndex = 0; columnIndex < expectedDataTable.Columns.Count; columnIndex++)
		{
			for (int rowIndex = 0; rowIndex < expectedDataTable.Rows.Count; rowIndex++)
			{
				Assert.AreEqual(expectedDataTable.Rows[rowIndex][columnIndex], commandsDataTable.Rows[rowIndex][columnIndex]);
			}
		}
	}
}