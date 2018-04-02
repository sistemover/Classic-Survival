using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotDriver : MonoBehaviour {

	public SlotType slotType;
	public ItemDriver itemDriver;

	void Awake()
	{
		itemDriver.mySlotType = slotType;
	}
}
