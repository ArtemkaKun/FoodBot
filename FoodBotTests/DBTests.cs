using System;
using FoodBot.DBSystem;
using FoodBot.Shared;
using FoodBot.VotingSystem;
using NUnit.Framework;

namespace FoodBotTests;

public class DBTests
{
	private readonly DB testDB = new();
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
		testDB.Initialize();
	}

	[Test]
	public void AddVotingStartParameters_PresentAndValidInDB_True ()
	{
		testDB.AddVotingStartParameters(testVotingStartParameters);

		if (testDB.TryGetVotingStartParametersByChatIdentifier(testVotingStartParameters.ChatIdentifier, out VotingStartParameters? parametersInDB) == true)
		{
			Assert.AreEqual(testVotingStartParameters, parametersInDB);
			testDB.RemoveVotingStartParameters(testVotingStartParameters);
		}
		else
		{
			Assert.Fail();
		}
	}

	[Test]
	public void AddVotingStartParametersAndThenRemove_NotPresentInDB_True ()
	{
		testDB.AddVotingStartParameters(testVotingStartParameters);
		testDB.RemoveVotingStartParameters(testVotingStartParameters);

		if (testDB.TryGetVotingStartParametersByChatIdentifier(testVotingStartParameters.ChatIdentifier, out VotingStartParameters? _) == false)
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
		testDB.AddVotingParameters(testVotingParameters);

		if (testDB.TryGetVotingParametersByChatIdentifier(testVotingParameters.ChatIdentifier, out VotingParameters? parametersInDB) == true)
		{
			Assert.AreEqual(testVotingParameters, parametersInDB);
			testDB.RemoveVotingParameters(testVotingParameters);
		}
		else
		{
			Assert.Fail();
		}
	}

	[Test]
	public void AddVotingParametersAndThenRemove_NotPresentInDB_True ()
	{
		testDB.AddVotingParameters(testVotingParameters);
		testDB.RemoveVotingParameters(testVotingParameters);

		if (testDB.TryGetVotingParametersByChatIdentifier(testVotingStartParameters.ChatIdentifier, out VotingParameters? _) == false)
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
		testDB.AddVotingEndParameters(testVotingEndParameters);

		if (testDB.TryGetVotingEndParametersByChatIdentifier(testVotingEndParameters.ChatIdentifier, out VotingEndParameters? parametersInDB) == true)
		{
			Assert.AreEqual(testVotingEndParameters, parametersInDB);
			testDB.RemoveVotingEndParameters(testVotingEndParameters);
		}
		else
		{
			Assert.Fail();
		}
	}

	[Test]
	public void AddVotingEndParametersAndThenRemove_NotPresentInDB_True ()
	{
		testDB.AddVotingEndParameters(testVotingEndParameters);
		testDB.RemoveVotingEndParameters(testVotingEndParameters);

		if (testDB.TryGetVotingEndParametersByChatIdentifier(testVotingStartParameters.ChatIdentifier, out VotingEndParameters? _) == false)
		{
			Assert.Pass();
		}
		else
		{
			Assert.Fail();
		}
	}
}