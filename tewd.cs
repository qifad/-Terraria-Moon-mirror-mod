using Microsoft.Xna.Framework.Graphics;
using IL.Terraria;
using Microsoft.Xna.Framework;
using On.Terraria;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Item = Terraria.Item;
using Main = Terraria.Main;
using Player = Terraria.Player;
using Projectile = Terraria.Projectile;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using ReLogic.Content;

namespace tewd
{
	public class tewd : Mod
	{
        public Effect shaderTest;
        public override void Load()
        {
            if (!Main.dedServ)
            {
                Ref<Effect> Test1145 = new Ref<Effect>(Assets.Request<Effect>("Ef/shaderTest", AssetRequestMode.ImmediateLoad).Value);
                Filters.Scene["shaderTest"] = new Filter(new TestScreenShaderData(Test1145, "shaderTest"), EffectPriority.VeryHigh);
                Filters.Scene["shaderTest"].Load();
            }

        }
    }
	
}