using FoodBot.Discord;
using FoodBot.OrdersSystem;
using FoodBot.VotingSystem;

namespace FoodBot;

public static class Program
{
	public static Bot BotClient { get; } = new();
	public static OrdersSystemDB OrdersSystemDB { get; } = new();
	public static VotingSystemDB VotingSystemDB { get; } = new();

	private static VotingManager VotingManager { get; set; } = null!;

	private static void Main ()
	{
		VotingSystemDB.Initialize();
		VotingManager = new VotingManager(VotingSystemDB);
		VotingManager.StartVotingThreads();

		OrdersSystemDB.Initialize();

		BotClient.Initialize().GetAwaiter().GetResult();
		StartBot().GetAwaiter().GetResult();
	}

	private static async Task StartBot ()
	{
		await BotClient.StartBot();
		await Task.Delay(-1);
	}
}