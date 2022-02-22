namespace FoodBot.VotingSystem;

public readonly struct VotingParameters : IEquatable<VotingParameters>
{
	public readonly ulong guildID;
	public readonly ulong channelID;
	public readonly TimeSpan startTime;
	public readonly string? startMessage;
	public readonly uint durationInMinutes;
	public readonly string? endMessage;

	public VotingParameters (VotingStartParameters startParameters, VotingMainParameters mainParameters, VotingEndParameters endParameters)
	{
		guildID = startParameters.GuildID;
		channelID = startParameters.ChannelID;
		startTime = startParameters.StartTime;
		startMessage = startParameters.Message;
		durationInMinutes = mainParameters.DurationInMinutes;
		endMessage = endParameters.Message;
	}

	public bool Equals (VotingParameters other)
	{
		return guildID == other.guildID
		 && channelID == other.channelID
		 && startTime.Equals(other.startTime)
		 && startMessage == other.startMessage
		 && durationInMinutes == other.durationInMinutes
		 && endMessage == other.endMessage;
	}

	public override bool Equals (object? obj)
	{
		return obj is VotingParameters other && Equals(other);
	}

	public override int GetHashCode ()
	{
		return HashCode.Combine(guildID, channelID, startTime, startMessage, durationInMinutes, endMessage);
	}

	public static bool operator == (VotingParameters left, VotingParameters right)
	{
		return left.Equals(right);
	}

	public static bool operator != (VotingParameters left, VotingParameters right)
	{
		return !left.Equals(right);
	}
}