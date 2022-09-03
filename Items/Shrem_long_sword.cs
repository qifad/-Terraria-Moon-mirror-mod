using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using tewd.Proj;

namespace tewd.Items
{
	public class Shrem_long_sword : ModItem
    { 
        public override void SetStaticDefaults()
        {
            
            Tooltip.SetDefault("This is a basic modded sword.");
        }

        public override void SetDefaults()
        {
            Item.damage = 1;
            Item.DamageType = DamageClass.Melee;
            Item.width = 96;
            Item.height = 96;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = 1;
            Item.knockBack = 6;
            Item.value = 10000;
            Item.rare = 2;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Slime_Sword_projectile>();
            Item.shootSpeed = 10f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
            float ceilingLimit = target.Y;
            if (ceilingLimit > player.Center.Y - 200f)
            {
                ceilingLimit = player.Center.Y - 200f;
            }
            // Loop these functions 3 times.
            for (int i = 0; i < 9; i++)
            {
                position = player.Center - new Vector2(Main.rand.NextFloat(401) * player.direction, 10f);
                position.Y -= 100 * i;
                Vector2 heading = target - position;

                if (heading.Y < 0f)
                {
                    heading.Y *= -1f;
                }

                if (heading.Y < 10f)
                {
                    heading.Y = 10f;
                }

                heading.Normalize();
                heading *= velocity.Length();
                heading.Y += Main.rand.Next(-40, 41) * 0.02f;
                Projectile.NewProjectile(source, position, heading, type, damage * 2, knockback, player.whoAmI, 0f, ceilingLimit);
            }

            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.RoyalGel, 2);
            recipe.AddIngredient(ItemID.Gel, 100);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }

	
	
}