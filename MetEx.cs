using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace MetEx
{
    public class MetEx : Mod
    {
        public override void Load()
        {
            On.Terraria.Main.DrawInterface_35_YouDied += Main_DrawInterface_35_YouDied;

            // All of this loading needs to be client-side.

            if (Main.netMode != NetmodeID.Server)
            {
                // First, you load in your shader file.
                // You'll have to do this regardless of what kind of shader it is,
                // and you'll have to do it for every shader file.
                // This example assumes you have both armour and screen shaders.

                Ref<Effect> Wavy = new Ref<Effect>(GetEffect("Effects/Wavy"));
                // To bind a screen shader, use this.
                // EffectPriority should be set to whatever you think is reasonable.   

                Filters.Scene["Wavy"] = new Filter(new ScreenShaderData(Wavy, "Wavy"), EffectPriority.VeryHigh);
            }
        }
        private void Main_DrawInterface_35_YouDied(On.Terraria.Main.orig_DrawInterface_35_YouDied orig)
        {
            orig(); // Idk kinda wanna change death textbut lazy rn lol
        }
    }
    public class PlaySound : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "playsound";

        public override string Usage
            => "/playsound <soundid>";

        public override string Description
            => "play a sound";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            Player player = Main.player[Main.myPlayer];
            var soundType = args[0];
            if (!int.TryParse(args[0], out int type))
            {
                if (type == 0)
                {
                    throw new UsageException($"{soundType} is not a valid SoundType");
                }
            }
            int sound = 1;
            if (args.Length >= 2)
            {
                sound = int.Parse(args[1]);
            }
            Main.PlaySound(type, player.Center);
        }
    }
}