using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class L_Persistencia : MonoBehaviour
{
	public Text Entrada;
	public Text DebugInput;

	string stringEntrada;

	void Awake()
	{
		LaboratoryDataToSave.current = new LaboratoryDataToSave ();
	}

	void SetearDatos()
	{
		stringEntrada = Entrada.text.ToString ();
		LaboratoryDataToSave.current.Entrada = stringEntrada;
	}

	public void B_Guardar()
	{
		SetearDatos ();
		L_Guardar ();
		Debug.Log ("Guardado: " + stringEntrada);
		DebugInput.text = "Datos Guardados Exitosamente";
	}

	public void B_Cargar()
	{
		L_Cargar (DebugInput, Entrada);

	}
	public static void L_Guardar()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/PersistantData.txt");
		bf.Serialize (file, LaboratoryDataToSave.current);
		file.Close ();
	}

	public static void L_Cargar(Text debug, Text entrada)
	{
		if (File.Exists (Application.persistentDataPath + "/PersistantData.txt")) 
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/PersistantData.txt", FileMode.Open);
			LaboratoryDataToSave.current = (LaboratoryDataToSave)bf.Deserialize (file); 
			debug.text = "Datos Cargados Exitosamente: " + LaboratoryDataToSave.current.Entrada;
		} 
		else 
		{
			debug.text = "No existe Data";
		}
	}
}

[System.Serializable]
public class LaboratoryDataToSave
{
	public static LaboratoryDataToSave current;

	public string Entrada;

}
