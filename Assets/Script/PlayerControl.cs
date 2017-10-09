using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : AttackableObject {
    
    Rigidbody rb;
    Quaternion camori;
    GameManager game;
    TimeManager time;

    Vector3 dir;
    Gun gun;

    float mouseY;

    [SerializeField] private bool lockMouse;


    // Use this for initialization
    void Awake () {

        rb = this.GetComponent<Rigidbody>();
        time = FindObjectOfType<TimeManager>();
        game = FindObjectOfType<GameManager>();
        gun = GetComponentInChildren<Gun>();


        if (lockMouse)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

        }


    }
	
	// Update is called once per frame
	void Update () {
        


        float mouseX = Input.GetAxis("Mouse X");
        Quaternion xQuaternion = Quaternion.AngleAxis(mouseX, Vector3.up);
        mouseY += Input.GetAxis("Mouse Y");
        mouseY = Mathf.Clamp(mouseY,-60, 60);
        gun.mouseY = mouseY;
        Camera.main.transform.localEulerAngles = Vector3.left * mouseY;
        transform.localRotation = transform.localRotation * xQuaternion;

        

        dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));


        if (dir.x != 0 || dir.z != 0)
        {
            time.customScale += (1 / time.SlowdownLength) * Time.deltaTime;
            time.customScale = Mathf.Clamp(time.customScale, 0, 1);
            time.ChangeScale(time.customScale);
        }
        else
        {
            time.SlowdownTime();
        }

        gun.isParentZ = (dir.z > 0);


        if (isDie)
        {
            Gameover();
        }
    }

    private void FixedUpdate()
    {
        Vector3 localDir = transform.TransformDirection(dir.normalized);
        rb.MovePosition(rb.position + localDir * 7 * Time.deltaTime);
    }

    void Gameover()
    {
        DestroyImmediate(this.gameObject);
    }

}
