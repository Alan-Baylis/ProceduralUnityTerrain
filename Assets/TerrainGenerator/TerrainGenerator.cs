using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainGenerator : MonoBehaviour {

    public GameObject baseBlockObject;

    // The length, in single units, of the block. Max size is (250 * /4) * lowPolySize.
    public int blockSize;
    public int lowPolySize;

    // The amount of verticies in 1 length of the block. (This changes with lowPolySize)
    public int blockLengthOfVerts;
    public int blockLengthOfTriangles;

    public int randomness;

    public int colorMaxHeight = 60;

    public float mountainScale;
    public int mountainSize;
    public int seed;

    int curX;
    int curY;

    public int amountOfBlocksX = 16;
    public int amountOfBlocksY = 16;

    System.Random rng;

    public Material mat;

    TerrainBlock[,] blocks;

	void Start () {

        blocks = new TerrainBlock[amountOfBlocksX, amountOfBlocksY];

        blockLengthOfVerts = (int)(((float)blockSize / (float)lowPolySize) + 0.5f);
        blockLengthOfVerts = blockLengthOfVerts * 4;

        blockLengthOfTriangles = blockLengthOfVerts / 4;
        rng = new System.Random(24719423);

        for (curY = 0; curY < amountOfBlocksY; curY++)
        {
            for (curX = 0; curX < amountOfBlocksY; curX++)
            {
                GameObject obj = Instantiate(baseBlockObject, new Vector3((float) (blockSize * curX), 0, (float) (blockSize * curY)), Quaternion.identity) as GameObject;
                TerrainBlock block = obj.GetComponent<TerrainBlock>();
                block.generator = this;
                block.getVariables();
                blocks[curX, curY] = block;
                block.generate();
            }
        }
	}

    public float getNewRng()
    {
        float next = (rng.Next(randomness) - (randomness / 2)) / 100f;
        return next;
    }

    public void createJoints(Vector3[] verticies)
    {
        if (curY > 0)
        {
            if (blocks[curX, curY - 1] != null)
            {
                joinDown(blocks[curX, curY].getVerts(), blocks[curX, curY - 1].getVerts());
            }
        }
        if (curY < amountOfBlocksY - 1)
        {
            if (blocks[curX, curY + 1] != null)
            {
                joinUp(blocks[curX, curY].getVerts(), blocks[curX, curY + 1].getVerts());
            }
        }


        if (curX > 0)
        {
            if (blocks[curX-1, curY] != null)
            {
               joinLeft(blocks[curX, curY].getVerts(), blocks[curX - 1, curY].getVerts());
            }
        }
        if (curX < amountOfBlocksX - 1)
        {
            if (blocks[curX + 1, curY] != null)
            {
               // joinRight(blocks[curX, curY].getVerts(), blocks[curX + 1, curY].getVerts());
            }
        }
    }

    internal float getYValue(int x, int z)
    {
        return Mathf.PerlinNoise((seed + x + (blockSize * curX)) / mountainScale, (seed + z + (blockSize * curY)) / mountainScale) * mountainSize;
    }

    private void joinDown(Vector3[] toBeJoined, Vector3[] toJoinTo) 
    {
        for (int i = 0; i < blockLengthOfVerts; i++)
        {
            Vector3 foundVec = toJoinTo[((blockLengthOfVerts * blockLengthOfTriangles) - blockLengthOfVerts) + i];

            toBeJoined[i] = new Vector3(foundVec.x, foundVec.y, foundVec.z - blockSize);
            //toBeJoined[i] = foundVec;
        }
    }

    private void joinUp(Vector3[] toBeJoined, Vector3[] toJoinTo)
    {
        for (int i = 0; i < blockLengthOfVerts; i++)
        {
            Vector3 VecJoiningTo = toJoinTo[i];

            toBeJoined[((blockLengthOfVerts * blockLengthOfTriangles) - blockLengthOfVerts) + i] = new Vector3(VecJoiningTo.x, VecJoiningTo.y, VecJoiningTo.z - blockSize);
            //toBeJoined[i] = foundVec;
        }
    }

    private void joinLeft(Vector3[] rightOne, Vector3[] leftOne)
    {
        int forCond = blockLengthOfVerts * blockLengthOfTriangles;
        for (int i = 0; i < forCond; i = i + blockLengthOfVerts)
        {
            Vector3 VecJoiningTo = leftOne[(i + blockLengthOfVerts) - 4];
            Vector3 changedVec = new Vector3(VecJoiningTo.x - blockSize, VecJoiningTo.y, VecJoiningTo.z);
            rightOne[i] = changedVec;
            rightOne[i + 1] = changedVec;
            rightOne[i + 2] = changedVec;
            rightOne[i + 3] = changedVec;
        }
    }

    private void joinRight(Vector3[] toBeJoined, Vector3[] toJoinTo)
    {
        for (int i = 0; i < blockLengthOfVerts; i++)
        {
            Vector3 foundVec = toJoinTo[((blockLengthOfVerts * blockLengthOfTriangles) - blockLengthOfVerts) + i];

            toBeJoined[i] = new Vector3(foundVec.x, foundVec.y, foundVec.z - blockSize);
            //toBeJoined[i] = foundVec;
        }
    }

}
