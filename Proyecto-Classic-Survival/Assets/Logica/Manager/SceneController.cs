using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour 
{
	public string startingLevelName;
	public string startingPositionName;

	public CanvasGroup faderCanvasGroup;
	public float fadeDuration = 1f;
	public bool isFading;


	public IEnumerator On()
	{
		Debug.Log ("Inicia SceneController");
		//Verificar si hay SavedPlayerSpawn.
		if (Persistant.Data.SavedPlayerSpawn != null) 
		{
			startingLevelName = Persistant.Data.SavedPlayerSpawn.levelName;
			startingPositionName = "p0";
		}

		//Inicia Corutina de todo.
		yield return StartCoroutine (Fade(1f));
		yield return StartCoroutine (LoadSceneAndSetActive(startingLevelName));
	}
	public IEnumerator Quit()
	{
		yield return StartCoroutine (Fade(1f));
		yield return StartCoroutine (RemoveSceneAndDeactive ());
	}
	public IEnumerator Fade(float finalAlpha)
	{
		isFading = true;
		faderCanvasGroup.blocksRaycasts = true;
		float fadeSpeed = Mathf.Abs (faderCanvasGroup.alpha - finalAlpha) / fadeDuration;
		while (!Mathf.Approximately(faderCanvasGroup.alpha, finalAlpha)) 
		{
			faderCanvasGroup.alpha = Mathf.MoveTowards (faderCanvasGroup.alpha, finalAlpha, fadeSpeed * Time.deltaTime);
			yield return null;
		}
		isFading = false;
		faderCanvasGroup.blocksRaycasts = false;
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