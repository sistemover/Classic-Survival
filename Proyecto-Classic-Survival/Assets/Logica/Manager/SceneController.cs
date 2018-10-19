using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour 
{
	public string startingSceneName;

	public void InitiateScene()
	{
		StartCoroutine (LoadSceneAndSetActive(startingSceneName));
	}
	public void QuitScene()
	{
		StartCoroutine(RemoveSceneAndDeactive ());
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

	private IEnumerator RemoveSceneAndDeactive()
	{
		yield return SceneManager.UnloadSceneAsync (SceneManager.GetActiveScene().buildIndex);
	}
}
