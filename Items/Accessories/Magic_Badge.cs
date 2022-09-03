using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using tewd.Proj;

namespace tewd.Items.Accessories
{
	public class Magic_Badge : ModItem
    {
        public override void SetStaticDefaults()
        {
            
            Tooltip.SetDefault("This is a basic modded sword.");
        }

        public override void SetDefaults()
        {
            Item.width = 80;
            Item.height = 80;
            Item.value = 10000;
            Item.rare = 2;
            // 告诉泰拉瑞亚，这个物品是个饰品
            Item.accessory = true;
            // 物品的面板防御数值，装备了以后就会增加
            Item.defense = 5;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {

            player.manaCost -= 0.15f;
            player.manaRegen += 10;
            player.GetDamage(DamageClass.Magic)+=0.18f;



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