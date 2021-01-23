using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour //############ i believe that this code works only for chunks with even size !!!!!!!!!!!!!!
{


    public const int MaxDev = 20; // maximum deviation for the new seed
    public int busyArea = 0, trees = 0, bushes = 0, grass = 0;
    public int[] localSeed = new[] { -1, -1, -1, -1 }; //density%, grass%, bush%, tree%
    private int[] initSeed = new[] { 30, 50, 30, 20 }; //60% - 25 ms 40% - 20ms 20% - 15ms
    private Dictionary<string, bool> takenArea = new Dictionary<string, bool>();
    public bool isSpawning;
    private Dictionary<string, TerrainElements.TerrainElement> terrainElements;
    public List<string> elementNames;

    public void Initialize(List<int[]> seeds)
    {
        terrainElements = gameObject.transform.parent.GetComponent<TerrainElements>().GetTerrainElements();
        elementNames = gameObject.transform.parent.GetComponent<TerrainElements>().GetElementNames();
        localSeed = GenerateSeed(seeds);
        StartCoroutine(GenerateTerrain(localSeed));
    }

    public IEnumerator GenerateTerrain(int[] seed)
    {
        if (IsSeedProper(seed))
        {
            int freeArea = Chunk.ChunkSize * Chunk.ChunkSize * seed[0] / 100;
            int amount;
            for (int x = 1; x < elementNames.Count; x++)
            {
                isSpawning = true;
                TerrainElements.TerrainElement toLoad = terrainElements[elementNames[x]];
                amount = freeArea * seed[x] / 100 / toLoad.area;
                if(x == 1)
                {
                    trees = amount;
                }
                if(x == 2)
                {
                    bushes = amount;
                }
                if(x == 3)
                {
                    grass = amount;
                }
                bool lastRound = x == elementNames.Count - 1;
                StartCoroutine(SpawnStruct(toLoad, amount, lastRound));
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    private IEnumerator SpawnStruct(TerrainElements.TerrainElement terrainElement, int amount, bool lastRound)
    {
        GameObject prefab = terrainElement.prefab;
        int width = terrainElement.width;
        int height = terrainElement.height;
        int fromCenter = Mathf.RoundToInt(Chunk.fromCenter);
        Vector3 spot;
        int x = Random.Range(-fromCenter, fromCenter - width + 1); //makes sure that whole terrain element gets spawned inside of the chunk
        int y = Random.Range(-fromCenter + height, fromCenter + 1); //because x and y represent top left 1x1 part of the element
        for (int i = 0; i < amount; i++)
        {
            while (!DoesFit(x, y, width, height)) //checks if terrain element can fit if it's top right corner is at x y
            {
                x = Random.Range(-fromCenter, fromCenter - width + 1);
                y = Random.Range(-fromCenter + height, fromCenter + 1);
            }

            spot = new Vector3(x + width / 2f, y - height / 2f, 0); //we align the sprite with the grid
            GameObject temp;
            temp = Instantiate(prefab, transform.position + spot, Quaternion.identity);
            temp.transform.parent = gameObject.transform;
            temp.transform.position += new Vector3(0, 0, terrainElement.layer);
            busyArea += width * height;
            yield return new WaitForSeconds(0.005f);
        }
        if (lastRound)
        {
            isSpawning = false;
        }
    }

    private string VecToString(int x, int y)
    {
        return x.ToString() + y.ToString();
    }

    private bool DoesFit(int xPos, int yPos, int width, int height){
        for(int x = xPos; x < xPos + width; x++) //xpos and ypos represent top left of element, so we need to check the rest of the right side for obstructions
        {
            for(int y = yPos; y > yPos - height; y--) // same, as above, but this time we check the area below y
            {
                if (takenArea.ContainsKey(VecToString(x, y)))
                {
                    return false;
                }
            }
        }
        for (int x = xPos; x < xPos + width; x++) //only works if we can safely insert element onto the chunk, fills out taken area
        {
            for (int y = yPos; y > yPos - height; y--)
            {
                takenArea.Add(VecToString(x, y), true);
            }
        }
        return true;
    }

    public int[] GenerateSeed(List<int[]> seeds)
    {
        int paramCount = Chunk.Params;

        List<int[]> initializedSeeds = new List<int[]>();
        foreach(int[] seed in seeds) // we filter out empty seeds and store them in nonNullSeeds
        {
            if(IsSeedProper(seed))
            {
                initializedSeeds.Add(seed);
            }
        }
        if(initializedSeeds.Count == 0) // if every single seed was null we give our seed a random value
        {
            return initSeed;
        }
        
        int[] averageOfParams = new int[paramCount]; //this array will be used to get the average of parameters from neighbouring cells
        System.Array.Clear(averageOfParams, 0, paramCount); // fill out with zeros

        foreach (int[] seed in initializedSeeds) //we sum up seed values
        {
            for(int x = 0; x < paramCount; x++)
            {
                averageOfParams[x] += seed[x];
            }
        }

        for(int x = 0; x < paramCount; x++) //gets the average for every value and randomly changes it by Max Deviation value
        {
            averageOfParams[x] /= initializedSeeds.Count;
            if(x != 0)
            {
                averageOfParams[x] += Random.Range(-MaxDev, MaxDev);
                if(averageOfParams[x] < 10)
                {
                    averageOfParams[x] = 10;
                }
                if(averageOfParams[x] >= 100)
                {
                    averageOfParams[x] = 99;
                }
            }
        }

        int leftTo100 = 100 - ArraySum(averageOfParams, 1, averageOfParams.Length - 1); // checks how far off are we from 100%

        int changeBy = leftTo100 / 3;
        int lastAdj = leftTo100 - changeBy * 3;
        for(int x = 1; x < paramCount; x++)
        {
            averageOfParams[x] += changeBy;
        }
        averageOfParams[Random.Range(1, 4)] += lastAdj;

        return averageOfParams;
    }

    private int ArraySum(int[] arr, int start, int end)
    {
        int total = 0;
        for(int x = start; x <= end; x++)
        {
            total += arr[x];
        }
        return total;
    }

    public int[] GetSeed()
    {
        return localSeed;
    }

    public bool IsSeedProper(int[] seed)
    {
        
        foreach(int x in seed)
        {
            if(x < 0)
            {
                return false;
            }
        }
        return true;
    }
}