using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefend
{
    public class Runner:Enemy
    {
        void OnEnable() {
            this.speed = Constants.SPEED_RUNNER;
            this.health = Constants.HEATH_RUNNER;
            this.armor = Constants.ARMOR_RUNNER;
            reward = Constants.REWARD_RUNNER;
            base.OnEnable();
        }
    }
}
