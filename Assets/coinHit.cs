using UnityEngine;
using System.Collections;

public class coinHit : MonoBehaviour {

    public GameObject coin;

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name == "Ball")
        {
            Destroy(coin);
        }
    }

}
