using FoodBot.VotingSystem;
using NUnit.Framework;

namespace FoodBotTests;

public class VotingManagerTests
{
	private readonly VotingManager testManager = new();
	
	[OneTimeSetUp]
	public void InitializeDB ()
	{
		testManager.Initialize();
	}
}