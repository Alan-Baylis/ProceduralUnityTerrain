using UnityEngine;
using System.Collections;

public class PieceGentleUp : TerrainPiece {

    static Vector3 baseRot = new Vector3(-26.28f, 0, 0);

    static Vector3 basePos = new Vector3(0, 0.251f, -0.003f);

    static float ymod = 0.5f;

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
