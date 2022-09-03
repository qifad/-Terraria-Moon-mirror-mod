using IL.Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

namespace tewd.enm
{
    [AutoloadBossHead]
    internal class Slim_controller : ModNPC
    {
        private bool dontDamage;
        private Vector2 targetOldPos;
        private int p;
        private int damage;
        private Vector2 center;
        private Vector2 vel;
        private int proj;
        private float Timer
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }
        private enum Soul
        {
            Atk1,
            Atk2,
            Atk3,
            Atk4,
        }

        private float ChargeTime
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        private float Timer1
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }
        private float Timer2
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        private int State
        {
            get => (int)NPC.ai[3];
            set => NPC.ai[3] = value;
        }
        private Soul Stater
        {
            get { return (Soul)(int)NPC.ai[0]; }
            set { NPC.ai[0] = (int)value; }
        }
        private void SwitchTo(Soul state)
        {
            State = (int)state;
        }
        
        private Player Target => Main.player[NPC.target];
        public override string BossHeadTexture => Texture;

        public int Damage { get; private set; }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("史莱姆控制器");
        }
        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.lifeMax = 15000;
            NPC.damage = 20;
            NPC.defDamage = 3;
            NPC.knockBackResist = 0f;
            NPC.width = 128;
            NPC.height = 128;
            NPC.defDefense = 3;
            NPC.value = Item.buyPrice(1, 30, 50, 10);
            NPC.npcSlots = 25f;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.buffImmune[24] = true;
            Music = MusicID.Boss1;
            NPC.boss = true;
            NPCID.Sets.TrailCacheLength[NPC.type] = 5;
            NPCID.Sets.TrailingMode[NPC.type] = 3;
        }



        public override void AI()
        {
            Timer++;
            if (Timer > 0&&Timer<200)
            {
                SwitchTo(Soul.Atk1);
            }
            switch (State)
            {
                case (int)Soul.Atk1:

                    if (NPC.ai[0] == 100 + 120)
                    {
                        for (float a = 0; a <= 2; a++)
                        {
                            Vector2 vel = (a.ToRotationVector2() * MathHelper.Pi / 36f) + NPC.velocity.SafeNormalize(Vector2.Zero) * 10;
                            var ysxb = Terraria.Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, vel, ProjectileID.EyeLaser,
                                                NPC.damage, 0.5f, Main.myPlayer);
                            NPC.velocity = NPC.velocity.RotatedBy(0.2f);
                            ysxb.hostile = true;
                            ysxb.friendly = false;
                        }
                    }
                    if (Timer > 200) SwitchTo(Soul.Atk2);
                    break;
                case (int)Soul.Atk2:

                    if (NPC.ai[0] == 100 + 120)
                    {
                        for (float a = 0; a <= 2; a++)
                        {
                            Vector2 vel = (a.ToRotationVector2() * MathHelper.Pi / 36f) + NPC.velocity.SafeNormalize(Vector2.Zero) * 10;
                            var ysxb = Terraria.Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, vel, ProjectileID.SwordBeam,
                                                NPC.damage, 0.5f, Main.myPlayer);
                            NPC.velocity = NPC.velocity.RotatedBy(0.2f);
                            ysxb.hostile = true;
                            ysxb.friendly = false;
                        }
                    }
                    if (Timer > 400) SwitchTo(Soul.Atk3);
                    break;
            }

        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return true;
        }


        private void ShootOnyx(Vector2 center, Vector2 vel)//私自定义的方法
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int proj = Projectile.NewProjectile(NPC.GetSource_FromAI(), center, vel, 12, Damage, 1.4f, Main.myPlayer);
                Main.projectile[proj].friendly = false;//修改友善与敌对
                Main.projectile[proj].hostile = true;
                Main.projectile[proj].scale = 1.7f;
            }
        }
        private void FightSayText(string Text)
        {
            Terraria.PopupText.NewText(new()
            {
                Color = Color.Yellow,
                DurationInFrames = 120,
                Text = Text,
                Velocity = Vector2.UnitY * -5
            }, NPC.Center);
        }
    }
}