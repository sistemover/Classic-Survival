using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CondicionesManager : MonoBehaviour 
{
	public List<Condiciones> condiciones;
	public void Init()
	{
		//Debug.Log (Persistant.Data.SavedCondiciones[0].condicion);
		if (Persistant.Data.SavedCondiciones == null)
			Debug.Log ("Las condiciones cargadas están por defecto");
		else
			StartCoroutine(CargarCondicionesGuardadas ());
	}
	public void SaveCondicionesState()
	{
		Persistant.Data.SavedCondiciones = new Condiciones[condiciones.Count];
		for (int i = 0; i < condiciones.Count; i++) 
			Persistant.Data.SavedCondiciones [i] = condiciones [i];
		
		Debug.Log ("Condiciones Actualizadas correctamente!!!");
	}
	IEnumerator CargarCondicionesGuardadas()
	{
		int c = Persistant.Data.SavedCondiciones.Length;
		for (int i = 0; i < c; i++)
			condiciones[i] = Persistant.Data.SavedCondiciones[i];
		yield return null;
	}
}
