using UnityEngine;
using System.Collections;
namespace TowerDefend
{
    public class EnemyDestroyDelegate : MonoBehaviour
    {
        public delegate void EnemyDelegate(GameObject enemy);
        public EnemyDelegate enemyDelegate;

        public void DisableGO()
        {
            if (enemyDelegate != null)
            {
                enemyDelegate(gameObject);
            }
        }
    }
}