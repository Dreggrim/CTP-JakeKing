using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TaskObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public static GameObject objectBeingDragged;
	public ProgramManager theMan;
	private int type, reward;
	private bool simActive;

	[SerializeField] private GameObject simTaskPrefab = null, popUpPrefab = null;
	private string taskText = "needs text";

	//task type options
	[SerializeField] private Dropdown objectOne = null, objectTwo = null;


	//reward options
	[SerializeField] private InputField PopUpText = null;
	private string PopUpString = "needs text";


	void Start()
	{
		objectOne.gameObject.SetActive (false);
		objectTwo.gameObject.SetActive (false);
	}



	void Update()
	{
		if (theMan.SimActive && !simActive && theMan.objectsCreated) {
			simActive = true;

			GameObject objectForSimOne = null, objectForSimTwo = null;

			foreach (GameObject Go in theMan.GetSimObjectList()) {
				if (Go.name == objectOne.GetComponentInChildren<Text> ().text && objectOne != null) {
					Debug.Log ("One gotten");
					objectForSimOne = Go;
				}

				if (Go.name == objectTwo.GetComponentInChildren<Text> ().text && objectTwo != null) {
					Debug.Log ("Two gotten");
					objectForSimTwo = Go;
				}

			}

			//if any of the two object are missing then do not create the object
			if (objectForSimOne != null && objectForSimTwo != null) {
				
				//create the SimTaskOBject
				GameObject simTask = Instantiate (simTaskPrefab, gameObject.transform.position, Quaternion.identity);
				simTask.transform.SetParent (transform.parent);
				//starting with the reward
				//add the reward script and object to the object
				simTask.AddComponent<TaskReward> ();
				simTask.GetComponent<TaskReward> ().SetUpPopUp (PopUpString, popUpPrefab, transform.parent);

				//depending on the type:
				//add a different type script to the object
				//link up the reward with the conditions

				//get the two objcets that are required for hte conditions
				foreach (GameObject Go in theMan.GetSimObjectList()) {
					if (Go.name == objectOne.GetComponentInChildren<Text> ().text && objectOne != null) {
						Debug.Log ("One gotten");
						objectForSimOne = Go;
					}

					if (Go.name == objectTwo.GetComponentInChildren<Text> ().text && objectTwo != null) {
						Debug.Log ("Two gotten");
						objectForSimTwo = Go;
					}

				}


				simTask.AddComponent<TouchTask> ();
				simTask.GetComponent<TouchTask> ().SetUp (objectForSimOne, objectForSimTwo, simTask.GetComponent<TaskReward> ());

				//set the task text for the new object
				simTask.GetComponentInChildren<Text> ().text = taskText;

				//give the object a reference to this object
				simTask.GetComponent<SimTaskObject> ().theMan = theMan;
				simTask.GetComponent<SimTaskObject> ().editorTaskObject = gameObject;

				//deactivate this object
				gameObject.SetActive (false);

			}

		} else {



		}

	}





	#region IBeginDragHandler implementation

	public void OnBeginDrag (PointerEventData eventData)
	{
		objectBeingDragged = gameObject;
		GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		transform.position = new Vector3(Input.mousePosition.x,Input.mousePosition.y, -10f);

		//x: 326 - -445    y: -577 - 290
		if (transform.position.x > 944) {
			transform.position = new Vector3(944, transform.position.y);
		}

		if (transform.position.x < 225) {
			transform.position = new Vector3(225, transform.position.y);
		}

		if (transform.position.y > 711) {
			transform.position = new Vector3(transform.position.x, 711);
		}

		if (transform.position.y < 70) {
			transform.position = new Vector3(transform.position.x, 70);
		}




	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		//drop the object
		objectBeingDragged = null;
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
	}

	#endregion

	public void OnTaskTypeIndexChange(int index)
	{
		type = index;

		switch (index) {
		//touch task
		case 0: 
			objectOne.ClearOptions ();
			objectTwo.ClearOptions ();

			List<string> nameList = new List<string> ();

			foreach (GameObject Go in theMan.GetObjectList()) {
				nameList.Add (Go.name);
			}

			objectOne.AddOptions (nameList);
			objectTwo.AddOptions (nameList);

			objectOne.gameObject.SetActive(true);
			objectTwo.gameObject.SetActive(true);
			break;
		
		case 1:

			objectOne.gameObject.SetActive(false);
			objectTwo.gameObject.SetActive(false);


			break;
		}

	}


	public void OnRewardIndexChange(int index)
	{

		reward = index;

		switch (index) {
		case 0:
			
			PopUpText.gameObject.SetActive (true);

			break;

		case 1:
			PopUpText.gameObject.SetActive (false);

			break;

		}
	}

	public void SetText(string text)
	{
		taskText = text;
	}

	public void SetPopUpText(string text)
	{
		PopUpString = text;
	}


	public void ToggleSimActive()
	{
		if (simActive) {
			simActive = false;
		} else {
			simActive = true;
		}
	}


	public void DeleteObject()
	{
		theMan.RemoveTaskFromList (gameObject);
		Destroy (gameObject);
	}

}
