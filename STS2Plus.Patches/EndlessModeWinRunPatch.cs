using System;
using System.Reflection;
using System.Threading.Tasks;
using HarmonyLib;
using STS2Plus.Reflection;
using STS2Plus.Ui;

namespace STS2Plus.Patches;

[HarmonyPatchCategory("MoreRules")]
[HarmonyPatch]
internal static class EndlessModeWinRunPatch
{
	[HarmonyTargetMethod]
	private static MethodBase? TargetMethod()
	{
		Type type = RuntimeTypeResolver.FindType("MegaCrit.Sts2.Core.Runs.RunManager") ?? RuntimeTypeResolver.FindTypeByName("RunManager");
		return (type == null) ? null : AccessTools.Method(type, "WinRun", (Type[])null, (Type[])null);
	}

	private static bool Prefix(object __instance, ref Task? __result)
	{
		if (!PlusState.IsEndlessModeActive() || !GameReflection.ShouldStartEndlessLoop(__instance))
		{
			return true;
		}
		ModEntry.Verbose("EndlessMode: intercepting WinRun");
		if (MultiplayerSafety.ShouldApplyAuthoritativeGameplayPatches())
		{
			ModEntry.Logger.Info("STS2Plus endless loop intercepting WinRun (host/singleplayer).", 1);
			GameReflection.TriggerEndlessLoop(__instance);
		}
		else
		{
			ModEntry.Logger.Info("STS2Plus endless loop intercepting WinRun (client, waiting for host).", 1);
		}
		__result = Task.CompletedTask;
		EndlessModeOverlay.Refresh();
		return false;
	}
}
