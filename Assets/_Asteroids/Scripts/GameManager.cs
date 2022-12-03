using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AsteroidsGame {
	public class GameManager : MonoBehaviour {
		private static GameManager instance;
		public static GameManager Instance => instance;

		public Camera MainCamera { get; private set; }
		public int Score { get; private set; }

		public UnityEvent scoreChanged;

		private void Awake() {
			instance = this;

			MainCamera = Camera.main;
		}

		private void Start() {
			AddScore(0);
		}

		public void AddScore(int scoreToAdd) {
			Score += scoreToAdd;
			scoreChanged?.Invoke();
		}
	}
}