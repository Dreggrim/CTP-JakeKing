using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeBlockGravity : CodeBlock {


	public override void SetName()
	{
		name = "Gravity";
	}


	public override void StartUp ()
	{
		transform.parent.gameObject.AddComponent<Rigidbody2D> ();
	}

	public override void BlockCode()
	{
		
	}

}
