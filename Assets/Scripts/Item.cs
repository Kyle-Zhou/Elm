using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    //on every item button

    public int index;

    private Transform originalParent;
    private Vector2 originalLocation;
    private bool isDragging;

    private float clickTime;
    private float clicked = 0;
    private float clickDelay = 1.2f;

    private Animator animator;
    private RectTransform rt;

    private int resourceAmount;

    public void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        rt = gameObject.GetComponent<RectTransform>();

    }

    public int GetResourceAmount()
    {
        return resourceAmount;
    }

    public void AddResourceAmount(int addAmount)
    {
        resourceAmount += addAmount;
        Debug.Log(resourceAmount);
        gameObject.transform.GetComponentInChildren<Text>().text = "x" + resourceAmount.ToString();

        if (resourceAmount <= 0)
        {
            Destroy(gameObject);
        }
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //if (OpenBackpack.instance.GetBackpackOn())
        //{
        //    BackpackPopup.instance.RemovePopUp(index);
        //}
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //clicked++;

            //if (clicked == 1) clickTime = Time.time;

            //if (clicked > 1 && Time.time - clickTime < clickDelay) { //double click
            //    clicked = 0;
            //    clickTime = 0;
            //}
            //else if (clicked > 2 || Time.time - clickTime > 1)
            //{
            //    clicked = 0;
                
            //}

            isDragging = true;
            originalParent = transform.parent;
            originalLocation = transform.localPosition;
            transform.SetParent(transform.parent.parent.parent.parent.parent);
            GetComponent<CanvasGroup>().blocksRaycasts = false;


        } else if (eventData.button == PointerEventData.InputButton.Right)
        {

            
            clickTime = Time.time;
            //start anim

            animator.SetTrigger("ItemFill");

        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) { 
            transform.position = Input.mousePosition;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Left) { 
            isDragging = false;
            transform.SetParent(originalParent);
            transform.localPosition = Vector3.zero;
            GetComponent<CanvasGroup>().blocksRaycasts = true;

        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            animator.SetTrigger("ItemIdle");

            if (clickTime + clickDelay <= Time.time) { //held Down

                return;

            } else { 

                if (gameObject.CompareTag("FoodButton"))
                {
                    if (HealthBar.instance.GetHealth() < 100) { 
                        animator.SetTrigger("ItemGreen");
                        EatingScript.instance.EatBlueberry();
                        animator.SetTrigger("ItemIdle");
                        SoundManager.PlaySound(SoundManager.Sound.crunchBite);
                    }
                } else if (gameObject.CompareTag("BreadButton"))
                {
                    if (HealthBar.instance.GetHealth() < 100)
                    {
                        animator.SetTrigger("ItemGreen");
                        EatingScript.instance.EatBread();
                        animator.SetTrigger("ItemIdle");
                        SoundManager.PlaySound(SoundManager.Sound.crunchBite);
                    }
                } else if (gameObject.CompareTag("PieButton"))
                {
                    if (HealthBar.instance.GetHealth() < 100)
                    {
                        animator.SetTrigger("ItemGreen");
                        EatingScript.instance.EatPie();
                        animator.SetTrigger("ItemIdle");
                        SoundManager.PlaySound(SoundManager.Sound.crunchBite);
                    }
                }
                else if (gameObject.CompareTag("VegetableButton"))
                {
                    if (HealthBar.instance.GetHealth() < 100)
                    {

                        animator.SetTrigger("ItemGreen");
                        EatingScript.instance.EatCarrot();
                        animator.SetTrigger("ItemIdle");
                        SoundManager.PlaySound(SoundManager.Sound.crunchBite);
                    }
                }
            }
            //end anim

        }
    }

    public void CentreFit()
    {

        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);

        Debug.Log("CentreFit()");

    }

    public void FitToSlot()
    {


        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(1, 1);
        rt.pivot = new Vector2(0.5f, 0.5f);

        Debug.Log("FitToSlot()");

    }

    public void Delete()
    {
        //Debug.Log("held down - anim");

        //if chest
        //c_slot, slots, container, ChestUI
        if (transform.parent.parent.parent.parent.CompareTag("Chest"))
        {
            GameObject chest = transform.parent.parent.parent.parent.gameObject;
            chest.GetComponent<ChestResources>().AddResource(-1, index);
            AddResourceAmount(-1);
        }
        //if inventory
        else
        {
            PlayerResources.instance.AddResource(-1, index);
            AddResourceAmount(-1);
            //PlayerResources.instance.AddResourceToInventory(-1, index);
        }
        SoundManager.PlaySound(SoundManager.Sound.deleteSwoosh);


    }

    public Transform GetOriginalParent()
    {
        return originalParent;
    }
    public Vector2 GetOriginalLocation()
    {
        return originalLocation;
    }


}