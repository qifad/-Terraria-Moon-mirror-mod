using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using tewd.Proj;

namespace tewd.Items
{
	public class w : ModItem
    { 
        public override void SetStaticDefaults()
        {
            
            Tooltip.SetDefault("This is a basic modded sword.");
        }

        public override void SetDefaults()
        {
            Item.damage = 50;
            Item.DamageType = DamageClass.Melee;
            Item.width = 132;
            Item.height = 138;
            Item.useTime = 3;
            Item.useAnimation = 3;
            Item.useStyle = 1;
            Item.knockBack = 6;
            Item.value = 10000;
            Item.rare = 2;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<wprojectile>();
            Item.shootSpeed = 10f;
        }

       

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DirtBlock, 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }

	
	
}