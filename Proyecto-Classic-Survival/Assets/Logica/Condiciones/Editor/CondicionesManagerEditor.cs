using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CondicionesManager))]
public class CondicionesManagerEditor : Editor
{
	CondicionesManager c;

	void OnEnable()
	{
		c = (CondicionesManager)target;
	}
	public override void OnInspectorGUI ()
	{	
		GUILayout.BeginHorizontal ("box");
			GUILayout.Label ("Listado de Condiciones");
		GUILayout.EndHorizontal ();

		GUILayout.BeginVertical ("box");
			GUILayout.BeginHorizontal ();
				GUILayout.Label ("Condición", GUILayout.Width(150));
				GUILayout.Label ("Status");
			GUILayout.EndHorizontal ();
			for (int cnt = 0; cnt < c.condiciones.Count; cnt++) 
			{
				
				GUILayout.BeginHorizontal ("box");
					if (c.condiciones[cnt].condicion == null)
						c.condiciones[cnt].condicion = GUILayout.TextField
							(
								"",
								GUILayout.Width(150)
							);
					else
						c.condiciones[cnt].condicion = GUILayout.TextField
							(
								c.condiciones[cnt].condicion,
								GUILayout.Width(150)
							);			
					c.condiciones [cnt].status = GUILayout.Toggle
						(
							c.condiciones [cnt].status,
							""
						);
					if (GUILayout.Button ("Quitar"))
						QuitarCondicion (cnt);

				GUILayout.EndHorizontal ();
			}

			if (GUILayout.Button ("Añadir Condición"))
				AgregarCondicion ();
		GUILayout.EndVertical ();
		
		//base.OnInspectorGUI ();
	}
	void AgregarCondicion()
	{
		c.condiciones.Add (new Condiciones());
	}

	void QuitarCondicion(int i)
	{
		c.condiciones.RemoveAt (i);
	}
}