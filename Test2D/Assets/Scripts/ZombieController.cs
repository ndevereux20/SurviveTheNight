using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public GameObject player;
    public GameObject zombie;
    public GameObject bullet;
    public GameObject[] TreesRocks; // holds the collider information for all the bounding boxes on the trees and rocks
    Animator anim;
    

    public float zombieMoveSpeed = 1.0f;
    public int hitPoints = 1;

    private Vector3 target; // the players location 
    private Vector3 moveLocation; // the location to zombie moves to each time frame
    private Vector3 newMoveLocation; // used for when an object is about to collide with a tree or a rock 

    public GameObject[] consumables; // an array of the consumables, i.e. ammo or health 
    private int zombieDrop; // decides wheter a zombie is to drop a consumable or not 
    private int choice; // chooses between the two consumables to drop 

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        // initially fills the TreesRocks object with colliders 
        player = GameObject.FindGameObjectsWithTag("Player")[0];  //returns GameObject[]
        TreesRocks = GameObject.FindGameObjectsWithTag("TreesRocks"); //returns GameObject[] of colliders 
    }

    // Update is called once per frame
    void Update()
    {
        if(PauseMenu.instance.gameIsPaused == false)
        {
            MoveZombie();
        }

    }

    void MoveZombie()
    {
        // Moves the zombie to the players location, which is updayed 
        // each frame. 
        if (player != null)
        {
            target = player.transform.position;
            moveLocation = target - transform.position;
            moveLocation.Normalize(); // make the distance a unit vector 
            Vector3 compare = transform.position + moveLocation * zombieMoveSpeed * Time.deltaTime;
            bool move = false;
            for (int i = 0; i < TreesRocks.Length; i++)
            {
                // Checks wheter a zombie is about to collide with a tree or a rock
                if (compare.x <= (TreesRocks[i].transform.position.x + 1.3) && compare.x >= (TreesRocks[i].transform.position.x - 1.3)
                     && compare.y <= (TreesRocks[i].transform.position.y + 1.3) && compare.y >= (TreesRocks[i].transform.position.y - 1.3))
                {
                    // this means the zombie is about to collide with a tree or a rock 
                    // so move 90 degrees in one direction until it is no longer going to collide with the tree 
                    newMoveLocation = new Vector3(-moveLocation.y, moveLocation.x, 0);

                    anim.SetBool("is_walking1", true);
                    anim.SetFloat("input1_x", newMoveLocation.x);
                    anim.SetFloat("input1_y", newMoveLocation.y);
                    

                    transform.position += newMoveLocation * zombieMoveSpeed * Time.deltaTime;
                    move = true;
                    break;
                }

            }
            // If the zombie is not going to collide with a tree 
            // move normally towards the player
            if (!move)
            {
                anim.SetBool("is_walking1", true);
                anim.SetFloat("input1_x", moveLocation.x);
                anim.SetFloat("input1_y", moveLocation.y);

                transform.position += moveLocation * zombieMoveSpeed * Time.deltaTime;

            }

        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        // Kill the zombie if a bullet hits it
        if (other.gameObject.tag == "Bullet")
        {
            hitPoints = hitPoints - 1;
            if (hitPoints == 0)
            {
                DropConsumable();
                Destroy(zombie);
            }
        }
    }

    public void DropConsumable()
    {
        zombieDrop = Random.Range(0, 5); 
        if(zombieDrop == 1)
        {
            choice = Random.Range(0, consumables.Length);
            Instantiate(consumables[choice], zombie.transform.position, Quaternion.identity);
        }
    }
}
