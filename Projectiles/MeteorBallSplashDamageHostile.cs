using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MetEx.Projectiles
{
    public class MeteorBallSplashDamageHostile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Meteor Ball Explosion");
        }
        public override void SetDefaults()
        {
            projectile.width = 80;
            projectile.height = 105;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.melee = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 20;
            projectile.ignoreWater = true;
            projectile.damage = 1;
        }
    }
}
