using UnityEngine;
using System.Collections;
namespace TowerDefend
{
    public class Butcher : Enemy
    {
        void OnEnable()
        {
            this.speed = Constants.SPEED_BUTCHER;
            this.health = Constants.HEATH_BUTCHER;
            this.armor = Constants.ARMOR_BUTCHER;
            reward = Constants.REWARD_BUTCHER;
            base.OnEnable();
        }
    }
}