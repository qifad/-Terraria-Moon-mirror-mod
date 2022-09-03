using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace tewd.Dusts
{
    public class Gold_particles : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            base.OnSpawn(dust);
            dust.noGravity = true;
            dust.velocity *= 2;
            dust.frame = new(0, Main.rand.Next() * 6, 6, 6);
            dust.scale *= 1.5f;
        }
        public override bool Update(Dust dust)
        {
            base.Update(dust);
            float light = 0.35f * dust.scale;

            Lighting.AddLight(dust.position, light, light, light);
            return true;
        }
    }
}
