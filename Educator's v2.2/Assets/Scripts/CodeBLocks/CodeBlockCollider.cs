using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeBlockCollider : CodeBlock {


	public override void SetName()
	{
		name = "Collider";
	}


	public override void StartUp ()
	{
		transform.parent.gameObject.AddComponent<BoxCollider2D> ();
	}

	public override void BlockCode()
	{

	}

}
