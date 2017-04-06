using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//UI block element that is show when an object is selected
public class UIBlockInfo : MonoBehaviour {

	private GameObject block;
	private GameObject selectedObject;

	//need ref to the inspector
	//[SerializeField] private Image inspector = null;

	public void SendBlockVariables()
	{
		//send the variables of the block to the inspector

	}

	public void SetBlock(GameObject bl)
	{
		block = bl;

		//then get the variables from the block

	}


	public void DeleteBlock()
	{
		selectedObject.GetComponent<ObjectData> ().RemoveBlockFromList (block);
		Destroy (block);
		Destroy (gameObject);

	}


	public void SetSelectedObject(GameObject SO)
	{
		selectedObject = SO;
	}

}
