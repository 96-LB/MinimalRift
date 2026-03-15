using HarmonyLib;
using RhythmRift;

namespace MinimalRift.Patches;


[HarmonyPatch(typeof(RRStageController))]
public static class RRStageControllerPatch {
    [HarmonyPatch(nameof(RRStageController.PlayActionRowVFX))]
    [HarmonyPrefix]
    public static void PlayActionRowVFX_Pre(RRStageController __instance, ref bool isEnemyKilled, ref bool isEnemyAttacking, bool finalHit, ref bool __state) {
        isEnemyKilled &= finalHit || Config.Enemies.HitVFX == FXAmount.Enabled;
        isEnemyAttacking |= Config.Enemies.HitVFX == FXAmount.Disabled;
        
        __state = __instance._isVibePowerActive;
        __instance._isVibePowerActive &= !Config.VibePower.DisableEnemyVFX;
    }
    
    [HarmonyPatch(nameof(RRStageController.PlayActionRowVFX))]
    [HarmonyPostfix]
    public static void PlayActionRowVFX_Post(RRStageController __instance, bool __state) {
        __instance._isVibePowerActive = __state;
    }
}
