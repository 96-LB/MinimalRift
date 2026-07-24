using HarmonyLib;
using RhythmRift;

namespace MinimalRift.Patches;


[HarmonyPatch(typeof(RRPlayerPerformanceVFX))]
public static class RRPlayerPerformanceVFXPatch {
    [HarmonyPatch(nameof(RRPlayerPerformanceVFX.UpdateVfx))]
    [HarmonyPrefix]
    public static void UpdateVfx(ref bool isVibePowerActive) {
        isVibePowerActive &= !Config.VibePower.DisableBackgroundVfx;
    }
}
