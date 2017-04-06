using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeBlockCollider : CodeBlock {

	//Set the name of the code block
	public override void SetName()
	{
		name = "Collider";
	}

	//This function gets called once it is like the start function
	public override void StartUp ()
	{
		transform.parent.gameObject.AddComponent<BoxCollider2D> ();
	}

	//This function gets called every frame like update
	public override void BlockCode()
	{

	}

}
