using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefend
{
    class Shadow:Enemy
    {
        void OnEnable()
        {
            this.speed = Constants.SPEED_SHADOW;
            this.health = Constants.HEATH_SHADOW;
            this.armor = Constants.ARMOR_SHADOW;
            reward = Constants.REWARD_SHADOW;
            base.OnEnable();
        }
    }
}
