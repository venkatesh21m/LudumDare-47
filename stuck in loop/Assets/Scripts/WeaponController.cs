using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    public LineRenderer LineRenderer;
    public Transform laserStartpos;
    [Space]
    public AudioSource source;
    public AudioSource effectsource;
    public AudioClip laserbeam;
    public AudioClip laserHit;

    [Space]
    public GameObject ParticleEffect;
    //public Texture2D target;
    // Start is called before the first frame update
    void Start()
    {
        LineRenderer.useWorldSpace = true;
        //Cursor.SetCursor(target, new Vector2(5, 5), CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if(Physics.Raycast(ray,out hit))
        {
            transform.LookAt(hit.point);
            if (Input.GetMouseButton(0) && (Input.GetAxis("Vertical")<0.2f)&& Input.GetAxis("Vertical") > -0.2f)
            {
                LineRenderer.SetPosition(0, laserStartpos.position);
                LineRenderer.SetPosition(1, hit.point);
                LineRenderer.gameObject.SetActive(true);

                source.Play();

                if (hit.collider.CompareTag("enemy"))
                {
                    if (hit.transform.GetComponent<Enemy2>())
                    {
                        hit.transform.GetComponent<Enemy2>().decreaseHealth();
                    }
                    else
                    {

                        //Destroy(Instantiate(ParticleEffect, hit.transform.position, Quaternion.identity),3);
                    
                        effectsource.PlayOneShot(laserHit);
                        Destroy(hit.collider.transform.parent.gameObject);

                    }
                }
            }
            else
            {
                LineRenderer.SetPosition(0, laserStartpos.position);
                LineRenderer.SetPosition(1, laserStartpos.position);
                LineRenderer.gameObject.SetActive(false);

                source.Pause();
            }
           
        }
    }
}
