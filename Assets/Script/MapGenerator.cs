using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapGenerator : MonoBehaviour {

    public int MapSizeX;
    public int MapSizeY;
    public GameObject[] prefab;


    [Range(0, 1)] public float Density = 0;
    [Range(0, 1)] public float outline = 0;
    [Range(1, 100)] public float Scale = 0;
    [Range(0, 10)] public int AreaSpace = 0;
    private int[,] MapData;

    public GameObject player;

    bool[,] ObstacleboolMap;

    List<COORD> calculatedTiles = new List<COORD>();

    float Tilecount;

    

    // Use this for initialization
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

    }

    public void SpawnEnemy(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int randx;
            int randy;

            do
            {
                randx = Random.Range(0, MapSizeX);
                randy = Random.Range(0, MapSizeY);

            } while (MapData[randx, randy] == 1);

            Instantiate(prefab[3], Vector3.zero + new Vector3(randx - MapSizeX / 2, 0.5f, randy - MapSizeY / 2) * Scale, prefab[3].transform.rotation, this.transform);
        }

    }



    int CountNeighbor(COORD coord,COORD origin)
    {
        calculatedTiles.Add(coord);
        return 1 +
            (new COORD(coord.x, coord.y - 1) != origin ? (!calculatedTiles.Exists(COORD => COORD == new COORD(coord.x, coord.y - 1)) && coord.y - 1 >= 0 && ObstacleboolMap[coord.x, coord.y - 1] ?
            CountNeighbor(new COORD(coord.x, coord.y - 1),coord) : 0) : 0)
            +
            (new COORD(coord.x - 1, coord.y) != origin ? (!calculatedTiles.Exists(COORD => COORD == new COORD(coord.x - 1, coord.y)) && coord.x - 1 >= 0 && ObstacleboolMap[coord.x - 1, coord.y] ?
            CountNeighbor(new COORD(coord.x - 1, coord.y),coord) : 0) : 0 )
            +
            (new COORD(coord.x, coord.y + 1) != origin ? (!calculatedTiles.Exists(COORD => COORD == new COORD(coord.x, coord.y + 1)) && coord.y + 1 < MapSizeY && ObstacleboolMap[coord.x, coord.y + 1] ?
            CountNeighbor(new COORD(coord.x, coord.y + 1),coord) : 0) : 0 )
            +
            (new COORD(coord.x + 1, coord.y) != origin ? (!calculatedTiles.Exists(COORD => COORD == new COORD(coord.x + 1, coord.y)) && coord.x + 1 < MapSizeX && ObstacleboolMap[coord.x + 1, coord.y] ?
            CountNeighbor(new COORD(coord.x + 1, coord.y),coord) : 0) : 0 );
    }

    bool isFullyAccesible()
    {
        calculatedTiles.Clear();
        int tilecount = 0;

        int currentX = MapSizeX / 2;
        int currentY = MapSizeY / 2;

        tilecount = CountNeighbor(new COORD(currentX,currentY), new COORD(currentX, currentY));
        return tilecount == Tilecount;
    }

    public void GenerateMap()
    {

        ObstacleboolMap = new bool[MapSizeX, MapSizeY];
        for (int i = 0; i < MapSizeX; i++)
        {
            for (int j = 0; j < MapSizeY; j++)
            {
                ObstacleboolMap[i, j] = true;
            }
        }

        if (transform.childCount != 0 && transform.FindChild("xxx") != null)
        {
            DestroyImmediate(transform.FindChild("xxx").gameObject);
        }

        int obstaclecount = 0;

        MapData = new int[MapSizeX, MapSizeY];

        int[] tempMap = new int[(int)MapSizeX * MapSizeY];

        for (int i = 0; i < (int)(Density * MapSizeX * MapSizeY); i++)
        {
            tempMap[i] = 1;
        }

        for (int i = 0; i < MapSizeX * MapSizeY; i++)
        {
            int SwapIndex = (int)Random.Range(i, MapSizeX * MapSizeY);
            int tempvar = tempMap[i];
            tempMap[i] = tempMap[SwapIndex];
            tempMap[SwapIndex] = tempvar;
        }

        int c = 0;

        for (int i = 0; i < MapSizeX; i++)
        {
            for (int j = 0; j < MapSizeY; j++)
            {
                MapData[i, j] = tempMap[c];
                c++;
            }
        }


        for (int m = 0; m < AreaSpace; m++)
        {
            for (int i = 0; i < MapSizeX; i++)
            {
                for (int j = 0; j < MapSizeY; j++)
                {
                    int obs = 0;
                    if (MapData[i, j] == 1 && i - 1 >= 0 && i + 1 < MapSizeX && j - 1 >= 0 && j + 1 < MapSizeY)
                    {
                        if (MapData[i - 1, j] == 0)
                        {
                            obs++;
                        }
                        if (MapData[i + 1, j] == 0)
                        {
                            obs++;
                        }
                        if (MapData[i, j - 1] == 0)
                        {
                            obs++;
                        }
                        if (MapData[i, j + 1] == 0)
                        {
                            obs++;
                        }

                        if (obs > 2)
                        {
                            MapData[i, j] = 0;
                        }
                    }

                }
            }
        }

        MapData[MapSizeX / 2, MapSizeY / 2] = 0;
        Tilecount = (int)MapSizeX * MapSizeY;


        GameObject mapholder = new GameObject();
        mapholder.transform.SetParent(this.transform);
        mapholder.name = "xxx";

        COORD[] mapcoord = new COORD[MapSizeX * MapSizeY];

        c = 0;

        for (int i = 0; i < MapSizeX; i++)
        {
            for (int j = 0; j < MapSizeY; j++)
            {
                mapcoord[c] = new COORD(i,j);
                c++;
            }
        }

        for (int i = 0; i < MapSizeX * MapSizeY; i++)
        {
            int SwapIndex = (int)Random.Range(i, MapSizeX * MapSizeY);
            COORD tempvar = mapcoord[i];
            mapcoord[i] = mapcoord[SwapIndex];
            mapcoord[SwapIndex] = tempvar;
        }

        for (int i = 0; i < (int)MapSizeX * MapSizeY; i++)
        {
            if (MapData[mapcoord[i].x, mapcoord[i].y] == 1)
            {
                ObstacleboolMap[mapcoord[i].x, mapcoord[i].y] = false;
                Tilecount--;
                if (isFullyAccesible())
                {
                    float RandomScale = Random.Range(0, 5);
                    GameObject tmp = Instantiate(prefab[MapData[mapcoord[i].x, mapcoord[i].y]],
                        new Vector3(mapcoord[i].x - (float)MapSizeX / 2, 
                        (MapData[mapcoord[i].x, mapcoord[i].y] == 1 ? RandomScale / 2 : 0),
                        mapcoord[i].y - (float)MapSizeY / 2),
                        prefab[MapData[mapcoord[i].x, mapcoord[i].y]].transform.rotation, mapholder.transform) as GameObject;

                    tmp.transform.localScale = new Vector3(1 - outline, 1 - outline, 1);
                    tmp.transform.localScale += Vector3.forward * RandomScale;
                }
                else
                {
                    float RandomScale = Random.Range(0, 5);
                    GameObject tmp = Instantiate(prefab[0], new Vector3(mapcoord[i].x - (float)MapSizeX / 2,
                        0,
                        mapcoord[i].y - (float)MapSizeY / 2),
                        prefab[0].transform.rotation, mapholder.transform) as GameObject;
                    tmp.transform.localScale = new Vector3(1 - outline, 1 - outline, 1);

                    Tilecount++;
                    ObstacleboolMap[mapcoord[i].x, mapcoord[i].y] = true;
                }

            }
            else
            {
                float RandomScale = Random.Range(0, 5);
                GameObject tmp = Instantiate(prefab[MapData[mapcoord[i].x, mapcoord[i].y]],
                    new Vector3(mapcoord[i].x - (float)MapSizeX / 2,
                    (MapData[mapcoord[i].x, mapcoord[i].y] == 1 ? RandomScale / 2 : 0),
                    mapcoord[i].y - (float)MapSizeY / 2), 
                    prefab[MapData[mapcoord[i].x, mapcoord[i].y]].transform.rotation, mapholder.transform) as GameObject;
                tmp.transform.localScale = new Vector3(1 - outline, 1 - outline, 1);
            }
        }


        for (int i = 0; i < MapSizeX+1; i++)
        {
            GameObject obj = Instantiate(prefab[1],new Vector3(i - 1 - (float)MapSizeX / 2, 10 , -1 - (float)MapSizeY / 2),prefab[1].transform.rotation, mapholder.transform) as GameObject;
            obj.transform.localScale += Vector3.forward * 20;
            obj.GetComponent<AttackableObject>().enabled = false;
            obj.GetComponent<MeshRenderer>().enabled = false;
        }
        for (int i = 0; i < MapSizeY + 1; i++)
        {
            GameObject obj = Instantiate(prefab[1], new Vector3(- 1 - (float)MapSizeX / 2, 10, i  - (float)MapSizeY / 2), prefab[1].transform.rotation, mapholder.transform) as GameObject;
            obj.transform.localScale += Vector3.forward * 20;
            obj.GetComponent<AttackableObject>().enabled = false;
            obj.GetComponent<MeshRenderer>().enabled = false;
        }
        for (int i = 0; i < MapSizeX + 1; i++)
        {
            GameObject obj = Instantiate(prefab[1], new Vector3(i  - (float)MapSizeX / 2, 10, MapSizeY - (float)MapSizeY / 2), prefab[1].transform.rotation, mapholder.transform) as GameObject;
            obj.transform.localScale += Vector3.forward * 20;
            obj.GetComponent<AttackableObject>().enabled = false;
            obj.GetComponent<MeshRenderer>().enabled = false;
        }
        for (int i = 0; i < MapSizeY + 1; i++)
        {
            GameObject obj = Instantiate(prefab[1], new Vector3(MapSizeX - (float)MapSizeX / 2, 10, i - 1 - (float)MapSizeY / 2), prefab[1].transform.rotation, mapholder.transform) as GameObject;
            obj.transform.localScale += Vector3.forward * 20;
            obj.GetComponent<AttackableObject>().enabled = false;
            obj.GetComponent<MeshRenderer>().enabled = false;
        }


        if (player != null)
            DestroyImmediate(player);

        int randx;
        int randy;

        do
        {
            randx = Random.Range(0, MapSizeX);
            randy = Random.Range(0, MapSizeY);

        } while (MapData[randx, randy] == 1);




        player = Instantiate(prefab[2], Vector3.zero + new Vector3(randx - MapSizeX / 2, 0.5f, randy - MapSizeY / 2) * Scale, prefab[2].transform.rotation, this.transform) as GameObject;

        mapholder.transform.localScale = Vector3.one * Scale;

        //SpawnEnemy(5);

    }

    public struct COORD
    {
        public int x, y;
        public COORD(int x_, int y_)
        {
            x = x_;
            y = y_;
        }
        public static bool operator ==(COORD lhs, COORD rhs)
        {
            return lhs.x == rhs.x && lhs.y == rhs.y;
        }
        public static bool operator !=(COORD lhs, COORD rhs)
        {
            return lhs.x != rhs.x || lhs.y != rhs.y;
        }
    }


}

