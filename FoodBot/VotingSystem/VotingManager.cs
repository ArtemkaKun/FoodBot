namespace FoodBot.VotingSystem;

public class VotingManager
{
	private IReadOnlyList<VotingParameters> VotingParametersList { get; set; }
	private CancellationTokenSource VotingThreadsCancellationToken { get; set; } = null!;

	public VotingManager (IReadOnlyList<VotingParameters> votingParametersList)
	{
		VotingParametersList = votingParametersList;
	}

	public void StartVotingThreads ()
	{
		VotingThreadsCancellationToken = new CancellationTokenSource();

		foreach (VotingParameters votingParameters in VotingParametersList)
		{
			DoVotingProcess(votingParameters, VotingThreadsCancellationToken.Token);
		}
	}

	public void ReactOnVotingParametersChanged (IReadOnlyList<VotingParameters> votingParametersList)
	{
		VotingThreadsCancellationToken.Cancel();
		VotingThreadsCancellationToken.Dispose();
		
		VotingParametersList = votingParametersList;
		StartVotingThreads();
	}

	private async Task DoVotingProcess (VotingParameters parameters, CancellationToken cancellationToken)
	{
		TimeSpan currentTime = DateTime.Now.TimeOfDay;
		TimeSpan timeToWait = currentTime < parameters.startTime ? parameters.startTime.Subtract(currentTime) : DateTime.Today.Subtract(currentTime).TimeOfDay + parameters.startTime;

		try
		{
			await Task.Delay(timeToWait, cancellationToken);
		}
		catch (TaskCanceledException _)
		{
			return;
		}

		while (true)
		{
			if (CheckIfNotifyToday() == true)
			{
				//notify start
				
				try
				{
					await Task.Delay(TimeSpan.FromMinutes(parameters.durationInMinutes), cancellationToken);
				}
				catch (TaskCanceledException _)
				{
					return;
				}
				
				//notify end

				try
				{
					await Task.Delay(new TimeSpan(24, 0, 0), cancellationToken);
				}
				catch (TaskCanceledException _)
				{
					return;
				}
			}
		}
	}

	private bool CheckIfNotifyToday ()
	{
		DayOfWeek todayDay = DateTime.Today.DayOfWeek;

		return todayDay != DayOfWeek.Saturday && todayDay != DayOfWeek.Sunday;
	}
}