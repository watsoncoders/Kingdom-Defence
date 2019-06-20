using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefend
{
    class Yvern:Enemy
    {
        void OnEnable()
        {
            this.speed = Constants.SPEED_YVERN;
            this.health = Constants.HEATH_YVERN;
            this.armor = Constants.ARMOR_YVERN;
            reward = Constants.REWARD_YVERN;
            base.OnEnable();
        }
    }
}
