using FoodBot;
using FoodBot.Discord.Commands.VotingCommand;
using NUnit.Framework;

namespace FoodBotTests;

[NonParallelizable]
public class VotingCommandsResultTests
{
	[SetUp]
	public void InitializeDB ()
	{
		Program.VotingSystemDB.Initialize();
	}

	[TearDown]
	public void CleanDB ()
	{
		Program.VotingSystemDB.Terminate();
	}
	
	[Test, NonParallelizable]
	public void SetVotingStartParameters_Success_True ()
	{
		string? errorMessage = VotingCommandsResult.SetVotingStartParameters(1, 1, "10:00", "Test start");
		Assert.IsNull(errorMessage);
	}

	[Test, NonParallelizable]
	public void SetVotingStartParametersWithInvalidGuidID_Success_False ()
	{
		string? errorMessage = VotingCommandsResult.SetVotingStartParameters(0, 1, "10:00", "Test start");
		Assert.IsNotNull(errorMessage);
	}
	
	[Test, NonParallelizable]
	public void SetVotingStartParametersWithInvalidChannelID_Success_False ()
	{
		string? errorMessage = VotingCommandsResult.SetVotingStartParameters(1, 0, "10:00", "Test start");
		Assert.IsNotNull(errorMessage);
	}
	
	[Test, NonParallelizable]
	public void SetVotingStartParametersWithInvalidTime_Success_False ()
	{
		string? errorMessage = VotingCommandsResult.SetVotingStartParameters(1, 1, "i10:00", "Test start");
		Assert.IsNotNull(errorMessage);
	}
	
	[Test, NonParallelizable]
	public void SetVotingStartParametersWithInvalidMessage_Success_False ()
	{
		string? errorMessage = VotingCommandsResult.SetVotingStartParameters(1, 1, "10:00", "");
		Assert.IsNotNull(errorMessage);
	}
}