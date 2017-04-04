using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeBlockRotate : CodeBlock {

	[SerializeField] private float rotateSpeed;


	public override void SetName()
	{
		name = "Rotate";
		rotateSpeed = 2f;


	}


	public override void StartUp ()
	{
		//none
	}

	public override void BlockCode()
	{
		//rotate an object
      	transform.parent.transform.Rotate (new Vector3 (0, 0, rotateSpeed));

	}
		


}
