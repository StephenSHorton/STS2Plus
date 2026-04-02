using System;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Modding;
using STS2Plus.Config;
using STS2Plus.Localization;

namespace STS2Plus;

[ModInitializer("Initialize")]
[ScriptPath("res://src/ModEntry.cs")]
public class ModEntry : Node
{
	private static Harmony? harmony;

	private const string ModId = "STS2Plus";

	public static MegaCrit.Sts2.Core.Logging.Logger Logger { get; } = new MegaCrit.Sts2.Core.Logging.Logger("STS2Plus", (LogType)0);

	public static void Verbose(string message)
	{
		if (ConfigManager.Current.VerboseLoggingEnabled)
			Logger.Info("[VERBOSE] " + message, 1);
	}


	public static void Initialize()
	{
		if (harmony == null)
		{
			ConfigManager.Load();
			harmony = new Harmony("sts2plus.core");
			PatchCategory("Core");
			PatchCategory("MoreRules");
			PlusLoc.MergeIntoModifiersTable();
			Logger.Info("STS2Plus initialized.", 1);
		}
	}

	private static void PatchCategory(string category)
	{
		try
		{
			harmony.PatchCategory(typeof(ModEntry).Assembly, category);
			Logger.Info("STS2Plus module loaded: " + category, 1);
		}
		catch (Exception value)
		{
			Logger.Error($"STS2Plus module failed: {category} -> {value}", 1);
		}
	}
}
