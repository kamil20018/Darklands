using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainElements : MonoBehaviour
{
    public List<string> elementNames; //elementNames[1] corresponds to localSeed[1]
    public Dictionary<string, TerrainElement> terrainElements;

    public struct TerrainElement
    {
        public GameObject prefab;
        public List<Sprite> skin; //we can fill this out later with multiple skins
        public int width;
        public int height;
        public int area;
        public int layer;
        public TerrainElement(GameObject prefab, int width, int height, int layer)
        {
            this.prefab = prefab;
            this.width = width;
            this.height = height;
            this.layer = layer;
            this.area = height * width;
            this.skin = new List<Sprite>();
        }
    }

    private void Awake()
    {
        elementNames = new List<string>();
        elementNames.Add("empty");
        terrainElements = new Dictionary<string, TerrainElement>();
        TerrainElement tree = new TerrainElement(Resources.Load<GameObject>("Prefabs/Tree"), 3, 3, 0); //width, height, layer
        TerrainElement bush = new TerrainElement(Resources.Load<GameObject>("Prefabs/Bush"), 1, 1, 0);
        TerrainElement grass = new TerrainElement(Resources.Load<GameObject>("Prefabs/Grass"), 1, 1, 0);
        elementNames.Add("tree");
        terrainElements.Add("tree", tree); // seed[3]
        elementNames.Add("bush");
        terrainElements.Add("bush", bush); // seed[2]
        elementNames.Add("grass");
        terrainElements.Add("grass", grass);  //seed[1]
    }

    public Dictionary<string, TerrainElement> GetTerrainElements()
    {
        return terrainElements;
    }

    public List<string> GetElementNames()
    {
        return elementNames;
    }
}
