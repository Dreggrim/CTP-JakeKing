using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Drag handeler script for adding new tasks to the simulation
public class NewTaskObjectDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	
	public static GameObject objectBeingDragged;

	//where the UI object being draged started from
	Vector3 startPos;

	//new object to be created on drop
	public GameObject newObject;

	//info that new object need
	[SerializeField] private ProgramManager theMan =  null;
	public Canvas canvas;


	#region IBeginDragHandler implementation

	public void OnBeginDrag (PointerEventData eventData)
	{
		//get its starting position
		objectBeingDragged = gameObject;
		startPos = transform.position;
		GetComponent<CanvasGroup> ().blocksRaycasts = false;
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
		//only if the simulation is not active will a new task object be created
		if (!theMan.SimActive && !EventSystem.current.IsPointerOverGameObject()) {

			//drop a new object into the level
			GameObject thing = Instantiate (newObject, Input.mousePosition, Quaternion.identity) as GameObject;
			thing.GetComponent<TaskObject> ().theMan = theMan;
			thing.transform.SetParent (canvas.transform);

			theMan.AddTaskToList (thing);
		}

		//send the dragged object back to its starting position.
		objectBeingDragged = null;
		transform.position = startPos;
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
	}

	#endregion





}
