using UnityEngine;
using System.Collections;
namespace TowerDefend
{
    public class Carrier : Enemy
    {

        void OnEnable()
        {
            this.speed = Constants.SPEED_CARRIER;
            this.health = Constants.HEATH_CARRIER;
            this.armor = Constants.ARMOR_CARRIER;
            reward = Constants.REWARD_CARRIER;
            base.OnEnable();
            InvokeRepeating("SpawnMinions", 5,Constants.CARIER_SPAWN_MINION_INTERVAL);
        }

        private void SpawnMinions()
        {
            //Debug.Log("Spawn");
            if (isPause) return;
            transform.GetChild(0).GetComponent<Animator>().Play("Spawn");
            GameObject go;
            for (int i = 0; i < 2; i++)
            {
                int j=Random.Range(0, 2);
                switch (j)
                {
                    case 0:
                        go = ObjectPoolerManager.Instance.eWyvernPooler.GetPooledObject();
                        go.GetComponent<Enemy>().Waypoints = this.Waypoints;
                        go.SetActive(true);
                        go.GetComponent<Enemy>().CurrentWaypoint = this.CurrentWaypoint;
                        //go.GetComponent<Enemy>().LastWaypointSwitchTime = Time.time - (Time.time - this.lastWaypointSwitchTime) * Constants.SPEED_CARRIER / Constants.SPEED_WYVERN + this.sumStunnedTime;
                        go.transform.position = transform.position + i * 0.2f * Vector3.left;
                        break;
                    case 1:
                        go = ObjectPoolerManager.Instance.eShadowPooler.GetPooledObject();
                        go.GetComponent<Enemy>().Waypoints = this.Waypoints;
                        go.SetActive(true);
                        go.GetComponent<Enemy>().CurrentWaypoint = this.CurrentWaypoint;
                        //go.GetComponent<Enemy>().LastWaypointSwitchTime = Time.time - (Time.time - this.lastWaypointSwitchTime) * Constants.SPEED_CARRIER / Constants.SPEED_SHADOW + this.sumStunnedTime;
                        go.transform.position = transform.position + i * 0.2f * Vector3.left;
                        break;
                    case 2:
                        go = ObjectPoolerManager.Instance.eGlutondragonPooler.GetPooledObject();
                        go.GetComponent<Enemy>().Waypoints = this.Waypoints;
                        go.SetActive(true);
                        go.GetComponent<Enemy>().CurrentWaypoint = this.CurrentWaypoint;
                        //go.GetComponent<Enemy>().LastWaypointSwitchTime = Time.time - (Time.time - this.lastWaypointSwitchTime) * Constants.SPEED_CARRIER / Constants.SPEED_GLUTONDRAGON + this.sumStunnedTime;
                        go.transform.position = transform.position + i * 0.2f * Vector3.left;
                        break;
                }
            }
        }
    }
}