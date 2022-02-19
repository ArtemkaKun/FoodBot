using System;
using System.IO;
using FoodBot.DBSystem;
using FoodBot.Shared;
using FoodBot.VotingSystem;
using NUnit.Framework;

namespace FoodBotTests;

public class DBTests
{
	private readonly DB testDB = new();

	[SetUp]
	public void InitializeDB ()
	{
		testDB.Initialize();
	}

	[TearDown]
	public void RemoveDB ()
	{
		File.Delete("./data.db");
	}

	[Test]
	public void AddVotingStartParameters_PresentAndValidInDB_True ()
	{
		VotingStartParameters testParameters = new()
		{
			ChatIdentifier = new DiscordChatIdentifier
			{
				ChannelID = 0, GuildID = 0
			},
			Message = "test", 
			StartTime = new TimeSpan(10, 0, 0)
		};

		testDB.AddVotingStartParameters(testParameters);

		if (testDB.TryGetVotingStartParametersByID(1, out VotingStartParameters? parametersInDB) == true)
		{
			Assert.AreEqual(testParameters, parametersInDB);
		}
		else
		{
			Assert.Fail();
		}
	}
}