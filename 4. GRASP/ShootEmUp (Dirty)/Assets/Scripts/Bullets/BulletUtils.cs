using ShootEmUp.Modules.Components;
using UnityEngine;

namespace ShootEmUp
{
    internal static class BulletUtils
    {
        internal static void TryApplyDamage(Bullet bullet, GameObject other)
        {
            if (!other.TryGetComponent(out TeamComponent team))
                return;

            if(IsFriendlyFire(bullet.GetPhysicsLayer(), team.IsPlayer))
                return;
            
            if (other.TryGetComponent(out HitPointsComponent hitPoints))
                hitPoints.ApplyDamage(bullet.Damage);
        }

        private static bool IsFriendlyFire(int bulletLayerInt, bool isPlayerTeam)
        {
            if (bulletLayerInt == (int)PhysicsLayer.ENEMY_BULLET && !isPlayerTeam)
                return true;
            else if (bulletLayerInt == (int)PhysicsLayer.PLAYER_BULLET && isPlayerTeam)
                return true;
            else
                 return false;
        }
    }
}