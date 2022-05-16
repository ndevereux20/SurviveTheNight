using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public static Shooting instance; 
    public GameObject bulletPrefab;
    public GameObject player;

    private Vector3 target;
    private Vector3 spawnLocation;
    private Vector3 shootLocation;
    private float rotation;

    public float bulletSpeed = 1.0f;
    public int reloadSpeed = 5; 
    public int startAmmo;
    public int curAmmo;
    public int maxClipSize;
    public int curClipAmmo;
    private bool isReloading; 


    // Start is called before the first frame update
    void Start()
    {
        curAmmo = startAmmo;
        instance = this;
        isReloading = false;
    }

    // Update is called once per frame
    void Update()
    {   
        // Determines where the bullet should be fired and the direction
        // To be used later when spawning the bullet 
        target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        shootLocation = target - player.transform.position;
        rotation = Mathf.Atan2(shootLocation.y, shootLocation.x) * Mathf.Rad2Deg;

        if (PauseMenu.instance.gameIsPaused == false)
        {
            ShootBullet();
            ManualReload();
        }
        
    }

    void ShootBullet()
    {
        if (Input.GetMouseButtonDown(0) && !isReloading && curAmmo + curClipAmmo > 0)
        {
            curClipAmmo = curClipAmmo - 1;
            if (Input.GetKeyDown(KeyCode.R) || curClipAmmo == 0 && curAmmo > 0)
            {
                StartCoroutine(Reload());
            }

            float distance = shootLocation.magnitude;
            Vector2 direction = shootLocation / distance;
            direction.Normalize();
            // Spawning the bullet at the right location around the player 
            GameObject b = Instantiate(bulletPrefab, (Vector2)player.transform.position + (Vector2.one * direction), Quaternion.Euler(0.0f, 0.0f, rotation));

            // Firing the bullet in the direction determined by user input 
            b.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

            // Makes sure the bullet doesn't collide with the player 
            Physics2D.IgnoreCollision(b.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
            StartCoroutine(DestroyBullet(b));
        }
    }

    void ManualReload()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    public IEnumerator DestroyBullet(GameObject other)
    {
        yield return new WaitForSeconds(2);
        Destroy(other);
    }
    public IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadSpeed);
        isReloading = false;
        if (curAmmo >= 30)
        {
            curAmmo = curAmmo - (maxClipSize - curClipAmmo);
            curClipAmmo = maxClipSize;
        }
        else if(curAmmo < 30)
        {
            curClipAmmo = curAmmo;
            curAmmo = 0;
        }
    }
}
