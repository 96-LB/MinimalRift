using System.Collections.Generic;
using HarmonyLib;
using RhythmRift;
using RhythmRift.Enemies;
using RiftOfTheNecroManager;
using UnityEngine;

namespace MinimalRift.Patches;



public class EnemyState : State<RREnemy, EnemyState> {
    public SpriteRenderer SpriteRenderer => Instance._spriteRenderer;
    public MaterialPropertyBlock MatPropBlock => Instance._enemyMatPropBlock;
    public Dictionary<string, Color> OriginalShaderColors { get; } = [];
    public void AddBurningFx() {
        SpriteRenderer.GetPropertyBlock(MatPropBlock);
        if(Config.HotCoals.EnableTint) {
            for(int i = 0; i < 15; i++) {
                // TODO: dict setdefault util in necromanager
                var fromKey = $"FromColor{i}";
                if(!OriginalShaderColors.TryGetValue(fromKey, out var fromColor)) {
                    fromColor = MatPropBlock.GetColor(fromKey);
                    OriginalShaderColors[fromKey] = fromColor;
                }
                
                var toKey = $"ToColor{i}";
                if(!OriginalShaderColors.TryGetValue(toKey, out var toColor)) {
                    toColor = MatPropBlock.GetColor(toKey);
                    OriginalShaderColors[toKey] = toColor;
                }
                
                if(toColor.a > 1e-6) {
                    MatPropBlock.SetColor(toKey, Color.Lerp(fromColor, toColor, Config.HotCoals.TintIntensity));
                }
            }
        } else {
            // disable tint
            MatPropBlock.SetFloat(RREnemy.IsStatusFXOnShaderPropertyId, 0);
        }
        SpriteRenderer.SetPropertyBlock(MatPropBlock);
    }
}

[HarmonyPatch(typeof(RREnemy))]
public static class EnemyPatch {
    [HarmonyPatch(nameof(RREnemy.AddStatusFx))]
    [HarmonyPostfix]
    public static void AddStatusFx(RREnemyStatusFxView statusFxView, RREnemy __instance) {
        var state = EnemyState.Of(__instance);
        if(statusFxView.EffectType == RREnemyStatusEffect.Burning) {
            state.AddBurningFx();
        }
    }
}
