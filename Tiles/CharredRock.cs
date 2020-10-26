using Libvaxy.Extensions;
using MetEx.Items.ItemTiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MetEx.Tiles
{
    public class CharredRock : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            drop = ModContent.ItemType<CharredRockItem>();
            AddMapEntry(Main.tileTexture[Type].GetAverageColor());
            soundType = SoundID.Tink;
            minPick = 40;
            Main.tileMerge[Type][TileID.Stone] = true;
            Main.tileMerge[TileID.Stone][Type] = true;
        }
        
        public override bool CreateDust(int i, int j, ref int type)
        {
            byte brightness = (byte)(Main.rand.Next(2) == 0 ? 100 : 150);
            Dust.NewDust(new Vector2(i, j).ToWorldCoordinates(), 16, 16, DustID.Smoke, 0, 0, 0, new Color(brightness, brightness, brightness), 1);
            return false;
        }
    }
}