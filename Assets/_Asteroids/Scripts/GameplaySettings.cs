using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame {
	[CreateAssetMenu(fileName = "New Gameplay Settings", menuName = "Asteroids/Create Gameplay Settings")]
	public class GameplaySettings : ScriptableObject {
		[SerializeField] private int playerHealth = 3;
		[SerializeField] private float playerSpeed = 500;
		[SerializeField] private float playerRotationSpeed = 250;
		[SerializeField, Range(0.25f, 1f)] private float playerDefaultFireRate = 0.25f;
		[SerializeField, Range(30, 60)] private float playerBurstAngle = 30f;
		[Space(25)]
		[SerializeField, Range(3, 6)] private int asteroidDefaultLife = 4;
		[SerializeField, Range(2.5f, 5f)] private float asteroidDestroyForce = 2.5f;
		[Space(25)]
		[SerializeField, Range(1f, 5f)] private float asteroidDefaultSpawnRate = 2.5f;
		[SerializeField, Range(0.5f, 1f)] private float asteroidMinimumSpawnRate = 1f;
		[SerializeField, Range(1, 100)] private float asteroidSpawnRateDecayRate = 50;
		[SerializeField, Range(5, 15)] private int asteroidMinimumSpawnForce;
		[SerializeField, Range(5, 15)] private int asteroidMaximumSpawnForce;
		[Space(25)]
		[SerializeField] private float powerUpDefaultSpawnRate = 5f;

		public int PlayerHealth { get => playerHealth; }
		public float PlayerSpeed { get => playerSpeed; }
		public float PlayerRotationSpeed { get => playerRotationSpeed; }
		public float PlayerDefaultFireRate { get => playerDefaultFireRate; }
		public float PlayerBurstAngle { get => playerBurstAngle; }

		public int AsteroidDefaultLife { get => asteroidDefaultLife; }
		public float AsteroidDestroyForce { get => asteroidDestroyForce; }

		public float AsteroidDefaultSpawnRate { get => asteroidDefaultSpawnRate; }
		public float AsteroidMinimumSpawnRate { get => asteroidMinimumSpawnRate; }
		public float AsteroidSpawnRateDecayRate { get => asteroidSpawnRateDecayRate; }
		public int AsteroidMinimumSpawnForce { get { return asteroidMinimumSpawnForce > asteroidMaximumSpawnForce ? asteroidMaximumSpawnForce : asteroidMinimumSpawnForce; } }
		public int AsteroidMaximumSpawnForce { get { return asteroidMinimumSpawnForce > asteroidMaximumSpawnForce ? asteroidMinimumSpawnForce : asteroidMaximumSpawnForce; } } 

		public float PowerUpDefaultSpawnRate { get => powerUpDefaultSpawnRate; }
	}
}