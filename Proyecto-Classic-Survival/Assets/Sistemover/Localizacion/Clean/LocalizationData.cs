[System.Serializable]
public class LocalizationData
{
	public LocalizationCanvas[] text;
	public LocalizationItem[] items;
}

[System.Serializable]
public class LocalizationCanvas
{
	public string key;
	public string value;
}

[System.Serializable]
public class LocalizationItem
{
	public string key;
	public string name;
	public string shortdescription;
	public string longdescription;
}

[System.Serializable]
public class ItemText
{
	public string name;
	public string shortdescription;
	public string longdescription;
}