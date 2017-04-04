using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchTask : MonoBehaviour {

	[SerializeField] private GameObject objectOne = null, objectTwo = null;
	[SerializeField] private TaskReward reward;
	private bool completed;

	public Vector3 extentsOne, extentsTwo;
	private float touchDistance = 0;

	public void SetUp(GameObject one, GameObject two, TaskReward rwd)
	{
		objectOne = one;
		objectTwo = two;

		reward = rwd;

		extentsOne = objectOne.GetComponent<SpriteRenderer> ().sprite.bounds.extents;
		extentsTwo = objectTwo.GetComponent<SpriteRenderer> ().sprite.bounds.extents;
		Debug.Log (touchDistance);
		touchDistance += Mathf.Sqrt ((extentsOne.x * extentsOne.x) + (extentsOne.y * extentsOne.y));
		touchDistance += Mathf.Sqrt ((extentsTwo.x * extentsTwo.x) + (extentsTwo.y * extentsTwo.y));
		touchDistance += 5f;
		Debug.Log (touchDistance);
	}


	// Update is called once per frame
	void Update () {




		//check to see if they are toutching
		if (Vector2.Distance(objectOne.transform.position, objectTwo.transform.position) <= touchDistance)
		{

			Debug.Log ("touch");

			//if so then show pop up and hide myself, but not deactivate
			reward.ShowPopUp();

			gameObject.GetComponent<Image> ().enabled = false;
			gameObject.GetComponentInChildren<Text> ().enabled = false;

		}
		
	}
}
