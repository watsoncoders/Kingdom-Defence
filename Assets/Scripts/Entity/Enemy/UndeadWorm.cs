using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefend
{
    class UndeadWorm:Enemy
    {
        void OnEnable()
        {
            this.speed = Constants.SPEED_UNDEADWORM;
            this.health = Constants.HEATH_UNDEADWORM;
            this.armor = Constants.ARMOR_UNDEADWORM;
            reward = Constants.REWARD_UNDEADWORM;
            base.OnEnable();
        }
    }
}
