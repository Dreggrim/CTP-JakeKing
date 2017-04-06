using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

//program manager to keep track of objects in the scene and activating the simulation
public class ProgramManager : MonoBehaviour {

	//has the simulation been activated and have all the SimObject been created
	public bool SimActive, objectsCreated;

	//variables controling the movment around the scene
	[SerializeField] private float scrollSpeed = 1, dragSpeed = 1;
	[SerializeField] Vector3 MouseStartPos, StartPos;

	//the highlight around the scene that shows when the simulation is active 
	[SerializeField] Image simActiveHighlight = null;

	//right click bar and the highlight
	[SerializeField] private GameObject rightClickBar = null;
	[SerializeField] private Image highlight = null;

	//the lists of object
	[SerializeField] private List<GameObject> ObjectList = new List<GameObject>();
	[SerializeField] private List<GameObject> TaskList = new List<GameObject>();
	[SerializeField] private List<GameObject> SimObjectList = new List<GameObject>();


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


		}
	}

	//togle the simulation
	public void ToggleSim()
	{
		if (SimActive) {
			objectsCreated = false;
			SimActive = false;

			//reset the SimObject list
			ClearSimObjectList ();

		} else {
			SimActive = true;

			simActiveHighlight.gameObject.SetActive(true);
			highlight.gameObject.SetActive (false);

			//if the anything is selected then deselect it beofre the sim starts
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
