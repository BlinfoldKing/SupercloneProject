using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : AttackableObject {


    NavMeshAgent agent;
    GameObject target;
    GameManager game;

    Gun gun;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        map = FindObjectOfType<MapGenerator>();
        game = FindObjectOfType<GameManager>();
        if (BirthParticle != null)
        {
            Instantiate(this.BirthParticle, this.transform.position, this.transform.rotation, map.transform);
            StartCoroutine(birth());
        }
        gun = GetComponentInChildren<Gun>();
    }

   
    private void Update()
    {

        if (isDie)
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            game.enemycount--;
            Instantiate(DeadParticle, new Vector3(transform.position.x, hitpoint.y, transform.position.z), DeadParticle.transform.rotation);
            Destroy(this.gameObject);

        }
        else if(enablemovement && map.player != null) { 

            target = map.player;

            if (target != null)
            {
                agent.SetDestination(target.transform.position);

            }

            

            RaycastHit hit;

            if (Vector3.Distance(target.transform.position, this.transform.position) <= 10 &&
                Physics.Raycast(this.transform.localPosition, this.transform.TransformDirection(Vector3.forward) * 100,
                out hit, 10))
            {

                this.transform.LookAt(target.transform);
                if (hit.transform.gameObject.GetComponent<PlayerControl>() != null)
                {
                    if (Vector3.Distance(this.transform.position,target.transform.position) <= 3)
                    {
                        agent.Stop();
                    }
                    gun.shot(gun.CurrentTime);
                    Debug.Log("hit");

                }

            }
        }
        else if(map.player == null)
        {
            isDie = true;   
        }
    }
}
