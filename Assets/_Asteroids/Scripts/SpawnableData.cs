using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids {
	[CreateAssetMenu(fileName = "New Spawn Data", menuName = "Asteroids/Create Spawnable Data")]
	public class SpawnableData : ScriptableObject {
		[SerializeField] private int id;
		[SerializeField] private SpawnableType type;
		[SerializeField] private GameObject prefab;
		[SerializeField] private int initialPoolCount;

		public int ID => id;
		public SpawnableType Type => type;
		public GameObject Prefab => prefab;
		public int InitialPoolCount => initialPoolCount;
	}

}