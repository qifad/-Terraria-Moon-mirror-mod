using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using tewd.Proj;

namespace tewd.Items
{
	public class ExampleLifeFruit : ModItem
	{
        public const int MaxExampleLifeFruits = 50;
        public const int LifePerFruit = 150;


        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault($"Permanently increases maximum life by {LifePerFruit}\nUp to {MaxExampleLifeFruits} can be used");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 10;
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.LifeFruit);
        }

        public override bool CanUseItem(Player player)
        {
            // Any mod that changes statLifeMax to be greater than 500 is broken and needs to fix their code.
            // This check also prevents this item from being used before vanilla health upgrades are maxed out.
            return player.statLifeMax == 500 && player.GetModPlayer<mdplayer>().exampleLifeFruits < MaxExampleLifeFruits;
        }

        public override bool? UseItem(Player player)
        {
            // Do not do this: player.statLifeMax += 2;
            player.statLifeMax2 += LifePerFruit;
            player.statLife += LifePerFruit;
            if (Main.myPlayer == player.whoAmI)
            {
                // This spawns the green numbers showing the heal value and informs other clients as well.
                player.HealEffect(LifePerFruit);
            }

            // This is very important. This is what makes it permanent.
            player.GetModPlayer<mdplayer>().exampleLifeFruits++;
            // This handles the 2 achievements related to using any life increasing item or getting to exactly 500 hp and 200 mp.
            // Ignored since our item is only useable after this achievement is reached
            // AchievementsHelper.HandleSpecialEvent(player, 2);
            //TODO re-add this when ModAchievement is merged?
            return true;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
    }

}
