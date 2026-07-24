using FMODUnity;
using HarmonyLib;
using RhythmRift;

namespace MinimalRift.Patches;


[HarmonyPatch(typeof(RREnemyController))]
public static class RREnemyControllerPatch {
    [HarmonyPatch(nameof(RREnemyController.TryQueueActionRowSoundsForEnemy))]
    [HarmonyPrefix]
    public static void TryQueueActionRowSoundsForEnemy_Pre(RREnemyController __instance, ref EventReference __state) {
        __state = __instance._vibeChainHitEventRef;
        if(Config.VibePower.DisableVibeChainSfx) {
            __instance._vibeChainHitEventRef = __instance._inputHitEventRef;
        }
    }
    
    [HarmonyPatch(nameof(RREnemyController.TryQueueActionRowSoundsForEnemy))]
    [HarmonyPostfix]
    public static void TryQueueActionRowSoundsForEnemy_Post(RREnemyController __instance, ref EventReference __state) {
        __instance._vibeChainHitEventRef = __state;
    }
}
