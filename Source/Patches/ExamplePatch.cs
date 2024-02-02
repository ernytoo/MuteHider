using GorillaLocomotion;
using HarmonyLib;
using mutehider.Source.Tools;

namespace mutehider.Source.Patches
{
    [HarmonyPatch(typeof(Player), "Awake")]
    public static class ExamplePatch
    {
        static void Postfix() => Logging.Debug(PluginInfo.Name, "patches added successfully.");
    }
}
