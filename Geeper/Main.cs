namespace Geeper
{
    using SMLHelper.V2.Utility;
    using QModManager.API.ModLoading;
    using System.IO;
    using System.Reflection;
    using HarmonyLib;
    using UnityEngine;

    [QModCore]
    public class Main
    {
        private static  Texture2D geeperTexture;

        private static Texture2D GeeperTexture => geeperTexture??= ImageUtils.LoadTextureFromFile(
            Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "", "Geeper.png"));

        [QModPatch]
        public static void Patch()
        {
            Harmony.CreateAndPatchAll(typeof(Main), "com.ahk1221.geeper");
        }

        [HarmonyPatch(typeof(Peeper), nameof(Peeper.Start))]
        [HarmonyPostfix]
        private static void Postfix(Peeper __instance)
        {
            var gameObject = __instance.gameObject;
            var texture = GeeperTexture;
            var model = gameObject.FindChild("model").FindChild("peeper").FindChild("aqua_bird").FindChild("peeper");
            var skinnedRenderer = model.GetComponent<SkinnedMeshRenderer>();
            skinnedRenderer.sharedMaterial.mainTexture = texture;
        }
    }
}
