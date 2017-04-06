using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class RightClickControlerNodes : MonoBehaviour {

	public Vector3 offSet, startPoint, releasePoint;
	[SerializeField] private GameObject sellectedObject;
	public Image bar;
	[SerializeField] private bool mouseOver, selectedObjectMoving;
	[SerializeField] private float timer;
	[SerializeField] private ProgramManager theMan;





	// Use this for initialization
	void Start () {

		offSet = new Vector3 (-125f, -90f, 0);
		selectedObjectMoving = false;
	}



	// Update is called once per frame
	void Update () {
		if (!theMan.SimActive) {





			if (sellectedObject != null) {

				//delelect
				if (Input.GetMouseButtonDown (0) && !sellectedObject.GetComponent<NodeControler> ().mouseOverYN) {

					//if the mouse is over the bar
					if (mouseOver) {				//this will work for now however it checks for all UI ellenetns not a specific one.

					} else {
						sellectedObject.GetComponent<NodeControler> ().Deselect ();
						sellectedObject = null;
					}
				}

				//if locked then no editing can be done to the object
				if (sellectedObject != null && !sellectedObject.GetComponent<NodeControler> ().locked) {


					//if the mouse is over the selected object and is being held down
					//it can be moved
					if (Input.GetMouseButton (0) && sellectedObject.GetComponent<NodeControler> ().mouseOverYN && !selectedObjectMoving) {

						timer += Time.deltaTime;

						if (timer >= 0.15f) {
							sellectedObject.GetComponent<NodeControler> ().ghostFollow = true;
							selectedObjectMoving = true;
						}
					}

					if (Input.GetMouseButtonUp (0) && sellectedObject.GetComponent<NodeControler> ().ghostFollow) {

						timer = 0f;


						//set the possition of the selected object to that of ghost
						sellectedObject.GetComponent<NodeControler> ().ghostFollow = false;
						selectedObjectMoving = false;
						sellectedObject.transform.position = new Vector3 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y, -450f);
						sellectedObject.GetComponent<NodeControler> ().objectGhost.transform.localPosition = Vector3.zero;

					}
				}

				if (Input.GetMouseButtonUp (0)) {

					timer = 0f;
				}
			}
		}
	}


	public void SetPosition()
	{

		transform.position = Input.mousePosition + offSet;

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
			sellectedObject.GetComponent<NodeControler> ().UpdateGhost ();

			//get and resent the size of the collider
			sellectedObject.GetComponent<BoxCollider2D> ().size = image.bounds.size;
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



}
