using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace TowerDefend
{
    class DarkWizard:Enemy
    {
        public override float Armor
        {
            get
            {
                return base.Armor;
            }
            set
            {
                GetComponentInChildren<Animator>().Play("Immune");
                //base.Armor = value;
            }
        }

        public override float SpeedMultiply
        {
            get
            {
                return base.SpeedMultiply;
            }
            set
            {
                GetComponentInChildren<Animator>().Play("Immune");
                //base.SpeedMultiply = value;
            }
        }

        public override float StunnedTime
        {
            get
            {
                return base.StunnedTime;
            }
            set
            {
                GetComponentInChildren<Animator>().Play("Immune");
            }
        }

        void OnEnable()
        {
            this.speed = Constants.SPEED_DARKWIZARD;
            this.health = Constants.HEATH_DARKWIZARD;
            this.armor = Constants.ARMOR_DARKWIZARD;
            reward = Constants.REWARD_DARKWIZARD;
            base.OnEnable();
        }
    }
}
