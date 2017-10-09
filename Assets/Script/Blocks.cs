using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : AttackableObject {

    

    private void Update()
    {
        if (isDie)
        {
            GameObject replace = Instantiate(map.prefab[0], new Vector3(transform.position.x, 0, transform.position.z), map.prefab[0].transform.rotation) as GameObject;
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            Instantiate(DeadParticle, new Vector3(transform.position.x, hitpoint.y, transform.position.z), DeadParticle.transform.rotation);
            replace.transform.localScale = new Vector3(1 - map.outline, 1 - map.outline, 1);
            replace.transform.localScale *= map.Scale;
            Destroy(this.gameObject);


        }
    }
}
