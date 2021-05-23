using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROtate : MonoBehaviour
{
    public float angle;

    public bool darkparticle;
    // Update is called once per frame

    private void Start()
    {
        if (darkparticle)
        {
            angle = Random.Range(20, 75f);
        }
    }
    void Update()
    {
        if (darkparticle)
        {
            transform.RotateAround(Vector3.zero, Vector3.up, angle * Time.deltaTime);
        }
        else
        {
            transform.RotateAroundLocal(Vector3.up, angle * Time.deltaTime);
        }
    }
}
