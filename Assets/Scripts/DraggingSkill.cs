using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TowerDefend
{
    class DraggingSkill : MonoBehaviour
    {
        private Camera mainCamera;
        public bool isDragging = false;
        private GameObject newSkill;
        private int manaCost;
        private float storeTimeScale;
        public SkillTypes skillTypes;

        #region Monobehavior
        // Use this for initialization
        void Start()
        {
            mainCamera = Camera.main;
        }

        void OnDisable()
        {
            isDragging = false;
            if (newSkill != null) Destroy(newSkill);
        }

        public void MouseDrag()
        {
            if (isDragging&&newSkill!=null)
                newSkill.transform.position = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        public void MouseUp()
        {
            Time.timeScale = storeTimeScale;
            if (GameController.instance.waveManager.Mana < manaCost)
            {
                GameController.instance.buttonManager.AlertDisplay("Not Enought Mana");
                Destroy(newSkill);
            }
            else
            {
                GameController.instance.waveManager.Mana -= manaCost;
                newSkill.GetComponent<Skill>().DoSkill();
            }
            GetComponentInParent<Animator>().Play("SkillBookClose");
            newSkill = null;
            isDragging = false;
        }

        public void MouseDown()
        {
            storeTimeScale = Time.timeScale;
            Time.timeScale = 0;
            if (!isDragging)
            {
                Vector2 location = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                
                switch (skillTypes)
                {
                    case SkillTypes.Time:
                        newSkill = Instantiate(Resources.Load("Time"),Vector3.zero,Quaternion.identity) as GameObject;
                        manaCost = Constants.TIME_COST;
                        break;
                    case SkillTypes.Frozen:
                        newSkill = Instantiate(Resources.Load("Frozen"), location, Quaternion.identity) as GameObject;
                        manaCost = Constants.FROZEN_COST;
                        break;
                    case SkillTypes.Mine:
                        newSkill = Instantiate(Resources.Load("Mine"), location, Quaternion.identity) as GameObject;
                        manaCost = Constants.MINE_COST;
                        break;
                    case SkillTypes.Meteor:
                        newSkill = Instantiate(Resources.Load("Meteor"), location, Quaternion.identity) as GameObject;
                        manaCost = Constants.METEOR_COST;
                        break;
                }
                isDragging = true;
            }
        }
        #endregion
    }
}
