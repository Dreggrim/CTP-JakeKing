using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimTaskObject : MonoBehaviour {

	public GameObject editorTaskObject;
	public ProgramManager theMan;

	
	// Update is called once per frame
	void Update () {

		if (!theMan.SimActive) {

			//destroy this object and reacativate the editor version
			editorTaskObject.SetActive(true);
			editorTaskObject.GetComponent<TaskObject> ().ToggleSimActive ();
			Destroy (gameObject);
		}		
	}
}
