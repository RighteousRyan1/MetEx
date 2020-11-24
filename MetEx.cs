using MetEx.Items.Equippables.Dyes;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace MetEx
{
    public class MetEx : Mod
    {
        public override void Load()
        {
            IL.Terraria.Main.DrawInterface_35_YouDied += Main_DrawInterface_35_YouDied1; ;
            if (Main.netMode != NetmodeID.Server)
            {
                // Screen Shaders

                Ref<Effect> Wavy = new Ref<Effect>(GetEffect("Effects/Wavy"));
                Filters.Scene["Wavy"] = new Filter(new ScreenShaderData(Wavy, "Wavy"), EffectPriority.VeryHigh);

                // Dyes Below

                Ref<Effect> dyeRef = new Ref<Effect>(GetEffect("Effects/DyeEffects/LightToDark"));
                GameShaders.Armor.BindShader(ModContent.ItemType<LightToDarkDye>(), new ArmorShaderData(dyeRef, "LightToDarkPass"));
            }
        }

        private void Main_DrawInterface_35_YouDied1(MonoMod.Cil.ILContext il)
        {
        }

        public override void PostUpdateEverything()
        {
        }
        public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {   
            Player player = Main.player[Main.myPlayer];
            if (player.ZoneMeteor && Main.dayTime)
            {
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/MeteorDay");
                priority = MusicPriority.BiomeMedium;
            }
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