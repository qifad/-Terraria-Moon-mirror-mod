using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using tewd.Proj;

namespace tewd.Items
{
	public class Advanced_blade_typhoon : ModItem
    { 
        public override void SetStaticDefaults()
        {
            
            Tooltip.SetDefault("This is a basic modded sword.");
        }

        public override void SetDefaults()
        {
            Item.damage = 370;
            Item.DamageType = DamageClass.Magic;
            Item.width = 72;
            Item.height = 72;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = 5;
            Item.knockBack = 6;
            Item.value = 10000;
            Item.rare = 2;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.Typhoon;
            Item.shootSpeed = 10f;
            Item.mana = 15;
            Item.noMelee = true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, Vector2.Normalize(Main.MouseWorld - player.Center) * 10, ProjectileID.Typhoon, damage, knockback, player.whoAmI);
            for (float j = 0; j <= 29; j++)
            {
                Vector2 vel = (j.ToRotationVector2() * MathHelper.Pi / 20) + velocity;
                Projectile.NewProjectile(source, position + velocity, vel, type, damage, knockback, player.whoAmI, 0, -1);
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