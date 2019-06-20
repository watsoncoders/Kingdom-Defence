using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefend
{
    class Block:Tower
    {
        void OnEnable() {
            base.OnEnable();
            towerType = TowerTypes.Block;
            cost = 3;
        }
    }
}
