using FoodBot.OrdersSystem;

namespace FoodBot.Discord.Commands.OrderCommand;

public static class OrderCommandsResults
{
	public static bool AddOrder (ulong guildID, ulong channelID, string personName, string orderText)
	{
		personName = personName.Trim();
		orderText = orderText.Trim();
		
		if (guildID == 0 || channelID == 0 || string.IsNullOrEmpty(personName) == true || string.IsNullOrEmpty(orderText) == true)
		{
			return false;
		}

		Order newOrder = new()
		{
			GuildID = guildID,
			ChannelID = channelID,
			Date = DateTime.Today,
			PersonName = personName,
			Text = orderText
		};
		
		Program.OrdersSystemDB.AddOrder(newOrder);
		return true;
	}
	
	public static bool RemoveOrder (ulong guildID, ulong channelID, uint orderID)
	{
		if (guildID == 0 || channelID == 0 || orderID == 0)
		{
			return false;
		}
		
		return Program.OrdersSystemDB.TryRemoveOrderByChatIdentifierAndID(guildID, channelID, orderID);
	}
}