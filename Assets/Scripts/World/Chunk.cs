using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour 
{
    public GameObject floorTile;
    public static int NeighboursCount = 9, Params = 4, ChunkSize;
    public static float fromCenter;

    private Vector3[] direction;
    private Dictionary<string, int> directions = new Dictionary<string, int>();
    public GameObject[] neighbours = new GameObject[NeighboursCount];

    void Awake()
    {
        ChunkSize = Mathf.RoundToInt(gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.x); //width and height are the same
        fromCenter = ChunkSize / 2f - 0.1f; //0.1f is just an offset 
        direction = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0), new Vector3(1, 0, 0), new Vector3(1, -1, 0), new Vector3(0, -1, 0), new Vector3(-1, -1, 0), new Vector3(-1, 0, 0), new Vector3(-1, 1, 0) };
        for (int x = 0; x < NeighboursCount; x++)
        {
            directions.Add(VecToString(direction[x]), x);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player") 
        {
            collision.gameObject.transform.parent = gameObject.transform;
            HandleNeighbours(); //new chunks + terrain generation
        }
        if(collision.gameObject.tag == "Enemy") //changing enemys parent so that it doesn't disappear with its previous chunk
        {
            collision.gameObject.transform.parent = gameObject.transform;
        }
    }

    public void HandleNeighbours()
    {
        List<int> newChunks = ActivateAndInstantiate(); //activates deactivated chunks, instantiates new ones and stores indexes of new chunks in a list

        for (int x = 0; x < NeighboursCount; x++)
        {
            for (int y = 0; y < NeighboursCount; y++)
            {
                string neighDir = VecToString((neighbours[y].transform.position - neighbours[x].transform.position) / ChunkSize);
                if (directions.ContainsKey(neighDir))
                {
                    neighbours[x].GetComponent<Chunk>().SetNeighbour(neighbours[y], directions[neighDir]);
                }
            }
        }

        for (int x = 0; x < NeighboursCount; x++)
        {
            if (newChunks.Contains(x))
            {
                List<int[]> seeds = neighbours[x].GetComponent<Chunk>().CollectSeeds();
                neighbours[x].transform.parent = gameObject.transform.parent;
                neighbours[x].GetComponent<TerrainGenerator>().Initialize(seeds);
                
            }
        }
    }

    public List<int> ActivateAndInstantiate()
    {
        List<int> newChunks = new List<int>();
        neighbours[0] = gameObject;
        for (int x = 1; x < NeighboursCount; x++)
        {
            if (neighbours[x] == null)
            {
                newChunks.Add(x);
                floorTile = Resources.Load<GameObject>("Prefabs/FloorTile");
                neighbours[x] = Instantiate(floorTile, transform.position + direction[x] * ChunkSize, Quaternion.identity);
            }

            if (neighbours[x] != null && !neighbours[x].activeSelf)
            {
                neighbours[x].SetActive(true);
            }
        }
        return newChunks;
    }

    public void SetNeighbour(GameObject tile, int direction)
    {
        neighbours[direction] = tile;
    }

    public string VecToString(Vector3 vector)
    {
        string result = "";
        result += Mathf.RoundToInt(vector.x);
        result += Mathf.RoundToInt(vector.y);
        result += Mathf.RoundToInt(vector.z);
        return result;
    }

    public List<int[]> CollectSeeds()
    {
        List<int[]> seeds = new List<int[]>();
        for (int x = 1; x < NeighboursCount; x++)
        {
            if (neighbours[x] != null)
            {
                seeds.Add(neighbours[x].GetComponent<TerrainGenerator>().GetSeed());
            }
        }
        return seeds;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            for (int x = 0; x < NeighboursCount; x++)
            {
                if (!neighbours[x].activeSelf)
                {
                    neighbours[x].SetActive(true);
                }
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 playerPos = collision.gameObject.transform.position;
            Vector3 chunkPos = transform.position;

            if (playerPos.y < chunkPos.y - fromCenter)
            {
                DisableChunks(8, 1, 2);
            }
            if (playerPos.y > chunkPos.y + fromCenter)
            {
                DisableChunks(4, 5, 6);
            }
            if (playerPos.x < chunkPos.x - fromCenter)
            {
                DisableChunks(2, 3, 4);
            }
            if (playerPos.x > chunkPos.x + fromCenter)
            {
                DisableChunks(8, 7, 6);
            }
        }
    }

    private void DisableChunks(params int[] chunks)
    {
        foreach (int x in chunks)
        {
            StartCoroutine(DisableChunk(x));
        }
    }

    private IEnumerator DisableChunk(int x)
    {
        while (neighbours[x].GetComponent<TerrainGenerator>().isSpawning)
        {
            yield return new WaitForSeconds(0.01f);
        }
        neighbours[x].SetActive(false);
    }
}


/*
using System;
					
public class Program
{
	public static void Main()
	{
		Console.WriteLine(test(x => x * x, 3.0f));
	}
	public static float test(Func<float, float> lambda, float number){
		return lambda(number);
	}
}
    public void PrintSeeds(List<int[]> seeds)
    {
        string result = "";
        foreach (int[] seed in seeds)
        {
            string line = "";
            foreach (int x in seed)
            {
                line += x;
            }
            result += line + "| ";
        }
        Debug.Log(result);
    }

    public void PrintArray(int[] array)
    {
        string result = "";
        foreach (int x in array)
        {
            result += x + ", ";
        }
        Debug.Log(result);
    }
*/

/* ######used to be inside of handle neighbours, stripped version seems to work tho
if (x == y)
{
neighbours[x].GetComponent<Chunk>().SetNeighbour(neighbours[y], 0);
continue;
}
else
{
string neighDir = VecToString((neighbours[y].transform.position - neighbours[x].transform.position) / ChunkSize);
if (directions.ContainsKey(neighDir))
{
    neighbours[x].GetComponent<Chunk>().SetNeighbour(neighbours[y], directions[neighDir]);
}
}
*/
