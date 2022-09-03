using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using tewd.Proj;

namespace tewd.Items
{
	public class Slime_Sword : ModItem
    { 
        public override void SetStaticDefaults()
        {
            
            Tooltip.SetDefault("This is a basic modded sword.");
        }

        public override void SetDefaults()
        {
            Item.damage = 25;
            Item.DamageType = DamageClass.Melee;
            Item.width = 64;
            Item.height = 64;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = 1;
            Item.knockBack = 6;
            Item.value = 10000;
            Item.rare = 2;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Slime_Sword_projectile>();
            Item.shootSpeed = 10f;
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