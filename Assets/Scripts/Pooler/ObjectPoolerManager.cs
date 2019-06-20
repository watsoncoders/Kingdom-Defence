using UnityEngine;
using System.Collections;
namespace TowerDefend
{
    public class ObjectPoolerManager : MonoBehaviour
    {
        [HideInInspector]
        public ObjectPooler eRunnerPooler, eDarkWarriorPooler, eButcherPooler, eGhostPooler, eHellguarderPooler, eDarkwizardPooler, eNecromancerPooler, eUndeadWormPooler, eShadowPooler, eGlutondragonPooler, eWyvernPooler, eCarrierPooler, eTyrantdragonPooler, eNightmareshipperPooler, eImmortaldevilPooler;
        [HideInInspector]
        public ObjectPooler heathBarPooler, soulUpgradePooler, longRangeUpgradePooler, magicUpgradePooler, canonUpgradePooler;
        [HideInInspector]
        public ObjectPooler normalTowerPooler, canonTowerPooler, longRangeTowerPooler, magicTowerPooler, soulTowerPooler,blockPooler;
        [HideInInspector]
        public ObjectPooler mortarPooler, arrowPooler, magicBallPooler,necromanceReviveEffectPooler,pathFragmentPooler,audioPooler;
        [HideInInspector]
        public ObjectPooler bloodEffectPooler, mortarExplodePooler, soulTowerBuffEffectPooler,hellGuardExplodePooler;
        public GameObject eRunnerPrefab, eDarkWarriorPrefab, eButcherPrefab, eGhostPrefab, eHellguarderPrefab, eDarkwizardPrefab, eNecromancerPrefab, eUndeadWormPrefab, eShadowPrefab, eGlutondragonPrefab, eWyvernPrefab, eCarrierPrefab, eTyrantdragonPrefab, eNightmareshipperPrefab, eImmortaldevilPrefab;
        public GameObject normalTowerPrefab, canonTowerPrefab, longRangeTowerPrefab,blockPrefab;
        public GameObject arrowPrefab, mortarPrefab, soulTowerBuffEffectPrefab,pathFragmentPrefab;
        public GameObject heathBarPrefab, bloodEffectPrefab, mortarExplodePrefab, magicBallPrefab, magicTowerPrefab, soulTowerPrefab, hellGuardExplodePrefab;
        public GameObject soulUpgradePrefab, longRangeUpgradePrefab, magicUpgradePrefab, canonUpgradePrefab, necromancerReviveEffectPrefab;
        public GameObject audioPrefab;
        //basic singleton implementation
        [HideInInspector]
        public static ObjectPoolerManager Instance { get; private set; }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                DestroyImmediate(gameObject);
        }

        void Start()
        {

            if (eRunnerPooler == null)
            {
                GameObject go = new GameObject("eRunnerPooler");
                eRunnerPooler = go.AddComponent<ObjectPooler>();
                eRunnerPooler.PooledObject = eRunnerPrefab;
                go.transform.parent = this.gameObject.transform;
                go.transform.localScale = Vector3.one;
                eRunnerPooler.Initialize();
            }

            if (eDarkWarriorPooler == null)
            {
                GameObject go = new GameObject("eDarkWarriorPooler");
                eDarkWarriorPooler = go.AddComponent<ObjectPooler>();
                eDarkWarriorPooler.PooledObject = eDarkWarriorPrefab;
                go.transform.parent = this.gameObject.transform;
                go.transform.localScale = Vector3.one;
                eDarkWarriorPooler.Initialize();
            }
            if (eButcherPooler == null)
            {
                GameObject go = new GameObject("eButcherPooler");
                eButcherPooler = go.AddComponent<ObjectPooler>();
                eButcherPooler.PooledObject = eButcherPrefab;
                go.transform.parent = this.gameObject.transform;
                go.transform.localScale = Vector3.one;
                eButcherPooler.Initialize();
            }
            if (eGhostPooler == null)
            {
                GameObject go = new GameObject("eGhostPooler");
                eGhostPooler = go.AddComponent<ObjectPooler>();
                eGhostPooler.PooledObject = eGhostPrefab;
                go.transform.parent = this.gameObject.transform;
                go.transform.localScale = Vector3.one;
                eGhostPooler.Initialize();
            }
            if (eHellguarderPooler == null)
            {
                GameObject go = new GameObject("eHellguarderPooler");
                eHellguarderPooler = go.AddComponent<ObjectPooler>();
                eHellguarderPooler.PooledObject = eHellguarderPrefab;
                go.transform.parent = this.gameObject.transform;
                go.transform.localScale = Vector3.one;
                eHellguarderPooler.Initialize();
            }
            if (eDarkwizardPooler == null)
            {
                GameObject go = new GameObject("eDarkwizardPooler");
                eDarkwizardPooler = go.AddComponent<ObjectPooler>();
                eDarkwizardPooler.PooledObject = eDarkwizardPrefab;
                go.transform.parent = this.gameObject.transform;
                go.transform.localScale = Vector3.one;
                eDarkwizardPooler.Initialize();
            }
            if (eNecromancerPooler == null)
            {
                GameObject go = new GameObject("eNecromancerPooler");
                eNecromancerPooler = go.AddComponent<ObjectPooler>();
                eNecromancerPooler.PooledObject = eNecromancerPrefab;
                go.transform.parent = this.gameObject.transform;
                go.transform.localScale = Vector3.one;
                eNecromancerPooler.Initialize();
            }
            if (eUndeadWormPooler == null)
            {
                GameObject go = new GameObject("eUndeadWormPooler");
                eUndeadWormPooler = go.AddComponent<ObjectPooler>();
                eUndeadWormPooler.PooledObject = eUndeadWormPrefab;
                go.transform.parent = this.gameObject.transform;
                go.transform.localScale = Vector3.one;
                eUndeadWormPooler.Initialize();
            }
            if (eShadowPooler == null)
            {
                GameObject go = new GameObject("eShadowPooler");
                eShadowPooler = go.AddComponent<ObjectPooler>();
                eShadowPooler.PooledObject = eShadowPrefab;
                go.transform.parent = this.gameObject.transform;
                go.transform.localScale = Vector3.one;
                eShadowPooler.Initialize();
            }
            if (eGlutondragonPooler == null)
            {
                GameObject go = new GameObject("eGlutondragonPooler");
                eGlutondragonPooler = go.AddComponent<ObjectPooler>();
                eGlutondragonPooler.PooledObject = eGlutondragonPrefab;
                go.transform.parent = this.gameObject.transform;
                go.transform.localScale = Vector3.one;
                eGlutondragonPooler.Initialize();
            }
            if (eWyvernPooler == null)
            {
                GameObject go = new GameObject("eWyvernPooler");
                eWyvernPooler = go.AddComponent<ObjectPooler>();
                eWyvernPooler.PooledObject = eWyvernPrefab;
                go.transform.parent = this.gameObject.transform;
                go.transform.localScale = Vector3.one;
                eWyvernPooler.Initialize();
            }
            if (eCarrierPooler == null)
            {
                GameObject go = new GameObject("eCarrierPooler");
                eCarrierPooler = go.AddComponent<ObjectPooler>();
                eCarrierPooler.PooledObject = eCarrierPrefab;
                go.transform.parent = this.gameObject.transform;
                go.transform.localScale = Vector3.one;
                eCarrierPooler.Initialize();
            }
            if (eTyrantdragonPooler == null)
            {
                GameObject go = new GameObject("eTyrantdragonPooler");
                eTyrantdragonPooler = go.AddComponent<ObjectPooler>();
                eTyrantdragonPooler.PooledObject = eTyrantdragonPrefab;
                go.transform.parent = this.gameObject.transform;
                go.transform.localScale = Vector3.one;
                eTyrantdragonPooler.Initialize();
            }
            if (eNightmareshipperPooler == null)
            {
                GameObject go = new GameObject("eNightmareshipperPooler");
                eNightmareshipperPooler = go.AddComponent<ObjectPooler>();
                eNightmareshipperPooler.PooledObject = eNightmareshipperPrefab;
                go.transform.parent = this.gameObject.transform;
                go.transform.localScale = Vector3.one;
                eNightmareshipperPooler.Initialize();
            }
            if (eImmortaldevilPooler == null)
            {
                GameObject go = new GameObject("eImmortaldevilPooler");
                eImmortaldevilPooler = go.AddComponent<ObjectPooler>();
                eImmortaldevilPooler.PooledObject = eImmortaldevilPrefab;
                go.transform.parent = this.gameObject.transform;
                go.transform.localScale = Vector3.one;
                eImmortaldevilPooler.Initialize();
            }




            if (arrowPooler == null)
            {
                GameObject go = new GameObject("arrowPooler");
                arrowPooler = go.AddComponent<ObjectPooler>();
                arrowPooler.PooledObject = arrowPrefab;
                go.transform.parent = this.gameObject.transform;
                go.transform.localScale = Vector3.one;
                arrowPooler.Initialize();
            }

            if (mortarPooler == null)
            {
                GameObject go = new GameObject("mortarPooler");
                mortarPooler = go.AddComponent<ObjectPooler>();
                mortarPooler.PooledObject = mortarPrefab;
                go.transform.parent = this.gameObject.transform;
                go.transform.localScale = Vector3.one;
                mortarPooler.Initialize();
            }

            if (magicBallPooler == null)
            {
                GameObject go = new GameObject("magicBallPooler");
                magicBallPooler = go.AddComponent<ObjectPooler>();
                magicBallPooler.PooledObject = magicBallPrefab;
                go.transform.parent = this.gameObject.transform;
                go.transform.localScale = Vector3.one;
                magicBallPooler.Initialize();
            }

            if (blockPooler == null)
            {
                GameObject go = new GameObject("blockPooler");
                blockPooler = go.AddComponent<ObjectPooler>();
                blockPooler.PooledObject = blockPrefab;
                go.transform.parent = this.gameObject.transform;
                go.transform.localScale = Vector3.one;
                blockPooler.Initialize();
            }

            if (normalTowerPooler == null)
            {
                GameObject go = new GameObject("normalTowerPooler");
                normalTowerPooler = go.AddComponent<ObjectPooler>();
                normalTowerPooler.PooledObject = normalTowerPrefab;
                go.transform.parent = this.gameObject.transform;
                go.transform.localScale = Vector3.one;
                normalTowerPooler.Initialize();
            }

            if (canonTowerPooler == null)
            {
                GameObject go = new GameObject("canonTowerPooler");
                canonTowerPooler = go.AddComponent<ObjectPooler>();
                canonTowerPooler.PooledObject = canonTowerPrefab;
                go.transform.parent = this.gameObject.transform;
                go.transform.localScale = Vector3.one;
                canonTowerPooler.Initialize();
            }

            if (longRangeTowerPooler == null)
            {
                GameObject go = new GameObject("longRangeTowerPooler");
                longRangeTowerPooler = go.AddComponent<ObjectPooler>();
                longRangeTowerPooler.PooledObject = longRangeTowerPrefab;
                go.transform.parent = this.gameObject.transform;
                go.transform.localScale = Vector3.one;
                longRangeTowerPooler.Initialize();
            }

            if (magicTowerPooler == null)
            {
                GameObject go = new GameObject("magicTowerPooler");
                magicTowerPooler = go.AddComponent<ObjectPooler>();
                magicTowerPooler.PooledObject = magicTowerPrefab;
                go.transform.parent = this.gameObject.transform;
                go.transform.localScale = Vector3.one;
                magicTowerPooler.Initialize();

            }

            if (soulTowerPooler == null)
            {
                GameObject go = new GameObject("soulTowerPooler");
                soulTowerPooler = go.AddComponent<ObjectPooler>();
                soulTowerPooler.PooledObject = soulTowerPrefab;
                go.transform.parent = this.gameObject.transform;
                go.transform.localScale = Vector3.one;
                soulTowerPooler.Initialize();
            }

            if (heathBarPooler == null)
            {
                GameObject go = new GameObject("heathBarPooler");
                GameObject p = GameObject.Find("CanvasHealthBar").gameObject;
                go.transform.SetParent(p.transform);
                go.transform.localScale = Vector3.one;
                heathBarPooler = go.AddComponent<ObjectPooler>();
                heathBarPooler.PooledObject = heathBarPrefab;
                go.transform.localScale = Vector3.one;
                heathBarPooler.Initialize();
            }

            if (bloodEffectPooler == null)
            {
                GameObject go = new GameObject("bloodEffectPooler");
                go.transform.SetParent(this.transform);
                go.transform.localScale = Vector3.one;
                bloodEffectPooler = go.AddComponent<ObjectPooler>();
                bloodEffectPooler.PooledObject = bloodEffectPrefab;
                bloodEffectPooler.Initialize();
            }

            if (soulTowerBuffEffectPooler == null)
            {
                GameObject go = new GameObject("soulTowerBuffEffectPooler");
                go.transform.SetParent(this.transform);
                go.transform.localScale = Vector3.one;
                soulTowerBuffEffectPooler = go.AddComponent<ObjectPooler>();
                soulTowerBuffEffectPooler.PooledObject = soulTowerBuffEffectPrefab;
                soulTowerBuffEffectPooler.Initialize();
            }

            if (soulUpgradePooler == null)
            {
                GameObject go = new GameObject("soulUpgradePooler");
                go.transform.SetParent(this.transform);
                go.transform.localScale = Vector3.one;
                soulUpgradePooler = go.AddComponent<ObjectPooler>();
                soulUpgradePooler.PooledObject = soulUpgradePrefab;
                soulUpgradePooler.Initialize();
            }

            if (longRangeUpgradePooler == null)
            {
                GameObject go = new GameObject("longRangeUpgradePooler");
                go.transform.SetParent(this.transform);
                go.transform.localScale = Vector3.one;
                longRangeUpgradePooler = go.AddComponent<ObjectPooler>();
                longRangeUpgradePooler.PooledObject = longRangeUpgradePrefab;
                longRangeUpgradePooler.Initialize();
            }

            if (magicUpgradePooler == null)
            {
                GameObject go = new GameObject("magicUpgradePooler");
                go.transform.SetParent(this.transform);
                go.transform.localScale = Vector3.one;
                magicUpgradePooler = go.AddComponent<ObjectPooler>();
                magicUpgradePooler.PooledObject = magicUpgradePrefab;
                magicUpgradePooler.Initialize();
            }

            if (canonUpgradePooler == null)
            {
                GameObject go = new GameObject("canonUpgradePooler");
                go.transform.SetParent(this.transform);
                go.transform.localScale = Vector3.one;
                canonUpgradePooler = go.AddComponent<ObjectPooler>();
                canonUpgradePooler.PooledObject = canonUpgradePrefab;
                canonUpgradePooler.Initialize();
            }

            if (necromanceReviveEffectPooler == null)
            {
                GameObject go = new GameObject("necromanceReviveEffectPooler");
                go.transform.SetParent(this.transform);
                go.transform.localScale = Vector3.one;
                necromanceReviveEffectPooler = go.AddComponent<ObjectPooler>();
                necromanceReviveEffectPooler.PooledObject = necromancerReviveEffectPrefab;
                necromanceReviveEffectPooler.Initialize();
            }

            if (hellGuardExplodePooler == null)
            {
                GameObject go = new GameObject("hellGuardExplodePooler");
                go.transform.SetParent(this.transform);
                go.transform.localScale = Vector3.one;
                hellGuardExplodePooler = go.AddComponent<ObjectPooler>();
                hellGuardExplodePooler.PooledObject = hellGuardExplodePrefab;
                hellGuardExplodePooler.Initialize();
            }
            if (mortarExplodePooler == null)
            {
                GameObject go = new GameObject("mortarExplodePooler");
                go.transform.SetParent(this.transform);
                go.transform.localScale = Vector3.one;
                mortarExplodePooler = go.AddComponent<ObjectPooler>();
                mortarExplodePooler.PooledObject = mortarExplodePrefab;
                mortarExplodePooler.Initialize();
            }

            if (pathFragmentPooler == null)
            {
                GameObject go = new GameObject("pathFragmentPooler");
                go.transform.SetParent(this.transform);
                go.transform.localScale = Vector3.one;
                pathFragmentPooler = go.AddComponent<ObjectPooler>();
                pathFragmentPooler.PooledObject = pathFragmentPrefab;
                pathFragmentPooler.Initialize();
            }

            if (audioPooler == null)
            {
                GameObject go = new GameObject("audioPooler");
                go.transform.SetParent(this.transform);
                go.transform.localScale = Vector3.one;
                audioPooler = go.AddComponent<ObjectPooler>();
                audioPooler.PooledObject = audioPrefab;
                audioPooler.Initialize();
                go.GetComponent<ObjectPooler>().alwayEnable = true;
            }
        }
    }
}