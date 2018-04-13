using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour 
{
	#region Singleton
	public static ColorManager Singleton;
	void Awake ()
	{
		if (Singleton != null)
			return;
		Singleton = this;
	}
	#endregion

	public Color Normal;
	public Color Denied;
	public Color Approved;
	public Color Pocket;
	public Color Pickup; 
	public Color Equip;

}
