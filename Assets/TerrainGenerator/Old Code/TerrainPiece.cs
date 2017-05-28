using UnityEngine;
using System.Collections;

public abstract class TerrainPiece : MonoBehaviour {

    public Vector3 gamePos;

    public virtual Vector3 getBasePos()
    {
        return Vector3.zero;
    }

    public virtual Vector3 getBaseRot()
    {
        return Vector3.zero;
    }

    public virtual Vector3 getRotationNorthFacing()
    {
        return getBaseRot();
    }

    public virtual Vector3 getRotationEastFacing()
    {
        return new Vector3(getBaseRot().x, 90, getBaseRot().z);
    }

    public virtual Vector3 getRotationSouthFacing()
    {
        return new Vector3(getBaseRot().x, 180, getBaseRot().z);
    }

    public virtual Vector3 getRotationWestFacing()
    {
        return new Vector3(getBaseRot().x, 270, getBaseRot().z);
    }


    public virtual Vector3 getPositionNorthFacing(Vector3 pos)
    {
        return pos + getBasePos();
    }

    public virtual Vector3 getPositionEastFacing(Vector3 pos)
    {
        return pos + getBasePos();
    }

    public virtual Vector3 getPositionSouthFacing(Vector3 pos)
    {
        return pos + getBasePos();
    }

    public virtual Vector3 getPositionWestFacing(Vector3 pos)
    {
        return pos + getBasePos();
    }

    public virtual GameObject getGameObject()
    {
        return null;
    }

    public virtual void setGamePos(Vector3 location)
    {
        gamePos = location;
    }

    public virtual float getYMod()
    {
        return 1;
    }
	
}
