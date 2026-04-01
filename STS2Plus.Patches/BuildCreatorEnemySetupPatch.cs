using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Creatures;
using STS2Plus.Features;

namespace STS2Plus.Patches;

[HarmonyPatchCategory("MoreRules")]
[HarmonyPatch(typeof(Creature), "AfterAddedToRoom")]
internal static class BuildCreatorEnemySetupPatch
{
	private static void Postfix(Creature __instance)
	{
		BuildCreatorRuntime.NormalizeEnemy(__instance);
	}
}
