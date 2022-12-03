using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame {
	public class GameManager : MonoBehaviour {
		private static GameManager instance;
		public static GameManager Instance => instance;

		public Camera MainCamera { get; private set; }
		public int Score { get; private set; }

		private void Awake() {
			instance = this;

			MainCamera = Camera.main;
		}

		public void AddScore(int scoreToAdd) {
			Score += scoreToAdd;
		}
	}
}