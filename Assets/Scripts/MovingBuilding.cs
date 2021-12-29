using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//THIS SCRIPT IS FOR MOVING AND PLACING THE BUILDING AFTER BEING BOUGHT

public class MovingBuilding : MonoBehaviour
{
    private Vector2 mousePos;

    private BoxCollider2D workShopCollider;
    public SpriteRenderer sr;
    private Color tmp;
    private bool placed;
    //public bool bought;
    [SerializeField]
    private LayerMask grassTileLayer, buildingLayer, resourceLayer, wallTileLayer;
    //[SerializeField]
    //private LayerMask grassTiles;

    private Vector2 intialPosUponBeingMoved;

    private void Start()
    {
        tmp = gameObject.GetComponent<SpriteRenderer>().color;
        workShopCollider = gameObject.GetComponent<BoxCollider2D>();
        //placed = true;

    }

    private void Update()
    {
        if (placed == false)
        {
            sr.sortingOrder = 4;

            FreezePlayer.instance.SetIsMovingBuilding(true);
            workShopCollider.enabled = false;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //if (gameObject.CompareTag("NormalTree"))
            //{
            //    transform.position = new Vector2(Mathf.Round(mousePos.x) - 0.1f, Mathf.Round(mousePos.y + 0.15f) + 0.5f);
            //    transform.GetChild(0).gameObject.SetActive(false);

            //}
            //else
            //{
                //if it's within the params it affects position in relation to mousePos

           transform.position = new Vector2(Mathf.Round(mousePos.x) + 0.5f, Mathf.Round(mousePos.y) + 0.5f);

            //}            //transform.position = new Vector2((mousePos.x), (mousePos.y));

            tmp = Color.red;
            tmp.a = 0.5f;
            gameObject.GetComponent<SpriteRenderer>().color = tmp;

            //Vector2 mouseRay = Camera.main.ScreenToWorldPoint(transform.position);

            //Vector2 pos = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
            //RaycastHit2D rayHit = Physics2D.Raycast(pos, Vector2.zero, Mathf.Infinity, allTilesLayer);

            Collider2D[] grass = Physics2D.OverlapCircleAll(gameObject.transform.position, 0.25f, grassTileLayer);
            Collider2D[] walls = Physics2D.OverlapCircleAll(gameObject.transform.position, 0.25f, wallTileLayer);
            Collider2D[] resources = Physics2D.OverlapCircleAll(gameObject.transform.position, 0.25f, resourceLayer);
            Collider2D[] buildings = Physics2D.OverlapCircleAll(gameObject.transform.position, 0.25f, buildingLayer);

            if (grass.Length != 0 && !grass[0].CompareTag("WallTilemap"))
            {
                //Debug.Log("h");
                if (walls.Length == 0 && resources.Length == 0 && buildings.Length == 0)
                {
                    sr.sortingOrder = 2;
                    tmp = Color.green;
                    tmp.a = 0.5f;
                    gameObject.GetComponent<SpriteRenderer>().color = tmp;

                    if (Input.GetMouseButtonDown(0))
                    {
                        //Instantiate(finalObject, transform.position, Quaternion.identity)
                        //gameObject.transform.position(transform.position);
                        //JUSST DISABLE THIS SCRIPT
                        workShopCollider.enabled = true;
                        tmp = Color.white;
                        tmp.a = 1f;
                        gameObject.GetComponent<SpriteRenderer>().color = tmp;
                        placed = true;
                        FreezePlayer.instance.SetFrozen(false);
                        FreezePlayer.instance.SetIsMovingBuilding(false);
                        SoundManager.PlaySound(SoundManager.Sound.thud);

                    }
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                transform.position = intialPosUponBeingMoved;
                workShopCollider.enabled = true;
                tmp = Color.white;
                tmp.a = 1f;
                gameObject.GetComponent<SpriteRenderer>().color = tmp;
                placed = true;
                FreezePlayer.instance.SetFrozen(false);
                FreezePlayer.instance.SetIsMovingBuilding(false);

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
