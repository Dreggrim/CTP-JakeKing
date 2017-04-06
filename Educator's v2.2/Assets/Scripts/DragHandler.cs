using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Drag handeler script for adding new objects to the scene
public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public static GameObject objectBeingDragged;

	//where the UI object being draged started from
	Vector3 startPos;

	//new object to be created on drop
	public GameObject newObject;

	//info that new object need
	[SerializeField] private GameObject rightClickBar = null;
	[SerializeField] private Image highlight = null;
	[SerializeField] private ProgramManager theMan =  null;

	#region IBeginDragHandler implementation

	public void OnBeginDrag (PointerEventData eventData)
	{
		//get its starting position
		objectBeingDragged = gameObject;
		startPos = transform.position;
	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		//move the object with the mouse
		transform.position = Input.mousePosition;
	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{

		//only if the simulation is not active will a new object be created
		if (!theMan.SimActive) {

			//drop a new object into the level and pass it the things it needs to know
			GameObject thing = Instantiate (newObject, Camera.main.ScreenToWorldPoint (new Vector3 (transform.position.x, transform.position.y, 400f)), Quaternion.identity) as GameObject;
			thing.GetComponent<ObjectData> ().highlight = highlight;
			thing.GetComponent<ObjectData> ().rightClickBar = rightClickBar;
			thing.GetComponent<ObjectData> ().theMan = theMan;
				
			theMan.AddObjectToList (thing);	

		}

		//send the dragged object back to its starting position.
		objectBeingDragged = null;
		transform.position = startPos;
		
	}

	#endregion





}
