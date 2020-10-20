using Terraria.ModLoader;

namespace MetEx.Projectiles
{
    public class MeteorBallSplashDamageFriendly : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Meteor Ball Explosion");
        }
        public override void SetDefaults()
        {
            projectile.width = 80;
            projectile.height = 105;
            projectile.friendly = true;
            projectile.hostile = false; // For some really odd reason, this does not do damage to hostile NPCs, fix soon
            projectile.melee = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 20;
            projectile.ignoreWater = true;
            projectile.damage = 1;
        }
    }
}
