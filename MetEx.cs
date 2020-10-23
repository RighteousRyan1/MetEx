using Terraria;
using Terraria.ModLoader;

namespace MetEx
{
    public class MetEx : Mod
    {
        public override void Load()
        {
            On.Terraria.Main.DrawInterface_35_YouDied += Main_DrawInterface_35_YouDied;
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