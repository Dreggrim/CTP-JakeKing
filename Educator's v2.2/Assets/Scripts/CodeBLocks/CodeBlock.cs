using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base class for the code Blocks
//A blocks code is run from the function. When a block is created it becomes a child of the object so all functionality is applied to its parent
public class CodeBlock : MonoBehaviour {

	//Set the name of the code block
	public virtual void SetName()
	{
		
	}

	//This function gets called once it is like the start function
	public virtual void StartUp ()
	{
		
	}


	//This function gets called every frame like update
	public virtual void BlockCode()
	{
		
	}

	//This function gets called when the object has been clicked
	public virtual void ClickEvent()
	{

	}



}
