using BepInEx;
using BepInEx.Unity.Mono;
using HarmonyLib;

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
        static bool setSpawnAdditionalShark(ref bool __result)
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
        static void Start(Network_Host_Entities __instance)
        {
            __instance.CreateAINetworkBehaviour(AI_NetworkBehaviourType.Shark, __instance.GetSharkSpawnPosition());
            __instance.CreateAINetworkBehaviour(AI_NetworkBehaviourType.Shark, __instance.GetSharkSpawnPosition());
        }

    }
}
