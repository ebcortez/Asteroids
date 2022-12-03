using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame {
	public class PoolManager : MonoBehaviour {
		private static PoolManager instance;
		public static PoolManager Instance => instance;

		[SerializeField] private List<PoolObjectData> spawnableDatas = new List<PoolObjectData>();
		private Transform poolManagerTransform;
		
		private Dictionary<int, Queue<GameObject>> poolDictionary = new Dictionary<int, Queue<GameObject>>();
		private Dictionary<PooledType, List<GameObject>> poolByTypeDictionary = new Dictionary<PooledType, List<GameObject>>();

		private void Awake() { 
			instance = this;

			poolManagerTransform = transform;
		}

		private void CreatePool(int id) {
			var spawnableData = spawnableDatas.Find(spawnableData => spawnableData.ID == id);
			if(spawnableData == null) Debug.LogException(new System.Exception("NO SPAWN DATA FOR THIS ID!"));
			var spawnablePrefab = Instantiate(spawnableData.Prefab, poolManagerTransform);
			spawnablePrefab.SetActive(false);

			Queue<GameObject> pool = new Queue<GameObject>();

			for (int i = 0; i < spawnableData.InitialPoolCount; i++) {
				pool.Enqueue(Instantiate(spawnablePrefab, poolManagerTransform));
			}
			poolDictionary.Add(spawnableData.ID, pool);
		}

		public GameObject GetObjectFromPool(int id) {
			if (!poolDictionary.ContainsKey(id)) {
				CreatePool(id);
			}

			var pooledObject = poolDictionary[id].Dequeue();
			if(pooledObject.activeInHierarchy) pooledObject.SetActive(false);
			pooledObject.SetActive(true);
			poolDictionary[id].Enqueue(pooledObject);

			return pooledObject;
		}

		private void CreatePool(PooledType pooledType) {
			var allSpawnableDataByType = spawnableDatas.FindAll(spawnableData => spawnableData.Type == pooledType);
			allSpawnableDataByType.ForEach(spawnableData => {
				if (spawnableData == null) Debug.LogException(new System.Exception("NO SPAWN DATA FOR THIS ID!"));
				var spawnablePrefab = Instantiate(spawnableData.Prefab, poolManagerTransform);
				spawnablePrefab.SetActive(false);

				List<GameObject> pool = new List<GameObject>();

				for (int i = 0; i < spawnableData.InitialPoolCount; i++) {
					pool.Add(Instantiate(spawnablePrefab, poolManagerTransform));
				}
				if(!poolByTypeDictionary.ContainsKey(pooledType)) poolByTypeDictionary.Add(spawnableData.Type, pool);
				else poolByTypeDictionary[pooledType].AddRange(pool);
			});
		}

		public GameObject GetObjectFromPool(PooledType poolType) {
			if (!poolByTypeDictionary.ContainsKey(poolType)) {
				CreatePool(poolType);
			}

			var pooledObject = poolByTypeDictionary[poolType][Random.Range(0, poolByTypeDictionary[poolType].Count)];
			do {
				pooledObject = poolByTypeDictionary[poolType][Random.Range(0, poolByTypeDictionary[poolType].Count)];
			} 
			while(pooledObject.activeInHierarchy);
			pooledObject.SetActive(true);

			return pooledObject;
		}
	} 
}
