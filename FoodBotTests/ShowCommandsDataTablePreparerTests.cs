using System.Collections.Generic;
using System.Data;
using FoodBot.Discord.Commands.ShowCommand;
using FoodBot.OrdersSystem;
using NUnit.Framework;

namespace FoodBotTests;

public class ShowCommandsDataTablePreparerTests
{
	[Test]
	public void ConvertOrdersToDataTable_ReturnedDataTableEqualToExpected_True ()
	{
		DataTable expectedDataTable = new();
		// TODO same values as in the ShowCommandsDataTablePreparer class. 23.04.2022. Artem Yurchenko
		expectedDataTable.Columns.Add("ID", typeof(uint));
		expectedDataTable.Columns.Add("Person", typeof(string));
		expectedDataTable.Columns.Add("Order", typeof(string));
		expectedDataTable.Rows.Add("1", "Tets", "Test Text");
		expectedDataTable.Rows.Add("2", "Tets1", "Test Text1");
		expectedDataTable.Rows.Add("3", "Tets2", "Test Text2");
		
		List<Order> orders = new()
		{
			new Order
			{
				ID = 1,
				PersonName = "Tets",
				Text = "Test Text"
			},
			new Order
			{
				ID = 2,
				PersonName = "Tets1",
				Text = "Test Text1"
			},
			new Order
			{
				ID = 3,
				PersonName = "Tets2",
				Text = "Test Text2"
			}
		};
		
		DataTable commandsDataTable = ShowCommandsDataTablePreparer.GetOrdersDataTable(orders);

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
	
	[Test]
	public void ConvertRandomOrdersToDataTable_ReturnedDataTableEqualToExpected_True ()
	{
		DataTable expectedDataTable = new();
		// TODO same values as in the ShowCommandsDataTablePreparer class. 23.04.2022. Artem Yurchenko
		expectedDataTable.Columns.Add("ID", typeof(uint));
		expectedDataTable.Columns.Add("Person", typeof(string));
		expectedDataTable.Columns.Add("Order", typeof(string));
		expectedDataTable.Rows.Add("3", "Tets2", "Test Text2");
		expectedDataTable.Rows.Add("1", "Tets", "Test Text");
		expectedDataTable.Rows.Add("2", "Tets1", "Test Text1");
		
		List<Order> orders = new()
		{
			new Order
			{
				ID = 3,
				PersonName = "Tets2",
				Text = "Test Text2"
			},
			new Order
			{
				ID = 1,
				PersonName = "Tets",
				Text = "Test Text"
			},
			new Order
			{
				ID = 2,
				PersonName = "Tets1",
				Text = "Test Text1"
			}
		};
		
		DataTable commandsDataTable = ShowCommandsDataTablePreparer.GetOrdersDataTable(orders);

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
	
	[Test]
	public void ConvertCountedOrdersToDataTable_ReturnedDataTableEqualToExpected_True ()
	{
		DataTable expectedDataTable = new();
		// TODO same values as in the ShowCommandsDataTablePreparer class. 23.04.2022. Artem Yurchenko
		expectedDataTable.Columns.Add("Order", typeof(string));
		expectedDataTable.Columns.Add("Count", typeof(uint));
		expectedDataTable.Rows.Add("Test Text", 3);
		expectedDataTable.Rows.Add("Test Text1", 2);
		expectedDataTable.Rows.Add("Test Text2", 1);

		List<Order> orders = new()
		{
			new Order
			{
				ID = 1,
				PersonName = "Tets",
				Text = "Test Text"
			},
			new Order
			{
				ID = 2,
				PersonName = "Tets1",
				Text = "Test Text"
			},
			new Order
			{
				ID = 3,
				PersonName = "Tets2",
				Text = "Test Text"
			},
			new Order
			{
				ID = 4,
				PersonName = "Tets3",
				Text = "Test Text1"
			},
			new Order
			{
				ID = 5,
				PersonName = "Tets4",
				Text = "Test Text2"
			},
			new Order
			{
				ID = 6,
				PersonName = "Tets5",
				Text = "Test Text1"
			}
		};

		DataTable commandsDataTable = ShowCommandsDataTablePreparer.GetCountedOrdersDataTable(orders);

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