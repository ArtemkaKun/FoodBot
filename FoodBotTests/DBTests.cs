using System;
using FoodBot.DBSystem;
using FoodBot.Shared;
using FoodBot.VotingSystem;
using NUnit.Framework;

namespace FoodBotTests;

public class DBTests
{
	private readonly DB testDB = new();
	
	private readonly VotingStartParameters testVotingStartParameters = new()
	{
		ChatIdentifier = new DiscordChatIdentifier
		{
			ChannelID = 0, GuildID = 0
		},
		Message = "test", 
		StartTime = new TimeSpan(10, 0, 0)
	};
	
	private readonly VotingParameters testVotingParameters = new()
	{
		ChatIdentifier = new DiscordChatIdentifier
		{
			ChannelID = 0, GuildID = 0
		},
		DurationInMinutes = 60
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
}