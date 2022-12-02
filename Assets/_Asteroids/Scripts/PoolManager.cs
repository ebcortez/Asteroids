using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids {
	public class PoolManager : MonoBehaviour {
		private static PoolManager instance;
		public static PoolManager Instance => instance;

		[SerializeField] private List<SpawnableData> spawnableDatas = new List<SpawnableData>();
		private Transform poolManagerTransform;
		
		private Dictionary<SpawnableType, Queue<GameObject>> poolDictionary = new Dictionary<SpawnableType, Queue<GameObject>>();

		private void Awake() { 
			instance = this;

			poolManagerTransform = transform;
		}

		private void CreatePool(SpawnableType type) {
			var spawnableData = spawnableDatas.Find(spawnableData => spawnableData.Type == type);
			if(spawnableData == null) Debug.LogException(new System.Exception("NO SPAWN DATA FOR THIS ID!"));
			var spawnablePrefab = Instantiate(spawnableData.Prefab, poolManagerTransform);
			spawnablePrefab.SetActive(false);

			Queue<GameObject> pool = new Queue<GameObject>();

			for (int i = 0; i < spawnableData.InitialPoolCount; i++) {
				pool.Enqueue(Instantiate(spawnablePrefab, poolManagerTransform));
			}
			poolDictionary.Add(spawnableData.Type, pool);
		}

		public GameObject GetObjectFromPool(SpawnableType type) {
			if (!poolDictionary.ContainsKey(type)) {
				CreatePool(type);
			}

			var pooledObject = poolDictionary[type].Dequeue();
			if(pooledObject.activeInHierarchy) pooledObject.SetActive(false);
			pooledObject.SetActive(true);
			poolDictionary[type].Enqueue(pooledObject);

			return pooledObject;
		}
	} 
}
