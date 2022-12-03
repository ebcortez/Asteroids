using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame {
	[CreateAssetMenu(fileName = "New Pool Object Data", menuName = "Asteroids/Create Spawnable Data")]
	public class PoolObjectData : ScriptableObject {
		[SerializeField] private int id;
		[SerializeField] private PooledType type;
		[SerializeField] private GameObject prefab;
		[SerializeField] private int initialPoolCount;

		public int ID => id;
		public PooledType Type => type;
		public GameObject Prefab => prefab;
		public int InitialPoolCount => initialPoolCount;
	}

}