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

	private readonly VotingParameters testVotingParameters = new()
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
	public void AddVotingParameters_PresentAndValidInDB_True ()
	{
		testVotingSystemDB.AddVotingParameters(testVotingParameters);

		if (testVotingSystemDB.TryGetVotingParametersByChatIdentifier(testVotingParameters.ChatIdentifier, out VotingParameters? parametersInDB) == true)
		{
			Assert.AreEqual(testVotingParameters, parametersInDB);
			testVotingSystemDB.RemoveVotingParametersByChatIdentifier(testVotingParameters.ChatIdentifier);
		}
		else
		{
			Assert.Fail();
		}
	}

	[Test]
	public void AddVotingParametersAndThenRemove_NotPresentInDB_True ()
	{
		testVotingSystemDB.AddVotingParameters(testVotingParameters);
		testVotingSystemDB.RemoveVotingParametersByChatIdentifier(testVotingParameters.ChatIdentifier);

		if (testVotingSystemDB.TryGetVotingParametersByChatIdentifier(testVotingStartParameters.ChatIdentifier, out VotingParameters? _) == false)
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
	public void AddVotingStartParametersAndThenGetAll_PresentAndValidInDB_True ()
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
	public void Add2VotingParametersWithSameChatIdentifier_ErrorReturned_True ()
	{
		testVotingSystemDB.AddVotingParameters(testVotingParameters);
		string? error = testVotingSystemDB.AddVotingParameters(testVotingParameters);

		if (error is "Voting parameters already exists for this chat!")
		{
			Assert.Pass();
			testVotingSystemDB.RemoveVotingParametersByChatIdentifier(testVotingParameters.ChatIdentifier);
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
	public void RemoveUnexistedVotingParameters_ErrorReturned_True ()
	{
		string? error = testVotingSystemDB.RemoveVotingParametersByChatIdentifier(testVotingParameters.ChatIdentifier);

		if (error is "No voting parameters for this chat!")
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
}