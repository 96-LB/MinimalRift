using System;
using FMOD;
using HarmonyLib;
using Shared.Audio;
using UnityEngine.UIElements.Collections;

namespace MinimalRift.Patches;


[HarmonyPatch(typeof(AudioManager))]
public static class AudioManagerPatch {
    [HarmonyPatch(nameof(AudioManager.PlayAudioEventInternal))]
    [HarmonyPostfix]
    public static void PlayAudioEventInternal(AudioManager __instance, ref Guid __result) {
        var player = __instance._activeAudioPlayersById.Get(__result);
        if(player?.eventRef.Guid == GUID.Parse("c8082176-a428-4446-b0e5-c7335e6212bf")) {
            player.eventInstance.setVolume(Config.VibePower.VibeChainSfx);
        }
    }
}
