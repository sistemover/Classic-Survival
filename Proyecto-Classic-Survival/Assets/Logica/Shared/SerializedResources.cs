public class SerializedResources{}

[System.Serializable]
public class Persistant
{
	public static Persistant Data;
	public PocketItem[] SavedPocketContainer;
	public PocketItem[] SavedEquipContainer;
	public Level[] SavedLevelContainer;
}

[System.Serializable]
public class PocketItem
{
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
	public ObjectsData[] LevelObjects;
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
	public int Amount;
	public float pX, pY, pZ, rX,rY,rZ;
}