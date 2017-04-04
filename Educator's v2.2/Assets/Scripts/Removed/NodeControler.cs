using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NodeControler : MonoBehaviour {

	public enum interactionType
	{
		Rotate,
		Move
	};


	public interactionType IT;

	//has the pobject been selected within editor?
	public bool selected, ghostFollow, mouseOverYN, locked;
	private Vector3 XMargin, YMargin;
	private float XMarginFloat, YMarginFloat;
	public GameObject objectGhost, rightClickBar;

	public Image highlight;
	public Sprite image;
	public ProgramManager theMan;


	private bool simActivated;
	[SerializeField] private Vector3 startPos, startScale;
	[SerializeField] private Quaternion startRot;

	// Use this for initialization
	void Start () {
		
		locked = false;
		selected = false;
		ghostFollow = false;

		//set up the ghost
		UpdateGhost();


		switch (IT) {
		case interactionType.Rotate:
			gameObject.GetComponent<SpriteRenderer>().color = Color.red;
			break;

		case interactionType.Move:
			gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
			break;
			
		}
	}

	// Update is called once per frame
	void Update () {

		if (!theMan.SimActive) {

			if (simActivated) {
				Debug.Log ("reset");
				//reset transform
				transform.rotation = startRot;
				transform.position = startPos;
				transform.localScale = startScale;
				simActivated = false;
			}




			if (ghostFollow) {
				objectGhost.transform.position = Camera.main.ScreenToWorldPoint (Input.mousePosition + new Vector3 (0, 0, -460f));
			}


			if (selected) {

				//set the size
				highlight.rectTransform.sizeDelta = new Vector2 (100f, 100f);
				highlight.rectTransform.localScale = this.transform.localScale / Camera.main.orthographicSize;

				highlight.gameObject.SetActive (true);
				highlight.transform.position = Camera.main.WorldToScreenPoint (this.transform.position);
			}
		} else {
			if (!simActivated) {

				//get transfor before changes
				startRot = transform.rotation;
				startPos = transform.position;
				startScale = transform.localScale;

				simActivated = true;
			}
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


	void OnMouseOver()
	{
		mouseOverYN = true;

		if (Input.GetMouseButtonDown (0) && !rightClickBar.GetComponent<RightClickControlerNodes> ().SelectedYN())  {

			Debug.Log ("clicked");
			selected = true;
			rightClickBar.GetComponent<RightClickControlerNodes> ().SetSelectedObject (this.gameObject);
		}

		if (Input.GetMouseButtonDown (1) && selected) {

			rightClickBar.GetComponent<RightClickControlerNodes> ().SetPosition ();
			rightClickBar.GetComponent<RightClickControlerNodes> ().bar.gameObject.SetActive (true);
		}
	}

	void OnMouseExit()
	{
		mouseOverYN = false;
	}



	public void Deselect()
	{
		rightClickBar.GetComponent<RightClickControlerNodes> ().bar.gameObject.SetActive (false);
		selected = false;
		highlight.gameObject.SetActive (false);
	}



	public void RunBehaviour(GameObject thing)
	{

		switch (IT) {
		case interactionType.Rotate:
			if (Input.GetMouseButton (0)) {
				thing.transform.Rotate (new Vector3 (0, 0, 1));
			}
			break;

		case interactionType.Move:
			thing.transform.Translate (Vector3.right);
			break;

		}
	}

}





