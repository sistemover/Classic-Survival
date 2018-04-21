using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class LoaderManager : MonoBehaviour 
{
	string mensage;

	#region Singleton
	public static LoaderManager Singleton;
	void Awake ()
	{
		if (Singleton != null)
			return;
		Singleton = this;
		Persistant.Data = new Persistant ();
	}
	#endregion

	public void Guardar()
	{		
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/PersistantData.txt");
		bf.Serialize (file, Persistant.Data);
		file.Close ();
		mensage = "Datos Guardados Exitosamente: " + Application.persistentDataPath + "/PersistantData.txt";
		Debug.Log (mensage);
	}
	public bool Cargar()
	{
		bool resultado = false;
		if (File.Exists (Application.persistentDataPath + "/PersistantData.txt")) 
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/PersistantData.txt", FileMode.Open);
			Persistant.Data = (Persistant)bf.Deserialize (file); 
			mensage = "Datos Cargados Exitosamente: " + Application.persistentDataPath + "/PersistantData.txt";
			resultado = true;
		} 
		else 
		{
			mensage = "No existe Data";
		}
		Debug.Log (mensage);
		return resultado;
	}
	public Item CargarItem(string path)
	{
		return Resources.Load(path) as Item;
	}
	public Sprite CargarSprite(string path)
	{
		return Resources.Load<Sprite> (path);
	}
	public TextAsset CargarTextAsset(string path)
	{
		return Resources.Load (path) as TextAsset;
	}
}
