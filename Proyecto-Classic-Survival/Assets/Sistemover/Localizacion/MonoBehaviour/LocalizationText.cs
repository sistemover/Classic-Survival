using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationText : MonoBehaviour 
{
	public string key;
	private LocalizationManager localizationManager;

	void Awake()
	{
		Debug.Log ("LocalizationText");
		localizationManager = GameManager.instance.localizationManager;
		localizationManager.onLocalizationChangedCallback += CargarTexto;
	}

	void CargarTexto()
	{
		Text texto = GetComponent<Text> ();
		texto.text = localizationManager.GetLocalizedText (key);
	}
}
