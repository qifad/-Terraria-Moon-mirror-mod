using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateMod.Utils;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace tewd.Proj
{
    class Photosynthetic_sanctions_Proj : ModProjectile
    {
        private int wd;
        public float ap;

        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Purple_gold_sword_projectile");
        }
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Melee;
            Projectile.width = 100;
            Projectile.height = 79;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
            Projectile.light = 0.11f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            //这里是重点：
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 100;

        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.velocity *= 0f;
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height
            , 303, 1f, 1f, 100, default(Color), 3f);
            // 粒子特效不受重力
            dust.noGravity = true;
        }
        public override void AI()
        {
            
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height
            , 298, 1f, 1f, 100, default(Color), 3f);
            // 粒子特效不受重力
            dust.noGravity = true;
            ap = Projectile.alpha;
            Projectile.rotation += 0.95f;
            wd++;
            if (wd >= 30&& wd <= 45)
            {
                Projectile.velocity *= 0f;
                Projectile.timeLeft = 180;
                ap--;
                //设置目标
                float Max = 0;
                NPC target = null;
                //可以追踪的极限距离
                foreach (NPC npc in Main.npc)
                {

                    float ToNpc = Vector2.Distance(Projectile.Center, npc.Center);
                    //获取弹幕到玩家的距离
                    if (ToNpc < Max)
                    {
                        Max = ToNpc;
                        //让距离是到这个npc的距离
                        target = npc;
                        //让target的值不为null，而等于这个npc
                    }
                }
                if (target != null)
                {
                    if (target.active && !target.friendly && target.CanBeChasedBy())
                    {
                        Vector2 vector = target.Center - Projectile.Center;
                        vector.Normalize();
                        vector *= 0;
                        Projectile.velocity = (Projectile.velocity * 10 + vector) / 10;

                    }

                }
            }
            if (wd <= 29)
            {
                //设置目标
                float Max = 800;
                NPC target = null;
                //可以追踪的极限距离
                foreach (NPC npc in Main.npc)
                {

                    float ToNpc = Vector2.Distance(Projectile.Center, npc.Center);
                    //获取弹幕到玩家的距离
                    if (ToNpc < Max)
                    {
                        Max = ToNpc;
                        //让距离是到这个npc的距离
                        target = npc;
                        //让target的值不为null，而等于这个npc
                    }
                }
                if (target != null)
                {
                    if (target.active && !target.friendly && target.CanBeChasedBy())
                    {
                        Vector2 vector = target.Center - Projectile.Center;
                        vector.Normalize();
                        vector *= 20;
                        Projectile.velocity = (Projectile.velocity * 10 + vector) / 10;

                    }

                }
            }

        }
        
    }

}
