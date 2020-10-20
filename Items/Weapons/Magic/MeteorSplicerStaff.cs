using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using MetEx.Projectiles;

namespace MetEx.Items.Weapons.Magic
{
	public class MeteorSplicerStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Meteor Splicer Staff");
			Tooltip.SetDefault("Shoots an explosive mini-meteor\nHas a chance to fire off 2 meteors, if you are lucky");
		}
        public override void SetDefaults()
        {
            item.damage = 30;
            item.magic = true;
            item.width = 32;
            item.height = 32;
            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.value = 10000;
            item.rare = ItemRarityID.Orange;
            item.UseSound = SoundID.Item73;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<MeteorBallFriendly>();
            item.shootSpeed = 12f;
            item.crit = 14;
            item.mana = 8;
            item.reuseDelay = 8;
            Item.staff[item.type] = true;
            item.scale = 3f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
            Vector2 shootFrom = Vector2.Normalize(new Vector2(speedX, speedY)) * 50f;
            if (Collision.CanHit(position, 0, 0, position + shootFrom, 0, 0))
            {
                position += shootFrom;
            }
            int numberProjectiles = 1 + Main.rand.Next(2);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX - (Main.rand.NextFloat() * 0.2f), speedY - (Main.rand.NextFloat() * 0.2f)).RotatedByRandom(MathHelper.ToRadians(5));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<MeteorBallFriendly>(), damage, knockBack, player.whoAmI);
            }
            return false;
		}
	}
}
			
			