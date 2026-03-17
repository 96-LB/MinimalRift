using HarmonyLib;
using RhythmRift.Enemies;
using RiftOfTheNecroManager;

namespace MinimalRift.Patches;

public class WyrmState : State<RRWyrmEnemy, WyrmState> {
    public static string Prefix => PluginData.Name + "::";
    
    private void Enable(string child) {
        var segments = child.Split('/');
        segments[^1] = Prefix + segments[^1];
        child = string.Join('/', segments);
        var obj = Instance.transform.Find($"ScalePoint/Sprite/{child}")?.gameObject;
        obj?.name = obj.name[Prefix.Length..];
    }
    
    private void Disable(string child) {
        // renaming prevents the animations from finding it
        var obj = Instance.transform.Find($"ScalePoint/Sprite/{child}")?.gameObject;
        obj?.name = Prefix + obj.name;
        obj?.SetActive(false);
    }
    
    private void EnableIfVfxEnabled(string child) {
        if (Config.Enemies.HitVFX == FxAmount.Enabled) {
            Enable(child);
        } else {
            Disable(child);
        }
    }
    
    private void DisableIfVfxDisabled(string child) {
        if (Config.Enemies.HitVFX == FxAmount.Disabled) {
            Disable(child);
        } else {
            Enable(child);
        }
    }
    
    public void HandleVfx() {
        EnableIfVfxEnabled("AttackParticle/DeathParticleSpark");
        DisableIfVfxDisabled("AttackParticle");
        DisableIfVfxDisabled("AttackParticle/AttackParticle00");
        DisableIfVfxDisabled("AttackParticle/AttackParticle01");
        DisableIfVfxDisabled("AttackParticle/AttackParticle02");
        DisableIfVfxDisabled("AttackParticle/AttackParticle03");
        DisableIfVfxDisabled("DeathParticle");
    }
}

[HarmonyPatch(typeof(RRWyrmEnemy))]
public static class RRWyrmEnemyPatch {
    [HarmonyPatch(nameof(RRWyrmEnemy.OnSpawn))]
    [HarmonyPostfix]
    public static void OnSpawn(RRWyrmEnemy __instance) {
        var state = WyrmState.Of(__instance);
        state.HandleVfx();
    }
}
