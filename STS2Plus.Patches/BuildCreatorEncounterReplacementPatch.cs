using HarmonyLib;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Runs;
using STS2Plus.Features;

namespace STS2Plus.Patches;

[HarmonyPatchCategory("MoreRules")]
[HarmonyPatch(/*Could not decode attribute arguments.*/)]
internal static class BuildCreatorEncounterReplacementPatch
{
	private static void Prefix(ref EncounterModel encounter, IRunState? runState)
	{
		encounter = BuildCreatorRuntime.ReplaceEncounterIfNeeded(encounter, runState);
	}
}
