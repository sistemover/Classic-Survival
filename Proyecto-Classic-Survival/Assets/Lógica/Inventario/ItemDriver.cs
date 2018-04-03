using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemDriver : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	//Canvas Objects
	public Image IconContainer;
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
		myItem = Resources.Load(myPocketItem.ItemPath) as Item;

		if(myItem != null)
			AsignarIcono();
		AsignarAmount(myPocketItem.Amount);
	}
	void AsignarIcono()
	{
		IconContainer.sprite = Resources.Load<Sprite> (myItem.IconoPequeño);
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
	public void TapSeleccionarItem()
	{
		FillDescription ();
	}
	void FillDescription()
	{
		if (mySlotType.Equals (SlotType.Pocket))
			GameManager.instance.canvasManager.inventarioCanvasManager.descriptionManager [0].FillDescription (myItem);
	}
	public void LimpiarSlot()
	{
		IconContainer.enabled = false;
		AmountContainer.text = "";
		myPocketItem = null;
		myItem = null;
	}
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
}