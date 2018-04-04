using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionManager : MonoBehaviour 
{
	public Text nombre;
	public Text descripcion;

	Item myItem;
	ItemText itemText;

	public void FillDescription(Item item)
	{
		myItem = item;
		if (myItem == null) 
		{
			nombre.text = "";
			descripcion.text = "";
			return;
		}
		itemText = GameManager.instance.localizationManager.GetlocalizedItem (myItem.name_key);
		nombre.text = itemText.name;
		descripcion.text = itemText.shortdescription;
	}

	public void TapExaminar()
	{
		if (myItem == null)
			return;

		CanvasManager canvasManager = GameManager.instance.canvasManager;
		GameObject menuExaminar = canvasManager.MenuExaminar;
		ExamineManager examineManager = menuExaminar.GetComponent<ExamineManager> ();

		AsignarIcono (examineManager.Icono);
		AsignarTextos (examineManager.Nombre, examineManager.Descripcion);

		canvasManager.TapExaminar ();
	}

	void AsignarIcono(Image icono)
	{
		icono.sprite = Resources.Load<Sprite> (myItem.IconoGrande);
		if (icono.sprite != null)
			icono.enabled = true;
		else
			icono.enabled = false;
	}

	void AsignarTextos(Text nombre, Text descripcion)
	{
		nombre.text = itemText.name;
		descripcion.text = itemText.longdescription;
	}
}
