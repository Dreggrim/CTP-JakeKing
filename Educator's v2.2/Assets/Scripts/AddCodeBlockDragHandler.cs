using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AddCodeBlockDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	public static GameObject objectBeingDragged;
	public GameObject newObject;
	Vector3 startPos;
	//public CodeBlock blockScript;

	//info that new object need

	[SerializeField] private Image blockEditor = null;


	#region IBeginDragHandler implementation

	public void OnBeginDrag (PointerEventData eventData)
	{
		objectBeingDragged = gameObject;
		startPos = transform.position;
		GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		transform.position = new Vector3(Input.mousePosition.x,Input.mousePosition.y, -10f);
	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{

		if (blockEditor.GetComponent<CodeBlockEditor> ().GetMouseOverYN ()) {

			if (blockEditor.GetComponent<CodeBlockEditor> ().ObjectSelected () == true) {

				//check if the selected object already has an object with the same name?






				//create a new block to add
				GameObject block = Instantiate (newObject);
				//make sure the selected object gets the new codeblock
				block.SendMessage ("SetName");

				//Debug.Log ("ADD NEW BLOCK");
				blockEditor.GetComponent<CodeBlockEditor> ().AddNewBlockToObject (block);
				
				//add a relevent blockUI
				blockEditor.GetComponent<CodeBlockEditor> ().AddToList (block.name, block, blockEditor.GetComponent<CodeBlockEditor>().RightCLickManager.GetComponent<RightClickControler>().GetSelected());



			}
		}

		objectBeingDragged = null;
		transform.position = startPos;
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
	}

	#endregion





}
