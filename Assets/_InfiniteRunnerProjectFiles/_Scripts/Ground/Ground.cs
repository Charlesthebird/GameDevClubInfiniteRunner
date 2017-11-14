using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {
    public GameObject destroyer;
    [HideInInspector]
    public float speed = -1.0f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var movement = new Vector3(speed, 0,0) * Time.deltaTime;
        transform.Translate(movement);

        if(this.transform.position.x <= destroyer.transform.position.x)
        {
            Destroy(this.gameObject);
        }
	}
}
