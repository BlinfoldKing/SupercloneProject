using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(MapGenerator))]
public class GameManager : MonoBehaviour {

    public int currentWave = 1;
    public int Score;
    public GameObject navplane;

    MapGenerator map;


    public int enemycount = 0;

	// Use this for initialization
	void Start () {
        map = GetComponent<MapGenerator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(enemycount == 0)
        {
            if (map.player != null && !map.player.GetComponent<AttackableObject>().isDie)
                currentWave++;
            map.MapSizeX = (currentWave/2  + 10);
            map.MapSizeY = (currentWave/2  + 10);
            map.outline = Random.Range(0, 0.75f);
            map.AreaSpace = (map.Density <= .5f) ? (int)Random.Range(0,6) :  (int)Random.Range(0,11);
            map.GenerateMap();
            enemycount += (int)(currentWave/2 + 1);
            map.SpawnEnemy(enemycount);
        }
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
}
