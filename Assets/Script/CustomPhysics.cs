using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class CustomPhysics : MonoBehaviour {

	List<GameObject> GravityPoints = new List<GameObject>();
    [Range(0, 1)] public float physicsModifier = 1;
    public float GravityAccel = 10f;
	public bool useGravity = false;
	public bool isGravitySource = false;
    public GameObject targetOrbit;
    Rigidbody rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        if (this.GetComponent<Rigidbody>() != null)
        {
            this.GetComponent<Rigidbody>().useGravity = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
		GravityPoints.Clear ();

        //Debug.Log(GravityPoints.Count);

		RaycastHit hit;
		int layer = 1 << 8;
		foreach (Collider col in Physics.OverlapSphere(transform.position,100f,layer)) { 
			GravityPoints.Add (col.gameObject);
		}

        if(targetOrbit != null)
        {
           // transform.Rotate(0, 5 * Time.deltaTime, 0);
        }


        if (useGravity)
        {
            if (GravityPoints.Count < 1)
            {
                rb.AddForce(Vector3.down * GravityAccel * physicsModifier);
            }
            else
				foreach (GameObject point in GravityPoints)
				{   
					Debug.DrawLine (transform.position, point.transform.position, Color.red);
					rb.AddForce((point.transform.position-transform.position).normalized * (point.GetComponent<CustomPhysics>().GravityAccel * physicsModifier *
                        ((100/Vector3.Distance(point.transform.position,transform.position))/10)) );
				}
        }

    }
		

}
