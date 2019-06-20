using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefend
{
    class TyrantDragon:HellGuarder
    {
        void OnEnable()
        {
            this.speed = Constants.SPEED_TYRANTDRAGON;
            this.health = Constants.HEATH_TYRANTDRAGON;
            this.armor = Constants.ARMOR_TYRANTDRAGON;
            reward = Constants.REWARD_TYRANTDRAGON;
            base.OnEnable();
        }
    }
}
