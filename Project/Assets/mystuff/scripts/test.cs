using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    public float        speed       = 10;
    public float        Sensitivity = 100;
    Vector3             mouse;
    Transform           Camera;
    CharacterController CC;

    private void Awake()
    {
        Camera           = transform.GetChild(0);
        CC               = gameObject.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible   = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement(); 
    }

    void Movement()
    {
        transform.position += transform.rotation * new Vector3(Input.GetAxisRaw("Horizontal"),0,
            Input.GetAxisRaw("Vertical")).normalized*speed*Time.deltaTime;

        mouse += new Vector3(-Input.GetAxisRaw("Mouse Y"), Input.GetAxisRaw("Mouse X"),0) * Sensitivity * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0,mouse.y,0);
        mouse.x = Mathf.Clamp(mouse.x,-90,90);
        Camera.localRotation = Quaternion.Euler(mouse.x, 0, 0);

        /*
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * speed * Time.deltaTime;
        }*/
    }
}
