using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//THIS SCRIPT IS FOR MOVING AND PLACING THE TREE CONTAINER

public class ContainerTemplateScript : MonoBehaviour
{
    //[SerializeField]
    //private GameObject finalObject;
    private Vector2 mousePos;

    private SpriteRenderer sr;
    private Color tmp;
    private static bool placed;
    public bool bought;
    [SerializeField]
    private LayerMask grassTileLayer, buildingLayer, resourceLayer, wallTileLayer;
    //[SerializeField]
    //private LayerMask grassTiles;
    private BoxCollider2D treeCollider;

    private GameObject childTree;

    private GameObject workShopMenu;

    private RaycastHit2D rayHit2;

    private void Start()
    {
        childTree = transform.GetChild(0).gameObject;
        sr = childTree.GetComponent<SpriteRenderer>();
        tmp = sr.color;
        treeCollider = gameObject.GetComponent<BoxCollider2D>();
        placed = false;

        if (gameObject.CompareTag("WoodChopper"))
        {
            childTree.transform.GetChild(0).gameObject.SetActive(true);
        }

    }

    private void Update()
    {
        if (placed == false)
        {
            FreezePlayer.instance.SetFrozen(true);

            sr.sortingOrder = 4;

            treeCollider.enabled = false;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                //if it's within the params it affects position in relation to mousePos
            transform.position = new Vector2(Mathf.Round(mousePos.x) + 0.5f, Mathf.Round(mousePos.y) + 0.5f);
            
            //transform.position = new Vector2((mousePos.x), (mousePos.y));

            tmp = Color.red;
            tmp.a = 0.5f;
            sr.color = tmp;

            //Vector2 mouseRay = Camera.main.ScreenToWorldPoint(transform.position);

            Collider2D[] grass = Physics2D.OverlapCircleAll(gameObject.transform.position, 0.25f, grassTileLayer);
            Collider2D[] walls = Physics2D.OverlapCircleAll(gameObject.transform.position, 0.25f, wallTileLayer);
            Collider2D[] resources = Physics2D.OverlapCircleAll(gameObject.transform.position, 0.25f, resourceLayer);
            Collider2D[] buildings = Physics2D.OverlapCircleAll(gameObject.transform.position, 0.25f, buildingLayer);

            //if (gameObject.CompareTag("GoldMine")) { 
            //Vector2 pos2 = new Vector2((Mathf.Round(mousePos.x))+1f, Mathf.Round(mousePos.y));
            //rayHit2 = Physics2D.Raycast(pos2, Vector2.zero, Mathf.Infinity, allTilesLayer);
            //} else {
            //Vector2 pos2 = pos;
            //    rayHit2 = rayHit;
            //}

            if (Input.GetMouseButtonDown(1))
            {
                FreezePlayer.instance.SetFrozen(false);
                FreezePlayer.instance.SetIsMovingBuilding(false);
                Destroy(gameObject);

                //if ((gameObject.CompareTag("WoodChopper")) || (gameObject.CompareTag("Forge"))) { 
                //    Destroy(workShopMenu);
                //}

                //else if (gameObject.CompareTag("TreeSapling"))
                //{
                //    PlayerResources.instance.AddResource(1, 10);
                //    PlayerResources.instance.AddResourceToInventory(1,10);
                //}


                enabled = false;


            }

            //if ((rayHit.collider != null) && (rayHit2.collider != null))
            if (grass.Length != 0 && !grass[0].CompareTag("WallTilemap"))
            {
                //Debug.Log("h");


                //if ((rayHit.collider.gameObject.CompareTag("GrassTile")) && rayHit2.collider.gameObject.CompareTag("GrassTile"))
                if (walls.Length == 0 && resources.Length == 0 && buildings.Length == 0)
                {
                    sr.sortingOrder = 2;
                    //Debug.Log("f");
                    tmp = Color.green;
                    tmp.a = 0.5f;
                    sr.color = tmp;

                    if (Input.GetMouseButtonDown(0))
                    {
                        //if (gameObject.CompareTag("WoodChopper"))
                        //{
                        //    BuildMenuScript.instance.BuyWoodChopper();
                        //    childTree.transform.GetChild(0).gameObject.SetActive(false);

                        //}
                        if (gameObject.CompareTag("Forge"))
                        {
                            BuildMenuScript.instance.BuyForge();
                        } else if (gameObject.CompareTag("Anvil"))
                        {
                            BuildMenuScript.instance.BuyWorkShop();
                        } else if (gameObject.CompareTag("Chest"))
                        {
                            BuildMenuScript.instance.BuyChest();
                        }
                        //} else if (childTree.CompareTag("NormalTree"))
                        //{
                        //    childTree.transform.GetChild(0).gameObject.SetActive(true);
                        //}

                        FreezePlayer.instance.SetFrozen(false);
                        FreezePlayer.instance.SetIsMovingBuilding(false);

                        SoundManager.PlaySound(SoundManager.Sound.thud);

                        tmp = Color.white;
                        tmp.a = 1.0f;
                        sr.color = tmp;
                        gameObject.GetComponent<ContainerTemplateScript>().enabled = false;
                        treeCollider.enabled = true;
                        placed = true;

                        enabled = false;


                    }
                }
            }
        }
    }

    public void SetWorkShopMenu(GameObject wsm)
    {
        workShopMenu = wsm;
    }




}
