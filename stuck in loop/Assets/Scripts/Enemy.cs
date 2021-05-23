using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Enemy : MonoBehaviour
{

    public GameObject projectile;
    public float speed;
    public Transform target;
    // Start is called before the first frame update
    private void Awake()
    {
        Transform t = FindObjectOfType<CinemachineBrain>().transform;
        transform.LookAt(t);
    }
    void Start()
    {
        transform.LookAt(target.position);
    }

    private void OnEnable()
    {
        transform.LookAt(Camera.main.transform.position);
    }
    // Update is called once per frame
    void Update()
    {
        projectile.transform.Translate(transform.up * speed * Time.deltaTime);
    }

    public void enablethis()
    {
        this.enabled = true;
        projectile.transform.localScale = new Vector3(.3f, .3f, .3f);
     //   projectile.transform.parent = transform.parent;
    }

   
}
