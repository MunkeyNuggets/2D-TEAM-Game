using UnityEngine;
using System.Collections;

public class moveUpDown : MonoBehaviour {

    public int speed = 2;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        MoveUpDown();
	}

    void MoveUpDown()
    {
        if (transform.position.y > 3.5)
        {
            transform.position += -transform.up * speed * Time.deltaTime;
        }
        if (transform.position.y < -3.5)
        {
            transform.position += transform.up * speed * Time.deltaTime;
        }

    }

}
