using Microsoft.Xna.Framework;
using TemplateMod.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using tewd.Proj;

namespace tewd.Items
{
	public class Photosynthetic_sanctions : ModItem
    { 
        public override void SetStaticDefaults()
        {
            
            Tooltip.SetDefault("This is a basic modded sword.");
        }

        public override void SetDefaults()
        {
            Item.damage = 350;
            Item.DamageType = DamageClass.Melee;
            Item.width = 110;
            Item.height = 114;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = 1;
            Item.knockBack = 6;
            Item.value = 10000;
            Item.rare = 2;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Photosynthetic_sanctions_Proj>();
            Item.shootSpeed = 15f;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            // 在武器的挥动判定区域添加一些火焰粒子特效
            Dust.NewDustDirect(hitbox.TopLeft(), hitbox.Width, hitbox.Height,
                298, 0, 0, 100, Color.White, 2f);
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