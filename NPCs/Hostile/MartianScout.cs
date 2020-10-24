using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MetEx.Projectiles;

namespace MetEx.NPCs.Hostile
{
    public class MartianScout : ModNPC
    {
        public override string Texture => "Terraria/NPC_" + NPCID.MartianDrone;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void SetDefaults()
        {
            npc.width = npc.height = 40;
            npc.damage = 40;
            npc.lifeMax = 600;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 130;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.defense = 15;
            npc.knockBackResist = 0f;
        }
        /// <summary>
        /// Another word for timer
        /// </summary>
        private float shootTimer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        public override void AI()
        {
            npc.TargetClosest();
            Player player = Main.player[npc.target];
            // Lighting may be overkill
            Lighting.AddLight(npc.Center, 0.5f, 0.5f, 1f);
            // Being under 20% life will initiate self destruction
            if (npc.life > npc.lifeMax / 5)
            {
                shootTimer++;
                Despawn(player);

                Hover(player);

                //I wanted the lasers to attempt slighty at predicting the player's position , probs needs more testing lel
                if (shootTimer % 40 == 0 || shootTimer % 50 == 0)
                {
                    if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Vector2 targetPos = new Vector2(player.Center.X + player.velocity.X * 16, player.Center.Y + player.velocity.Y * 16);
                        Vector2 vel = targetPos - npc.Center;
                        vel.Normalize();
                        Projectile.NewProjectile(npc.Center, vel * 9, ProjectileID.EyeFire, npc.damage / 3, 6f, Main.myPlayer);
                    }
                }
                if (shootTimer % 300 == 0)
                {
                    Projectile.NewProjectile(npc.Center, new Vector2(0, 7), ModContent.ProjectileType<MartianGroundstriker>(), npc.damage / 2, 6f, Main.myPlayer);
                }
            }
            else if (npc.life < npc.lifeMax / 5)
            {
                SelfDestruct(player, 12.5f, 64);
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.player.ZoneMeteor ? 0.25f : 0f;
        }
        /// <summary>
        /// Self destruction will cause it to blow up whenever its close to the player
        /// </summary>
        private void SelfDestruct(Player player, float velMult, float explodeDist)
        {
            if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 velocity = player.Center - npc.Center;
                velocity.Normalize();
                npc.velocity = velocity * velMult;

                if(Vector2.Distance(npc.Center, player.Center) < explodeDist)
                {
                    for(int i =0; i < 6; i++)
                    {
                        Dust.NewDustDirect(npc.Center, npc.width, npc.height, DustID.Electric, 0, -5);
                    }
                    npc.life = 0;
                }
            }
        }
        /// <summary>
        /// Despawn distance is 75Tiles , possibly overkill
        /// </summary>
        private void Despawn(Player player)
        {
            if (npc.target < 0 || npc.target == 255 || player.dead || !player.active || Vector2.Distance(player.Center, npc.Center) > 1200)
            {
                npc.TargetClosest(false);
                npc.velocity.Y -= 0.1f;
                //npc.rotation = npc.velocity.ToRotation();

                if (npc.timeLeft > 60)
                {
                    npc.timeLeft = 60;
                }
            }
        }
        /// <summary>
        /// Hovers around the given player , similar to Skeletron Head's Hover
        /// Probs gonna tweak this
        /// </summary>
        private void Hover(Player player)
        {
            if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
            {

                if (npc.position.Y > player.position.Y - 250f)
                {
                    if (npc.velocity.Y > 0f)
                    {
                        npc.velocity.Y *= 0.98f;
                    }
                    npc.velocity.Y -= 0.02f;
                    if (npc.velocity.Y > 2f)
                    {
                        npc.velocity.Y = 2f;
                    }
                }

                if (npc.position.Y < player.position.Y - 250f)
                {
                    if (npc.velocity.Y < 0f)
                    {
                        npc.velocity.Y *= 0.98f;
                    }
                    npc.velocity.Y += 0.02f;
                    if (npc.velocity.Y < -2f)
                    {
                        npc.velocity.Y = -2f;
                    }
                }

                if (npc.position.X + npc.width / 2 > player.position.X + player.width / 2)
                {
                    if (npc.velocity.X > 0f)
                    {
                        npc.velocity.X *= 0.98f;
                    }
                    npc.velocity.X -= 0.05f;
                    if (npc.velocity.X > 8f)
                    {
                        npc.velocity.X = 8f;
                    }
                }
                if (npc.position.X + npc.width / 2 < player.position.X + player.width / 2)
                {
                    if (npc.velocity.X < 0f)
                    {
                        npc.velocity.X *= 0.98f;
                    }
                    npc.velocity.X += 0.05f;
                    if (npc.velocity.X < -8f)
                    {
                        npc.velocity.X = -8f;
                    }
                }

            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            Dust.NewDustDirect(npc.Center, npc.width, npc.height, DustID.Electric, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f);
        }
    }
}