using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

    public float speed = 10f;
    public float damage = 1f;

	// Use this for initialization
	void Start () {
		
	}

    private void FixedUpdate()
    {
        this.transform.Translate(Vector3.forward * speed * Time.deltaTime);

    }

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.GetComponent<AttackableObject>()!= null && speed >0)
        {
            collision.gameObject.GetComponent<AttackableObject>().TakeDamage(damage);
            collision.gameObject.GetComponent<AttackableObject>().hitpoint = this.transform.position;
        }
        speed = 0; 
    }

}
