using System.Reflection;
using System.Text;
using Discord.Commands;
using ParameterInfo = System.Reflection.ParameterInfo;

namespace FoodBot.Discord.Commands.HelpCommand;

public static class BotCommandsCollector
{
	private const string COMMAND_ARGUMENT_NAME_TEMPLATE = " <{0}>";
	private const string COMMAND_NAME_TEMPLATE = "!{0}";
	
	public static Dictionary<string, string> GetCommandDescriptionMap ()
	{
		IEnumerable<MethodInfo> commandsMethods = GetAllCommandsMethods();
		Dictionary<string, string> commandDescriptionMap = new();

		foreach (MethodInfo method in commandsMethods)
		{
			commandDescriptionMap.Add($"{GetCommandNameWithAliases(method)}{GetMethodArgumentNames(method)}", method.GetCustomAttribute<SummaryAttribute>()?.Text ?? string.Empty);
		}

		return commandDescriptionMap;
	}
	
	private static IEnumerable<MethodInfo> GetAllCommandsMethods ()
	{
		return Assembly.GetExecutingAssembly()
					   .GetTypes()
					   .Where(typeUnit => typeUnit.IsAssignableTo(typeof(ModuleBase<SocketCommandContext>)))
					   .OrderBy(commandType => commandType.FullName)
					   .SelectMany(commandsModule => commandsModule.GetMethods().Where(method => method.GetCustomAttribute<CommandAttribute>() != null));
	}
	
	private static string GetCommandNameWithAliases (MemberInfo command)
	{
		string? commandName = GetCommandName(command);
		string aliasesString = GetAliasesString(command);

		if (string.IsNullOrEmpty(aliasesString) == true)
		{
			return $"{string.Format(COMMAND_NAME_TEMPLATE, commandName)},";
		}

		return $"{string.Format(COMMAND_NAME_TEMPLATE, commandName)}, {aliasesString}";
	}

	private static string? GetCommandName (MemberInfo command)
	{
		string? commandsGroupName = command.DeclaringType?.GetCustomAttribute<GroupAttribute>()?.Prefix;
		string? commandMethodName = command.GetCustomAttribute<CommandAttribute>()?.Text;

		return string.IsNullOrEmpty(commandsGroupName) == false ? $"{commandsGroupName} {commandMethodName}" : commandMethodName;
	}

	private static string GetAliasesString (MemberInfo command)
	{
		string[]? commandAliases = command.GetCustomAttribute<AliasAttribute>()?.Aliases;
		string aliasesString = string.Empty;
		
		for (int aliasPointer = 0; aliasPointer < commandAliases?.Length; aliasPointer++)
		{
			aliasesString += string.Format(COMMAND_NAME_TEMPLATE, commandAliases[aliasPointer]);
		}

		return aliasesString;
	}

	private static string GetMethodArgumentNames (MethodBase command)
	{
		StringBuilder argumentsNamesStringBuilder = new();

		foreach (ParameterInfo argument in command.GetParameters())
		{
			argumentsNamesStringBuilder.Append(string.Format(COMMAND_ARGUMENT_NAME_TEMPLATE, argument.Name));
		}

		return argumentsNamesStringBuilder.ToString();
	}
}