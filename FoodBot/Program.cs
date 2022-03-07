using FoodBot.DBSystem;
using FoodBot.VotingSystem;

namespace FoodBot;

public static class Program
{
	private static VotingSystemDB VotingSystemDB { get; } = new();
	private static VotingManager VotingManager { get; set; } = null!;

	public static void Main ()
	{
		VotingSystemDB.Initialize();
		VotingManager = new VotingManager(VotingSystemDB);
		VotingManager.StartVotingThreads();
	}
}