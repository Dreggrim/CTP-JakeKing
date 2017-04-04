using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEditor;

public class ObjectData : MonoBehaviour {

	//has the pobject been selected within editor?
	public bool selected, ghostFollow, mouseOverYN, locked;
	private Vector3 XMargin, YMargin;
	private float XMarginFloat, YMarginFloat;
	public GameObject objectGhost, rightClickBar;
	public Image highlight;
	public Sprite image;
	public ProgramManager theMan;

	//link node
	//what linker this object is attached to
	public GameObject linkNode;
	private bool simActivated = false;

	//array to record the code blocks
	[SerializeField] private List<GameObject> blockList = new List<GameObject>();
	[SerializeField] private GameObject SimObjectPrefab = null;

	// Use this for initialization
	void Start () {

		gameObject.name = "New Object";

		locked = false;
		selected = false;
		ghostFollow = false;

		//set up the ghost
		UpdateGhost();

	}
	
	// Update is called once per frame
	void Update () {


		//if the simulation is not active then allow the player to edit.
		if (!theMan.SimActive && !simActivated) {

			if (selected) {
				UpdateHighlight ();

				if (Input.GetKeyDown (KeyCode.Delete)) {

					theMan.RemoveObjectFromList (gameObject);
					Deselect ();
					Destroy (gameObject);

				}
			}



			if (ghostFollow) {
				objectGhost.transform.position = Camera.main.ScreenToWorldPoint (Input.mousePosition + new Vector3 (0, 0, 12f));
			}

		}
	

		//if at any point the simulation is activated then spawn this object's SimObject.
		if (theMan.SimActive) {
			simActivated = true;

			//spawn the SimObject
			GameObject newObject = Instantiate (SimObjectPrefab) as GameObject; 

			//set up the references
			newObject.GetComponent<SimObject>().SetUp(theMan, gameObject.GetComponent<SpriteRenderer>().sprite, blockList, gameObject);
			newObject.name = name;

			theMan.AddSimObjectToList (newObject);

			//give the object the code blocks
			foreach (GameObject block in blockList) {
				block.transform.SetParent(newObject.transform);
			}


			//send through its transform
			newObject.transform.localScale = gameObject.transform.localScale;
			newObject.transform.rotation = gameObject.transform.rotation;
			newObject.transform.position = gameObject.transform.position;

			this.gameObject.SetActive (false);

		}


	}


	void SetMargins()
	{
		XMarginFloat = this.GetComponent<SpriteRenderer> ().bounds.size.x / 2f;
		YMarginFloat = this.GetComponent<SpriteRenderer> ().bounds.size.y / 2f;

		XMargin = Vector3.zero;
		YMargin = Vector3.zero;

		XMargin.x += XMarginFloat;
		YMargin.y += YMarginFloat;

	
	}


	public void UpdateGhost()
	{
		//update its sprite
		objectGhost.GetComponent<SpriteRenderer> ().sprite = this.GetComponent<SpriteRenderer> ().sprite;
		SetMargins ();
	}


	public void UpdateHighlight()
	{
		highlight.transform.position = Camera.main.WorldToScreenPoint (this.transform.position);
		highlight.rectTransform.sizeDelta = new Vector2 (XMarginFloat * 160, YMarginFloat * 160);
		highlight.rectTransform.localScale = this.transform.localScale / Camera.main.orthographicSize;
	}



	void OnMouseOver()
	{
		mouseOverYN = true;

		if (Input.GetMouseButtonDown (0) && !rightClickBar.GetComponent<RightClickControler> ().SelectedYN())  {


			selected = true;
			highlight.gameObject.SetActive (true);

			//set the position of the highlight
			UpdateHighlight();



			rightClickBar.GetComponent<RightClickControler> ().SetSelectedObject (this.gameObject);

			//send the blocks to the UI
			rightClickBar.GetComponent<RightClickControler> ().SendCodeBlocksToUI (blockList);
		
		}

		if (Input.GetMouseButtonDown (1) && selected) {


			rightClickBar.GetComponent<RightClickControler> ().SetPosition ();
			rightClickBar.GetComponent<RightClickControler> ().OpenBar (BARTYPE.OBJECT);
		}
	}



	void OnMouseExit()
	{
		mouseOverYN = false;
	}


	public void Deselect()
	{
		
		rightClickBar.GetComponent<RightClickControler> ().Closebar ();
		rightClickBar.GetComponent<RightClickControler> ().RemoveCodeBlocksFromUI ();
		selected = false;
		highlight.gameObject.SetActive (false);

	}




	public void ToggleSimActive()
	{
		if (simActivated == true) {
			simActivated = false;
		} else {
			simActivated = true;
		}
	}


	public void AddBlockToList(GameObject block)
	{
		blockList.Add (block);
	}

	public void RemoveBlockFromList(GameObject block)
	{
		blockList.Remove (block);
	}






}