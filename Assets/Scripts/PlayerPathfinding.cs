using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlayerPathfinding : MonoBehaviour
{

    public static PlayerPathfinding instance;
    public GameObject aStar;
    private GameObject target;
    private static bool pathfinding;

    private void Start()
    {
        pathfinding = false;
        instance = this;
    }

    public void Pathfind(){
        Rescan();
        pathfinding = true;
        Debug.Log("pathfinding true");
        //gameObject.GetComponent<PlayerMovement>().enabled = false;
        gameObject.GetComponent<AIPath>().enabled = true;
        gameObject.GetComponent<AIDestinationSetter>().enabled = true;
        gameObject.GetComponent<AIDestinationSetter>().target = target.transform;


      // gameObject.GetComponent<CircleCollider2D>().isTrigger = true;

    }

    public void CancelPathFind()
    {
        gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
        pathfinding = false;
        gameObject.GetComponent<AIPath>().enabled = false;
        gameObject.GetComponent<AIDestinationSetter>().enabled = false;

        Debug.Log("cancel path find");

        //if no error
        target.GetComponent<TemplateScript>().DestroyBuildingAndMenu();
        //else
        //target.GetComponent<MovingBuilding>().
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (target != null) {

            if (collision.gameObject.name == target.name)
            {
                gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
                target.GetComponent<TemplateScript>().OfficiallyBuild();
                pathfinding = false;
                gameObject.GetComponent<AIPath>().enabled = false;
                gameObject.GetComponent<AIDestinationSetter>().enabled = false;
            }
        }

    }

    public void SetCurrentTarget(GameObject reference)
    {
        target = reference;
    }

    public bool GetPathfinding()
    {
        return pathfinding;
    }

    public void Rescan()
    {
        aStar.GetComponent<AstarPath>().Scan();
        Debug.Log(1);
    }

}
