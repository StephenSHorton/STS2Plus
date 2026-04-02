using System;
using System.Reflection;
using System.Threading.Tasks;
using HarmonyLib;
using STS2Plus.Reflection;
using STS2Plus.Ui;

namespace STS2Plus.Patches;

[HarmonyPatchCategory("MoreRules")]
[HarmonyPatch]
internal static class EndlessModeArchitectWinRunPatch
{
	[HarmonyTargetMethod]
	private static MethodBase? TargetMethod()
	{
		Type type = RuntimeTypeResolver.FindType("MegaCrit.Sts2.Core.Models.Events.TheArchitect") ?? RuntimeTypeResolver.FindTypeByName("TheArchitect");
		return (type == null) ? null : AccessTools.Method(type, "WinRun", (Type[])null, (Type[])null);
	}

	private static bool Prefix(ref Task? __result)
	{
		if (!PlusState.IsEndlessModeActive() || !GameReflection.ShouldStartEndlessLoop(null))
		{
			return true;
		}
		if (MultiplayerSafety.ShouldApplyAuthoritativeGameplayPatches())
		{
			ModEntry.Logger.Info("STS2Plus endless loop skipped TheArchitect.WinRun animation (host/singleplayer).", 1);
			Type type = RuntimeTypeResolver.FindType("MegaCrit.Sts2.Core.Runs.RunManager");
			object obj = (((object)type == null) ? null : AccessTools.Property(type, "Instance")?.GetValue(null));
			if (obj != null)
			{
				GameReflection.TriggerEndlessLoop(obj);
			}
		}
		else
		{
			ModEntry.Logger.Info("STS2Plus endless loop skipped TheArchitect.WinRun animation (client).", 1);
		}
		__result = Task.CompletedTask;
		EndlessModeOverlay.Refresh();
		return false;
	}
}
