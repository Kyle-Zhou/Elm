using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ResourceStates : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite passive, selected;
    public ResourceSpawner spawner;
    public List<Drop> drops;
    public int health;
}