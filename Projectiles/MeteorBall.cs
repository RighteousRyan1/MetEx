using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Newtonsoft.Json.Serialization;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace MetEx.Projectiles
{
    public class MeteorBall : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Meteor Spit");
        }
        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 13;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.tileCollide = true;
            projectile.timeLeft = 99999;
            projectile.light = 0.1f;
            projectile.ignoreWater = false;
            projectile.damage = 12;
            projectile.penetrate = 1;
            projectile.hostile = true;
            projectile.friendly = false;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.NewProjectile(projectile.Center + new Vector2(0, -25), Vector2.Zero, ModContent.ProjectileType<MeteorBallSplashDamage>(), 15, 10);
            Main.PlaySound(SoundID.Item100, projectile.Center);
            SpawnBrightDusts();
            SpawnHighFlyDusts();
            SpawnMeteorBreakingDusts();
            SpawnOrangeDusts();
            projectile.Kill();
            return false;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            Projectile.NewProjectile(projectile.Center + new Vector2(0, -25), Vector2.Zero, ModContent.ProjectileType<MeteorBallSplashDamage>(), 15, 10);
            Main.PlaySound(SoundID.Item100, projectile.Center);
            SpawnBrightDusts();
            SpawnHighFlyDusts();
            SpawnMeteorBreakingDusts();
            SpawnOrangeDusts();
            projectile.Kill();
        }
        public override void AI()
        {
            SpawnTrailDusts();
            // 12 x 13 dimensions
            // projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
            projectile.rotation = projectile.velocity.ToRotation();
            projectile.velocity.Y = projectile.velocity.Y + 0.2f; // 0.1f for arrow gravity, 0.4f for knife gravity
            if (projectile.velocity.Y > 16f)
            {
                projectile.velocity.Y = 16f;
            }
        }
        // Fun personal note: Shader Type 77 is a rainbow
        /// <summary>
        /// Spawn the high shooting dusts
        /// they are small yet fast
        /// </summary>
        private void SpawnTrailDusts()
        {
            Dust dust;
            Vector2 position = projectile.Center;
            dust = Main.dust[Dust.NewDust(position, 12, 13, 6, 0f, 0f, 0, new Color(255, 100, 0), 1f)];
            dust.noGravity = true;
        }
        private void SpawnHighFlyDusts()
        {
            for (int spawnTimes = 0; spawnTimes < 8; spawnTimes++)
            {
                Dust dust;
                Vector2 position = projectile.Center;
                dust = Main.dust[Terraria.Dust.NewDust(position, 20, 0, 43, 0f, -5f, 0, new Color(255, 226, 0), 1f)];
            }
        }
        private void SpawnMeteorBreakingDusts()
        {
            for (int spawnTimes = 0; spawnTimes < 8; spawnTimes++)
            {
                Dust dust;
                Vector2 position = projectile.Center;
                dust = Main.dust[Terraria.Dust.NewDust(position, 20, 0, 81, 0f, -2.25f, 0, new Color(255, 100, 0), 0.92f)];
            }
        }
        private void SpawnBrightDusts()
        {
            for (int spawnTimes = 0; spawnTimes < 8; spawnTimes++)
            {
                Dust dust;
                Vector2 position = projectile.Center;
                dust = Main.dust[Terraria.Dust.NewDust(position, 20, 0, 87, 0f, -3.5f, 0, new Color(255, 0, 0), 0.9210526f)];
            }
        }
        private void SpawnOrangeDusts()
        {
            for (int spawnTimes = 0; spawnTimes < 8; spawnTimes++)
            {
                Dust dust;
                Vector2 position = projectile.Center;
                dust = Main.dust[Terraria.Dust.NewDust(position, 20, 0, 106, 0f, -2.75f, 0, new Color(255, 0, 0), 0.9210526f)];
                dust.shader = GameShaders.Armor.GetSecondaryShader(6, Main.LocalPlayer);
            }
        }
    }
}
