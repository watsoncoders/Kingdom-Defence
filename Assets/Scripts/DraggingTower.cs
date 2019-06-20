using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Collections;
using Assets.Scripts;
namespace TowerDefend
{
    public class DraggingTower : MonoBehaviour
    {
        private bool canBuild;
        private Camera mainCamera;
        bool isDragging = false;
        private float storeTimeScale;
        private GameObject newTower;
        public TowerTypes towerTypes;

        #region Monobehavior
        // Use this for initialization
        void Start()
        {
            mainCamera = Camera.main;
        }

        void OnEnable()
        {
            BuildStateManager();
            GameController.instance.waveManager.Subscribe(BuildStateManager);
        }

        void OnDisale()
        {
            GameController.instance.waveManager.UnSubscribe(BuildStateManager);
        }

        public void OnMouseDrag()
        {
            if (!canBuild) return;
            if (isDragging)
            {
                newTower.transform.position = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
                //check placable
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, Mathf.Infinity);
                Transform target;
                if (hits.Where(x => x.collider.gameObject.tag == Strings.PLACE_HOLDER).Count() > 0)
                    target = hits.Where(x => x.collider.gameObject.tag == Strings.PLACE_HOLDER)
                            .First().collider.transform;
                else
                {
                    target = null;
                    //return;
                }
                if (hits.Length > 0)
                {
                    if (hits.Where(x => x.collider.gameObject.tag == Strings.PLACE_HOLDER).Count() > 0 &&
                        ((!GameController.instance.waveManager.AStarMap) || CheckBlockingPath(target)) &&
                        GameController.instance.waveManager.CanBuildTower())
                    {
                        //Debug.Log(target.position);
                        if (towerTypes != TowerTypes.Block)
                            newTower.transform.GetChild(0).GetComponent<TowerSpriteManager>().AOEColor(true);
                    }
                    else
                    {
                        if (towerTypes != TowerTypes.Block)
                            newTower.transform.GetChild(0).GetComponent<TowerSpriteManager>().AOEColor(false);
                    }
                }
            }
        }

        public void OnMouseUp()
        {
            if (!canBuild) return;
            Time.timeScale = storeTimeScale;
            PlaceHolderDisplay(false);
            newTower.GetComponentInChildren<TowerSpriteManager>().AOEManager(false);
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, Mathf.Infinity);
            if (hits.Length > 0)
            {
                Transform target;
                if (hits.Where(x => x.collider.gameObject.tag == Strings.PLACE_HOLDER).Count() > 0)
                    target = hits.Where(x => x.collider.gameObject.tag == Strings.PLACE_HOLDER)
                            .First().collider.transform;
                else
                {
                    isDragging = false;
                    newTower.SetActive(false);
                    target = null;
                    return;
                }
                if (hits.Where(x => x.collider.gameObject.tag == Strings.PLACE_HOLDER).Count() > 0 &&
                    ((!GameController.instance.waveManager.AStarMap) || CheckBlockingPath(target)) &&
                    GameController.instance.waveManager.CanBuildTower())
                {
                    //Debug.Log(CheckBlockingPath(target));
                    newTower.transform.position = (Vector2)target.position;
                    if (GameController.instance.waveManager.AStarMap && !CheckBlockingPath(newTower.transform.GetChild(2)))
                    {
                        isDragging = false;
                        newTower.SetActive(false);
                        return;
                    }
                    
                    newTower.transform.position = new Vector3(newTower.transform.position.x, newTower.transform.position.y, newTower.transform.position.y);

                    AudioManager.Instance.PlaySound(Constants.TOWER_PLACED);
                    newTower.GetComponent<Tower>().enabled = true;
                    newTower.GetComponent<Tower>().PlaceHolder = hits.Where(x => x.collider.gameObject.tag == Strings.PLACE_HOLDER)
                        .First().collider.gameObject;
                    newTower.GetComponent<Tower>().Buy();
                    if (GameController.instance.waveManager.AStarMap)
                        AstarPath.active.Scan();
                }
                else
                {
                    newTower.SetActive(false);
                }
            }
            else
                newTower.SetActive(false);
            isDragging = false;
        }
        public void OnMouseDown()
        {
            if (!canBuild) return;
            storeTimeScale = Time.timeScale;
            Time.timeScale = 0;
            //if we have money and we can drag a new towerr
            if (!isDragging)
            {
                PlaceHolderDisplay(true);
                Vector2 location = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                isDragging = true;
                switch (towerTypes)
                {
                    case TowerTypes.Block:
                        newTower = ObjectPoolerManager.Instance.blockPooler.GetPooledObject();
                        newTower.transform.position = transform.position;
                        newTower.SetActive(true);
                        newTower.GetComponent<Tower>().enabled = true;
                        newTower.GetComponent<Tower>().enabled = false;
                        //newTower.GetComponent<Tower>().SwitchAOE();
                        break;
                    case TowerTypes.Normal:
                        newTower = ObjectPoolerManager.Instance.normalTowerPooler.GetPooledObject();
                        newTower.transform.position = transform.position;
                        newTower.SetActive(true);
                        newTower.GetComponent<Tower>().enabled = true;
                        newTower.GetComponent<Tower>().enabled = false;
                        //newTower.GetComponent<Tower>().SwitchAOE();
                        break;
                    case TowerTypes.Canon:
                        newTower = ObjectPoolerManager.Instance.canonTowerPooler.GetPooledObject();
                        newTower.transform.position = transform.position;
                        newTower.GetComponent<Tower>().enabled = false;
                        newTower.SetActive(true);
                        //newTower.GetComponent<Tower>().SwitchAOE();
                        break;
                    case TowerTypes.LongRange:
                        newTower = ObjectPoolerManager.Instance.longRangeTowerPooler.GetPooledObject();
                        newTower.transform.position = transform.position;
                        newTower.SetActive(true);
                        newTower.GetComponent<Tower>().enabled = false;
                        //newTower.GetComponent<Tower>().SwitchAOE();
                        break;
                    case TowerTypes.Magic:
                        newTower = ObjectPoolerManager.Instance.magicTowerPooler.GetPooledObject();
                        newTower.transform.position = transform.position;
                        newTower.SetActive(true);
                        newTower.GetComponent<Tower>().enabled = false;
                        //newTower.GetComponent<Tower>().SwitchAOE();
                        break;
                    case TowerTypes.Soul:
                        newTower = ObjectPoolerManager.Instance.soulTowerPooler.GetPooledObject();
                        newTower.transform.position = transform.position;
                        newTower.GetComponent<SoulTower>().enabled = false;
                        newTower.SetActive(true);
                        //newTower.GetComponent<SoulTower>().SwitchAOE();
                        break;
                }
                newTower.GetComponentInChildren<TowerSpriteManager>().AOEManager(true);
            }
        }
        #endregion

        private void BuildStateManager()
        {
            switch (towerTypes)
            {
                case TowerTypes.Block:
                    canBuild = GameController.instance.waveManager.Block != 0 ? true : false;
                    break;
                case TowerTypes.Normal:
                    canBuild = (GameController.instance.waveManager.NormalTower != 0&&GameController.instance.waveManager.NormalTowerCondition[0]) ? true : false;
                    break;
                case TowerTypes.Canon:
                    canBuild = (GameController.instance.waveManager.CanonTower != 0&&GameController.instance.waveManager.CanonTowerCondition[0]) ? true : false;
                    break;
                case TowerTypes.LongRange:
                    canBuild = (GameController.instance.waveManager.LongRangTower != 0&&GameController.instance.waveManager.LongRangeTowerCondition[0]) ? true : false;
                    break;
                case TowerTypes.Magic:
                    canBuild = (GameController.instance.waveManager.MagicTower != 0&&GameController.instance.waveManager.MagicTowerCondition[0]) ? true : false;
                    break;
                case TowerTypes.Soul:
                    canBuild = (GameController.instance.waveManager.SoulTower != 0&&GameController.instance.waveManager.SoulTowerCondition[0]) ? true : false;
                    break;
            }

            if (towerTypes != TowerTypes.Soul)
                SpriteManager();
        }

        private void SpriteManager()
        {
            if(towerTypes==TowerTypes.Block)
            transform.GetChild(1).GetComponent<Text>().text = GameController.instance.waveManager.Block.ToString();
            if (!canBuild) transform.GetChild(0).gameObject.SetActive(true);
            else
                transform.GetChild(0).gameObject.SetActive(false);
        }

        private bool CheckBlockingPath(Transform t)
        {
            bool b = GameController.instance.waveManager.CheckBlockPath(t);
            return b;

        }

        void PlaceHolderDisplay(bool flag)
        {
            GameObject placeHolders = GameObject.Find("PlaceHolders");

            for (int i = 0; i < placeHolders.transform.childCount; i++)
            {
                if (flag)
                {
                    placeHolders.transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                }
                else
                    placeHolders.transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            }

        }
    }
}