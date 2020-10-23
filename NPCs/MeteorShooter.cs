using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MetEx.Projectiles;
using MetEx.Items.Weapons.Magic;

namespace MetEx.NPCs
{
	public class MeteorShooter : ModNPC
	{
        /// <summary>
        /// Time until this NPC shoots projectiles
        /// </summary>
        public int shootTimer;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Meteor Shooter"); // NOTE: Wip Name?
            Main.npcFrameCount[npc.type] = 11;
		}
        public override void SetDefaults()
        {
            npc.buffImmune[BuffID.OnFire] = true;
            npc.width = 32;
            npc.height = 22;
            npc.defense = 17;
            npc.lifeMax = 60;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 60f;
            npc.knockBackResist = 0f;
            Main.npcFrameCount[npc.type] = 11;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return spawnInfo.player.ZoneMeteor ? .025f : 0f;
		}
        public override void HitEffect(int hitDirection, double damage) // Implement at a later date?
        {
        }
        public override void NPCLoot() // Implement at a later date
        {
            if (Main.rand.NextFloat() < 0.075f)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<MeteorSplicerStaff>());
            }
        }
        public override void AI()
        {
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            base.AI();
            FireProjectiles();
        }
        /// <summary>
        /// Define whether this slime is shooting projectiles
        /// </summary>
        private void FireProjectiles()
        {
            Player player = Main.player[npc.target];
            bool playerIsLeftofNPC = player.Center.X <= npc.Center.X;
            bool playerIsRightofNPC = player.Center.X > npc.Center.X;
            shootTimer++;
            if (shootTimer > 60 && shootTimer < 120)
            {
                if (Main.rand.NextFloat() < 0.05f && playerIsRightofNPC)
                {
                    Main.PlaySound(SoundID.Item42, npc.Center);
                    Projectile.NewProjectile(npc.Top + new Vector2(0, -3), new Vector2(Main.rand.Next(1, 4), Main.rand.Next(-5, -2)), ModContent.ProjectileType<MeteorBallHostile>(), 12, 5);
                }
                if (Main.rand.NextFloat() < 0.05f && playerIsLeftofNPC)
                {
                    Main.PlaySound(SoundID.Item42, npc.Center);
                    Projectile.NewProjectile(npc.Top + new Vector2(0, -3), new Vector2(Main.rand.Next(-4, -1), Main.rand.Next(-5, -2)), ModContent.ProjectileType<MeteorBallHostile>(), 12, 5);
                }
            }
            if (shootTimer == 300)
            {
                shootTimer = 0;
            }
        }
    }
}