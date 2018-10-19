using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour 
{
	public string startingSceneName;

	public void Init()
	{
		StartCoroutine (LoadSceneAndSetActive(startingSceneName));
	}

	private IEnumerable On()
	{
		yield return StartCoroutine (LoadSceneAndSetActive(startingSceneName));
	}

	private IEnumerator LoadSceneAndSetActive(string sceneName)
	{
		yield return SceneManager.LoadSceneAsync (sceneName, LoadSceneMode.Additive);
		Scene newlyLoadedScene = SceneManager.GetSceneAt (SceneManager.sceneCount - 1);
		SceneManager.SetActiveScene (newlyLoadedScene);
	}
}
