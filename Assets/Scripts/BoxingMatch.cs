using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingMatch : MonoBehaviour {

	public GameObject AgentA;
	public GameObject AgentB;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void MatchReset() {
		// Place agents back to their starting spots
		AgentA.transform.position = new Vector3( 0f, 0.4f , -2f);
		AgentB.transform.position = new Vector3( 0f, 0.4f , 2f);
	}
}
