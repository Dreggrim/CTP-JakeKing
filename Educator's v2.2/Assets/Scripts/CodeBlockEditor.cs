using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//The editor that handdles showing what block object are on an object
public class CodeBlockEditor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	//The UI object prefab that populates list
	public GameObject UIObject;

	//the list of UI objects
	[SerializeField] private List<GameObject> UIBlockList = new List<GameObject>();

	//is this element moused over?
	[SerializeField] private bool mouseOverYN;

	//rightclick manager
	[SerializeField] public GameObject RightCLickManager = null;

	//Adding a new UI element to the list
	public void AddToList(string name, GameObject block, GameObject selecedObject)
	{
		Debug.Log ("add to list");
		//add a UI object to the the list and have the text read the same as the name
		//make this object a child of the editor.
		GameObject thing = Instantiate (UIObject) as GameObject;
		thing.GetComponent<UIBlockInfo> ().SetBlock (block);
		thing.GetComponent<UIBlockInfo> ().SetSelectedObject (selecedObject);
		thing.transform.SetParent(gameObject.transform);
		thing.transform.FindChild ("Text").GetComponent<Text> ().text = name;
		UIBlockList.Add (thing);
	}

	//add a new block to an object
	//this is called when ever a new block is added
	public void AddNewBlockToObject(GameObject block)
	{
		RightCLickManager.GetComponent<RightClickControler> ().AddNewBlockToObject (block);
	}


	//remove an object from the list
	void RemoveFromList(int i)
	{
		Destroy (UIBlockList [i]);
		UIBlockList.RemoveAt (i);
	}


	//completly clear the list of UI objects
	public void ClearList()
	{
		foreach (GameObject BlockUI in UIBlockList) {
			Destroy (BlockUI);
		}

		UIBlockList.Clear ();
	}


	//functons to keep track of if the mouse if over this ellement or not
	public void OnPointerEnter(PointerEventData eventData)
	{
		mouseOverYN = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		mouseOverYN = false;
	}

	//get the mouse over boolean
	public bool GetMouseOverYN()
	{
		return mouseOverYN;
	}

	//check to see if there is an object selected
	public bool ObjectSelected()
	{
		return RightCLickManager.GetComponent<RightClickControler> ().SelectedYN ();
	}



}
