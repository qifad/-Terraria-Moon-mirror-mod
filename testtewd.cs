using IL.Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using On.Terraria;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Item = Terraria.Item;
using Main = Terraria.Main;
using Player = Terraria.Player;
using Projectile = Terraria.Projectile;

namespace tewd
{
    public class testtewd : ScreenShaderData
    {
        public testtewd(string passName) : base(passName)
        {

        }
        public testtewd(Ref<Effect> shader, string passName) : base(shader,passName)
        {

        }
        public override void Apply()
        {
            base.Apply();
        }
    }
}