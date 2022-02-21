using FoodBot.Shared;
using FoodBot.VotingSystem;
using Microsoft.EntityFrameworkCore;

namespace FoodBot.DBSystem;

public class VotingSystemDB : DbContext
{
	private const string PATH_TO_DB_FILE = @"Data Source=VotingData.db";

	private DbSet<VotingStartParameters> VotingStartParameters { get; init; } = null!;
	private DbSet<VotingParameters> VotingParameters { get; init; } = null!;
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
	
	public void RemoveVotingStartParameters (VotingStartParameters parameterToRemove)
	{
		Remove(parameterToRemove);
		SaveChanges();
	}
	
	public string? AddVotingParameters (VotingParameters newVotingParameters)
	{
		if (TryGetVotingParametersByChatIdentifier(newVotingParameters.ChatIdentifier, out _) == true)
		{
			return "Voting parameters already exists for this chat!";
		}
		
		Add(newVotingParameters);
		SaveChanges();
		
		return null;
	}
	
	public bool TryGetVotingParametersByChatIdentifier (DiscordChatIdentifier chatID, out VotingParameters? foundParameters)
	{
		foundParameters = VotingParameters.SingleOrDefault(votingParameters => votingParameters.ChatIdentifier == chatID);

		return foundParameters != null;
	}
	
	public void RemoveVotingParameters (VotingParameters parameterToRemove)
	{
		Remove(parameterToRemove);
		SaveChanges();
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
	
	public void RemoveVotingEndParameters (VotingEndParameters parameterToRemove)
	{
		Remove(parameterToRemove);
		SaveChanges();
	}
}