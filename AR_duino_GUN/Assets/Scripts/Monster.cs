using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Monster : MonoBehaviour {

    GameObject target;
    float speed = 1f;

    // Use this for initialization
    void Start () {
        target = GameObject.Find("ARCore Device"); // get target name
	}

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.transform); // only look at target
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime); // go foward to target
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player") // if monster to get hit player
        {
            Debug.Log("BOMB!");
            SceneManager.LoadScene("End"); // go to End scene
        }
    }
}
