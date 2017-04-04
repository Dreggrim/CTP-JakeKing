using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewTaskObjectDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	public static GameObject objectBeingDragged;
	public GameObject newObject;
	Vector3 startPos;
	public Canvas canvas;

	//info that new object need
	[SerializeField] private ProgramManager theMan =  null;

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
		transform.position = Input.mousePosition;
	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{

		if (!theMan.SimActive && !EventSystem.current.IsPointerOverGameObject()) {

			//drop a new object into the level
			GameObject thing = Instantiate (newObject, Input.mousePosition, Quaternion.identity) as GameObject;
			thing.GetComponent<TaskObject> ().theMan = theMan;
			thing.transform.SetParent (canvas.transform);

			theMan.AddTaskToList (thing);
		}

		objectBeingDragged = null;
		transform.position = startPos;
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
	}

	#endregion





}
