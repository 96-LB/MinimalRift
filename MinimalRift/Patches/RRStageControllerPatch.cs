using HarmonyLib;
using RhythmRift;
using Shared.FX;

namespace MinimalRift.Patches;


[HarmonyPatch(typeof(RRStageController))]
public static class RRStageControllerPatch {
    [HarmonyPatch(nameof(RRStageController.PlayActionRowVFX))]
    [HarmonyPrefix]
    public static void PlayActionRowVFX_Pre(RRStageController __instance, ref bool isEnemyKilled, ref bool isEnemyAttacking, bool finalHit, ref bool __state) {
        isEnemyKilled &= finalHit || Config.Enemies.HitVFX == FxAmount.Enabled;
        isEnemyAttacking |= Config.Enemies.HitVFX == FxAmount.Disabled;
        
        __state = __instance._isVibePowerActive;
        __instance._isVibePowerActive &= !Config.VibePower.DisableEnemyVfx;
    }
    
    [HarmonyPatch(nameof(RRStageController.PlayActionRowVFX))]
    [HarmonyPostfix]
    public static void PlayActionRowVFX_Post(RRStageController __instance, bool __state) {
        __instance._isVibePowerActive = __state;
    }
    
    [HarmonyPatch(nameof(RRStageController.HandleKilledBoundEnemy))]
    [HarmonyPrefix]
    public static void HandleKilledBoundEnemy_Pre(RRStageController __instance, ref (bool, VFXEffect?) __state) {
        var finalHit = !__instance._isTutorial && !__instance._isPracticeMode && __instance.ShouldPlayFinalHitVFX;
        
        __state = (__instance._isVibePowerActive, __instance._killAttackPrefab);
        __instance._killAttackPrefab = Config.Enemies.HitVFX == FxAmount.Enabled || finalHit ? __instance._killAttackPrefab : null;
        __instance._isVibePowerActive &= !Config.VibePower.DisableEnemyVfx;
    }
    
    [HarmonyPatch(nameof(RRStageController.HandleKilledBoundEnemy))]
    [HarmonyPostfix]
    public static void HandleKilledBoundEnemy_Post(RRStageController __instance, (bool, VFXEffect?) __state) {
        (__instance._isVibePowerActive, __instance._killAttackPrefab) = __state;
    }
}
