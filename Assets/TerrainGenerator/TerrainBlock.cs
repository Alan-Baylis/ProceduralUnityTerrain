using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainBlock : MonoBehaviour{

    Vector3[] vertices;
    Vector2[] uv;
    Color[] vertColor;

    public Material mat;

    public bool drawGizmo = false;

    // The length, in single units, of the block. Max size is (250 * lowPolySize) / 4.
    int blockSize;
    // The amount of verticies in 1 length of the block. (This changes with lowPolySize)
    int blockLengthOfVerts;
    int blockLengthOfTriangles;


    int colorMaxHeight;

    int lowPolySize;

    public TerrainGenerator generator;

    public void getVariables()
    {
        blockSize = generator.blockSize;
        blockLengthOfVerts = generator.blockLengthOfVerts;
        blockLengthOfTriangles = generator.blockLengthOfTriangles;
        colorMaxHeight = generator.colorMaxHeight;
        lowPolySize = generator.lowPolySize;
        
    }

    public Vector3[] getVerts()
    {
        return vertices;
    }

	public void generate () {

        // If the block size is 25, and the poly size is 2, the blockLengthOfVerts will be 13. .5 lengths are rounded up.
        //blockLengthOfVerts = (int) (( (float) blockSize / (float) lowPolySize) + 0.5f);
        //blockLengthOfVerts = blockLengthOfVerts * 4;

        //blockLengthOfTriangles = blockLengthOfVerts / 4;

        gameObject.AddComponent<MeshFilter>();
        MeshRenderer rend = gameObject.AddComponent<MeshRenderer>();
        Mesh mesh = GetComponent<MeshFilter>().mesh;

        rend.material = generator.mat;

        mesh.Clear();
        //Why is this recalculated three times? Nobody knows.
        int blockLengthOfVertsSQR = blockLengthOfVerts * blockLengthOfVerts;
        vertices = new Vector3[blockLengthOfVertsSQR/4];
        uv = new Vector2[blockLengthOfVertsSQR/4];
        vertColor = new Color[blockLengthOfVertsSQR/4];

        int total = 0;

        for (int y = 0; y < blockSize; y = y + lowPolySize)
        {
            for (int x = 0; x < blockSize; x = x + lowPolySize)
            {

                //float avgY = getYAverageFromNeighbours(total);
                //float ynum = getNewRng();

                //vinf.gradient = ynum + avgY;

                float ynum = getYValue(x, y);

                float xN = x + getNewRng();
                float yN = y + getNewRng();

                Vector3 rand = new Vector3(xN, ynum, yN);
                Vector2 uvVec = new Vector2((float)x / (float)blockSize, (float)y / (float)blockSize);

                vertices[total] = rand;
                vertices[total + 1] = rand;
                vertices[total + 2] = rand;
                vertices[total + 3] = rand;

                uv[total] = uvVec;
                uv[total + 1] = uvVec;
                uv[total + 2] = uvVec;
                uv[total + 3] = uvVec;

                total = total + 4;
            }
        }

        // Block length of triangles is really squares, as both triangles are calculated at the same time.
        int[] triangles = new int[(blockLengthOfTriangles * blockLengthOfTriangles)  * 6]; 
        
        total = 0;

        int forCond = (((blockLengthOfTriangles * blockLengthOfTriangles) - blockLengthOfTriangles) * 4) - 1;
        for (int i = 0; i < forCond; i = i + 4)
        {
            if (i / 4 % blockLengthOfTriangles == blockLengthOfTriangles - 1)
            {
                continue;
            }

            triangles[total + 1] = i;
            triangles[total] = i + 5;
            triangles[total + 2] = i + blockLengthOfVerts + 2;

            triangles[total + 3] = i + blockLengthOfVerts + 2;
            triangles[total + 4] = i + blockLengthOfVerts + 7;
            triangles[total + 5] = i + 5;

            float tempColor = (float)i / (float)forCond;

            //Color squareCol = new Color( (float) Random.Range(1, 100) / 100, (float) Random.Range(1, 100) / 100, (float) Random.Range(1, 100) / 100);
            Color squareCol = new Color( ( (vertices[i].y / colorMaxHeight)) - 0.3f, 1, 0);

            vertColor[i] = squareCol;
            vertColor[i + 5] = squareCol;
            vertColor[i + blockLengthOfVerts + 2] = squareCol;

            vertColor[i + blockLengthOfVerts + 2] = squareCol;
            vertColor[i + blockLengthOfVerts + 7] = squareCol;
            vertColor[i + 5] = squareCol;

            total = total + 6;
        }

        generator.createJoints(vertices);
        
        mesh.vertices = vertices;
        //mesh.uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, blockSize), new Vector2(blockSize, 0), new Vector2(blockSize,blockSize) };
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.colors = vertColor;
        ;

        vertices = mesh.vertices;

        

        mesh.RecalculateNormals();
        gameObject.AddComponent<MeshCollider>();
	}

    /*
    private float getYAverageFromNeighbours(int vertIndex) 
    {
        int vertmod = vertIndex % blockLengthOfVerts;

        bool top = true;
        bool bot = true;
        bool left = true;
        bool right = true;

        List<float> yValues = new List<float>();

        if (vertmod != 0)
        {
            left = false;
            if (vertices[vertIndex - 1].Equals(Vector3.zero) == true)
            {
                yValues.Add(vertices[vertIndex - 1].y);
            }
        }

        if (vertmod + 1 != blockLengthOfVerts)
        {
            right = false;
            if (vertices[vertIndex + 1].Equals(Vector3.zero) == true) ;
            {
                yValues.Add(vertices[vertIndex + 1].y);
            }
        }

        if (vertIndex < (blockLengthOfVerts * blockLengthOfVerts) - blockLengthOfVerts)
        {
            top = false;
            if (vertices[vertIndex + blockLengthOfVerts].Equals(Vector3.zero) == true) ;
            {
                yValues.Add(vertices[vertIndex + blockLengthOfVerts].y);
            }
        }

        if (vertIndex > blockLengthOfVerts)
        {
            bot = false;
            if (vertices[vertIndex - blockLengthOfVerts].Equals(Vector3.zero) == true) ;
            {
                yValues.Add(vertices[vertIndex - blockLengthOfVerts].y);
            }
        }

        if (!top && !left)
        {
            if (vertices[vertIndex + blockLengthOfVerts - 1].Equals(Vector3.zero) == true) ;
            {
                yValues.Add(vertices[vertIndex + blockLengthOfVerts - 1].y);
            }
        }

        if (!top && !right)
        {
            if (vertices[vertIndex + blockLengthOfVerts + 1].Equals(Vector3.zero) == true) ;
            {
                yValues.Add(vertices[vertIndex + blockLengthOfVerts + 1].y);
            }
        }

        if (!bot && !left)
        {
            if (vertices[vertIndex - blockLengthOfVerts - 1] != null)
            {
                yValues.Add(vertices[vertIndex - blockLengthOfVerts - 1].y);
            }
        }

        if (!bot && !right)
        {
            if (vertices[vertIndex - blockLengthOfVerts + 1] != null)
            {
                yValues.Add(vertices[vertIndex - blockLengthOfVerts + 1].y);
            }
        }

        return calcMeanAverage(yValues);
    }
    */
    /*
    private float calcMeanAverage(List<float> list)
    {
        float total = 0;
        foreach (float f in list)
        {
            total = total + f;
        }
        return total / list.Count;
    }
    */
    public float getNewRng()
    {
        return generator.getNewRng();
    }

    public float getYValue(int x, int z)
    {
        return generator.getYValue(x, z);
    }

    // Draw vertices - SLOW
    
    
    void OnDrawGizmos()
    {
        if (drawGizmo)
        {
            foreach (Vector3 vec in vertices)
            {
                Gizmos.DrawWireSphere(transform.TransformPoint(vec), 0.1f);
            }
        }
         
    }
     
    
	
	void Update () {
	
	}
}
