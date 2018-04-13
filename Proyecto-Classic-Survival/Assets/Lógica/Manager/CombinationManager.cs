using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CombinationManager : MonoBehaviour 
{
	public Dictionary<string, CombinationItem> CombinationDictionary;
	public CombinationItem ActualCombinationItem;

	public void OpenDictionary()
	{
		CombinationDictionary = new Dictionary<string, CombinationItem> ();
		ActualCombinationItem = new CombinationItem ();

		TextAsset jsonOnTextAsset = LoaderManager.Singleton.CargarTextAsset ("DataBase/combinationData");
		string jsonOnString = jsonOnTextAsset.ToString ();
		CombinationData loadedData = JsonUtility.FromJson<CombinationData> (jsonOnString);

		for (int i = 0; i < loadedData.container.Length; i++) 
		{
			ActualCombinationItem = new CombinationItem ();
			ActualCombinationItem.needKey = loadedData.container [i].needKey;
			ActualCombinationItem.needAmount = loadedData.container [i].needAmount;
			ActualCombinationItem.itemPath = loadedData.container [i].itemPath;
			ActualCombinationItem.amount = loadedData.container [i].amount;
			CombinationDictionary.Add (loadedData.container [i].combKey, ActualCombinationItem);
			ActualCombinationItem = new CombinationItem ();
		}

		Debug.Log ("Diccionario de Combinaciones Abierto!");
	}

	public void CloseDictionary()
	{
		CombinationDictionary = new Dictionary<string, CombinationItem> ();
		ActualCombinationItem = new CombinationItem ();
		Debug.Log ("Diccionario de Combinaciones Cerrado!");
	}

	public string GetCombinationItem (string key)
	{
		string result = "";
		if (CombinationDictionary.ContainsKey (key)) 
		{
			ActualCombinationItem = CombinationDictionary [key];
			result = ActualCombinationItem.needKey;
		}
		return result;
	}
}
