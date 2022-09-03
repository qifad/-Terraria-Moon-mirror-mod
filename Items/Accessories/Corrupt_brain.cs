using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using tewd.Proj;

namespace tewd.Items.Accessories
{
	public class Corrupt_brain : ModItem
    {
        private int buffIndex;

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
            Item.defense = 5;
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