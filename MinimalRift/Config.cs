using BepInEx.Configuration;
using RiftOfTheNecroManager;

namespace MinimalRift;


public static class Config {
    public static class HotCoals {
        const string GROUP = "Hot Coals";
        
        public static Setting<bool> EnableFlames { get; } = new(GROUP, "Enable Flames", true, "Enables the flame visual effect on coaled enemies.");
        public static Setting<float> FlameOpacity { get; } = new(GROUP, "Flame Opacity", 1f, "Controls the opacity of the flame visual effect on coaled enemies.", new AcceptableValueRange<float>(0f, 1f));
        public static Setting<bool> EnableTint { get; } = new(GROUP, "Enable Red Tint", true, "Enables the red tint effect on coaled enemies.");
        public static Setting<float> TintIntensity { get; } = new(GROUP, "Tint Intensity", 1f, "Controls the intensity of the red tint effect on coaled enemies.", new AcceptableValueRange<float>(0.5f, 1f));
    }
}
