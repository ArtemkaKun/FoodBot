using Microsoft.EntityFrameworkCore;

namespace FoodBot.OrdersSystem;

public class OrdersSystemDB : DbContext
{
	private const string PATH_TO_DB_FILE = @"Data Source=OrdersData.db";
	
	private DbSet<Order> Orders { get; init; } = null!;
	
	// TODO same two methods as in VotingSystemDB class. 30.03.2022. Artem Yurchenko
	protected override void OnConfiguring (DbContextOptionsBuilder options)
	{
		options.UseSqlite(PATH_TO_DB_FILE);
	}

	public void Initialize ()
	{
		Database.EnsureCreated();
	}

	public void AddOrder (Order newOrder)
	{
		Add(newOrder);
		SaveChanges();
	}

	public List<Order> GetTodayOrdersByChatIdentifier (ulong guildID, ulong channelID)
	{
		return Orders.Where(order => order.GuildID == guildID && order.ChannelID == channelID && order.Date == DateTime.Today).ToList();
	}

	public void RemoveOrder (Order orderToRemove)
	{
		Remove(orderToRemove);
		SaveChanges();
	}
}