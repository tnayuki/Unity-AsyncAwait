using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineDispatcher : MonoBehaviour {
	public static CoroutineDispatcher Get() {
		CoroutineDispatcher other = FindObjectOfType<CoroutineDispatcher>();
		
		if (other) {
			return other;
		}

		GameObject gameObject = new GameObject();
		DontDestroyOnLoad(gameObject);

		return gameObject.AddComponent<CoroutineDispatcher>();
	}

	public void DispatchCoroutine(IEnumerator coroutine) {
		StartCoroutine(coroutine);
	}	
}
