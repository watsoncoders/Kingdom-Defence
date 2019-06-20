using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using UnityEngine;

namespace TowerDefend
{
    class Wyvern:Enemy
    {
        private bool isHealing;
        void OnEnable()
        {
            this.speed = Constants.SPEED_WYVERN;
            this.health = Constants.HEATH_WYVERN;
            this.armor = Constants.ARMOR_WYVERN;
            reward = Constants.REWARD_WYVERN;
            base.OnEnable();

        }
    }
}
