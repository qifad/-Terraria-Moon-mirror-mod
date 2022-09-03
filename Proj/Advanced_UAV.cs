using Microsoft.Xna.Framework;
using Mono.Cecil;
using System;
using System.Linq;
using TemplateMod.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.PlayerDrawLayer;

namespace tewd.Proj
{
    // This file contains all the code necessary for a minion
    // - ModItem - the weapon which you use to summon the minion with
    // - ModBuff - the icon you can click on to despawn the minion
    // - ModProjectile - the minion itself

    // It is not recommended to put all these classes in the same file. For demonstrations sake they are all compacted together so you get a better overwiew.
    // To get a better understanding of how everything works together, and how to code minion AI, read the guide: https://github.com/tModLoader/tModLoader/wiki/Basic-Minion-Guide
    // This is NOT an in-depth guide to advanced minion AI

    public class Advanced_UAVBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Example Minion");
            Description.SetDefault("The example minion will fight for you");

            Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
            Main.buffNoTimeDisplay[Type] = true; // The time remaining won't display on this buff
        }

        public override void Update(Player player, ref int buffIndex)
        {
            // If the minions exist reset the buff time, otherwise remove the buff from the player
            if (player.ownedProjectileCounts[ModContent.ProjectileType<ExampleSimpleMinion>()] > 0)
            {
                player.buffTime[buffIndex] = 18000;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }

    public class Advanced_UAV_calling_staff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("高级战斗机召唤杖");
            Tooltip.SetDefault("可以召唤高级战斗机来为你战斗");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; // This lets the player target anywhere on the whole screen while using a controller
            ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.damage = 70;
            Item.knockBack = 3f;
            Item.mana = 10; // mana cost
            Item.width = 58;
            Item.height = 58;
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.useStyle = ItemUseStyleID.Swing; // how the player's arm moves when using the item
            Item.value = Item.sellPrice(gold: 30);
            Item.rare = ItemRarityID.Cyan;
            Item.UseSound = SoundID.Item44; // What sound should play when using the item

            // These below are needed for a minion weapon
            Item.noMelee = true; // this item doesn't do any melee damage
            Item.DamageType = DamageClass.Summon; // Makes the damage register as summon. If your item does not have any damage type, it becomes true damage (which means that damage scalars will not affect it). Be sure to have a damage type
            Item.buffType = ModContent.BuffType<ExampleSimpleMinionBuff>();
            // No buffTime because otherwise the item tooltip would say something like "1 minute duration"
            Item.shoot = ModContent.ProjectileType<Advanced_UAV>(); // This item creates the minion projectile
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            // Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position
            position = Main.MouseWorld;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
            player.AddBuff(Item.buffType, 2);

            // Minions have to be spawned manually, then have originalDamage assigned to the damage of the summon item
            var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
            projectile.originalDamage = Item.damage;

            // Since we spawned the projectile manually already, we do not need the game to spawn it for ourselves anymore, so return false
            return false;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.

    }

    // This minion shows a few mandatory things that make it behave properly.
    // Its attack pattern is simple: If an enemy is in range of 43 tiles, it will fly to it and deal contact damage
    // If the player targets a certain NPC with right-click, it will fly through tiles to it
    // If it isn't attacking, it will float near the player with minimal movement
    public class Advanced_UAV : ModProjectile
    {
        private float Timer
        {
            get { return Projectile.ai[0]; }
            set { Projectile.ai[0] = value; }
        }

        private float State
        {
            get { return Projectile.ai[1]; }
            set { Projectile.ai[1] = value; }
        }
        public Vector2 TargetLocation = new Vector2();

        private static float _nearPlayerSpeed = 0.1f;
        private int damage;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("微型破烂战斗机");
        }
        public override void SetDefaults()
        {
            Projectile.width = 21;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 1145141919;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.scale = 1.1f;
            Projectile.damage = 5;
            // 召唤物必备的属性
            Main.projPet[Projectile.type] = true;
            Projectile.netImportant = true;
            Projectile.minionSlots = 1f;
            Projectile.minion = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
        }

        /// <summary>
        /// 没有接触伤害
        /// </summary>
        /// <returns></returns>
        public override bool MinionContactDamage()
        {
            return false;
        }
        /// <summary>
        /// 寻找最近的敌方单位
        /// </summary>
        /// <param name="position"></param>
        /// <param name="maxDistance"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static NPC FindCloestEnemy(Vector2 position, float maxDistance, Func<NPC, bool> predicate)
        {
            float maxDis = maxDistance;
            NPC res = null;
            foreach (var npc in Main.npc.Where(n => n.active && !n.friendly && predicate(n)))
            {
                float dis = Vector2.Distance(position, npc.Center);
                if (dis < maxDis)
                {
                    maxDis = dis;
                    res = npc;
                }
            }
            return res;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            var modPlayer = player.GetModPlayer<mdplayer>();
            // 玩家死亡会让召唤物消失
            if (player.dead)
            {
                modPlayer.Gliders = false;
            }
            if (modPlayer.Gliders)
            {
                // 如果Gliders不为true那么召唤物弹幕只有两帧可活
                Projectile.timeLeft = 2;
            }
            // 弹幕的姿态调整
            Projectile.direction = (Projectile.spriteDirection = -Math.Sign(Projectile.velocity.X));
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 10f;
            Projectile.netUpdate = true;
            NPC npc = FindCloestEnemy(Projectile.Center, 1200f, (n) =>
            {
                return n.CanBeChasedBy() &&
                !n.dontTakeDamage && Collision.CanHitLine(Projectile.Center, 1, 1, n.Center, 1, 1);
            });
            if (TargetLocation == Vector2.Zero && npc != null && Vector2.Distance(Projectile.Center, player.Center) < 700)
            {
                TargetLocation = npc.Center;
            }
            // 如果鼠标没有控制而且周围没有敌人
            if (npc == null && TargetLocation == Vector2.Zero)
            {
                State = 0;
                Timer = 0;
            }


            if (State == 0)
            {
                MoveAroundPlayer(player);
                if (npc != null || TargetLocation != Vector2.Zero) { State = 1; }
            }
            else if (State == 1)
            {
                Timer++;
                Vector2 diff = TargetLocation - Projectile.Center;
                float distance = diff.Length();
                diff.Normalize();
                Projectile.rotation = diff.ToRotation() + 1.57f;
                // 射击
                if (Timer % 15 < 1)
                {
                    var ysxb = Terraria.Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.Center+ Projectile.velocity + diff * 50, diff * 10f, ProjectileID.BulletHighVelocity,
                                        Projectile.damage, 0.5f, Main.myPlayer);

                }
                if (Timer % 40 < 1)
                {
                    var ysxb = Terraria.Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.Center + Projectile.velocity + diff * 50, diff * 10f, ProjectileID.NanoBullet,
                                        Projectile.damage, 0.5f, Main.myPlayer);

                }
                if (distance > 500)
                {
                    Projectile.velocity = (Projectile.velocity * 20f + diff * 5) / 21f;
                }
                else
                {
                    Projectile.velocity *= 0.97f;
                }
                // 让召唤物不至于靠的太近
                if (distance > 300)
                {
                    Projectile.velocity = (Projectile.velocity * 40f + diff * 5) / 41f;
                }
                else if (distance < 150)
                {
                    Projectile.velocity = (Projectile.velocity * 20f + diff * -4) / 21f;
                }
                TargetLocation = Vector2.Zero;
            }


            // 召唤物弹幕的后续处理，轨迹，限制等
            if (Projectile.velocity.Length() > 16)
            {
                Projectile.velocity *= 0.98f;
            }
            if (Math.Abs(Projectile.velocity.X) < 0.01f || Math.Abs(Projectile.velocity.Y) < 0.01f)
            {
                Projectile.velocity = Main.rand.NextVector2Circular(1, 1) * 2f;
            }

            if (Projectile.velocity.Length() > 6)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height,
                    MyDustId.OrangeFire, -Projectile.velocity.X, -Projectile.velocity.Y, 100, Color.Red, 1f);
                _ = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height,
                    MyDustId.BlueIce, -Projectile.velocity.X, -Projectile.velocity.Y, 100, Color.Red, 1f);
                dust.noGravity = true;
                dust.position = Projectile.Center - Projectile.velocity;
            }

        }

        /// <summary>
        /// 让召唤物绕着玩家运动
        /// </summary>
        /// <param name="player"></param>
        private void MoveAroundPlayer(Player player)
        {
            Vector2 diff = Projectile.Center - player.Center;
            diff.Normalize();
            //diff = diff.RotatedBy(MathHelper.PiOver2);
            Projectile.velocity -= diff * 0.1f;

            if (Projectile.Center.X < player.Center.X)
            {
                Projectile.velocity.X += _nearPlayerSpeed;
            }
            if (Projectile.Center.X > player.Center.X)
            {
                Projectile.velocity.X -= _nearPlayerSpeed;
            }
            if (Projectile.Center.Y < player.Center.Y)
            {
                Projectile.velocity.Y += _nearPlayerSpeed;
            }
            if (Projectile.Center.Y > player.Center.Y)
            {
                Projectile.velocity.Y -= _nearPlayerSpeed;
            }

        }
    }
}
