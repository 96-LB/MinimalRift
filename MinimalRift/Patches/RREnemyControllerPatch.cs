using System;
using FMOD;
using FMODUnity;
using HarmonyLib;
using RhythmRift;
using Shared.Audio;
using UnityEngine.UIElements.Collections;

namespace MinimalRift.Patches;


[HarmonyPatch(typeof(RREnemyController))]
public static class RREnemyControllerPatch {
    [HarmonyPatch(nameof(RREnemyController.TryQueueActionRowSoundsForEnemy))]
    [HarmonyPrefix]
    public static void TryQueueActionRowSoundsForEnemy_Pre(RREnemyController __instance, ref EventReference? __state) {
        if(Config.VibePower.DisableVibeChainSfx) {
            (__instance._vibeChainHitEventRef, __state) = (__instance._inputHitEventRef, __instance._vibeChainHitEventRef);
        }
    }
    
    [HarmonyPatch(nameof(RREnemyController.TryQueueActionRowSoundsForEnemy))]
    [HarmonyPostfix]
    public static void TryQueueActionRowSoundsForEnemy_Post(RREnemyController __instance, ref EventReference? __state) {
        if(__state != null) {
            __instance._vibeChainHitEventRef = __state.Value;
        }
    }
}
