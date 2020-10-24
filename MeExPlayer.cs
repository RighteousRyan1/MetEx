using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using MetEx.Buffs.Bad;
using Terraria.Graphics.Effects;

namespace MetEx
{
    public class MetExPlayer : ModPlayer
    {
        public override void UpdateBiomeVisuals()
        {
            bool isExhausted = player.HasBuff(ModContent.BuffType<HeatExhaustion>());
            player.ManageSpecialBiomeVisuals("Wavy", isExhausted);
        }
    }
}