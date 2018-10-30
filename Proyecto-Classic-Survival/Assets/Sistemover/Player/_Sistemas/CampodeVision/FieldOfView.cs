using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FieldOfView : MonoBehaviour 
{
	public float timeToScan = 0.2f;
	public float viewRadius;
	public float interactionDistance = 1f;
	public IEnumerator coroutina;
	[Range(0,360)]public float viewAngle;

	public LayerMask targetMask;
	public LayerMask obstacleMask;

	public List<VTargets> vTargets = new List <VTargets>();
	public List<Transform> VisibleTargets = new List<Transform>();

	//**************************************************************************************************************

	void Start()
	{
		coroutina = FindTargetsWithDelay (timeToScan);
		StartCoroutine (coroutina);
	}

	IEnumerator FindTargetsWithDelay(float delay)
	{
		while (true) 
		{
			yield return new WaitForSeconds (delay);
			FindVisibleTargets ();
		}
	}

	void FindVisibleTargets()
	{
		bool mPickup = GameManager.instance.canvasManager.MenuPickup.activeInHierarchy;
		bool mInventario = GameManager.instance.canvasManager.MenuInventario.activeInHierarchy;
		bool mExaminar = GameManager.instance.canvasManager.MenuExaminar.activeInHierarchy;
		if (mPickup || mInventario || mExaminar)
			return;
		
		vTargets.Clear ();
		VisibleTargets.Clear ();
		Collider[] targetsInViewRadius = Physics.OverlapSphere (transform.position, viewRadius, targetMask);

		for (int i = 0; i < targetsInViewRadius.Length; i++) 
		{
			Transform target = targetsInViewRadius [i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;

			if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle/2) 
			{
				float distToTarget = Vector3.Distance (transform.position, target.position);
				if (!Physics.Raycast (transform.position, dirToTarget, distToTarget, obstacleMask)) {
					vTargets.Add (new VTargets (target, distToTarget));
				}
			}
		}
		//Ordernar lista.
		vTargets = vTargets.OrderBy(p=>p.Distance).ToList();
		for (int i = 0; i < vTargets.Count; i++) 
		{
			if (i == 6)
				break;
			if (vTargets [i].Distance <= interactionDistance)
				VisibleTargets.Add (vTargets [i].Target);	
		}
		if (VisibleTargets == null || VisibleTargets.Count == 0) 
		{
			GameObject a = GameManager.instance.touchGamePadManager.A;
			if (!a.activeInHierarchy) {
				return;
			}
			Debug.Log ("Apagando A...");
			a.SetActive (false);
			return;
		}
		switch (VisibleTargets[0].tag)
		{
			case "Pickable":
				SetInteractionPickup ();
				break;
			case "Door":
				SetInteractionDoor ();
				break;
			case "Dialog":
				SetInteractionDialog ();
				break;
			default:
				break;
		}
	}
	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
	{
		if (!angleIsGlobal) 
		{
			angleInDegrees += transform.eulerAngles.y;
		}

		float xAngle = Mathf.Sin(angleInDegrees * Mathf.Deg2Rad);
		float zAngle = Mathf.Cos(angleInDegrees * Mathf.Deg2Rad);
		return new Vector3(xAngle, 0, zAngle);
	}
	void SetInteractionPickup()
	{
		int e = 0;
		List <int> id = new List<int> ();
		for (int i = 0; i < vTargets.Count; i++) 
		{
			if (vTargets[i].Distance <= interactionDistance) {
				if (vTargets[i].Target.tag=="Pickable") 
				{
					e++;
					id.Add (vTargets[i].Target.GetComponent<ObjectID>().ID);
					if (e == 6)
						break;
				}
			}
		}
		id = id.OrderBy (p=>p).ToList ();
		vTargets = vTargets.OrderBy(p=>p.Distance).ToList();
		gameObject.GetComponent<PlayerManager> ().PlayerInteractor.SetObjectInteraction (id, "Pickable");
	}
	void SetInteractionDoor()
	{
		gameObject.GetComponent<PlayerManager> ().PlayerInteractor.SetLevelInteraction (VisibleTargets[0], "Door");
	}
	void SetInteractionDialog()
	{
		gameObject.GetComponent<PlayerManager> ().PlayerInteractor.SetLevelInteraction (VisibleTargets[0], "Dialog");
	}
}

public class VTargets
{
	public Transform Target { get; set;}
	public float Distance { get; set;}

	public VTargets(Transform t, float d)
	{
		this.Target = t;
		this.Distance = d;
	}
}
