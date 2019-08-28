using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class End : MonoBehaviour {
    TextMeshPro txt;

	// Use this for initialization
	void Start () {
        txt = GetComponent<TextMeshPro>();
	}
	
	// Update is called once per frame
	void Update () {
        txt.text = Shot.score.ToString();
	}
}
