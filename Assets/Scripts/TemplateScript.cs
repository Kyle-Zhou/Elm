using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//THIS SCRIPT IS FOR MOVING AND PLACING THE BUILDING

public class TemplateScript : MonoBehaviour
{
    //[SerializeField]
    //private GameObject finalObject;
    private Vector2 mousePos;

    public SpriteRenderer sr;
    private Color tmp;
    private static bool placed;
    public bool bought;
    [SerializeField]
    private LayerMask grassTileLayer, buildingLayer, resourceLayer, wallTileLayer;
    //[SerializeField]
    //private LayerMask grassTiles;
    private BoxCollider2D workShopCollider;

    private GameObject workShopMenu;

    private void Start()
    {
        tmp = gameObject.GetComponent<SpriteRenderer>().color;
        workShopCollider = gameObject.GetComponent<BoxCollider2D>();
        placed = false;

    }

    private void Update()
    {
        if (placed == false) {
            workShopCollider.enabled = false;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                //if it's within the params it affects position in relation to mousePos
            transform.position = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
            
            //transform.position = new Vector2((mousePos.x), (mousePos.y));

            tmp = Color.red;
            tmp.a = 0.5f;
            gameObject.GetComponent<SpriteRenderer>().color = tmp;

            Collider2D[] grass = Physics2D.OverlapCircleAll(gameObject.transform.position, 0.5f, grassTileLayer);
            Collider2D[] walls = Physics2D.OverlapCircleAll(gameObject.transform.position, 0.5f, wallTileLayer);
            Collider2D[] resources = Physics2D.OverlapCircleAll(gameObject.transform.position, 0.5f, resourceLayer);
            Collider2D[] buildings = Physics2D.OverlapCircleAll(gameObject.transform.position, 0.5f, buildingLayer);


            if (Input.GetMouseButtonDown(1))
            {
                //gameObject.SetActive(false);
                FreezePlayer.instance.SetFrozen(false);
                FreezePlayer.instance.SetIsMovingBuilding(false);
                Destroy(workShopMenu);
                Destroy(gameObject);

            }

            if (grass.Length != 0) { 
                //Debug.Log("h");

                //if (rayHit.collider.gameObject.CompareTag("BarrierTile"))
                //{
                //    Debug.Log("b");
                //}

                if (walls.Length == 0 && resources.Length == 0 && buildings.Length == 0) {
                    tmp = Color.green;
                    tmp.a = 0.5f;
                    gameObject.GetComponent<SpriteRenderer>().color = tmp;

                    if (Input.GetMouseButtonDown(0)) {

                        //PlayerPathfinding.instance.SetCurrentTarget(gameObject);
                        //PlayerPathfinding.instance.Pathfind();

                        //tmp.a = 0.0f;
                        //gameObject.GetComponent<SpriteRenderer>().color = tmp;

                        //comment this line for pathfinding
                        OfficiallyBuild();

                        placed = true;
                        workShopCollider.enabled = true;

                        FreezePlayer.instance.SetFrozen(false);
                        FreezePlayer.instance.SetIsMovingBuilding(false);

                    }
                } 
            }
        } 
    }

    public void OfficiallyBuild()
    {
        tmp = Color.white;
        tmp.a = 1.0f;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;
        gameObject.GetComponent<TemplateScript>().enabled = false;
        workShopCollider.enabled = true;
        placed = true;

        if (gameObject.CompareTag("WorkShop"))
        {
            BuildMenuScript.instance.BuyWorkShop();
        }
    }

    public void SetWorkShopMenu(GameObject wsm)
    {
        workShopMenu = wsm;
    }

    public void DestroyBuildingAndMenu()
    {
        Destroy(workShopMenu);
        Destroy(gameObject);
    }

    //public void SetPlaced(bool place)
    //{
    //    placed = place;
    //}




}
