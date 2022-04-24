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

	[Test, NonParallelizable]
	public void SetVotingMainParameters_Success_True ()
	{
		string? errorMessage = VotingCommandsResult.SetVotingMainParameters(1, 1, 10);
		Assert.IsNull(errorMessage);
	}

	[Test, NonParallelizable]
	public void SetVotingMainParametersWithInvalidGuidID_Success_False ()
	{
		string? errorMessage = VotingCommandsResult.SetVotingMainParameters(0, 1, 10);
		Assert.IsNotNull(errorMessage);
	}

	[Test, NonParallelizable]
	public void SetVotingMainParametersWithInvalidChannelID_Success_False ()
	{
		string? errorMessage = VotingCommandsResult.SetVotingMainParameters(1, 0, 10);
		Assert.IsNotNull(errorMessage);
	}

	[Test, NonParallelizable]
	public void SetVotingMainParametersWithInvalidTime_Success_False ()
	{
		string? errorMessage = VotingCommandsResult.SetVotingMainParameters(1, 1, 0);
		Assert.IsNotNull(errorMessage);
	}

	[Test, NonParallelizable]
	public void SetVotingEndParameters_Success_True ()
	{
		string? errorMessage = VotingCommandsResult.SetVotingEndParameters(1, 1, "Test end");
		Assert.IsNull(errorMessage);
	}

	[Test, NonParallelizable]
	public void SetVotingEndParametersWithInvalidGuidID_Success_False ()
	{
		string? errorMessage = VotingCommandsResult.SetVotingEndParameters(0, 1, "Test end");
		Assert.IsNotNull(errorMessage);
	}

	[Test, NonParallelizable]
	public void SetVotingEndParametersWithInvalidChannelID_Success_False ()
	{
		string? errorMessage = VotingCommandsResult.SetVotingEndParameters(1, 0, "Test end");
		Assert.IsNotNull(errorMessage);
	}

	[Test, NonParallelizable]
	public void SetVotingEndParametersWithInvalidMessage_Success_False ()
	{
		string? errorMessage = VotingCommandsResult.SetVotingEndParameters(1, 1, "");
		Assert.IsNotNull(errorMessage);
	}
	
	[Test, NonParallelizable]
	public void GetVotingParameters_Success_True ()
	{
		VotingCommandsResult.SetVotingStartParameters(1, 1, "10:00", "Test start");
		VotingCommandsResult.SetVotingMainParameters(1, 1, 10);
		VotingCommandsResult.SetVotingEndParameters(1, 1, "Test end");
		
		(string? message, string? errorMessage) = VotingCommandsResult.GetVotingParameters(1, 1);
		Assert.IsTrue(string.IsNullOrEmpty(message) == false && message == "Start voting at 10:00:00 with a message \"Test start\", voting duration is 10 minutes, end message is \"Test end\"" && string.IsNullOrEmpty(errorMessage) == true);
	}
	
	[Test, NonParallelizable]
	public void GetVotingParametersWithInvalidGuidID_Success_False ()
	{
		(string? message, string? errorMessage) = VotingCommandsResult.GetVotingParameters(0, 1);
		Assert.IsTrue(string.IsNullOrEmpty(message) == true && string.IsNullOrEmpty(errorMessage) == false && errorMessage == "Wrong input parameters");
	}
	
	[Test, NonParallelizable]
	public void GetVotingParametersWithInvalidChannelID_Success_False ()
	{
		(string? message, string? errorMessage) = VotingCommandsResult.GetVotingParameters(1, 0);
		Assert.IsTrue(string.IsNullOrEmpty(message) == true && string.IsNullOrEmpty(errorMessage) == false && errorMessage == "Wrong input parameters");
	}
	
	[Test, NonParallelizable]
	public void GetInexistentVotingParameters_Success_False ()
	{
		(string? message, string? errorMessage) = VotingCommandsResult.GetVotingParameters(1, 1);
		Assert.IsTrue(string.IsNullOrEmpty(message) == true && string.IsNullOrEmpty(errorMessage) == false && errorMessage == "No voting parameters found");
	}

	[Test, NonParallelizable]
	public void RemoveVotingStartParameters_Success_True ()
	{
		VotingCommandsResult.SetVotingStartParameters(1, 1, "10:00", "Test start");
		string? errorMessage = VotingCommandsResult.RemoveVotingStartParameters(1, 1);
		Assert.IsNull(errorMessage);
	}
	
	[Test, NonParallelizable]
	public void RemoveVotingStartParametersWithInvalidGuidID_Success_False ()
	{
		string? errorMessage = VotingCommandsResult.RemoveVotingStartParameters(0, 1);
		Assert.IsNotNull(errorMessage);
	}
	
	[Test, NonParallelizable]
	public void RemoveVotingStartParametersWithInvalidChannelID_Success_False ()
	{
		string? errorMessage = VotingCommandsResult.RemoveVotingStartParameters(1, 0);
		Assert.IsNotNull(errorMessage);
	}
	
	[Test, NonParallelizable]
	public void RemoveInexistentVotingStartParametersWithInvalidChannelID_Success_False ()
	{
		string? errorMessage = VotingCommandsResult.RemoveVotingStartParameters(1, 1);
		Assert.IsNotNull(errorMessage);
	}
	
	[Test, NonParallelizable]
	public void RemoveVotingMainParameters_Success_True ()
	{
		VotingCommandsResult.SetVotingMainParameters(1, 1, 10);
		string? errorMessage = VotingCommandsResult.RemoveVotingMainParameters(1, 1);
		Assert.IsNull(errorMessage);
	}
	
	[Test, NonParallelizable]
	public void RemoveVotingMainParametersWithInvalidGuidID_Success_False ()
	{
		string? errorMessage = VotingCommandsResult.RemoveVotingMainParameters(0, 1);
		Assert.IsNotNull(errorMessage);
	}
	
	[Test, NonParallelizable]
	public void RemoveVotingMainParametersWithInvalidChannelID_Success_False ()
	{
		string? errorMessage = VotingCommandsResult.RemoveVotingMainParameters(1, 0);
		Assert.IsNotNull(errorMessage);
	}
	
	[Test, NonParallelizable]
	public void RemoveInexistentVotingMainParameters_Success_False ()
	{
		string? errorMessage = VotingCommandsResult.RemoveVotingMainParameters(1, 1);
		Assert.IsNotNull(errorMessage);
	}
	
}