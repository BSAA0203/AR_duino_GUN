using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Spawn : MonoBehaviour
{
    // Use this for initialization
    public GameObject origin;
    public Transform here;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("spwan", 10, 1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void spwan()
    {
        float spawn_x = Random.Range(-23f, 20f);
        Debug.Log("New Spawn!");
        GameObject clone_monster = (GameObject)Instantiate(origin,new Vector3(spawn_x,here.position.y,here.position.z),here.rotation);
    }
}
