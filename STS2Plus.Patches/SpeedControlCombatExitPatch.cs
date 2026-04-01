using HarmonyLib;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using STS2Plus.Ui;

namespace STS2Plus.Patches;

[HarmonyPatchCategory("Core")]
[HarmonyPatch(typeof(NCombatRoom), "_ExitTree")]
internal static class SpeedControlCombatExitPatch
{
	private static void Prefix()
	{
		SpeedControlOverlay.SetMainMenuVisible(visible: false);
		SpeedControlOverlay.Show();
	}
}
