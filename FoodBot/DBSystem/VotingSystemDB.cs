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

	public void AddVotingStartParameters (VotingStartParameters newVotingStartParameters)
	{
		Add(newVotingStartParameters);
		SaveChanges();
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
	
	public void AddVotingParameters (VotingParameters newVotingParameters)
	{
		Add(newVotingParameters);
		SaveChanges();
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
	
	public void AddVotingEndParameters (VotingEndParameters newVotingParameters)
	{
		Add(newVotingParameters);
		SaveChanges();
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