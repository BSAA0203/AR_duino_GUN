using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Monster : MonoBehaviour {

    GameObject target;
    float speed = 1f;

    // Use this for initialization
    void Start () {
        target = GameObject.Find("ARCore Device");
	}

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.transform);
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("BOMB!");
            SceneManager.LoadScene("End");
        }
    }
}
