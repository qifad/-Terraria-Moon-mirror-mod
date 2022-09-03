
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using tewd.Proj;

namespace tewd.Items
{
	public class ExampleWhip : ModItem
	{
		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			// This method quickly sets the whip's properties.
			// Mouse over to see its parameters.
			Item.DefaultToWhip(ModContent.ProjectileType<ExampleWhipProjectile>(),100, 10, 2); 

			Item.shootSpeed = 15;
			Item.rare = ItemRarityID.Green;
            Item.autoReuse = true;
            Item.channel = true;
			Item.useAnimation = 12;
			Item.useTime = 12;
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
	}
}
