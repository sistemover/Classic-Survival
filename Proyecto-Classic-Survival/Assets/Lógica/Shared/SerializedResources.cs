public class SerializedResources{}

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