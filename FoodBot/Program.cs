using FoodBot.DBSystem;
using FoodBot.VotingSystem;

namespace FoodBot;

public static class Program
{
	private static VotingSystemDB VotingSystemDB { get; } = new();
	private static VotingManager VotingManager { get; } = new(VotingSystemDB);

	public static void Main ()
	{
		VotingSystemDB.Initialize();
		VotingManager.StartVotingThreads();
	}
}