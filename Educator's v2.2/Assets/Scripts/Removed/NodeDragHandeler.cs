using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NodeDragHandeler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public static GameObject objectBeingDragged;
	public GameObject newObject;
	Vector3 startPos;

	//info that new object need
	[SerializeField] private GameObject rightClickBar;
	[SerializeField] private Image highlight;
	[SerializeField] private ProgramManager theMan;
	public int type;


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
			GameObject thing = Instantiate (newObject, Camera.main.ScreenToWorldPoint (new Vector3(transform.position.x, transform.position.y, 50f)), Quaternion.identity) as GameObject;
			thing.GetComponent<NodeControler> ().highlight = highlight;
			thing.GetComponent<NodeControler> ().rightClickBar = rightClickBar;
			thing.GetComponent<NodeControler> ().theMan = theMan;
		

			switch (type) {
			case 0:
				thing.GetComponent<NodeControler> ().IT = NodeControler.interactionType.Rotate;
				break;
			case 1:
				thing.GetComponent<NodeControler>().IT = NodeControler.interactionType.Move;
					break;


			}



		}
		objectBeingDragged = null;
		transform.position = startPos;

	}

	#endregion





}
