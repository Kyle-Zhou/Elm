using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangedMob : Mob
{
    public Vector3 destination;
    public GameObject projectile;
    public Collider2D[] targets;

    public abstract void Shoot();

    public float CalculateAngle(Vector2 trajectory)
    {
        float angle = Mathf.Atan2(trajectory.y, trajectory.x) * Mathf.Rad2Deg;

        if (angle < 0)
        {
            angle += 360;
        }

        return angle;
    }
}
