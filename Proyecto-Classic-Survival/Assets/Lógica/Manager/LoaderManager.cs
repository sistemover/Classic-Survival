using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderManager : MonoBehaviour 
{
	#region Singleton
	public static LoaderManager Singleton;
	void Awake ()
	{
		if (Singleton != null)
			return;
		Singleton = this;
	}
	#endregion

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
