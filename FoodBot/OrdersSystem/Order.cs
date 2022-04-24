using FoodBot.DBSystem;

namespace FoodBot.OrdersSystem;

public record Order : EntityWithChatIdentifier
{
	public DateTime Date { get; init; }
	public string PersonName { get; init; } = null!;

	public string Text { get; set; } = null!;
}