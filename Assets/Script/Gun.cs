using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public float CooldownInterval = 1;
    public float maxBullet = Mathf.Infinity;

    public GameObject bulletObj; 

    public Transform Gunpoint;

    [HideInInspector] public float BulletCount = Mathf.Infinity;
    
    float lastShot;
    public float CurrentTime;
    float NextShot;

    public bool isParentZ;

    public float mouseY;

	// Use this for initialization
	void Start () {
		
	}
	
    public void shot(float CurrentTime)
    {
        if (CurrentTime >= NextShot)
        {
            lastShot = CurrentTime;
            NextShot = lastShot + CooldownInterval;
           
            if (isParentZ)
            {
                Instantiate(bulletObj, Gunpoint.position + transform.forward, Gunpoint.transform.rotation);
            }
            else
            {
                Instantiate(bulletObj, Gunpoint.position, Gunpoint.transform.rotation);
            }
           
        }
    }

	// Update is called once per frame
	void FixedUpdate () {

        CurrentTime = Time.time;

        if (this.transform.parent.gameObject.GetComponent<PlayerControl>()!= null)
        {
            this.transform.localEulerAngles = Vector3.left * mouseY;


            if (Input.GetMouseButton(0))
            {
                shot(CurrentTime);
            }
        }
	}
}
