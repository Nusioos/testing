using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class generation : MonoBehaviour
{
    Mesh mesh;

    [SerializeField, Range(1, 1000)]
    private int resolution = 10;

    [SerializeField, Range(0, 1)]
    private float czynnik = 0.05f;
    private Vector3[] vertices;

    public MeshFilter cube;
    public List<MeshFilter> cubes = new List<MeshFilter>();
    public List<MeshFilter> sourceFilter = new List<MeshFilter>();
    public MeshFilter fast;
    [SerializeField] private MeshFilter targetFilter;

    [SerializeField, Range(0, 2)]
    public bool hasInstantiated = false;
    public int w = 0;
    public float offset_X;
    public float offset_Y;
    public float offset_Z;
    
    private float offset_X_Random;
    private float offset_Y_Random;
    private float offset_Z_Random;
    
    public Material combinedMeshMaterial;
    Vector3 simba;

    private void Awake()
    {


        offset_X_Random = Random.Range(00f, 1000f);
        offset_Y_Random = Random.Range(00f, 1000f);
        offset_Z_Random = Random.Range(0f, 1000f);

        Generate();
    }

    private void Generate()
    {
   
       
       float noiseScale = Random.Range(0.12f, 0.12f);
        List<CombineInstance> combineList = new List<CombineInstance>();

        for (int x = 0; x < resolution * 2; x++)
        {
            for (int z = 0; z < resolution * 2; z++)
            {
                for (int y = 0; y < resolution / 3; y++)
                {

                    if (Perlin3D(x * noiseScale, y * noiseScale , z * noiseScale ) > 0.5f)
                    {
                        Vector3 pos = new Vector3(x , y , z );
                        MeshFilter ws = Instantiate(fast, pos, Quaternion.identity, transform);
                        sourceFilter.Add(ws);

                        CombineInstance ci = new CombineInstance
                        {
                            mesh = ws.mesh,
                            transform = ws.transform.localToWorldMatrix
                        };
                        combineList.Add(ci);
                        Destroy(ws.gameObject);

                        if (combineList.Sum(c => c.mesh.vertexCount) > 50000)
                        {
                            CreateNewMeshObject(combineList.ToArray());
                            combineList.Clear();
                        }
                    }
                }
            }
        }

        if (combineList.Count > 0)
        {
            CreateNewMeshObject(combineList.ToArray());
        }
    }

    private void CreateNewMeshObject(CombineInstance[] combineInstances)
    {
        Mesh newMesh = new Mesh();
        newMesh.CombineMeshes(combineInstances, true, true); // Combine the meshes with mergeSubMeshes and useMatrices set to true

        if (newMesh.vertexCount > 0)
        {
            MeshFilter newCube = Instantiate(cube, transform);
            newCube.mesh = newMesh;

            // Assign the combined mesh material
            MeshRenderer meshRenderer = newCube.gameObject.GetComponent<MeshRenderer>();
            if (meshRenderer != null && combinedMeshMaterial != null)
            {
                meshRenderer.material = combinedMeshMaterial;
            }

            cubes.Add(newCube);
        }
    }

    private void Update()
    {

    }

    public static float Perlin3D(float x, float y, float z)
    {
        float ab = Mathf.PerlinNoise(x, y);
        float bc = Mathf.PerlinNoise(y, z);
        float ac = Mathf.PerlinNoise(x, z);

        float ba = Mathf.PerlinNoise(y, x);
        float cb = Mathf.PerlinNoise(z, y);
        float ca = Mathf.PerlinNoise(z, x);

        float abc = ab + bc + ac + ba + cb + ca;
        return abc / 6f;
    }
}