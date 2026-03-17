using BepInEx.Configuration;
using RiftOfTheNecroManager;

namespace MinimalRift;


public static class Config {
    public static class VibePower {
        const string GROUP = "Vibe Power";
        
        public static Setting<bool> DisableBackgroundVFX { get; } = new(GROUP, "Disable Background VFX", false, "Disables the background visual effects for vibe power.");
        public static Setting<bool> DisableEnemyVFX { get; } = new(GROUP, "Disable Enemy Hit VFX", false, "Disables the enemy hit visual effects for vibe power.");
    }
    
    public static class HotCoals {
        const string GROUP = "Hot Coals";
        
        public static Setting<bool> DisableFlames { get; } = new(GROUP, "Disable Flames", false, "Disables the flame visual effect on coaled enemies.");
        public static Setting<float> FlameOpacity { get; } = new(GROUP, "Flame Opacity", 1f, "Controls the opacity of the flame visual effect on coaled enemies.", new AcceptableValueRange<float>(0f, 1f));
        public static Setting<bool> DisableTint { get; } = new(GROUP, "Disable Red Tint", false, "Disables the red tint effect on coaled enemies.");
        public static Setting<float> TintIntensity { get; } = new(GROUP, "Tint Intensity", 1f, "Controls the intensity of the red tint effect on coaled enemies.", new AcceptableValueRange<float>(0.5f, 1f));
    }
    
    public static class Enemies {
        const string GROUP = "Enemies";
        
        public static Setting<FxAmount> HitVFX { get; } = new(GROUP, "Enemy Hit VFX", FxAmount.Enabled, "Controls the visual effects for enemy hits.");
    }
}
