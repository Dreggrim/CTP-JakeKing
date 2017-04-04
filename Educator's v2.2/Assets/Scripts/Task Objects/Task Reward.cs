using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskReward : MonoBehaviour {

	[SerializeField] private GameObject popUpPrefab = null, popUp = null;

	public void SetUpPopUp(string popUpText, GameObject popPrefab, Transform canvas)
	{
		popUpPrefab = popPrefab;

		popUp = Instantiate (popUpPrefab);
		popUp.GetComponentInChildren<Text> ().text = popUpText;
		popUp.transform.SetParent (canvas.transform);
		popUp.gameObject.SetActive (false);
	}



	public void ShowPopUp()
	{
		popUp.gameObject.SetActive (true);
		popUp.GetComponent<RectTransform> ().position = Vector3.zero;
	}
}
