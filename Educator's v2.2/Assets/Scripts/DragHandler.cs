using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	public static GameObject objectBeingDragged;
	public GameObject newObject;
	Vector3 startPos;

	//info that new object need
	[SerializeField] private GameObject rightClickBar = null;
	[SerializeField] private Image highlight = null;
	[SerializeField] private ProgramManager theMan =  null;

	#region IBeginDragHandler implementation

	public void OnBeginDrag (PointerEventData eventData)
	{
		objectBeingDragged = gameObject;
		startPos = transform.position;
	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{

		if (!theMan.SimActive) {

			//drop a new object into the level
			GameObject thing = Instantiate (newObject, Camera.main.ScreenToWorldPoint (new Vector3 (transform.position.x, transform.position.y, 400f)), Quaternion.identity) as GameObject;
			thing.GetComponent<ObjectData> ().highlight = highlight;
			thing.GetComponent<ObjectData> ().rightClickBar = rightClickBar;
			thing.GetComponent<ObjectData> ().theMan = theMan;
				
			theMan.AddObjectToList (thing);	

		}

		objectBeingDragged = null;
		transform.position = startPos;
		
	}

	#endregion





}
