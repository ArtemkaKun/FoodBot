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

	// TODO wrap into debug preprocessor since used only for tests. 24.04.2022. Artem Yurchenko
	public void Terminate ()
	{
		ChangeTracker.Clear();
		Database.EnsureDeleted();
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

	public bool TryUpdateOrderTextByChatIdentifierAndID (ulong guildID, ulong channelID, uint orderID, string newText)
	{
		Order? orderToUpdate = Orders.FirstOrDefault(order => order.GuildID == guildID && order.ChannelID == channelID && order.ID == orderID);

		if (orderToUpdate != null)
		{
			orderToUpdate.Text = newText;
			SaveChanges();
			return true;
		}

		return false;
	}

	public bool TryRemoveOrderByChatIdentifierAndID (ulong guildID, ulong channelID, uint orderID)
	{
		Order? orderToRemove = Orders.FirstOrDefault(order => order.GuildID == guildID && order.ChannelID == channelID && order.ID == orderID);

		if (orderToRemove != null)
		{
			RemoveOrder(orderToRemove);
			SaveChanges();
			return true;
		}

		return false;
	}

	public List<Order> GetTodayOrdersByChatIdentifierAndPersonName (ulong guildID, ulong channelID, string personName)
	{
		return Orders.Where(order => order.GuildID == guildID && order.ChannelID == channelID && order.Date == DateTime.Today && order.PersonName == personName).ToList();
	}
}