using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//THIS SCRIPT IS FOR MOVING AND PLACING THE BUILDING AFTER BEING BOUGHT

public class ContainerMovementScript : MonoBehaviour
{

    private Vector2 mousePos;
    private SpriteRenderer sr;
    private Color tmp;
    private static bool placed;
    [SerializeField]
    private LayerMask grassTileLayer, buildingLayer, resourceLayer, wallTileLayer;
    private BoxCollider2D treeCollider;

    private GameObject childTree;
    private GameObject child;

    private Vector2 intialPosUponBeingMoved;

    private RaycastHit2D rayHit2;


    private void Start()
    {
        childTree = transform.GetChild(0).gameObject;
        sr = childTree.GetComponent<SpriteRenderer>();
        tmp = childTree.GetComponent<SpriteRenderer>().color;
        treeCollider = gameObject.GetComponent<BoxCollider2D>();

        //placed = true;

    }

    private void Update()
    {
        if (placed == false)
        {

            sr.sortingOrder = 4;

            //if (gameObject.CompareTag("WoodChopper"))
            //{
            //    childTree.transform.GetChild(0).gameObject.SetActive(true);
            //}

            FreezePlayer.instance.SetIsMovingBuilding(true);
            treeCollider.enabled = false;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            transform.position = new Vector2(Mathf.Round(mousePos.x) + 0.5f, Mathf.Round(mousePos.y) + 0.5f);

            tmp = Color.red;
            tmp.a = 0.5f;
            childTree.GetComponent<SpriteRenderer>().color = tmp;


            //Vector2 pos = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
            //RaycastHit2D rayHit = Physics2D.Raycast(pos, Vector2.zero, Mathf.Infinity, allTilesLayer);

            //if (gameObject.CompareTag("GoldMine"))
            //{
            //    Vector2 pos2 = new Vector2((Mathf.Round(mousePos.x)) + 1f, Mathf.Round(mousePos.y));
            //    rayHit2 = Physics2D.Raycast(pos2, Vector2.zero, Mathf.Infinity, allTilesLayer);
            //}
            //else
            //{
            //    Vector2 pos2 = pos;
            //    rayHit2 = rayHit;
            //}


            Collider2D[] grass = Physics2D.OverlapCircleAll(gameObject.transform.position, 0.25f, grassTileLayer);
            Collider2D[] walls = Physics2D.OverlapCircleAll(gameObject.transform.position, 0.25f, wallTileLayer);
            Collider2D[] resources = Physics2D.OverlapCircleAll(gameObject.transform.position, 0.25f, resourceLayer);
            Collider2D[] buildings = Physics2D.OverlapCircleAll(gameObject.transform.position, 0.25f, buildingLayer);

            //if ((rayHit.collider != null) && (rayHit2.collider != null))
            if (grass.Length != 0 && !grass[0].CompareTag("WallTilemap"))
            {
                //Debug.Log("h");
                //if ((rayHit.collider.gameObject.CompareTag("GrassTile")) && rayHit2.collider.gameObject.CompareTag("GrassTile"))
                if (walls.Length == 0 && resources.Length == 0 && buildings.Length == 0)
                {
                    sr.sortingOrder = 2;
                    tmp = Color.green;
                    tmp.a = 0.5f;
                    childTree.GetComponent<SpriteRenderer>().color = tmp;

                    if (Input.GetMouseButtonDown(0))
                    {
          
                        treeCollider.enabled = true;
                        tmp = Color.white;
                        tmp.a = 1f;
                        childTree.GetComponent<SpriteRenderer>().color = tmp;
                        placed = true;
                        FreezePlayer.instance.SetFrozen(false);
                        FreezePlayer.instance.SetIsMovingBuilding(false);

                        SoundManager.PlaySound(SoundManager.Sound.thud);


                        //if (gameObject.CompareTag("WoodChopper"))
                        //{
                        //    childTree.transform.GetChild(0).gameObject.SetActive(false);
                        //}

                        enabled = false;

                    }
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                transform.position = intialPosUponBeingMoved;
                treeCollider.enabled = true;
                tmp = Color.white;
                tmp.a = 1f;
                childTree.GetComponent<SpriteRenderer>().color = tmp;
                placed = true;
                FreezePlayer.instance.SetFrozen(false);
                FreezePlayer.instance.SetIsMovingBuilding(false);
                //if (gameObject.CompareTag("WoodChopper"))
                //{
                //    childTree.transform.GetChild(0).gameObject.SetActive(false);
                //}

                enabled = false;

            }

        }
    }

    public void SetPlaced(bool place)
    {
        intialPosUponBeingMoved = transform.position;
        placed = place;
    }

    public void SetInitalPosition()
    {
        intialPosUponBeingMoved = transform.position;
    }

}
