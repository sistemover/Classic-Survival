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
		Debug.Log ("Awake LoaderManager");
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
        /*
		for (int i = 0; i < Persistant.Data.SavedCondiciones.Length; i++) {
			Debug.Log (Persistant.Data.SavedCondiciones[i].condicion + " " + Persistant.Data.SavedCondiciones[i].status);
		}*/

	}
	public bool Cargar()
	{
		bool resultado = false;
		if (File.Exists (Application.persistentDataPath + "/PersistantData.txt")) 
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/PersistantData.txt", FileMode.Open);
			Persistant.Data = (Persistant)bf.Deserialize (file);
			file.Close ();
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
	public void SavePlayerPosition()
	{	
		GameManager gameManager = GameManager.instance;
		Persistant.Data.SavedPlayerSpawn = new SavedPlayerSpawn ();
		Persistant.Data.SavedPlayerSpawn.activeCamera = gameManager.ActualCameraManager.ActiveCamera;
		Persistant.Data.SavedPlayerSpawn.levelName = gameManager.ActualLevelManager.LevelName;
		Persistant.Data.SavedPlayerSpawn.posX = gameManager.LocalPlayer.transform.position.x;
		Persistant.Data.SavedPlayerSpawn.posY = gameManager.LocalPlayer.transform.position.y;
		Persistant.Data.SavedPlayerSpawn.posZ = gameManager.LocalPlayer.transform.position.z;
		Persistant.Data.SavedPlayerSpawn.rotX = gameManager.LocalPlayer.transform.rotation.eulerAngles.x;
		Persistant.Data.SavedPlayerSpawn.rotY = gameManager.LocalPlayer.transform.rotation.eulerAngles.y;
		Persistant.Data.SavedPlayerSpawn.rotZ = gameManager.LocalPlayer.transform.rotation.eulerAngles.z;
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
	public GameObject CargarGameObject(string path)
	{
		return Resources.Load (path) as GameObject;
	}
}
