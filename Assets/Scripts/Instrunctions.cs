using UnityEngine;
using System.Collections;

public class Instrunctions : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject menu = GameObject.Find("Menu");
		Destroy(menu);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
