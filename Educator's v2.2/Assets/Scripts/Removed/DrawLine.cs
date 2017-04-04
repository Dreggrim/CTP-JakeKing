using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawLine : MonoBehaviour {

	[SerializeField] private List<GameObject> Nodes;
	public int i;
	// Update is called once per frame
	void Update () {

		foreach (GameObject element in Nodes) {
			
			transform.GetComponent<LineRenderer> ().SetPosition (i, element.transform.position);
			i++;
		}

		i = 0;
	}


	void AddNode()
	{


	}
}
