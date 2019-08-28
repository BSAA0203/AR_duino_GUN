using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {

    public GameObject explo;
    public GameObject cross;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Fire!");
            RaycastHit hit;

            if(Physics.Raycast(cross.transform.position,cross.transform.forward,out hit))
            {
                if(hit.transform.name== "Mon_00")
                {
                    Destroy(hit.transform.gameObject);

                    GameObject boom = Instantiate(explo, hit.point, Quaternion.LookRotation(hit.normal));
                }
            }
        }
	}

    
}
