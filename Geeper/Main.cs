using System;
using System.IO;
using System.Reflection;
using Harmony;
using UnityEngine;

namespace Geeper
{
    public class Main
    {
        public static void Patch()
        {
            var harmony = HarmonyInstance.Create("com.ahk1221.geeper");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        // Ripped from: https://github.com/RandyKnapp/SubnauticaModSystem/blob/master/SubnauticaModSystem/Common/Utility/ImageUtils.cs
        public static Texture2D LoadTexture(string path, TextureFormat format = TextureFormat.BC7, int width = 2, int height = 2)
        {
            if (File.Exists(path))
            {
                byte[] data = File.ReadAllBytes(path);
                Texture2D texture2D = new Texture2D(width, height, format, false);
                if (texture2D.LoadImage(data))
                {
                    return texture2D;
                }
            }
            else
            {
                Console.WriteLine("[Geeper] ERROR: File not found " + path);
            }
            return null;
        }
    }

    [HarmonyPatch(typeof(Peeper))]
    [HarmonyPatch("Start")]
    internal class Peeper_Start_Patch
    {
        static void Postfix(Peeper __instance)
        {
            var gameObject = __instance.gameObject;
            var texture = Main.LoadTexture(@"./QMods/Geeper/Geeper.png");
            var model = gameObject.FindChild("model").FindChild("peeper").FindChild("aqua_bird").FindChild("peeper");
            var skinnedRenderer = model.GetComponent<SkinnedMeshRenderer>();
            skinnedRenderer.sharedMaterial.mainTexture = texture;
        }
    }
}
