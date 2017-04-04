using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum BARTYPE{
	LINKER = 0,
	OBJECT = 1,
	TASK = 2
}




public class RightClickControler : MonoBehaviour {

	public Vector3 offSet, startPoint, releasePoint;
	[SerializeField] private GameObject sellectedObject;
	public Image bar;
	[SerializeField] private bool mouseOver, selectedObjectMoving;
	[SerializeField] private float timer;
	[SerializeField] private ProgramManager theMan = null;
	[SerializeField] private Image UIBlockEditor = null;

	//keep a record of all the options availible on the rightclick bar and enable and disable them as they are needed
	//selectImage
	public GameObject selectImage;
	//toggleLock
	public GameObject toggleLock;
	//scale
	public GameObject scale;
	//layer order
	public GameObject layerOrder;
	//rotation
	public GameObject rotation;
	//Object Name
	public GameObject objectName;

	int numberOfActiveOptions = 0;



	// Use this for initialization
	void Start () {
	
		offSet = new Vector3 (-125f, -90f, 0);
		selectedObjectMoving = false;

		//generic options
		selectImage.SetActive (false);
		toggleLock.SetActive (false);
		scale.SetActive (false);
		layerOrder.SetActive (false);
		rotation.SetActive (false);
		objectName.SetActive (false);


	}

	 
	
	// Update is called once per frame
	void Update () {

		bar.rectTransform.sizeDelta  = new Vector2(143.5f, 35 * numberOfActiveOptions);


		if (!theMan.SimActive) {
	
			if (sellectedObject != null) {

				//delelect
				if (Input.GetMouseButtonDown (0) && !sellectedObject.GetComponent<ObjectData> ().mouseOverYN && !EventSystem.current.IsPointerOverGameObject()) {

					//if the mouse is over the bar
					if (mouseOver) {				//this will work for now however it checks for all UI ellenetns not a specific one.

					} else {
						DelselectObject ();
					}
				}

				//if locked then no editing can be done to the object
				if (sellectedObject != null && !sellectedObject.GetComponent<ObjectData> ().locked) {


					//if the mouse is over the selected object and is being held down
					//it can be moved
					if (Input.GetMouseButton (0) && sellectedObject.GetComponent<ObjectData> ().mouseOverYN && !selectedObjectMoving) {

						timer += Time.deltaTime;

						if (timer >= 0.10f) {
							sellectedObject.GetComponent<ObjectData> ().ghostFollow = true;
							selectedObjectMoving = true;
						}
					}

					if (Input.GetMouseButtonUp (0) && sellectedObject.GetComponent<ObjectData> ().ghostFollow) {

						timer = 0f;

						//set the possition of the selected object to that of ghost
						sellectedObject.GetComponent<ObjectData> ().ghostFollow = false;
						selectedObjectMoving = false;
						sellectedObject.transform.position = new Vector3 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y, 0f);
						sellectedObject.GetComponent<ObjectData> ().objectGhost.transform.localPosition = Vector3.zero;
						sellectedObject.GetComponent<ObjectData> ().UpdateHighlight ();

					}

				}

				if (Input.GetMouseButtonUp (0)) {

					timer = 0f;
				}
			}
		}
	}


	public void OpenBar(BARTYPE type)
	{

		switch (type)
		{
		case BARTYPE.LINKER:
			selectImage.SetActive (false);
			toggleLock.SetActive (true);
			scale.SetActive (false);
			layerOrder.SetActive (false);
			rotation.SetActive (false);
			objectName.SetActive (false);
			break;
		case BARTYPE.OBJECT:
			selectImage.SetActive (true);
			toggleLock.SetActive (true);
			scale.SetActive (true);
			layerOrder.SetActive (true);
			rotation.SetActive (true);
			objectName.SetActive (true);

			objectName.GetComponent<InputField> ().text = sellectedObject.name;
			break;
		case BARTYPE.TASK:
			selectImage.SetActive (false);
			toggleLock.SetActive (true);
			scale.SetActive (true);
			layerOrder.SetActive (false);
			rotation.SetActive (true);
			objectName.SetActive (true);
			break;

		}

		bar.gameObject.SetActive (true);

	}


	public void Closebar()
	{
		bar.gameObject.SetActive(false);
	}


	public void SetPosition()
	{

		transform.position = Input.mousePosition + offSet;

		//x: 326 - -445    y: -577 - 290
		if (transform.position.x > 1050) {
			transform.position = new Vector3(1050, transform.position.y);
		}

		if (transform.position.x < 100) {
			transform.position = new Vector3(100, transform.position.y);
		}

		if (transform.position.y > 711) {
			transform.position = new Vector3(transform.position.x, 711);
		}

		if (transform.position.y < 200) {
			transform.position = new Vector3(transform.position.x, 200);
		}



	}


	public void SetSelectedObject(GameObject thing)
	{
		sellectedObject = thing;

	}


	public void ChangeImage()
	{
		Sprite image;

		//get the path to the image you want
		string path = EditorUtility.OpenFilePanel("Sellect Image", "", "");

		if (path != null) {
			//read the data
			byte[] data = File.ReadAllBytes (path);

			//save the data into a texture
			Texture2D texture = new Texture2D (64, 64, TextureFormat.ARGB32, false);
			texture.LoadImage (data);
			texture.name = Path.GetFileNameWithoutExtension (path);

			//create the sprite from the textrue and set it to the selected object
			image = Sprite.Create (texture, new Rect (0.0f, 0.0f, texture.width, texture.height), new Vector2 (0.5f, 0.5f), 100.0f);
			sellectedObject.GetComponent<SpriteRenderer> ().sprite = image;

			//resize the ghost
			sellectedObject.GetComponent<ObjectData> ().UpdateGhost ();

			//get and resent the size of the collider
			sellectedObject.GetComponent<BoxCollider2D> ().size = image.bounds.size;

			//update the highlight
			sellectedObject.GetComponent<ObjectData> ().UpdateHighlight ();
		}
	}


	public bool SelectedYN()
	{
		if (sellectedObject == null) {
			return false;
		} else {
			return true;
		}
	}


	public void ToggleMouseOver()
	{
		if (mouseOver) {
			mouseOver = false;
		} else {
			mouseOver = true;
		}
	}


	public void ToggleLock()
	{
		if (sellectedObject.GetComponent<ObjectData> ().locked) {
			sellectedObject.GetComponent<ObjectData> ().locked = false;
		} else {
			sellectedObject.GetComponent<ObjectData> ().locked = true;
		}
	}



	public void ChangeObjectName(string name)
	{
		sellectedObject.name = name;
	}


	public void IncreaseScale()
	{
		sellectedObject.transform.localScale = sellectedObject.transform.localScale + Vector3.one;
		sellectedObject.GetComponent<ObjectData> ().UpdateHighlight ();
	}

	public void DecreaseScale()
	{
		if (sellectedObject.transform.localScale.x > 1 || sellectedObject.transform.localScale.y > 1 || sellectedObject.transform.localScale.z > 1) {
			sellectedObject.transform.localScale = sellectedObject.transform.localScale - Vector3.one;
		} else {
			sellectedObject.transform.localScale = Vector3.one;
		}
		sellectedObject.GetComponent<ObjectData> ().UpdateHighlight ();
	}

	public void IncreaseZ()
	{
		sellectedObject.transform.position = sellectedObject.transform.position - new Vector3 (0, 0, 1);
		sellectedObject.GetComponent<ObjectData> ().UpdateHighlight ();
	}

	public void DecreaseZ()
	{
		sellectedObject.transform.position = sellectedObject.transform.position + new Vector3 (0, 0, 1);
		sellectedObject.GetComponent<ObjectData> ().UpdateHighlight ();
	}

	public void RotateClockwise()
	{
		sellectedObject.transform.Rotate (new Vector3 (0, 0, -5f));
		sellectedObject.GetComponent<ObjectData> ().UpdateHighlight ();
	}

	public void RotateCounterClockwise()
	{
		sellectedObject.transform.Rotate (new Vector3 (0, 0, 5f));
		sellectedObject.GetComponent<ObjectData> ().UpdateHighlight ();
	}







	public void SendCodeBlocksToUI(List<GameObject> blocks)
	{
		//for each block tell the UI that the corisponding UI element in the UI need to be shown.
		foreach (GameObject block in blocks) {

			UIBlockEditor.GetComponent<CodeBlockEditor> ().AddToList (block.name, block, sellectedObject);

		}
	}
		
	public void RemoveCodeBlocksFromUI ()
	{
		UIBlockEditor.GetComponent<CodeBlockEditor> ().ClearList ();
	}


	public void AddNewBlockToObject(GameObject block)
	{
		sellectedObject.GetComponent<ObjectData> ().AddBlockToList (block);
	}


	public void DelselectObject()
	{
		sellectedObject.GetComponent<ObjectData> ().Deselect ();

		sellectedObject = null;
	}

	public GameObject GetSelected()
	{
		return sellectedObject;
	}
		


}
