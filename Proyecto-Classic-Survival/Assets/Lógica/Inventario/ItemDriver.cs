using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemDriver : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	//Canvas Objects
	public Image IconContainer;
	public Image Highlight;
	public Text AmountContainer;

	//Public and Hide Objects
	[HideInInspector]public PocketItem myPocketItem;
	[HideInInspector]public Item myItem;
	[HideInInspector]public SlotType mySlotType;

	//Private Objects
	private Transform originalParent;
	private Vector3 originalPosition;

	public void AgregarItem(PocketItem pocketItem)
	{
		myPocketItem = pocketItem;
		myItem = LoaderManager.singleton.CargarItem (myPocketItem.ItemPath);

		if(myItem != null)
			AsignarIcono();
		AsignarAmount(myPocketItem.Amount);
	}
	void AsignarIcono()
	{
		IconContainer.sprite = LoaderManager.singleton.CargarSprite (myItem.IconoPequeño);
		if (IconContainer.sprite != null)
			IconContainer.enabled = true;
		else
			Debug.Log ("No se encontró Sprite en Carpeta Resources!");
	}
	void AsignarAmount(int amount)
	{
		if (amount == 0)
			AmountContainer.text = "";
		else
			AmountContainer.text = amount.ToString ();
	}
	public void LimpiarSlot()
	{
		IconContainer.enabled = false;
		AmountContainer.text = "";
		myPocketItem = null;
		myItem = null;
	}
	public void TapSeleccionarItem()
	{
		InventarioCanvasManager inventarioCanvasManager = GameManager.instance.canvasManager.inventarioCanvasManager;

		inventarioCanvasManager.descriptionManager [0].FillDescription (myItem);

		LimpiarHighlight (inventarioCanvasManager.PocketItemsDriver, inventarioCanvasManager.descriptionManager [0]);
		LimpiarHighlight (inventarioCanvasManager.PickupItemsDriver, inventarioCanvasManager.descriptionManager [0]);
		
		if (myItem == null)
			return;
		AplicarHighlight (inventarioCanvasManager.descriptionManager[0]);
	}
	void LimpiarHighlight(ItemDriver[] itemDrivers, DescriptionManager descriptionManager)
	{
		Image nombre = descriptionManager.nombre.GetComponentInParent<Image> ();
		Button descripcion = descriptionManager.descripcion.GetComponentInParent<Button> ();
		nombre.color = Color.black;
		ColorBlock colorBlock = descripcion.colors;
		colorBlock.normalColor = Color.black;
		descripcion.colors = colorBlock;

		for (int i = 0; i < itemDrivers.Length; i++) 
			itemDrivers [i].Highlight.color = Color.black;
	}
	void AplicarHighlight(DescriptionManager descriptionManager)
	{
		Color color = Color.black;
		Image nombre = descriptionManager.nombre.GetComponentInParent<Image> ();
		Button descripcion = descriptionManager.descripcion.GetComponentInParent<Button> ();

		if(mySlotType.Equals(SlotType.Pocket))
			color = Color.blue;
		else if (mySlotType.Equals (SlotType.Pickup))
			color = Color.cyan;

		nombre.color = color;
		ColorBlock colorBlock = descripcion.colors;
		colorBlock.normalColor = color;
		descripcion.colors = colorBlock;
		Highlight.color = color;
	}

	#region |Algoritmos de DRAG|
	public void OnBeginDrag(PointerEventData eventData)
	{
		originalParent = this.transform.parent;
		originalPosition = this.transform.position;
		this.transform.SetParent (this.transform.parent.parent.parent.parent);
		GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}
	public void OnDrag(PointerEventData eventData)
	{
		if (myPocketItem != null)
			this.transform.position = eventData.position;
	}
	public void OnEndDrag(PointerEventData eventData)
	{
		this.transform.SetParent (originalParent);
		this.transform.position = originalPosition;
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
	}
	#endregion
}