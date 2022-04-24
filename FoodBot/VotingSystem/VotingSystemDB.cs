using Microsoft.EntityFrameworkCore;

namespace FoodBot.VotingSystem;

public class VotingSystemDB : DbContext
{
	public event Action<IReadOnlyList<VotingParameters>>? OnVotingParametersChanged;

	private const string PATH_TO_DB_FILE = @"Data Source=VotingData.db";

	private DbSet<VotingStartParameters> VotingStartParameters { get; init; } = null!;
	private DbSet<VotingMainParameters> VotingMainParameters { get; init; } = null!;
	private DbSet<VotingEndParameters> VotingEndParameters { get; init; } = null!;

	// TODO same two methods as in OrdersSystemDB class. 30.03.2022. Artem Yurchenko
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

	public string? AddVotingStartParameters (VotingStartParameters newVotingStartParameters)
	{
		if (TryGetVotingStartParametersByChatIdentifier(newVotingStartParameters.GuildID, newVotingStartParameters.ChannelID, out _) == true)
		{
			return "Voting start parameters already exists for this chat!";
		}

		Add(newVotingStartParameters);
		SaveChanges();
		OnVotingParametersChanged?.Invoke(GetAllVotingParameters());

		return null;
	}

	public bool TryGetVotingStartParametersByChatIdentifier (ulong guildID, ulong channelID, out VotingStartParameters? foundParameters)
	{
		foundParameters = VotingStartParameters.SingleOrDefault(votingStartParameters => votingStartParameters.GuildID == guildID && votingStartParameters.ChannelID == channelID);

		return foundParameters != null;
	}

	public string? RemoveVotingStartParametersByChatIdentifier (ulong guildID, ulong channelID)
	{
		if (TryGetVotingStartParametersByChatIdentifier(guildID, channelID, out VotingStartParameters? foundParameters) == false)
		{
			return "No voting start parameters for this chat!";
		}

		Remove(foundParameters);
		SaveChanges();
		OnVotingParametersChanged?.Invoke(GetAllVotingParameters());

		return null;
	}

	public string? AddVotingMainParameters (VotingMainParameters newVotingMainParameters)
	{
		if (TryGetVotingMainParametersByChatIdentifier(newVotingMainParameters.GuildID, newVotingMainParameters.ChannelID, out _) == true)
		{
			return "Voting main parameters already exists for this chat!";
		}

		Add(newVotingMainParameters);
		SaveChanges();
		OnVotingParametersChanged?.Invoke(GetAllVotingParameters());

		return null;
	}

	public bool TryGetVotingMainParametersByChatIdentifier (ulong guildID, ulong channelID, out VotingMainParameters? foundParameters)
	{
		foundParameters = VotingMainParameters.SingleOrDefault(votingMainParameters => votingMainParameters.GuildID == guildID && votingMainParameters.ChannelID == channelID);

		return foundParameters != null;
	}

	public string? RemoveVotingMainParametersByChatIdentifier (ulong guildID, ulong channelID)
	{
		if (TryGetVotingMainParametersByChatIdentifier(guildID, channelID, out VotingMainParameters? foundParameters) == false)
		{
			return "No voting main parameters for this chat!";
		}

		Remove(foundParameters);
		SaveChanges();
		OnVotingParametersChanged?.Invoke(GetAllVotingParameters());

		return null;
	}

	public string? AddVotingEndParameters (VotingEndParameters newVotingEndParameters)
	{
		if (TryGetVotingEndParametersByChatIdentifier(newVotingEndParameters.GuildID, newVotingEndParameters.ChannelID, out _) == true)
		{
			return "Voting end parameters already exists for this chat!";
		}

		Add(newVotingEndParameters);
		SaveChanges();
		OnVotingParametersChanged?.Invoke(GetAllVotingParameters());

		return null;
	}

	public bool TryGetVotingEndParametersByChatIdentifier (ulong guildID, ulong channelID, out VotingEndParameters? foundParameters)
	{
		foundParameters = VotingEndParameters.SingleOrDefault(votingParameters => votingParameters.GuildID == guildID && votingParameters.ChannelID == channelID);

		return foundParameters != null;
	}

	public string? RemoveVotingEndParametersByChatIdentifier (ulong guildID, ulong channelID)
	{
		if (TryGetVotingEndParametersByChatIdentifier(guildID, channelID, out VotingEndParameters? foundParameters) == false)
		{
			return "No voting end parameters for this chat!";
		}

		Remove(foundParameters);
		SaveChanges();
		OnVotingParametersChanged?.Invoke(GetAllVotingParameters());

		return null;
	}

	public IReadOnlyList<VotingParameters> GetAllVotingParameters ()
	{
		List<VotingParameters> votingParameters = new();

		foreach (VotingStartParameters votingStartParameters in VotingStartParameters)
		{
			if (TryGetVotingMainParametersByChatIdentifier(votingStartParameters.GuildID, votingStartParameters.ChannelID, out VotingMainParameters? foundMainParameters) == true && TryGetVotingEndParametersByChatIdentifier(votingStartParameters.GuildID, votingStartParameters.ChannelID, out VotingEndParameters? foundEndParameters) == true)
			{
				votingParameters.Add(new VotingParameters(votingStartParameters, foundMainParameters!, foundEndParameters!));
			}
		}

		return votingParameters;
	}
}