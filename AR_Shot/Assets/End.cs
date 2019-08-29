using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class End : MonoBehaviour {
    TextMeshProUGUI txt;

	// Use this for initialization
	void Start () {
        txt = GetComponent<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update () {
        txt.text = "Score : "+Shot.score.ToString();
	}
}
