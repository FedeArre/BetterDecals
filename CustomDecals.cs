using System;
using System.IO;
using UnityEngine;

namespace BetterDecals
{
    public class BetterDecals : Mod
    {
        public override string ID => "BetterDecals";
        public override string Name => "Better Decals";
        public override string Author => "Federico Arredondo";
        public override string Version => "1.0";

        tools toolsComponent;
        PlayerRayCasting playerRc;
        TextureList textures;

        public override void SecondPassOnLoad()
        {
            toolsComponent = GameObject.Find("Player").GetComponent<tools>();
            playerRc = toolsComponent.PlayerCam.GetComponent<PlayerRayCasting>();
            textures = playerRc.TexturList;

            if (!Directory.Exists(Application.dataPath + "/../Decals"))
            {
                Directory.CreateDirectory(Application.dataPath + "/../Decals/");
            }

            string[] decals = Directory.GetFiles(Application.dataPath + "/../Decals/");

            foreach (string decalPath in decals)
            {
                try
                {
                    byte[] textureBytes = File.ReadAllBytes(decalPath);
                    Texture2D textureTemp = new Texture2D(2, 2);
                    textureTemp.LoadImage(textureBytes);
                    Array.Resize(ref textures.Decals, textures.Decals.Length + 1);
                    textures.Decals[textures.Decals.Length-1] = textureTemp;
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error loading a custom decal! {decalPath} | {e}");
                }
            }
        }

        public override void Update()
        {
            if(tools.tool == 19)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    textures.ThisDecal.MultiplyScale(0.9f);
                }

                if (Input.GetKeyDown(KeyCode.X))
                {
                    textures.ThisDecal.MultiplyScale(1.1f);
                }
            }
        }
    }
}
