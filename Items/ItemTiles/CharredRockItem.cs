using Libvaxy.Content;
using MetEx.Tiles;
using Terraria.ModLoader;

namespace MetEx.Items.ItemTiles
{
    public class CharredRockItem : SimpleTileItem
    {
        public CharredRockItem() : base(
            "Charred Rock",
            999, 
            20, 
            ModContent.TileType<CharredRock>()) { }
    }
}