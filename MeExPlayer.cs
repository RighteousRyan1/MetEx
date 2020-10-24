using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using MetEx.Buffs.Bad;
using Terraria.Graphics.Effects;

namespace MetEx
{
    public class MetExPlayer : ModPlayer
    {
        public override void PostUpdate()
        {
            if (player.HasBuff(ModContent.BuffType<HeatExhaustion>()))
            {
                // Filters.Scene["Wavy"] = new Filter(new Terraria.Graphics.Shaders.ScreenShaderData(wavy, "Wavy"), EffectPriority.VeryHigh);
            }
        }
    }
}