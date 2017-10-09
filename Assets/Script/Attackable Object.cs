using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableObject : MonoBehaviour {

    [SerializeField]protected float health;
    [SerializeField]protected GameObject DeadParticle;
    [SerializeField]public GameObject BirthParticle;
    [HideInInspector]public Vector3 hitpoint;
    public bool isDie = false;
    protected MapGenerator map;
    protected bool enablemovement = false;


    protected IEnumerator birth()
    {
        this.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(2.5f);
        this.GetComponent<MeshRenderer>().enabled = true;
        enablemovement = true;
    }

    void Awake()
    {
        map = FindObjectOfType<MapGenerator>();
        if (BirthParticle != null)
        {
            Instantiate(this.BirthParticle, this.transform.position, this.transform.rotation, map.transform);
            StartCoroutine(birth());
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log(health);
        if (health <= 0)
        {
            isDie = true;
        }
    }
	
}
