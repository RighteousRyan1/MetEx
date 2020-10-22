using Terraria;
using System;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;


namespace MetEx.Projectiles
{
    public class MartianGroundstriker : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.LaserMachinegunLaser;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Martian Groundstriker");

        }
        public override void SetDefaults()
        {
            projectile.hostile = true;
            projectile.width = 16;
            projectile.height = 16;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.timeLeft = 240;
            projectile.aiStyle = -1;
            
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0.1f, 0.1f, 0.3f);

        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return base.OnTileCollide(oldVelocity);
        }
        // Spawns 2 horizontal moving projectiles on death
        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(projectile.Center + new Vector2(0 , -16), new Vector2(5 , 0) , ProjectileID.LaserMachinegunLaser, projectile.damage, 5f, Main.player[projectile.owner].whoAmI);
            Projectile.NewProjectile(projectile.Center + new Vector2(0, -16), new Vector2(-5, 0), ProjectileID.LaserMachinegunLaser, projectile.damage, 5f, Main.player[projectile.owner].whoAmI);
        }
    }
}