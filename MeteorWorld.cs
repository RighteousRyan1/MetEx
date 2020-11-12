using System.Collections.Generic;
using Libvaxy.WorldGen;
using MetEx.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace MetEx
{
    public class MeteorWorld : ModWorld
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int index = tasks.FindIndex(pass => pass.Name == "Mushrooms");

            if (index == -1)
                return;

            tasks.Insert(15, new PassLegacy("Meteor", progress =>
            {
                progress.Message = "Slamming your world...";

                int worldEdgeOffset = 400;
                int meteorX = WorldGen.genRand.Next(worldEdgeOffset, Main.maxTilesX - worldEdgeOffset);

                Point craterPoint =  WorldGenUtils.FindDownwardsTile(meteorX, (int)WorldGen.worldSurfaceHigh + 60);

                int craterYOffset = 15;
                
                GenerateCrater(craterPoint.X, craterPoint.Y + craterYOffset, WorldGen.genRand.Next(450, 500), 300);
                progress.Value = 0.5f;

                Point meteorPoint = WorldGenUtils.FindDownwardsTile(craterPoint.X, craterPoint.Y + craterYOffset);
                meteorPoint.Y -= 15;
                
                GenerateMeteorChunk(meteorPoint.X, meteorPoint.Y);
            }));
        }

        public static void GenerateCrater(int i, int j, int width, int elongation)
        {
            int maxY = j + width / 2 * width / 2 / elongation * -1;
            int extraHeightClear = 60;
            
            for (int x = -width / 2; x < width / 2; x++)
            {
                Point point = new Point(i + x, j + x * x / elongation * -1);
                
                if (Main.tile[point.X, point.Y].active())
                    WorldGenUtils.NoiseRunner(point.X, point.Y, ModContent.TileType<CharredRock>(), 20, 0.09f, -1);
                
                WorldGenUtils.FillAir(point.X, maxY - extraHeightClear, 1, point.Y - maxY + extraHeightClear);

                for (int y = maxY; y < point.Y; y++)
                    Main.tile[point.X, y].wall = WallID.Stone;
            }
        }
        
        public static void GenerateMeteorChunk(int i, int j)
        {
            float radius = WorldGen.genRand.NextFloat(50f, 60f);
            float frequency = WorldGen.genRand.NextFloat(0.0035f, 0.005f);
            WorldGenUtils.NoiseRunner(i, j, TileID.Meteorite, radius, frequency);
        }
    }
}