using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace MetEx.Items.Equippables.Dyes
{
	public class LightToDarkDye : ModItem
	{
        public override void SetStaticDefaults()
        {
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(8, 8));
			DisplayName.SetDefault("Light to Dark Dye");
        }
        public override void SetDefaults()
		{		
			item.width = 20;
			item.height = 20;
			item.maxStack = 99;
			item.value = Item.sellPrice(0, 1, 50, 0);
			item.rare = ItemRarityID.Purple;
			byte dye = item.dye;
			item.dye = dye;
		}
	}
}