using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Monster : MonoBehaviour
{

    // Use this for initialization
    float speed = 1f;
    public GameObject target;
    public GameObject origin;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("spwan", 3, 3);
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

    void spwan()
    {
        float spawn_x = Random.Range(-23f, 19f);
        Debug.Log("New Spawn!");
        GameObject clone_monster = (GameObject)Instantiate(origin, new Vector3(spawn_x, -3, 10), transform.rotation);
    }


}
