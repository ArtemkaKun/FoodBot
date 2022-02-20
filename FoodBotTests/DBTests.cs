using System;
using FoodBot.DBSystem;
using FoodBot.Shared;
using FoodBot.VotingSystem;
using NUnit.Framework;

namespace FoodBotTests;

public class DBTests
{
	private readonly DB testDB = new();
	private readonly VotingStartParameters testParameters = new()
	{
		ChatIdentifier = new DiscordChatIdentifier
		{
			ChannelID = 0, GuildID = 0
		},
		Message = "test", 
		StartTime = new TimeSpan(10, 0, 0)
	};

	[OneTimeSetUp]
	public void InitializeDB ()
	{
		testDB.Initialize();
	}

	[Test]
	public void AddVotingStartParameters_PresentAndValidInDB_True ()
	{
		testDB.AddVotingStartParameters(testParameters);

		if (testDB.TryGetVotingStartParametersByChatIdentifier(testParameters.ChatIdentifier, out VotingStartParameters? parametersInDB) == true)
		{
			Assert.AreEqual(testParameters, parametersInDB);
			testDB.RemoveVotingStartParameters(testParameters);
		}
		else
		{
			Assert.Fail();
		}
	}
	
	[Test]
	public void AddVotingStartParametersAndThenRemove_NotPresentInDB_True ()
	{
		testDB.AddVotingStartParameters(testParameters);
		testDB.RemoveVotingStartParameters(testParameters);

		if (testDB.TryGetVotingStartParametersByChatIdentifier(testParameters.ChatIdentifier, out VotingStartParameters? _) == false)
		{
			Assert.Pass();
		}
		else
		{
			Assert.Fail();
		}
	}
}