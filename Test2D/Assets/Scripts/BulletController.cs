using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject bullet;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        // Removes the bullet when it hits a zombie
        if (other.gameObject.tag == "Zombie")
        {
            Destroy(bullet);
        }
    }
}
