using System;
using FoodBot.DBSystem;
using FoodBot.Shared;
using FoodBot.VotingSystem;
using NUnit.Framework;

namespace FoodBotTests;

public class VotingSystemDBTests
{
	private readonly VotingSystemDB testVotingSystemDB = new();
	private static readonly DiscordChatIdentifier TEST_CHAT_IDENTIFIER = new() {ChannelID = 0, GuildID = 0};

	private readonly VotingStartParameters testVotingStartParameters = new()
	{
		ChatIdentifier = TEST_CHAT_IDENTIFIER,
		Message = "test",
		StartTime = new TimeSpan(10, 0, 0)
	};

	private readonly VotingMainParameters testVotingMainParameters = new()
	{
		ChatIdentifier = TEST_CHAT_IDENTIFIER,
		DurationInMinutes = 60
	};

	private readonly VotingEndParameters testVotingEndParameters = new()
	{
		ChatIdentifier = TEST_CHAT_IDENTIFIER,
		Message = "testend"
	};

	[OneTimeSetUp]
	public void InitializeDB ()
	{
		testVotingSystemDB.Initialize();
	}

	[Test]
	public void AddVotingStartParameters_PresentAndValidInDB_True ()
	{
		testVotingSystemDB.AddVotingStartParameters(testVotingStartParameters);

		if (testVotingSystemDB.TryGetVotingStartParametersByChatIdentifier(testVotingStartParameters.ChatIdentifier, out VotingStartParameters? parametersInDB) == true)
		{
			Assert.AreEqual(testVotingStartParameters, parametersInDB);
			testVotingSystemDB.RemoveVotingStartParametersByChatIdentifier(testVotingStartParameters.ChatIdentifier);
		}
		else
		{
			Assert.Fail();
		}
	}

	[Test]
	public void AddVotingStartParametersAndThenRemove_NotPresentInDB_True ()
	{
		testVotingSystemDB.AddVotingStartParameters(testVotingStartParameters);
		testVotingSystemDB.RemoveVotingStartParametersByChatIdentifier(testVotingStartParameters.ChatIdentifier);

		if (testVotingSystemDB.TryGetVotingStartParametersByChatIdentifier(testVotingStartParameters.ChatIdentifier, out VotingStartParameters? _) == false)
		{
			Assert.Pass();
		}
		else
		{
			Assert.Fail();
		}
	}

	[Test]
	public void AddVotingMainParameters_PresentAndValidInDB_True ()
	{
		testVotingSystemDB.AddVotingMainParameters(testVotingMainParameters);

		if (testVotingSystemDB.TryGetVotingMainParametersByChatIdentifier(testVotingMainParameters.ChatIdentifier, out VotingMainParameters? parametersInDB) == true)
		{
			Assert.AreEqual(testVotingMainParameters, parametersInDB);
			testVotingSystemDB.RemoveVotingMainParametersByChatIdentifier(testVotingMainParameters.ChatIdentifier);
		}
		else
		{
			Assert.Fail();
		}
	}

	[Test]
	public void AddVotingMainParametersAndThenRemove_NotPresentInDB_True ()
	{
		testVotingSystemDB.AddVotingMainParameters(testVotingMainParameters);
		testVotingSystemDB.RemoveVotingMainParametersByChatIdentifier(testVotingMainParameters.ChatIdentifier);

		if (testVotingSystemDB.TryGetVotingMainParametersByChatIdentifier(testVotingStartParameters.ChatIdentifier, out VotingMainParameters? _) == false)
		{
			Assert.Pass();
		}
		else
		{
			Assert.Fail();
		}
	}

	[Test]
	public void AddVotingEndParameters_PresentAndValidInDB_True ()
	{
		testVotingSystemDB.AddVotingEndParameters(testVotingEndParameters);

		if (testVotingSystemDB.TryGetVotingEndParametersByChatIdentifier(testVotingEndParameters.ChatIdentifier, out VotingEndParameters? parametersInDB) == true)
		{
			Assert.AreEqual(testVotingEndParameters, parametersInDB);
			testVotingSystemDB.RemoveVotingEndParametersByChatIdentifier(testVotingEndParameters.ChatIdentifier);
		}
		else
		{
			Assert.Fail();
		}
	}

	[Test]
	public void AddVotingEndParametersAndThenRemove_NotPresentInDB_True ()
	{
		testVotingSystemDB.AddVotingEndParameters(testVotingEndParameters);
		testVotingSystemDB.RemoveVotingEndParametersByChatIdentifier(testVotingEndParameters.ChatIdentifier);

		if (testVotingSystemDB.TryGetVotingEndParametersByChatIdentifier(testVotingStartParameters.ChatIdentifier, out VotingEndParameters? _) == false)
		{
			Assert.Pass();
		}
		else
		{
			Assert.Fail();
		}
	}

	[Test]
	public void Add2VotingStartParametersWithSameChatIdentifier_ErrorReturned_True ()
	{
		testVotingSystemDB.AddVotingStartParameters(testVotingStartParameters);
		string? error = testVotingSystemDB.AddVotingStartParameters(testVotingStartParameters);

		if (error is "Voting start parameters already exists for this chat!")
		{
			Assert.Pass();
			testVotingSystemDB.RemoveVotingStartParametersByChatIdentifier(testVotingStartParameters.ChatIdentifier);
		}
		else
		{
			Assert.Fail();
		}
	}

	[Test]
	public void Add2VotingMainParametersWithSameChatIdentifier_ErrorReturned_True ()
	{
		testVotingSystemDB.AddVotingMainParameters(testVotingMainParameters);
		string? error = testVotingSystemDB.AddVotingMainParameters(testVotingMainParameters);

		if (error is "Voting main parameters already exists for this chat!")
		{
			Assert.Pass();
			testVotingSystemDB.RemoveVotingMainParametersByChatIdentifier(testVotingMainParameters.ChatIdentifier);
		}
		else
		{
			Assert.Fail();
		}
	}

	[Test]
	public void Add2VotingEndParametersWithSameChatIdentifier_ErrorReturned_True ()
	{
		testVotingSystemDB.AddVotingEndParameters(testVotingEndParameters);
		string? error = testVotingSystemDB.AddVotingEndParameters(testVotingEndParameters);

		if (error is "Voting end parameters already exists for this chat!")
		{
			Assert.Pass();
			testVotingSystemDB.RemoveVotingEndParametersByChatIdentifier(testVotingEndParameters.ChatIdentifier);
		}
		else
		{
			Assert.Fail();
		}
	}

	[Test]
	public void RemoveUnexistedVotingStartParameters_ErrorReturned_True ()
	{
		string? error = testVotingSystemDB.RemoveVotingStartParametersByChatIdentifier(testVotingStartParameters.ChatIdentifier);

		if (error is "No voting start parameters for this chat!")
		{
			Assert.Pass();
		}
		else
		{
			Assert.Fail();
		}
	}

	[Test]
	public void RemoveUnexistedVotingMainParameters_ErrorReturned_True ()
	{
		string? error = testVotingSystemDB.RemoveVotingMainParametersByChatIdentifier(testVotingMainParameters.ChatIdentifier);

		if (error is "No voting main parameters for this chat!")
		{
			Assert.Pass();
		}
		else
		{
			Assert.Fail();
		}
	}

	[Test]
	public void RemoveUnexistedVotingEndParameters_ErrorReturned_True ()
	{
		string? error = testVotingSystemDB.RemoveVotingEndParametersByChatIdentifier(testVotingEndParameters.ChatIdentifier);

		if (error is "No voting end parameters for this chat!")
		{
			Assert.Pass();
		}
		else
		{
			Assert.Fail();
		}
	}

	[Test]
	public void Add2VotingStartParametersAndThenGetAll_PresentAndValidInDB_True ()
	{
		testVotingSystemDB.AddVotingStartParameters(testVotingStartParameters);
		
		testVotingSystemDB.AddVotingStartParameters(new VotingStartParameters
		{
			ChatIdentifier = new DiscordChatIdentifier
			{
				ChannelID = 1, GuildID = 1
			},
			Message = "test2",
			StartTime = new TimeSpan(100)
		});

		if (testVotingSystemDB.TryGetVotingEndParametersByChatIdentifier(testVotingStartParameters.ChatIdentifier, out VotingEndParameters? _) == false)
		{
			Assert.Pass();
		}
		else
		{
			Assert.Fail();
		}
	}
}