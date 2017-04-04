using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CodeBlockEditor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public GameObject UIObject;
	[SerializeField] private List<GameObject> UIBlockList = new List<GameObject>();
	[SerializeField] private bool mouseOverYN;
	[SerializeField] public GameObject RightCLickManager = null;


	public void AddToList(string name, GameObject block, GameObject selecedObject)
	{
		Debug.Log ("add to list");
		//add a UI object to the the list and have te htesxt read the same as the name
		//make this object a child of the editor.
		GameObject thing = Instantiate (UIObject) as GameObject;
		thing.GetComponent<UIBlockInfo> ().SetBlock (block);
		thing.GetComponent<UIBlockInfo> ().SetSelectedObject (selecedObject);
		thing.transform.SetParent(gameObject.transform);
		thing.transform.FindChild ("Text").GetComponent<Text> ().text = name;
		UIBlockList.Add (thing);
	}


	public void AddNewBlockToObject(GameObject block)
	{
		
		RightCLickManager.GetComponent<RightClickControler> ().AddNewBlockToObject (block);

	}



	void RemoveFromList(int i)
	{
		Destroy (UIBlockList [i]);
		UIBlockList.RemoveAt (i);
	}



	public void ClearList()
	{
		foreach (GameObject BlockUI in UIBlockList) {
			Destroy (BlockUI);
		}

		UIBlockList.Clear ();
	}



	public void OnPointerEnter(PointerEventData eventData)
	{
		mouseOverYN = true;
	}


	public void OnPointerExit(PointerEventData eventData)
	{
		mouseOverYN = false;
	}


	public bool GetMouseOverYN()
	{
		return mouseOverYN;
	}


	public bool ObjectSelected()
	{
		return RightCLickManager.GetComponent<RightClickControler> ().SelectedYN ();
	}



}
