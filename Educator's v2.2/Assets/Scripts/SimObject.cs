using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//simulation object
public class SimObject : MonoBehaviour {

	//the image that this object is using
	public Sprite image;

	//array to record the code blocks
	[SerializeField] private List<GameObject> blockList;

	//its deactivated editor version
	[SerializeField] private GameObject editorObject;

	//the program manager
	[SerializeField] public ProgramManager theMan;

	//if the object should run behaviour when clicked on
	[SerializeField] private bool onClickEvents;

	//have the blocks been set up yet
	private bool blockStartUp;

	//for setting up the object information on creation
	public void SetUp(ProgramManager man, Sprite spr, List<GameObject> blocks, GameObject EO)
	{
		theMan = man;
		image = spr;
		gameObject.GetComponent<SpriteRenderer> ().sprite = image;
		blockList = blocks;
		editorObject = EO;
		onClickEvents = false;
	}


	// Update is called once per frame
	void Update () {

		if (!blockStartUp) {

			foreach (GameObject block in blockList) {
				block.SendMessage("StartUp");
			}
			blockStartUp = true;
		}


		//run the behaviour for the blocks
		foreach (GameObject block in blockList) {
			block.SendMessage("BlockCode");
		}


		if (theMan.SimActive != true) {

			//when the Simulation ends this object will need to make all its children not children
			foreach (GameObject block in blockList) {
				block.transform.SetParent(null);
			}

			//and also turn the editor object back on and change its SimActive bool to false;
			editorObject.SetActive(true);
			editorObject.GetComponent<ObjectData> ().ToggleSimActive ();

			Destroy (gameObject);

		}

	}


	void OnMouseOver()
	{
		
		if (onClickEvents) {
			
			foreach (GameObject block in blockList) {
				block.SendMessage("ClickEvent");
			}

		}

	}


	public void SetOnCLickEvents(bool YN)
	{
		onClickEvents = YN;
	}


}
