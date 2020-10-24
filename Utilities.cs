using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using MetEx.NPCs.Hostile;

namespace MetEx
{
    public class Lists
    {
        public static List<int> MeteorEnemies = new List<int>
        {
            ModContent.NPCType<MeteorShooter>(),
            ModContent.NPCType<MartianScout>(),
            ModContent.NPCType<MeteorSlime>(),
        };
    }
}