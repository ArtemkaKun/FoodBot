using System.Collections.Generic;
using FoodBot.Discord.Commands.ShowCommand;
using FoodBot.OrdersSystem;
using NUnit.Framework;

namespace FoodBotTests;

public class ShowCommandResultTests
{
	[Test]
	public void GetShowCommandResultString_EqualsToExpectedString_True ()
	{
		string expectedHelpCommandAnswer = "```ID | Person | Order      | "
		  + "\n--------------------------\n"
		  + "1  | Test   | Test Text  | \n"
		  + "2  | Test1  | Test Text1 | \n"
		  + "3  | Test2  | Test Text2 | \n```";
		
		List<Order> orders = new()
		{
			new Order
			{
				ID = 1,
				PersonName = "Test",
				Text = "Test Text"
			},
			new Order
			{
				ID = 2,
				PersonName = "Test1",
				Text = "Test Text1"
			},
			new Order
			{
				ID = 3,
				PersonName = "Test2",
				Text = "Test Text2"
			}
		};
		
		string actualShowCommandAnswer = ShowCommandResult.GetShowCommandAnswer(orders);
		Assert.AreEqual(expectedHelpCommandAnswer, actualShowCommandAnswer);
	}
	
	[Test]
	public void GetShowSortedCommandResultString_EqualsToExpectedString_True ()
	{
		string expectedHelpCommandAnswer = "```ID | Person | Order      | "
		  + "\n--------------------------\n"
		  + "1  | Test   | Test Text  | \n"
		  + "2  | Test1  | Test Text1 | \n"
		  + "3  | Test2  | Test Text2 | \n```";
		
		List<Order> orders = new()
		{
			new Order
			{
				ID = 3,
				PersonName = "Test2",
				Text = "Test Text2"
			},
			new Order
			{
				ID = 1,
				PersonName = "Test",
				Text = "Test Text"
			},
			new Order
			{
				ID = 2,
				PersonName = "Test1",
				Text = "Test Text1"
			}
		};
		
		string actualShowCommandAnswer = ShowCommandResult.GetShowSortedCommandAnswer(orders);
		Assert.AreEqual(expectedHelpCommandAnswer, actualShowCommandAnswer);
	}
	
	[Test]
	public void GetShowCountCommandResultString_EqualsToExpectedString_True ()
	{
		string expectedHelpCommandAnswer = "```Order      | Count | "
		  + "\n--------------------\n"
		  + "Test Text1 | 3     | \n"
		  + "Test Text2 | 2     | \n"
		  + "Test Text3 | 1     | \n```";
		
		List<Order> orders = new()
		{
			new Order
			{
				ID = 1,
				PersonName = "Test",
				Text = "Test Text1"
			},
			new Order
			{
				ID = 1,
				PersonName = "Test",
				Text = "Test Text2"
			},
			new Order
			{
				ID = 1,
				PersonName = "Test",
				Text = "Test Text1"
			},
			new Order
			{
				ID = 1,
				PersonName = "Test",
				Text = "Test Text2"
			},
			new Order
			{
				ID = 1,
				PersonName = "Test",
				Text = "Test Text3"
			},
			new Order
			{
				ID = 1,
				PersonName = "Test",
				Text = "Test Text1"
			}
		};
		
		string actualShowCommandAnswer = ShowCommandResult.GetShowCountCommandAnswer(orders);
		Assert.AreEqual(expectedHelpCommandAnswer, actualShowCommandAnswer);
	}
}