using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


//an Editor verstion of the object class, it handles some of the editing
public class ObjectData : MonoBehaviour {

	//has the object been selected within editor?
	public bool selected, ghostFollow, mouseOverYN, locked;

	//Margins of the sprite
	private Vector3 XMargin, YMargin;
	private float XMarginFloat, YMarginFloat;

	//right click bar and the object ghost
	public GameObject objectGhost, rightClickBar;

	//the is the highlight that apears over selected objects
	public Image highlight;

	//the images the sprite is using
	//null until one is selected
	public Sprite image;

	//the program manager
	public ProgramManager theMan;

	//local variable for if the simulation is active
	private bool simActivated = false;

	//array to record the code blocks
	[SerializeField] private List<GameObject> blockList = new List<GameObject>();

	//The SimObject prefab
	[SerializeField] private GameObject SimObjectPrefab = null;


	void Start () {

		//set up the object so its is ready to be edited
		gameObject.name = "New Object";

		locked = false;
		selected = false;
		ghostFollow = false;

		//set up the ghost
		UpdateGhost();

	}
	

	void Update () {
		
		//if the simulation is not active then allow the player to edit.
		if (!theMan.SimActive && !simActivated) {

			//is selected then the highlist needs to be updated
			if (selected) {
				UpdateHighlight ();

				//is selected an the delete key is pressed then delete the object
				if (Input.GetKeyDown (KeyCode.Delete)) {

					theMan.RemoveObjectFromList (gameObject);
					Deselect ();
					Destroy (gameObject);

				}
			}


			//if the object is being moved then the ghost will follow the mouse
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

			//add it to the program manager's list of objects
			theMan.AddSimObjectToList (newObject);

			//give the object the code blocks
			foreach (GameObject block in blockList) {
				block.transform.SetParent(newObject.transform);
			}
				
			//send through its transform
			newObject.transform.localScale = gameObject.transform.localScale;
			newObject.transform.rotation = gameObject.transform.rotation;
			newObject.transform.position = gameObject.transform.position;

			//deactivate this object
			this.gameObject.SetActive (false);

		}


	}

	//get the margins of the sprite
	void SetMargins()
	{
		XMarginFloat = this.GetComponent<SpriteRenderer> ().bounds.size.x / 2f;
		YMarginFloat = this.GetComponent<SpriteRenderer> ().bounds.size.y / 2f;

		XMargin = Vector3.zero;
		YMargin = Vector3.zero;

		XMargin.x += XMarginFloat;
		YMargin.y += YMarginFloat;

	
	}

	//make sure the ghost has the same sprite and transform
	public void UpdateGhost()
	{
		//update its sprite
		objectGhost.GetComponent<SpriteRenderer> ().sprite = this.GetComponent<SpriteRenderer> ().sprite;
		SetMargins ();
	}

	//make sure the highlight matchs its selected object
	public void UpdateHighlight()
	{
		highlight.transform.position = Camera.main.WorldToScreenPoint (this.transform.position);
		highlight.rectTransform.sizeDelta = new Vector2 (XMarginFloat * 160, YMarginFloat * 160);
		highlight.rectTransform.localScale = transform.localScale / Camera.main.orthographicSize;
		highlight.rectTransform.rotation = transform.rotation;
	}


	//when the object is moused over it can open the right click bar with the correct inputs 
	void OnMouseOver()
	{
		mouseOverYN = true;

		//is theleft mouse button is pressed while over the object then select this object
		if (Input.GetMouseButtonDown (0) && !rightClickBar.GetComponent<RightClickControler> ().SelectedYN())  {

			selected = true;
			highlight.gameObject.SetActive (true);

			//set the position of the highlight
			UpdateHighlight();

			rightClickBar.GetComponent<RightClickControler> ().SetSelectedObject (this.gameObject);

			//send the blocks to the UI
			rightClickBar.GetComponent<RightClickControler> ().SendCodeBlocksToUI (blockList);
		
		}

		//is the right mouse button is pressed then open up the rightclick bar
		if (Input.GetMouseButtonDown (1) && selected) {

			rightClickBar.GetComponent<RightClickControler> ().SetPosition ();
			rightClickBar.GetComponent<RightClickControler> ().OpenBar (BARTYPE.OBJECT);
		}
	}



	void OnMouseExit()
	{
		mouseOverYN = false;
	}

	//deselect this object
	public void Deselect()
	{
		
		rightClickBar.GetComponent<RightClickControler> ().Closebar ();
		rightClickBar.GetComponent<RightClickControler> ().RemoveCodeBlocksFromUI ();
		selected = false;
		highlight.gameObject.SetActive (false);

	}

	//toggle the local sim active variable
	public void ToggleSimActive()
	{
		if (simActivated == true) {
			simActivated = false;
		} else {
			simActivated = true;
		}
	}

	//add a block to this object list of code blocks
	public void AddBlockToList(GameObject block)
	{
		blockList.Add (block);
	}

	//remove a block from this object list of code blocks
	public void RemoveBlockFromList(GameObject block)
	{
		blockList.Remove (block);
	}






}