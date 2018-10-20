using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FieldOfView : MonoBehaviour 
{
	public float timeToScan = 0.2f;
		public float viewRadius;
		[Range(0,360)]public float viewAngle;

		public LayerMask targetMask;
		public LayerMask obstacleMask;

		public List<VTargets> vTargets = new List <VTargets>();
		public List<Transform> VisibleTargets = new List<Transform>();

		//**************************************************************************************************************

		void Start()
		{
			StartCoroutine ("FindTargetsWithDelay", timeToScan);
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

					//Agrega el target y la distancia a la lista.
					if (!Physics.Raycast (transform.position, dirToTarget, distToTarget, obstacleMask)) 
					{
						vTargets.Add (new VTargets(target, distToTarget));
						VisibleTargets.Add (target);
					}						
				}
			}
			//Ordernar lista.
			vTargets = vTargets.OrderBy(p=>p.Distance).ToList();
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
