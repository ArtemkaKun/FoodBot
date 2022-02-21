using FoodBot.Shared;
using FoodBot.VotingSystem;
using Microsoft.EntityFrameworkCore;

namespace FoodBot.DBSystem;

public class VotingSystemDB : DbContext
{
	private const string PATH_TO_DB_FILE = @"Data Source=VotingData.db";

	private DbSet<VotingStartParameters> VotingStartParameters { get; init; } = null!;
	private DbSet<VotingMainParameters> VotingMainParameters { get; init; } = null!;
	private DbSet<VotingEndParameters> VotingEndParameters { get; init; } = null!;

	protected override void OnConfiguring (DbContextOptionsBuilder options)
	{
		options.UseSqlite(PATH_TO_DB_FILE);
	}

	public void Initialize ()
	{
		Database.EnsureCreated();
	}

	public string? AddVotingStartParameters (VotingStartParameters newVotingStartParameters)
	{
		if (TryGetVotingStartParametersByChatIdentifier(newVotingStartParameters.ChatIdentifier, out _) == true)
		{
			return "Voting start parameters already exists for this chat!";
		}

		Add(newVotingStartParameters);
		SaveChanges();

		return null;
	}

	public bool TryGetVotingStartParametersByChatIdentifier (DiscordChatIdentifier chatID, out VotingStartParameters? foundParameters)
	{
		foundParameters = VotingStartParameters.SingleOrDefault(votingStartParameters => votingStartParameters.ChatIdentifier == chatID);

		return foundParameters != null;
	}

	public string? RemoveVotingStartParametersByChatIdentifier (DiscordChatIdentifier chatID)
	{
		if (TryGetVotingStartParametersByChatIdentifier(chatID, out VotingStartParameters? foundParameters) == false)
		{
			return "No voting start parameters for this chat!";
		}

		Remove(foundParameters);
		SaveChanges();

		return null;
	}

	// public IReadOnlyList<VotingStartParameters> GetAllVotingStartParameters ()
	// {
	// 	return 
	// }

	public string? AddVotingMainParameters (VotingMainParameters newVotingMainParameters)
	{
		if (TryGetVotingMainParametersByChatIdentifier(newVotingMainParameters.ChatIdentifier, out _) == true)
		{
			return "Voting main parameters already exists for this chat!";
		}

		Add(newVotingMainParameters);
		SaveChanges();

		return null;
	}

	public bool TryGetVotingMainParametersByChatIdentifier (DiscordChatIdentifier chatID, out VotingMainParameters? foundParameters)
	{
		foundParameters = VotingMainParameters.SingleOrDefault(votingMainParameters => votingMainParameters.ChatIdentifier == chatID);

		return foundParameters != null;
	}

	public string? RemoveVotingMainParametersByChatIdentifier (DiscordChatIdentifier chatID)
	{
		if (TryGetVotingMainParametersByChatIdentifier(chatID, out VotingMainParameters? foundParameters) == false)
		{
			return "No voting main parameters for this chat!";
		}

		Remove(foundParameters);
		SaveChanges();

		return null;
	}

	public string? AddVotingEndParameters (VotingEndParameters newVotingEndParameters)
	{
		if (TryGetVotingEndParametersByChatIdentifier(newVotingEndParameters.ChatIdentifier, out _) == true)
		{
			return "Voting end parameters already exists for this chat!";
		}

		Add(newVotingEndParameters);
		SaveChanges();

		return null;
	}

	public bool TryGetVotingEndParametersByChatIdentifier (DiscordChatIdentifier chatID, out VotingEndParameters? foundParameters)
	{
		foundParameters = VotingEndParameters.SingleOrDefault(votingParameters => votingParameters.ChatIdentifier == chatID);

		return foundParameters != null;
	}

	public string? RemoveVotingEndParametersByChatIdentifier (DiscordChatIdentifier chatID)
	{
		if (TryGetVotingEndParametersByChatIdentifier(chatID, out VotingEndParameters? foundParameters) == false)
		{
			return "No voting end parameters for this chat!";
		}

		Remove(foundParameters);
		SaveChanges();

		return null;
	}
}