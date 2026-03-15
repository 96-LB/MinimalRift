using HarmonyLib;
using RhythmRift;
using UnityEngine;

namespace MinimalRift.Patches;


[HarmonyPatch(typeof(RRStatusFxController))]
public static class RRStatusFxControllerPatch {
    [HarmonyPatch(nameof(RRStatusFxController.GetFxObjectForStatusEffect))]
    [HarmonyPostfix]
    public static void GetFxObjectForStatusEffect(RREnemyStatusEffect statusEffect, RREnemyStatusFxView __result) {
        if(statusEffect != RREnemyStatusEffect.Burning) {
            return;
        }
        
        foreach(var particles in __result.GetComponentsInChildren<ParticleSystem>()) {
            var main = particles.main;
            main.startColor = new(Color.white.AlphaMultiplied(Config.HotCoals.DisableFlames ? 0 : Config.HotCoals.FlameOpacity));
        }
    }
}
