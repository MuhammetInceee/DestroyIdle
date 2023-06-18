using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Manager
{
    public class PoolingManager : Singleton<PoolingManager>
    {
        [SerializeField] private GameObject orePrefab;
        [SerializeField] private GameObject ingotPrefab;

        [SerializeField] private int poolSize;

        [SerializeField] private List<GameObject> oreList;
        [SerializeField] private List<GameObject> ingotList;

        private void Awake() => CreatePool();

        private void CreatePool()
        {
            CreateObjects(poolSize, orePrefab, transform.GetChild(0), oreList);
            CreateObjects(poolSize, ingotPrefab, transform.GetChild(1), ingotList);
        }

        private void CreateObjects(int count, GameObject prefab, Transform parent, ICollection<GameObject> objectList)
        {
            for (var i = 0; i < count; i++)
            {
                var obj = Instantiate(prefab, transform.position, Quaternion.identity, parent);
                obj.SetActive(false);
                objectList.Add(obj);
            }
        }

        internal GameObject GetAvailableOre() => GetAvailableObject(oreList);
        internal GameObject GetAvailableIngot() => GetAvailableObject(ingotList);
        internal void ReturnPoolIngot(GameObject obj) => ReturnObjectToPool(obj, transform.GetChild(1));
        internal void ReturnPoolOre(GameObject obj) => ReturnObjectToPool(obj, transform.GetChild(0));

        private GameObject GetAvailableObject(IEnumerable<GameObject> objectList) =>
            objectList.FirstOrDefault(obj => !obj.activeSelf);

        private void ReturnObjectToPool(GameObject obj, Transform parent)
        {
            obj.SetActive(false);
            obj.transform.SetParent(parent);
            obj.transform.position = Vector3.zero;
        }

    }
}
