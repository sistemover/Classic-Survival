using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationManager : MonoBehaviour 
{
	//LLAVES
	public string StartLanguageKey;

	//DICCIONARIOS
	private Dictionary<string, string> localizedText;
	private Dictionary<string, ItemText> localizedItem;
	private Dictionary<string, DialogText> localizedDialog;

	//MENSAJES
	private string missingTextString = "Localized text no encontrado.";

	//DELEGATES
	public delegate void OnlocalizationChanged();
	public OnlocalizationChanged onLocalizationChangedCallback;

	//**************************************************************************************************************

	public void Init()
	{
		SelectLanguage (StartLanguageKey);
	}

	public void LoadLocalizedText(string fileName)
	{
		localizedText = new Dictionary<string, string> ();
		localizedItem = new Dictionary<string, ItemText> ();
		localizedDialog = new Dictionary<string, DialogText> ();

		TextAsset jsonOnTextAsset = LoaderManager.Singleton.CargarTextAsset ("DataBase/"+fileName);
		string jsonOnString = jsonOnTextAsset.ToString ();
		LocalizationData loadedData = JsonUtility.FromJson<LocalizationData> (jsonOnString);

		for (int i = 0; i < loadedData.text.Length; i++)
			localizedText.Add (loadedData.text [i].key, loadedData.text [i].value);
		
		for (int i = 0; i < loadedData.items.Length; i++) 
		{
			ItemText itemText = new ItemText ();
			itemText.name = loadedData.items [i].name;
			itemText.shortdescription = loadedData.items [i].shortdescription;
			itemText.longdescription = loadedData.items [i].longdescription;
			localizedItem.Add (loadedData.items [i].key, itemText);
		}
		for (int i = 0; i < loadedData.dialogs.Length; i++) 
		{
			//Debug.Log (loadedData.dialogs[i].key);
			DialogText dialogText = new DialogText ();
			dialogText.dialog = loadedData.dialogs [i].dialog;
			dialogText.soundclip = loadedData.dialogs [i].soundclip;
			localizedDialog.Add (loadedData.dialogs [i].key, dialogText);
		}
	}

	public string GetLocalizedText(string key)
	{
		string result = missingTextString;
		if(localizedText.ContainsKey(key))
			result = localizedText[key];
		return result;
	}

	public ItemText GetlocalizedItem(string key)
	{
		ItemText result = new ItemText ();
		if(localizedItem.ContainsKey(key))
			result = localizedItem[key];
		return result;
	}

	public DialogText GetlocalizedDialog(string key)
	{
		DialogText result = new DialogText ();
		if(localizedDialog.ContainsKey(key))
			result = localizedDialog[key];
		return result;
	}

	//Validación e invocación del Callback
	public void UpdateText()
	{
		if (onLocalizationChangedCallback != null)
			onLocalizationChangedCallback.Invoke ();
	}

	//Botón para cambio de idioma, invoca el callback onLocalizationChanged
	public void SelectLanguage(string fileName)
	{
		LoadLocalizedText (fileName);
		UpdateText ();
	}
}
