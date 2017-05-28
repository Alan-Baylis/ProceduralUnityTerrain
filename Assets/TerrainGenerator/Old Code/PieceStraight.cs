using UnityEngine;
using System.Collections;

public class PieceStraight : TerrainPiece {
    static Vector3 baseRot = new Vector3(0, 0, 0);

    static Vector3 basePos = new Vector3(0, 0, 0);

    static float ymod = 0;

    public override Vector3 getBasePos()
    {
        return basePos;
    }

    public override Vector3 getBaseRot()
    {
        return baseRot;
    }

    public override float getYMod()
    {
        return ymod;
    }
}
