using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item 
{
	public EquipType equipType;
	public EquipMainType equipMainType;
	public EquipSubType equipSubType;

	public override void Use()
	{
		base.Use ();
	}
}
