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
using TemplateMod.Utils;

namespace tewd.Proj
{
    class Flame_staff_proj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Purple_gold_sword_projectile");
        }
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Magic;
            Projectile.width = 88;
            Projectile.height = 32;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
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
            target.AddBuff(BuffID.Frostburn, 200);
            target.AddBuff(BuffID.Frostburn2, 200);
            // 火焰粒子特效
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height
                , MyDustId.Fire, 1f, 1f, 100, default(Color), 2f);
            // 粒子特效不受重力
            dust.noGravity = true;
        }
        public override void AI()
        {
            // 火焰粒子特效
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height
                , MyDustId.Fire, 1f, 1f, 100, default(Color), 2f);
            // 粒子特效不受重力
            dust.noGravity = true;
            //设置目标
            float Max = 600;
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
            float v = Projectile.velocity.ToRotation();//这个是一个函数（?）可以不用去计算转换角度
            Projectile.rotation = v + 3.14f;//旋转代码贴图，3.14f指旋转弧度，相当于垂直翻转
            //Projectile.rotation = Projectile.velocity.ToRotation(); 不重要

        }
        
    }

}
