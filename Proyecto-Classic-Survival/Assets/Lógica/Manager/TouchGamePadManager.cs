using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchGamePadManager : MonoBehaviour 
{
	public Image BaseStickL;
	public Image StickL;
	public Image BaseStickR;
	public Image StickR;
	public GameObject A;
	public GameObject B;
	public GameObject X;
	public GameObject Y;

	public void ActivarDesactivarGamePad(bool active)
	{
		Image[] Sticks = { BaseStickL, BaseStickR, StickL, StickR };
		GameObject[] Botones = { A,B,X,Y};
		for (int i = 0; i < Sticks.Length; i++)
			Sticks [i].enabled = active;

		for (int i = 0; i < Botones.Length; i++)
			Botones [i].SetActive (active);
	}

	public void ActivarDesactivarLeftGamePad(bool active)
	{
		BaseStickL.enabled = active;
		StickL.enabled = active;
		X.SetActive (active);
		Y.SetActive (active);
	}

	public void ActivarDesactivarRightGamePad(bool active)
	{
		BaseStickR.enabled = active;
		StickR.enabled = active;
		A.SetActive (active);
		B.SetActive (active);
	}
}
