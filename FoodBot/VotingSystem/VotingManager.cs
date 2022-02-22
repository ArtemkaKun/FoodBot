using FoodBot.DBSystem;

namespace FoodBot.VotingSystem;

public class VotingManager
{
	private readonly VotingSystemDB testVotingSystemDB = new();
	
	public void Initialize ()
	{
		testVotingSystemDB.Initialize();
	}
}