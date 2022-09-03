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
namespace tewd.Proj
{
	public class ExampleWhipProjectile233 : ModProjectile
	{
		public override void SetStaticDefaults() {
            // This makes the projectile use whip collision detection and allows flasks to be applied to it.
            ProjectileID.Sets.IsAWhip[Type] = true;
        }

		public override void SetDefaults() {
			// This method quickly sets the whip's properties.
			Projectile.DefaultToWhip();

			// use these to change from the vanilla defaults
			Projectile.WhipSettings.Segments = 50;
			Projectile.WhipSettings.RangeMultiplier = 5f;
		}

		private float Timer {
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		private float ChargeTime {
			get => Projectile.ai[1];
			set => Projectile.ai[1] = value;
		}
		public override void AI()
		{
            // 火焰粒子特效
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height
                , DustID.Firefly, 1f, 1f, 100, default(Color), 4f);
            // 粒子特效不受重力
            dust.noGravity = true;

        }

		// This example uses PreAI to implement a charging mechanic.




	}
}
