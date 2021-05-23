using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Horizontalspeed;
    public float Verticalspeed;
    public Transform centre;

    [Space]
    public float Health = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");

        //transform.RotateAround(centre.position, Vector3.up, -h * Horizontalspeed * Time.deltaTime);
        //transform.RotateAround(centre.position, Vector3.forward, -v * Verticalspeed * Time.deltaTime);
    }

    public void decreaseHealth()
    {
        Health -= 1;
        if (Health <= 0)
        {
            // signal gamemanager
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            foreach (var item in enemies)
            {
                Destroy(item.gameObject);
            }

            Enemy2[] enemy2 = FindObjectsOfType<Enemy2>();
            foreach (var item in enemy2)
            {
                Destroy(item.gameObject);
            }
            GameManager_.instance.StartHumanLife();

        }
    }
}
