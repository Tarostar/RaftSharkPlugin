using BepInEx;
using BepInEx.Unity.Mono;
using HarmonyLib;
using UnityEngine;

namespace RaftSharkPlugin;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    private readonly Harmony harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);

    private void Awake()
    {
        harmony.PatchAll();
    }

    // spawn additional shark when shark is killed
    [HarmonyPatch]
    class SpawnAdditionalShark
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(AI_State_Decay_Shark), "SpawnAdditionalShark")]
        static bool SetSpawnAdditionalShark(ref bool __result)
        {
            __result = true;
            return false;
        }
    }

    // spawn two additional sharks at startup
    [HarmonyPatch(typeof(Network_Host_Entities))]
    [HarmonyPatch("Start")]
    class SharkPatch
    {
        [HarmonyPostfix]
        static void Start(Network_Host_Entities __instance, bool ___spawnShark, int ___sharkCount)
        {
            Debug.Log($"SpawnSharks {___spawnShark} - New Game {GameManager.IsInNewGame} - QuickStart {GameManager.QuickStartGame}");

            // annoyingly none of these shark counters actually show the count of sharks - maybe they have not been set at this point?
            AI_NetworkBehavior_Shark[] array= UnityEngine.Object.FindObjectsOfType<AI_NetworkBehavior_Shark>();
            Debug.Log($"Sharks {__instance.SharkCount}, array of sharks {array.Length} - again shark count {___sharkCount}");

            if ((GameManager.IsInNewGame || GameManager.QuickStartGame) && ___spawnShark)
            {
                Debug.Log($"Sharks arrive...");

                __instance.CreateAINetworkBehaviour(AI_NetworkBehaviourType.Shark, __instance.GetSharkSpawnPosition());
                __instance.CreateAINetworkBehaviour(AI_NetworkBehaviourType.Shark, __instance.GetSharkSpawnPosition());
            }
        }
    }

    // make sharks deadly - fast and lots of damage
    [HarmonyPatch(typeof(AI_State_Attack_Entity_Shark), "Start")]
    class SharkAttackPatch
    {
        [HarmonyPrefix]
        static void SetAttackPlayerDamage(ref int ____attackPlayerDamage, ref float ___attackSwimSpeedMultiplier)
        {
            ____attackPlayerDamage = 200;
            ___attackSwimSpeedMultiplier = 3f;
        }
    }
}
