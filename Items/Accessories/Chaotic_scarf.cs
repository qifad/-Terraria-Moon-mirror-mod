using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using tewd.Proj;

namespace tewd.Items.Accessories
{
	public class Chaotic_scarf : ModItem
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
            // ����̩�����ǣ������Ʒ�Ǹ���Ʒ
            Item.accessory = true;
            // ��Ʒ����������ֵ��װ�����Ժ�ͻ�����
            Item.defense = 6;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.statLifeMax >= 500&&player.statLifeMax2<=650)
            {
                player.statLifeMax2 += 150;
            }
            if (player.statLifeMax2 >= 800 &&player.statLifeMax2<=1101)
            {
                player.statLifeMax2 += 300;
            }
            
            
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