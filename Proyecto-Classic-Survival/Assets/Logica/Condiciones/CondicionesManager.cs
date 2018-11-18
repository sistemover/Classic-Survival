using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CondicionesManager : MonoBehaviour 
{
	public List<Condiciones> condiciones;
	public void Init()
	{
		if (Persistant.Data.SavedCondiciones == null)
			Debug.Log ("La data cargada está por defecto");
		else
			StartCoroutine(CargarCondicionesGuardadas ());
	}
	IEnumerator CargarCondicionesGuardadas()
	{
		int c = Persistant.Data.SavedCondiciones.Length;
		for (int i = 0; i < c; i++)
			condiciones[i] = Persistant.Data.SavedCondiciones[i];
		yield return null;
	}
}
