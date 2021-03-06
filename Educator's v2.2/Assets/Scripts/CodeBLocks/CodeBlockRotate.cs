﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeBlockRotate : CodeBlock {

	[SerializeField] private float rotateSpeed;

	//Set the name of the code block
	public override void SetName()
	{
		name = "Rotate";
	}

	//This function gets called once it is like the start function
	public override void StartUp ()
	{
		rotateSpeed = 2f;
	}

	//This function gets called every frame like update
	public override void BlockCode()
	{
		//rotate an object
      	transform.parent.transform.Rotate (new Vector3 (0, 0, rotateSpeed));

	}
		


}
