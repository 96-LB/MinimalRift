using HarmonyLib;
using RhythmRift;
using RiftOfTheNecroManager;
using UnityEngine;

namespace MinimalRift.Patches;


[HarmonyPatch(typeof(RRStatusFxController))]
public static class FxControllerPatch {
    [HarmonyPatch(nameof(RRStatusFxController.GetFxObjectForStatusEffect))]
    [HarmonyPostfix]
    public static void GetFxObjectForStatusEffect(RREnemyStatusEffect statusEffect, RREnemyStatusFxView __result) {
        if(statusEffect != RREnemyStatusEffect.Burning) {
            return;
        }
        
        foreach(var particles in __result.GetComponentsInChildren<ParticleSystem>()) {
            var main = particles.main;
            main.startColor = new(new Color(1, 1, 1, Config.HotCoals.EnableFlames ? Config.HotCoals.FlameOpacity : 0));
        }
    }
}
