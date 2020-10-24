using Terraria.ID;
using System.Collections.Generic;
using System.Security.Policy;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace MetEx.Buffs.Bad
{
    public class HeatExhaustion : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Heat Exhaustion");
            Description.SetDefault("You feel overwhelmed...\nHeat related debuffs damage you more\nYour jumps are shorter\nYou are physically exhausted\nYour vision is getting worse");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            canBeCleared = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            Player.jumpSpeed -= (int)(Player.jumpSpeed * 0.40f);
            if (player.lifeRegen > 0)
            {
                player.lifeRegen = 0;
            }
            player.lifeRegenTime = 0;

            if (player.HasBuff(BuffID.Burning))
            {
                player.lifeRegen -= 10;
            }
            if (player.HasBuff(BuffID.OnFire))
            {
                player.lifeRegen -= 8;
            }
            else
            {
                player.lifeRegen -= 1;
            }
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
        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            bool NPCKill = damageSource.SourceNPCIndex >= 0;
            if (player.HasBuff(ModContent.BuffType<HeatExhaustion>()))
            {
                if (NPCKill)
                {
                    damageSource = PlayerDeathReason.ByCustomReason($"{player.name}'s immense exhaustion caused their organs to be extracted by {Main.npc[damageSource.SourceNPCIndex].FullName}.");
                }
                // Ugg work ok fiune time to slep - Ryan
            }
        }
    }
    public class ModifyMeteorSpawnPool : GlobalNPC
    {
        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            Player p = Main.player[Main.myPlayer];
            if (!p.ZoneMeteor)
            {
                return;
            }
            if (p.ZoneMeteor)
            {
                pool.Clear();

                foreach (var id in Lists.MeteorEnemies)
                {
                    pool.Add(id, 0.5f);
                }
            }
        }
    }
}