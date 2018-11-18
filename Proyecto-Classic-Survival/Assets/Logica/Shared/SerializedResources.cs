using UnityEngine;
public class SerializedResources{}

[System.Serializable]
public class Persistant
{
	public static Persistant Data;
	public PocketItem[] SavedPocketContainer;
	public PocketItem[] SavedEquipContainer;
	public Level[] SavedLevelContainer;
	public SavedPlayerSpawn SavedPlayerSpawn;
	public Condiciones[] SavedCondiciones; 
}

[System.Serializable]
public class Dialogos
{
	public string condicion;
	public string[] dialKeys;
}

[System.Serializable]
public class Condiciones
{
	public string condicion;
	public bool status;
}

[System.Serializable]
public class PocketItem
{
	public int id;
	public string ItemPath = "Items/";
	public int Amount;
}

[System.Serializable]
public class CombinationData
{
	public CombinationContainer[] container;
}

[System.Serializable]
public class CombinationContainer
{
	public string combKey;
	public string needKey;
	public int needAmount;
	public string itemPath;
	public int amount;
}

[System.Serializable]
public class CombinationItem
{
	public string needKey;
	public int needAmount;
	public string itemPath;
	public int amount;
}
[System.Serializable]
public class Level
{
	public string key;
	public ObjectsSerData[] LevelSerObject;
}

[System.Serializable]
public class ObjectsSerData
{
	public int ID;
	public bool isActive;
	public string path;
	public int amount;
	public float posX,posY,posZ,rotX,rotY,rotZ;
}

[System.Serializable]
public class GamePlus
{
	public ObjectsData[] LevelObjects;
}

[System.Serializable]
public class ObjectsData
{
	public int ID;
	public bool isActive;
	public string path;
	public int amount;
	public Vector3 position;
	public Vector3 rotation;
}

[System.Serializable]
public class PlayerSpawn
{
	public string key;
	public int activeCamera;
	public Vector3 position;
	public Vector3 rotation;
}

[System.Serializable]
public class SavedPlayerSpawn
{
	public string levelName;
	public int activeCamera;
	public float posX,posY,posZ,rotX,rotY,rotZ;
}