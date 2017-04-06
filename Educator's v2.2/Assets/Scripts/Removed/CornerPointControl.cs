using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CornerPointControl : MonoBehaviour {

	public enum Points
	{
		BotLeft,
		BotRight,
		TopLeft,
		TopRight
	}


	public Points position;
	public GameObject parent;
	public float X, Y;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		X = parent.GetComponent<Image> ().sprite.bounds.size.x / 2f * 10;
		Y = parent.GetComponent<Image> ().sprite.bounds.size.y / 2f * 10;
	
		switch (position) {

		case Points.BotLeft:
			transform.position = new Vector3 (parent.transform.position.x -X, parent.transform.position.y -Y, transform.position.z);
			break;

		case Points.BotRight:
			transform.position = new Vector3 (parent.transform.position.x + X, parent.transform.position.y -Y, transform.position.z);
			break;

		case Points.TopLeft:
			transform.position = new Vector3 (parent.transform.position.x + X, parent.transform.position.y + Y, transform.position.z);
			break;

		case Points.TopRight:
			transform.position = new Vector3 ( parent.transform.position.x -X, parent.transform.position.y + Y, transform.position.z);
			break;

		}


		transform.localScale = Vector3.one;

	}
}
