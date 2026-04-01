using HarmonyLib;
using MegaCrit.Sts2.Core.Nodes.Screens.MainMenu;
using STS2Plus.Ui;

namespace STS2Plus.Patches;

[HarmonyPatchCategory("Core")]
[HarmonyPatch(typeof(NMainMenu), "_Ready")]
internal static class SpeedControlMainMenuPatch
{
	private static void Prefix()
	{
		SpeedControlOverlay.SetMainMenuVisible(visible: true);
		SpeedControlOverlay.Hide(resetSpeed: true);
	}
}
