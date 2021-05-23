using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public Vector3 speed;
    public float Health = 100;

    public Transform[] raystartpos;

    [Space]
    public GameObject particleeffect;
    // Start is called before the first frame update
    void Start()
    {
        speed = new Vector3(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10));
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Rotate(speed);
        foreach (var item in raystartpos)
        {
            RaycastHit hit;
            if(Physics.SphereCast(new Ray(item.position,item.forward), .5f,out hit))
            {
                if (hit.transform.CompareTag("MainCamera"))
                {
                    hit.transform.GetComponent<PlayerController>().decreaseHealth();
                }
            }
        }
    }

    public void decreaseHealth()
    {
        Health -= .5f*Time.deltaTime;
        if (Health <= 0)
        {
            Destroy(Instantiate(particleeffect, transform.position, Quaternion.identity),3);
            Destroy(this.gameObject);
        }
    }
}
