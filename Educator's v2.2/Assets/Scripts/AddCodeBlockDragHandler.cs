using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;



//Drag handeler script for adding new code blocks to an object
public class AddCodeBlockDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public static GameObject objectBeingDragged;

	//where the UI object being draged started from
	Vector3 startPos;

	//new object to be created on drop
	public GameObject newObject;

	//info that new object need
	[SerializeField] private Image blockEditor = null;


	#region IBeginDragHandler implementation

	public void OnBeginDrag (PointerEventData eventData)
	{
		//get its starting position and make it so it does not block raycasts
		objectBeingDragged = gameObject;
		startPos = transform.position;
		GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		//move the object with the mouse
		transform.position = new Vector3(Input.mousePosition.x,Input.mousePosition.y, -10f);
	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{

		//if the dragged object is over the correct UI element then add the new code block
		if (blockEditor.GetComponent<CodeBlockEditor> ().GetMouseOverYN ()) {

			//only apply a new block if there is an object selected
			if (blockEditor.GetComponent<CodeBlockEditor> ().ObjectSelected () == true) {

				//check if the selected object already has an object with the same name?


				//create a new block to add
				GameObject block = Instantiate (newObject);

				//make sure the selected object gets the new codeblock
				block.SendMessage ("SetName");

				blockEditor.GetComponent<CodeBlockEditor> ().AddNewBlockToObject (block);
				
				//add a relevent blockUI
				blockEditor.GetComponent<CodeBlockEditor> ().AddToList (block.name, block, blockEditor.GetComponent<CodeBlockEditor>().RightCLickManager.GetComponent<RightClickControler>().GetSelected());

			}
		}

		//send the dragged object back to its starting position.
		objectBeingDragged = null;
		transform.position = startPos;
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
	}

	#endregion





}
