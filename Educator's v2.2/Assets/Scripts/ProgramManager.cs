using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ProgramManager : MonoBehaviour {


	public bool SimActive, objectsCreated;
	[SerializeField] private float scrollSpeed = 1, dragSpeed = 1;
	[SerializeField] Vector3 MouseStartPos, StartPos;
	[SerializeField] Image simActiveHighlight = null;

	[SerializeField] private GameObject rightClickBar = null;
	[SerializeField] private Image highlight = null;


	[SerializeField] private List<GameObject> ObjectList = new List<GameObject>();
	[SerializeField] private List<GameObject> TaskList = new List<GameObject>();
	[SerializeField] private List<GameObject> SimObjectList = new List<GameObject>();

	// Update is called once per frame
	void Update () {

		if (SimActive) {
			objectsCreated = true;
			foreach (GameObject Go in ObjectList) {

				if (Go.activeSelf == true) {
					objectsCreated = false;
					break;
				}
			}

		} else {
			
			simActiveHighlight.gameObject.SetActive (false);

			//zoom control
			Camera.main.orthographicSize -= Input.GetAxis ("Mouse ScrollWheel") * scrollSpeed;

			//clamp the zoom (mathf.clamp wasnt working...)
			if (Camera.main.orthographicSize > 40) {
				Camera.main.orthographicSize = 40;
			}

			if (Camera.main.orthographicSize < 1) {
				Camera.main.orthographicSize = 1;
			}


	

			//camera drag
			if (Input.GetMouseButtonDown (2)) {
				MouseStartPos = Camera.main.ScreenToViewportPoint (Input.mousePosition);
				StartPos = Camera.main.transform.position;
			}
	
			if (Input.GetMouseButton (2)) {
				Camera.main.transform.position = StartPos + (dragSpeed * Camera.main.orthographicSize) * (MouseStartPos - Camera.main.ScreenToViewportPoint (Input.mousePosition));
			}

			//clamp the cmaera movment?
			//dont know if i will have a canvas to work on or not

		}
	}


	public void ToggleSim()
	{
		if (SimActive) {
			objectsCreated = false;
			SimActive = false;
			ClearSimObjectList ();

		} else {
			SimActive = true;

			simActiveHighlight.gameObject.SetActive(true);
			highlight.gameObject.SetActive (false);


			if (rightClickBar.GetComponent<RightClickControler> ().SelectedYN()) {
				rightClickBar.GetComponent<RightClickControler> ().DelselectObject ();
			}
		}
	}


	public void AddObjectToList(GameObject obj)
	{
		ObjectList.Add (obj);
	}
		
	public List<GameObject> GetObjectList()
	{
		return ObjectList;
	}

	public void RemoveObjectFromList(GameObject obj)
	{
		ObjectList.Remove (obj);
	}

	public void AddTaskToList(GameObject task)
	{
		TaskList.Add (task);
	}

	public List<GameObject> GetTaskList()
	{
		return TaskList;
	}

	public void RemoveTaskFromList(GameObject task)
	{
		TaskList.Remove (task);
	}

	public void AddSimObjectToList(GameObject task)
	{
		SimObjectList.Add (task);
	}

	public List<GameObject> GetSimObjectList()
	{
		return SimObjectList;
	}

	public void ClearSimObjectList()
	{
		SimObjectList.Clear ();
	}



}
