using UnityEngine;
using System.Collections;
using System.Linq;

public class T1 : MonoBehaviour
{

    int sizex = 50;
    int startsize = 0;
    int sizey = 50;

    public float sunRotateSpeed = 0.2f;

    public GameObject gentleup;
    public GameObject straight;
    public GameObject sun;
    public TerrainPiece[,] array;

    public bool go  =true;

    // Use this for initialization
    void Start()
    {
        /*
        array = new TerrainPiece[sizex, sizey];
        int y = 0;
        while(y < sizey)
        {
            y++;
            int x = 0;
            while(x < sizex)
            {
                x++;
                TerrainPiece newP = getNewPiece(array, x, y);
                array[x, y] = newP;
                Instantiate(newP.gameObject, newP.getPositionEastFacing(newP.gamePos), Quaternion.Euler(newP.getRotationEastFacing()));
            }

        }
        */
    }

    public TerrainPiece[,] getArray()
    {
        return array;
    }

    TerrainPiece getNewPiece(TerrainPiece[,] array, int x, int y)
    {
        int rand = Random.Range(1, 3);
        TerrainPiece prev = null;

        if(x-1 > 0) {
            prev = array[x-1,y];
        }
        if (rand == 1)
        {
            if(prev != null) {
                TerrainPiece piece = gentleup.GetComponent<TerrainPiece>();
                piece.setGamePos(new Vector3(x, prev.gamePos.y + prev.getYMod(), y));
                return piece;
            }
            else
            {
                TerrainPiece piece = gentleup.GetComponent<TerrainPiece>();
                piece.setGamePos(new Vector3(x, 1, y));
                return piece;
            }
        }
        else
        {
            if (prev != null)
            {
                TerrainPiece piece = straight.GetComponent<TerrainPiece>();
                piece.setGamePos(new Vector3(x, prev.gamePos.y + prev.getYMod(), y));
                return piece;
            }
            else
            {
                TerrainPiece piece = straight.GetComponent<TerrainPiece>();
                piece.setGamePos(new Vector3(x, 1, y));
                return piece;
            }
        }

        return null;
    }

    int getNewHeightO(int[,] array, int x, int y)
    {
        int newNum = Random.Range(-1, 2);
        //int calcSize = 0;
        ArrayList listVal = new ArrayList();
        if (x > 0)
        {
            if (array[x - 1, y] != 0)
            {
                //calcSize = calcSize + (array[x-1, y] - calcSize);
                listVal.Add(array[x - 1, y]);
            }
            if (y < sizey - 1)
            {
                if (array[x - 1, y + 1] != 0)
                {
                    //calcSize = calcSize + (array[x-1, y+1] - calcSize);
                    listVal.Add(array[x - 1, y + 1]);
                }
            }

            if (y > 0)
            {
                if (array[x - 1, y - 1] != 0)
                {
                    //calcSize = calcSize + (array[x-1, y-1] - calcSize);
                    listVal.Add(array[x - 1, y - 1]);
                }
            }
        }
        if (y > 0)
        {
            if (array[x, y - 1] != 0)
            {
                //calcSize = calcSize + (array[x , y -1] - calcSize);
                listVal.Add(array[x, y - 1]);
            }
            if (x < sizex - 1)
            {
                if (array[x + 1, y - 1] != 0)
                {
                    //calcSize = calcSize + (array[x +1, y-1 ] - calcSize);
                    listVal.Add(array[x + 1, y - 1]);
                }
            }
        }
        if (x < sizex - 1)
        {
            if (array[x + 1, y] != 0)
            {
                //calcSize = calcSize + (array[x+1 , y ] - calcSize);
                listVal.Add(array[x - 1, y]);
            }
            if (y < sizey - 1)
            {
                if (array[x + 1, y + 1] != 0)
                {
                    //calcSize = calcSize + (array[x +1, y+1 ] - calcSize);
                    listVal.Add(array[x + 1, y + 1]);
                }
            }
        }

        if (y < sizey - 1)
        {
            if (array[x, y + 1] != 0)
            {
                listVal.Add(array[x, y + 1]);
                //calcSize = calcSize + (array[x , y+1 ] - calcSize);
            }
        }

        int sum = 0;
        foreach (int i in listVal)
        {
            sum = sum + i;
        }

        return (sum / listVal.Count) + newNum;
    }

    // Update is called once per frame
    void Update()
    {
        if (go)
        {
            sun.transform.Rotate(new Vector3(sunRotateSpeed, 0, 0));
        }
        
    }
}
