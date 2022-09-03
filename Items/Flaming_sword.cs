using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using tewd.Proj;

namespace tewd.Items
{
	public class Flaming_sword : ModItem
    { 
        public override void SetStaticDefaults()
        {
            
            Tooltip.SetDefault("This is a basic modded sword.");
        }

        public override void SetDefaults()
        {
            Item.damage = 45;
            Item.DamageType = DamageClass.Melee;
            Item.width = 80;
            Item.height = 80;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = 1;
            Item.knockBack = 6;
            Item.value = 10000;
            Item.rare = 2;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Flaming_sword_proj>();
            Item.shootSpeed = 5f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, Vector2.Normalize(Main.MouseWorld - player.Center) * 20, ModContent.ProjectileType<Flaming_sword_proj>(), damage, knockback, player.whoAmI);
            for (float j = 0; j <= 2; j++)
            {
                Vector2 vel = (j.ToRotationVector2() * MathHelper.Pi / 1) + velocity;
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