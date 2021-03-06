namespace FoodBot.VotingSystem;

public class VotingManager
{
	private IReadOnlyList<VotingParameters> VotingParametersList { get; set; }
	private CancellationTokenSource VotingThreadsCancellationToken { get; set; } = null!;

	public VotingManager (VotingSystemDB votingDB)
	{
		VotingParametersList = votingDB.GetAllVotingParameters();
		votingDB.OnVotingParametersChanged += ReactOnVotingParametersChanged;
	}

	public void StartVotingThreads ()
	{
		VotingThreadsCancellationToken = new CancellationTokenSource();

		foreach (VotingParameters votingParameters in VotingParametersList)
		{
			DoVotingProcess(votingParameters, VotingThreadsCancellationToken.Token);
		}
	}

	private void ReactOnVotingParametersChanged (IReadOnlyList<VotingParameters> votingParametersList)
	{
		VotingThreadsCancellationToken.Cancel();
		VotingThreadsCancellationToken.Dispose();

		VotingParametersList = votingParametersList;
		StartVotingThreads();
	}

	private async Task DoVotingProcess (VotingParameters parameters, CancellationToken cancellationToken)
	{
		// TODO Duplicated block of try-catch code. 26.04.2022. Artem Yurchenko
		try
		{
			await WaitUntilStartTime(parameters.startTime, cancellationToken);
		}
		catch (TaskCanceledException)
		{
			return;
		}

		while (true)
		{
			if (CheckIfNotifyToday() == true)
			{
				await Program.BotClient.SendMessage(parameters.guildID, parameters.channelID, string.IsNullOrEmpty(parameters.startMessage) == false ? parameters.startMessage : "Let's order some food");

				try
				{
					await Task.Delay(TimeSpan.FromMinutes(parameters.durationInMinutes), cancellationToken);
				}
				catch (TaskCanceledException)
				{
					return;
				}

				await Program.BotClient.SendMessage(parameters.guildID, parameters.channelID, string.IsNullOrEmpty(parameters.endMessage) == false ? parameters.endMessage : "Food voting was finished");

				// TODO Duplicated block of try-catch code. 26.04.2022. Artem Yurchenko
				try
				{
					await WaitUntilStartTime(parameters.startTime, cancellationToken);
				}
				catch (TaskCanceledException)
				{
					return;
				}
			}
			else
			{
				// TODO Duplicated block of try-catch code. 26.04.2022. Artem Yurchenko
				try
				{
					await WaitUntilStartTime(parameters.startTime, cancellationToken);
				}
				catch (TaskCanceledException)
				{
					return;
				}
			}
		}
	}

	private static async Task WaitUntilStartTime (TimeSpan startTime, CancellationToken cancellationToken)
	{
		TimeSpan currentTime = DateTime.Now.TimeOfDay;
		TimeSpan timeToWait = currentTime < startTime ? startTime.Subtract(currentTime) : DateTime.Today.Subtract(currentTime).TimeOfDay + startTime;

		await Task.Delay(timeToWait, cancellationToken);
	}

	private bool CheckIfNotifyToday ()
	{
		DayOfWeek todayDay = DateTime.Today.DayOfWeek;

		return todayDay != DayOfWeek.Saturday && todayDay != DayOfWeek.Sunday;
	}
}