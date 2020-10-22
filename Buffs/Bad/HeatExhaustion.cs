using System.Collections.Generic;
using System.Security.Policy;
using Terraria;
using Terraria.ModLoader;

namespace MetEx.Buffs.Bad
{
    public class HeatExhaustion : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Heat Exhaustion");
            Description.SetDefault("You feel overwhelmed...");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            canBeCleared = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            if (player.lifeRegen > 0)
            {
                player.lifeRegen = 0;
            }
            player.lifeRegenTime = 0;
            player.lifeRegen -= 1;
        }
    }
    public class ExhaustedPlayer : ModPlayer
    {
        public override void PostUpdateRunSpeeds()
        {
            if (player.ZoneMeteor)
            {
                player.AddBuff(ModContent.BuffType<HeatExhaustion>(), 2, false);
            }
            if (player.HasBuff(ModContent.BuffType<HeatExhaustion>()))
            {
                player.maxRunSpeed = 1.8f;
                player.accRunSpeed = 1.8f;
            }
        }
    }
    public class ModifyMeteorSpawnPool : GlobalNPC
    {
        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            Player p = Main.player[Main.myPlayer];
            foreach (var id in Lists.MeteorEnemies)
            {
                if (!p.ZoneMeteor)
                {
                    return;
                }
                if (p.ZoneMeteor)
                {
                    pool.Clear();
                    pool.Add(id, 1f);
                }
            }
        }
    }
}