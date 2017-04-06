using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToRemove : CodeBlock {

	//Set the name of the code block
	public override void SetName()
	{
		name = "Click To Remove";
	}

	//This function gets called once it is like the start function
	public override void StartUp ()
	{
		transform.parent.GetComponent<SimObject> ().SetOnCLickEvents (true);
	}


	//This function gets called when the object has been clicked
	public override void ClickEvent()
	{

		if (Input.GetMouseButtonDown (0)) {

			transform.parent.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			transform.parent.gameObject.GetComponent<Collider2D> ().enabled = false;
		}
	}



}
