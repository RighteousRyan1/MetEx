using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace MetEx.NPCs.Hostile
{
    public class MeteorSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Meteorite Slime");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.BlueSlime];
        }

        public override void SetDefaults()
        {
            npc.width = 32;
            npc.height = 22;
            npc.damage = 12;
            npc.defense = 7;
            npc.lifeMax = 85;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 100f;
            npc.knockBackResist = 1f;
            npc.aiStyle = -1;
            npc.alpha = 60;
            animationType = NPCID.BlueSlime;
            npc.noGravity = false;
            npc.friendly = false;
        }
        public override void AI()
        {
            npc.ai[0]++;
            npc.TargetClosest();
            if(npc.ai[0] >= 180 && npc.velocity.Y <= 0.001f)
            {
                npc.velocity.Y -= 12;
                npc.velocity.X += (Main.player[npc.target].Center.X > npc.Center.X ? 1 : -1) * 6;
                npc.ai[0] = 0;
                npc.ai[1] = 0;
            }
            if (npc.velocity.Y < 12 && npc.velocity.Y >= 0.001f)
            {
                npc.velocity.Y += 0.1f;
            }
            if (npc.velocity.Y > 6)
            {
                Dust.NewDustPerfect(npc.Center, DustID.Fire);
            }
            if ((npc.velocity.Y <= 0.001f && npc.oldVelocity.Y > 6f) || (npc.collideY && npc.ai[1] == 0))
            {
                Vector2 origin = npc.Center;
                float radius = 8;
                int numLocations = 60;
                for (int i = 0; i < numLocations; i++)
                {
                    Vector2 position = origin + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / numLocations * i) + Main.rand.NextFloat(-1, 1)) * radius;
                    Dust dust = Dust.NewDustPerfect(position, DustID.Fire);
                    dust.velocity = Vector2.Normalize(position - npc.Center);
                }
                if (npc.collideY && npc.ai[1] == 0)
                {
                    npc.ai[1] = 1;
                }
            }
            npc.rotation = npc.velocity.X / 48;

            if (npc.collideY)
            {
                npc.velocity.X *= 0.9f;
            }
        }
    }
}