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
			testVotingSystemDB.RemoveVotingStartParameters(testVotingStartParameters);
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
		testVotingSystemDB.RemoveVotingStartParameters(testVotingStartParameters);

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
			testVotingSystemDB.RemoveVotingParameters(testVotingParameters);
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
		testVotingSystemDB.RemoveVotingParameters(testVotingParameters);

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
			testVotingSystemDB.RemoveVotingEndParameters(testVotingEndParameters);
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
		testVotingSystemDB.RemoveVotingEndParameters(testVotingEndParameters);

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
		testVotingSystemDB.RemoveVotingEndParameters(testVotingEndParameters);

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