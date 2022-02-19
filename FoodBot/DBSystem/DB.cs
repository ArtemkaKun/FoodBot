using FoodBot.VotingSystem;
using Microsoft.EntityFrameworkCore;

namespace FoodBot.DBSystem;

public class DB : DbContext
{
	private const string PATH_TO_DB_FILE = @"Data Source=data.db";

	private DbSet<VotingStartParameters> VotingStartParameters { get; init; } = null!;

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
	
	public bool TryGetVotingStartParametersByID (uint id, out VotingStartParameters? foundedParameters)
	{
		foundedParameters = VotingStartParameters.SingleOrDefault(votingStartParameters => votingStartParameters.ID == id);

		return foundedParameters != null;
	}
	
	public void RemoveVotingStartParameters (VotingStartParameters parameterToRemove)
	{
		Remove(parameterToRemove);
		SaveChanges();
	}
}