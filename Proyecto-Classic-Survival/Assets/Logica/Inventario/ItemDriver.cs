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

	//Public GameObjects
	public GameObject slotDriver;

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
		myItem = LoaderManager.Singleton.CargarItem (myPocketItem.ItemPath);

		if (myItem != null) 
			AsignarIcono ();
		AsignarAmount(myPocketItem.Amount);
	}
	void AsignarIcono()
	{
		IconContainer.sprite = LoaderManager.Singleton.CargarSprite (myItem.IconoPequeño);
		if (IconContainer.sprite != null)
			IconContainer.enabled = true;
		else
			Debug.Log ("No se encontró Sprite en Carpeta Resources!");
	}
	void AsignarAmount(int amount)
	{
		if (amount == 0) 
		{
			if (!myItem.isStackable) 
			{
				AmountContainer.text = "";
			}
			if (myItem.isEquipment) 
			{
				if(myItem.GetEquip ().equipMainType.Equals (EquipMainType.Being))
					AmountContainer.text = amount.ToString();
			}
		} 
		else 
		{
			AmountContainer.text = amount.ToString ();
		}
	}
	public void LimpiarSlot()
	{
		IconContainer.enabled = false;
		AmountContainer.text = "";
		myPocketItem = null;
		myItem = null;
	}
	public void ActivarDesactivarSlot(bool valor)
	{
		slotDriver.SetActive (valor);
	}
	public void TapSeleccionarItem()
	{
		InventarioCanvasManager inventarioCanvasManager = GameManager.instance.canvasManager.inventarioCanvasManager;

		inventarioCanvasManager.descriptionManager [0].FillDescription (myItem);

		LimpiarHighlight (inventarioCanvasManager.PocketItemsDriver, inventarioCanvasManager.descriptionManager [0]);
		LimpiarHighlight (inventarioCanvasManager.PickupItemsDriver, inventarioCanvasManager.descriptionManager [0]);
		LimpiarHighlight (inventarioCanvasManager.EquipItemsDriver, inventarioCanvasManager.descriptionManager [0]);
		
		if (myItem == null)
			return;
		AplicarHighlight (inventarioCanvasManager.descriptionManager[0]);
	}
	void LimpiarHighlight(ItemDriver[] itemDrivers, DescriptionManager descriptionManager)
	{
		Image nombre = descriptionManager.nombre.GetComponentInParent<Image> ();
		Button descripcion = descriptionManager.descripcion.GetComponentInParent<Button> ();
		nombre.color = ColorManager.Singleton.Normal;
		ColorBlock colorBlock = descripcion.colors;
		colorBlock.normalColor = ColorManager.Singleton.Normal;
		descripcion.colors = colorBlock;

		for (int i = 0; i < itemDrivers.Length; i++) 
			itemDrivers [i].Highlight.color = ColorManager.Singleton.Normal;
	}
	void AplicarHighlight(DescriptionManager descriptionManager)
	{
		Color color = ColorManager.Singleton.Normal;
		Image nombre = descriptionManager.nombre.GetComponentInParent<Image> ();
		Button descripcion = descriptionManager.descripcion.GetComponentInParent<Button> ();

		if(mySlotType.Equals(SlotType.Pocket))
			color = ColorManager.Singleton.Pocket;
		else if (mySlotType.Equals (SlotType.Pickup))
			color = ColorManager.Singleton.Pickup;
		else if (mySlotType.Equals (SlotType.Equip))
			color = ColorManager.Singleton.Equip;

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
		this.transform.SetParent (this.transform.parent.parent.parent.parent.parent);
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

		//Se suelta Slot fuera del Inventario.
		if (this.mySlotType.Equals(SlotType.Pocket) || this.mySlotType.Equals(SlotType.Equip)) 
		{
			if (eventData.pointerEnter == null) 
			{
				GameManager.instance.inventarioManager.DropToWorld (this.myPocketItem, this.mySlotType);
			}
		}

	}
	#endregion
}