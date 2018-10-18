using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UseDriver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
	private Image slotImage
	{
		get
		{
			return gameObject.GetComponent<Image> ();
		}
	}

	ItemDriver dropDriver;
	PocketItem dropPocket;
	Item dropItem;

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (eventData.pointerDrag == null)
			return;
		Init (eventData);
		if (dropItem == null)
			return;
		if (!dropItem.isUsable)
			slotImage.color = ColorManager.Singleton.Denied;
		else
			slotImage.color = ColorManager.Singleton.Approved;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		slotImage.color = ColorManager.Singleton.Normal;
	}
	public void OnDrop(PointerEventData eventData)
	{

	}
	void Init(PointerEventData eventData)
	{
		dropDriver = eventData.pointerDrag.GetComponent<ItemDriver> ();
		dropPocket = dropDriver.myPocketItem;
		if (dropDriver.myItem != null)
			dropItem = dropDriver.myItem;
	}
}