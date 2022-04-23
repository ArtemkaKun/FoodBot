using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FoodBot.VotingSystem;
using NUnit.Framework;

namespace FoodBotTests;

public class VotingSystemDBTests
{
	private readonly VotingSystemDB testVotingSystemDB = new();
	
	// TODO same code as in OrderSystemDBTests. 30.03.2022. Artem Yurchenko
	private const ulong TEST_GUILD_ID = 0;
	private const ulong TEST_CHANNEL_ID = 0;

	private readonly VotingStartParameters testVotingStartParameters = new()
	{
		GuildID = TEST_GUILD_ID,
		ChannelID = TEST_CHANNEL_ID,
		Message = "test",
		StartTime = new TimeSpan(10, 0, 0)
	};

	private readonly VotingMainParameters testVotingMainParameters = new()
	{
		GuildID = TEST_GUILD_ID,
		ChannelID = TEST_CHANNEL_ID,
		DurationInMinutes = 60
	};

	private readonly VotingEndParameters testVotingEndParameters = new()
	{
		GuildID = TEST_GUILD_ID,
		ChannelID = TEST_CHANNEL_ID,
		Message = "testend"
	};

	[OneTimeSetUp]
	public void InitializeDB ()
	{
		testVotingSystemDB.Initialize();
	}

	[OneTimeTearDown]
	public void Terminate ()
	{
		File.Delete("./VotingData.db");
	}

	[Test]
	public void AddVotingStartParameters_PresentAndValidInDB_True ()
	{
		testVotingSystemDB.AddVotingStartParameters(testVotingStartParameters);

		if (testVotingSystemDB.TryGetVotingStartParametersByChatIdentifier(testVotingStartParameters.GuildID, testVotingStartParameters.ChannelID, out VotingStartParameters? parametersInDB) == true)
		{
			Assert.AreEqual(testVotingStartParameters, parametersInDB);
			testVotingSystemDB.RemoveVotingStartParametersByChatIdentifier(testVotingStartParameters.GuildID, testVotingStartParameters.ChannelID);
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
		testVotingSystemDB.RemoveVotingStartParametersByChatIdentifier(testVotingStartParameters.GuildID, testVotingStartParameters.ChannelID);

		if (testVotingSystemDB.TryGetVotingStartParametersByChatIdentifier(testVotingStartParameters.GuildID, testVotingStartParameters.ChannelID, out VotingStartParameters? _) == false)
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

		if (testVotingSystemDB.TryGetVotingMainParametersByChatIdentifier(testVotingMainParameters.GuildID, testVotingMainParameters.ChannelID, out VotingMainParameters? parametersInDB) == true)
		{
			Assert.AreEqual(testVotingMainParameters, parametersInDB);
			testVotingSystemDB.RemoveVotingMainParametersByChatIdentifier(testVotingMainParameters.GuildID, testVotingMainParameters.ChannelID);
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
		testVotingSystemDB.RemoveVotingMainParametersByChatIdentifier(testVotingMainParameters.GuildID, testVotingMainParameters.ChannelID);

		if (testVotingSystemDB.TryGetVotingMainParametersByChatIdentifier(testVotingMainParameters.GuildID, testVotingMainParameters.ChannelID, out VotingMainParameters? _) == false)
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

		if (testVotingSystemDB.TryGetVotingEndParametersByChatIdentifier(testVotingEndParameters.GuildID, testVotingEndParameters.ChannelID, out VotingEndParameters? parametersInDB) == true)
		{
			Assert.AreEqual(testVotingEndParameters, parametersInDB);
			testVotingSystemDB.RemoveVotingEndParametersByChatIdentifier(testVotingEndParameters.GuildID, testVotingEndParameters.ChannelID);
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
		testVotingSystemDB.RemoveVotingEndParametersByChatIdentifier(testVotingEndParameters.GuildID, testVotingEndParameters.ChannelID);

		if (testVotingSystemDB.TryGetVotingEndParametersByChatIdentifier(testVotingEndParameters.GuildID, testVotingEndParameters.ChannelID, out VotingEndParameters? _) == false)
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
			testVotingSystemDB.RemoveVotingStartParametersByChatIdentifier(testVotingStartParameters.GuildID, testVotingStartParameters.ChannelID);
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
			testVotingSystemDB.RemoveVotingMainParametersByChatIdentifier(testVotingMainParameters.GuildID, testVotingMainParameters.ChannelID);
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
			testVotingSystemDB.RemoveVotingEndParametersByChatIdentifier(testVotingEndParameters.GuildID, testVotingEndParameters.ChannelID);
		}
		else
		{
			Assert.Fail();
		}
	}

	[Test]
	public void RemoveUnexistedVotingStartParameters_ErrorReturned_True ()
	{
		string? error = testVotingSystemDB.RemoveVotingStartParametersByChatIdentifier(testVotingStartParameters.GuildID, testVotingStartParameters.ChannelID);

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
		string? error = testVotingSystemDB.RemoveVotingMainParametersByChatIdentifier(testVotingMainParameters.GuildID, testVotingMainParameters.ChannelID);

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
		string? error = testVotingSystemDB.RemoveVotingEndParametersByChatIdentifier(testVotingEndParameters.GuildID, testVotingEndParameters.ChannelID);

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
	public void Add2VotingParametersAndThenGetAll_PresentAndValidInDB_True ()
	{
		testVotingSystemDB.AddVotingStartParameters(testVotingStartParameters);
		testVotingSystemDB.AddVotingMainParameters(testVotingMainParameters);
		testVotingSystemDB.AddVotingEndParameters(testVotingEndParameters);

		ulong secondChatGuildID = 1;
		ulong secondChatChannelID = 1;
		VotingStartParameters secondStartParameters = new() {StartTime = new TimeSpan(100), GuildID = secondChatGuildID, ChannelID = secondChatChannelID, Message = "test2"};
		VotingMainParameters secondMainParameters = new() {GuildID = secondChatGuildID, ChannelID = secondChatChannelID, DurationInMinutes = 1};
		VotingEndParameters secondEndParameters = new() {GuildID = secondChatGuildID, ChannelID = secondChatChannelID, Message = "test2End"};
		testVotingSystemDB.AddVotingStartParameters(secondStartParameters);
		testVotingSystemDB.AddVotingMainParameters(secondMainParameters);
		testVotingSystemDB.AddVotingEndParameters(secondEndParameters);

		IReadOnlyList<VotingParameters> votingParameters = testVotingSystemDB.GetAllVotingParameters();

		if (votingParameters.Count != 2)
		{
			Assert.Fail();
		}

		if (votingParameters.Contains(new VotingParameters(testVotingStartParameters, testVotingMainParameters, testVotingEndParameters)) == false || votingParameters.Contains(new VotingParameters(secondStartParameters, secondMainParameters, secondEndParameters)) == false)
		{
			Assert.Fail();
		}
	}
}