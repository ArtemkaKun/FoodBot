using FoodBot.OrdersSystem;

namespace FoodBot.Discord.Commands.OrderCommand;

public static class OrderCommandsResults
{
	public static bool AddOrder (ulong guildID, ulong channelID, string personName, string orderText)
	{
		personName = personName.Trim();
		orderText = orderText.Trim();

		if (guildID == 0 || channelID == 0 || string.IsNullOrEmpty(personName) == true || string.IsNullOrEmpty(orderText) == true) //TODO same as for RemoveOrder and UpdateOrder methods. 23.04.2022. Artem Yurchenko
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
		if (guildID == 0 || channelID == 0 || orderID == 0) //TODO same as for AddOrder and UpdateOrder methods. 23.04.2022. Artem Yurchenko
		{
			return false;
		}

		return Program.OrdersSystemDB.TryRemoveOrderByChatIdentifierAndID(guildID, channelID, orderID);
	}

	public static bool UpdateOrder (ulong guildID, ulong channelID, uint orderID, string newText)
	{
		if (guildID == 0 || channelID == 0 || orderID == 0 || string.IsNullOrEmpty(newText) == true) //TODO same as for RemoveOrder and AddOrder methods. 23.04.2022. Artem Yurchenko
		{
			return false;
		}

		return Program.OrdersSystemDB.TryUpdateOrderTextByChatIdentifierAndID(guildID, channelID, orderID, newText);
	}
}