using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefend{
    class DarkWarrior:Enemy
    {
        void OnEnable()
        {
            this.speed = Constants.SPEED_DARKWARRIOR;
            this.health = Constants.HEATH_DARKWARRIOR;
            this.armor = Constants.ARMOR_DARKWARRIOR;
            reward = Constants.REWARD_DARKWARRIOR;
            base.OnEnable();

        }
    }
}
