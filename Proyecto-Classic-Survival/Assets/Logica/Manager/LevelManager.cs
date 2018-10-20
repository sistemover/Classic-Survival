﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour 
{
	public int Round = 0;
	public string LevelName;
	public GameObject ObjectContainer;
	public List<GamePlus> GamePlus = new List<GamePlus>();
	public List<PlayerSpawn> SpawnList = new List<PlayerSpawn> ();
	GameManager gameManager;

	#region CRUD Objeto

	public ObjectsData newO;
	public int idToReplace;
	public int idToEliminate;

	public void TapAgregar()
	{
		//Agregar elemento a Array ya declarado
		int length = GamePlus[Round].LevelObjects.Length;
		ObjectsData[] newObjects = new ObjectsData[length + 1];

		for (int i = 0; i < length; i++)
			newObjects [i] = GamePlus [Round].LevelObjects [i];		

		newObjects[newObjects.Length - 1] = newO;
		GamePlus[Round].LevelObjects = newObjects;

		//Instancia nuevo Objeto en mundo físico.
		CargarLevelObjects (GamePlus[Round], length);

		GuardarConfiguración ();
	}

	public void TapReemplazar(int id, string path, int amount)
	{
		ObjectsData actualObject = new ObjectsData ();

		//tengo el objeto idToReplace, y quiero reemplazarlo por el newO
		for (int i = 0; i < GamePlus[Round].LevelObjects.Length; i++)
			if (id == GamePlus [Round].LevelObjects [i].ID) 
				actualObject = GamePlus [Round].LevelObjects [i];

		//Identificar objeto físico mediante ID y Destruirlo.
		GameObject go = GetFisicObject(actualObject.ID);
		Destroy (go);


		//Intercambiar elemento viejo por elemento nuevo.
		actualObject.path = path;
		actualObject.amount = amount;

		//Instanciar nuevo Objeto en mundo físico.
		for (int i = 0; i < GamePlus [Round].LevelObjects.Length; i++)
			if (GamePlus [Round].LevelObjects [i].ID == actualObject.ID)
				CargarLevelObjects (GamePlus [Round], i);
		
		GuardarConfiguración ();
	}

	public void TapEliminar()
	{
		int e = 0;

		//Declarar nuevo array para dataObjects.
		int length = GamePlus[Round].LevelObjects.Length - 1;
		ObjectsData[] newObjects = new ObjectsData[length];

		//Llenar nuevo Array con todos menos el que queremos eliminar.
		for (int i = 0; i < length; i++) 
		{
			if (GamePlus [Round].LevelObjects [i].ID == idToEliminate) 
				e = i + 1;
			
			newObjects [i] = GamePlus [Round].LevelObjects [e];
			e++;
		}

		//Asignar nuevo array reestructurado al array original.
		GamePlus[Round].LevelObjects = newObjects;

		//Destruir objeto físico.
		GameObject go = GetFisicObject(idToEliminate);
		Destroy (go);
		GuardarConfiguración ();
	}

	GameObject GetFisicObject(int id)
	{
		GameObject go = null;
		int childCount = ObjectContainer.transform.childCount;

		for (int i = 0; i < childCount; i++)
			if (ObjectContainer.transform.GetChild (i).GetComponent<ObjectID> ().ID == id) 
				go = ObjectContainer.transform.GetChild (i).gameObject;		

		return go;
	}

	#endregion

	#region Guardar/Cargar Configuración de Nivel
	public void GuardarConfiguración()
	{
		Level local = new Level ();
		local.key = LevelName;
		local.LevelSerObject = new ObjectsSerData[GamePlus [Round].LevelObjects.Length];
		for (int i = 0; i < local.LevelSerObject.Length; i++) 
		{
			local.LevelSerObject[i] = new ObjectsSerData();
			local.LevelSerObject [i].ID = GamePlus [Round].LevelObjects [i].ID;
			local.LevelSerObject [i].isActive = GamePlus [Round].LevelObjects [i].isActive;
			local.LevelSerObject [i].path = GamePlus [Round].LevelObjects [i].path;
			local.LevelSerObject [i].amount = GamePlus [Round].LevelObjects [i].amount;
			local.LevelSerObject [i].posX = GamePlus [Round].LevelObjects [i].position.x;
			local.LevelSerObject [i].posY = GamePlus [Round].LevelObjects [i].position.y;
			local.LevelSerObject [i].posZ = GamePlus [Round].LevelObjects [i].position.z;
			local.LevelSerObject [i].rotX = GamePlus [Round].LevelObjects [i].rotation.x;
			local.LevelSerObject [i].rotY = GamePlus [Round].LevelObjects [i].rotation.y;
			local.LevelSerObject [i].rotZ = GamePlus [Round].LevelObjects [i].rotation.z;
		}

		Level[] levelContainer = Persistant.Data.SavedLevelContainer;

		//Verifico y guardo si no hay data.
		if (levelContainer == null) 
		{
			Persistant.Data.SavedLevelContainer = new Level[1];
			Persistant.Data.SavedLevelContainer [0] = local;

			LoaderManager.Singleton.Guardar ();
		} 
		else 
		{
			//En caso de existir, reemplazo con la nueva data.
			for (int i = 0; i < levelContainer.Length; i++) 
			{
				if (levelContainer [i].key == LevelName) 
				{
					levelContainer [i].LevelSerObject = local.LevelSerObject;
					LoaderManager.Singleton.Guardar ();
					Debug.Log ("Reemplazo");
					return;
				}
			}

			// en caso de que no exista, añado un item con la info local en el persistente.
			int length = levelContainer.Length;
			Level[] newLevel = new Level[length + 1];

			for (int i = 0; i < length; i++)
				newLevel [i] = levelContainer [i];		

			newLevel[newLevel.Length - 1] = local;
			Persistant.Data.SavedLevelContainer = newLevel;

			LoaderManager.Singleton.Guardar ();
		}
	}

	public void CargarConfiguracion()
	{
		Level[] levelContainer = Persistant.Data.SavedLevelContainer;

		if (levelContainer == null) 
		{
			Debug.Log ("CargaLevel null");
			return;
		}

		for (int i = 0; i < levelContainer.Length; i++) 
		{
			if (levelContainer[i].key == LevelName) 
			{
				GamePlus[Round].LevelObjects = new ObjectsData[levelContainer[i].LevelSerObject.Length];
				for (int e = 0; e < levelContainer[i].LevelSerObject.Length; e++) 
				{
					GamePlus [Round].LevelObjects [e] = new ObjectsData ();
					GamePlus [Round].LevelObjects [e].ID = levelContainer [i].LevelSerObject [e].ID;
					GamePlus [Round].LevelObjects [e].isActive = levelContainer [i].LevelSerObject [e].isActive;
					GamePlus [Round].LevelObjects [e].path = levelContainer [i].LevelSerObject [e].path;
					GamePlus [Round].LevelObjects [e].amount = levelContainer [i].LevelSerObject [e].amount;

					GamePlus [Round].LevelObjects [e].position.x = levelContainer [i].LevelSerObject [e].posX;
					GamePlus [Round].LevelObjects [e].position.y =	levelContainer [i].LevelSerObject [e].posY;
					GamePlus [Round].LevelObjects [e].position.z =levelContainer [i].LevelSerObject [e].posZ;

					GamePlus [Round].LevelObjects [e].rotation.x = levelContainer [i].LevelSerObject [e].rotX;
					GamePlus [Round].LevelObjects [e].rotation.y = levelContainer [i].LevelSerObject [e].rotY;
					GamePlus [Round].LevelObjects [e].rotation.z = levelContainer [i].LevelSerObject [e].rotZ;
				}
				Debug.Log ("Datos cargados exitosamente!!");
			}
		}
	}

	#endregion

	public IEnumerator Start()
	{
		Debug.Log ("********* LevelManager Inicia la carga **********");
		Init ();
		string startingPositionName = gameManager.sceneController.startingPositionName;
		int startingCameraIndex = gameManager.sceneController.startingCameraIndex;
		Vector3 position = new Vector3();
		Vector3 rotation = new Vector3();

		for (int i = 0; i < SpawnList.Count; i++) {
			if (SpawnList[i].key==startingPositionName) {
				startingCameraIndex = SpawnList [i].activeCamera;
				position = SpawnList [i].position;
				rotation = SpawnList [i].rotation;
			}
		}

		//Seteando Player.
		PlayerManager player = gameManager.LocalPlayer;
		player.Init ();
		player.transform.position = position;
		player.transform.rotation =Quaternion.Euler(rotation.x,rotation.y,rotation.z);

		//Seteando Camaras.
		gameManager.ActualCameraManager.ActiveCamera = startingCameraIndex;
		gameManager.ActualCameraManager.Init ();
		yield return new WaitForSeconds (1f);
		StartCoroutine (gameManager.sceneController.Fade (0f));
		Debug.Log ("********** LevelManager Carga Completa **********");
	}
	void Init()
	{
		gameManager = GameManager.instance;
		CargarConfiguracion ();
		for (int i = 0; i < GamePlus [Round].LevelObjects.Length; i++) 
		{	
			if(GamePlus [Round].LevelObjects[i].isActive)
				CargarLevelObjects (GamePlus[Round], i);
		}
	}

	void CargarLevelObjects(GamePlus actualGP, int i)
	{
		Item item;
		int amount;
		Vector3 pos = new Vector3();
		Vector3 rot = new Vector3();
		GameObject go;

		item = LoaderManager.Singleton.CargarItem (actualGP.LevelObjects[i].path);
		go = LoaderManager.Singleton.CargarGameObject(item.Modelo);

		pos = new Vector3
			(
				actualGP.LevelObjects [i].position.x, 
				actualGP.LevelObjects [i].position.y, 
				actualGP.LevelObjects [i].position.z
			);
		rot = actualGP.LevelObjects [i].rotation;
		go.GetComponent<ObjectID> ().ID = actualGP.LevelObjects[i].ID;
		go = Instantiate (go, pos, Quaternion.Euler(rot.x,rot.y,rot.z));
		go.transform.parent = ObjectContainer.transform;
	}
}