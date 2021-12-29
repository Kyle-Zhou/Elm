using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class BackpackSlots : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{

    public GameObject mark;
    private Transform parentBackpack; //matsbackpack
    private GameObject incomingItem;

    private Transform currentChildItem;

    private Transform incomingParentContainer; //or backpack 
    private Transform chestUI;
    private int currentSlotValue;

    private Animator animator;

    private void Start()
    {
        parentBackpack = transform.parent.parent;
        animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        mark.SetActive(true);
        mark.transform.position = gameObject.transform.position;
        SoundManager.PlaySound(SoundManager.Sound.click);
        animator.SetTrigger("pulse");
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mark.SetActive(false);
        animator.SetTrigger("exitPulse");


    }

    public void OnDrop(PointerEventData eventData)
    {
        incomingItem = eventData.pointerDrag.gameObject;
        incomingParentContainer = incomingItem.transform.parent.parent.parent;

        incomingItem.GetComponent<Item>().CentreFit();


        if (parentBackpack.name != "SlotsHotbar") { 
            currentSlotValue = parentBackpack.GetComponent<BackpackManager>().DroppedOnSlot(gameObject);  //gets the index of this slot (the one being dropped on)
        }

        if (incomingItem.transform.parent.name == gameObject.name) //checks if incoming item's former parent is the same as the current slot
        {
            incomingItem.GetComponent<Item>().FitToSlot();
            return;
        } else
        {
            if (incomingParentContainer.name == "BackpackMats" || incomingParentContainer.name == "SlotsHotbar") { //backpack to backpack

                //checks if slot is filled
                if (gameObject.transform.childCount <= 0) { //if slot being dropped on is empty

                    //set the parent/slot of the dragged item to this one
                    incomingItem.transform.SetParent((parentBackpack.gameObject.GetComponent<BackpackManager>().slots[currentSlotValue]).transform);
                    //change the position accordingly
                    incomingItem.transform.localPosition = Vector3.zero;

                } else if (gameObject.transform.childCount >= 1) //if slot being dropped on has an item there
                {

                    currentChildItem = transform.GetChild(0);
                    currentChildItem.GetComponent<Item>().CentreFit();

                    if (incomingItem.name == currentChildItem.name)
                    {
                        currentChildItem.GetComponent<Item>().AddResourceAmount(incomingItem.GetComponent<Item>().GetResourceAmount());
                        currentChildItem.GetComponent<Item>().FitToSlot();
                        Destroy(incomingItem);
                        return;
                    } else
                    {

                        //original position and location of item being dropped
                        Transform originalParentSlot = incomingItem.GetComponent<Item>().GetOriginalParent();
                        Vector2 originalLocation = incomingItem.GetComponent<Item>().GetOriginalLocation();

                        //set the item at the location of drop's parent/slot to the dropped item's parent/slot
                        parentBackpack.gameObject.GetComponent<BackpackManager>().slots[currentSlotValue].transform.GetChild(0).SetParent(originalParentSlot);
                        //set the dropped item's parent/slot to the parent/slot of the existing item at that slot. 
                        incomingItem.transform.SetParent((parentBackpack.gameObject.GetComponent<BackpackManager>().slots[currentSlotValue]).transform);

                        //set the dropped item's position to the position of the new slot
                        incomingItem.transform.localPosition = Vector3.zero;
                        //set the position of the original item at this slot to the dropped item's old position
                        originalParentSlot.transform.GetChild(0).localPosition = originalLocation;

                        currentChildItem.GetComponent<Item>().FitToSlot(); // does this work?

                    }

                }

                incomingItem.GetComponent<Item>().FitToSlot();


            }
            else //chest to backpack
            {

                //checks if slot is filled
                if (gameObject.transform.childCount <= 0)
                {

                    //swapping resources
                    PlayerResources.instance.AddResource(incomingItem.GetComponent<Item>().GetResourceAmount(), incomingItem.GetComponent<Item>().index);
                    chestUI = incomingParentContainer.parent;
                    chestUI.GetComponent<ChestResources>().AddResource(-incomingItem.GetComponent<Item>().GetResourceAmount(), incomingItem.GetComponent<Item>().index);
                    //Debug.Log("pr " + PlayerResources.instance.GetWoodCount());
                    //Debug.Log("cr " + chestUI.GetComponent<ChestResources>().GetWood());


                    //set the parent/slot of the dragged item to this one
                    incomingItem.transform.SetParent((parentBackpack.gameObject.GetComponent<BackpackManager>().slots[currentSlotValue]).transform);
                    //change the position accordingly
                    incomingItem.transform.localPosition = Vector3.zero;

                }
                else if (gameObject.transform.childCount >= 1)
                {

                    currentChildItem = transform.GetChild(0);
                    currentChildItem.GetComponent<Item>().CentreFit();

                    if (incomingItem.name == currentChildItem.name)
                    {
                        currentChildItem.GetComponent<Item>().AddResourceAmount(incomingItem.GetComponent<Item>().GetResourceAmount());
                        currentChildItem.GetComponent<Item>().FitToSlot();

                        Destroy(incomingItem);
                        return;
                    }
                    else
                    {

                        PlayerResources.instance.AddResource(incomingItem.GetComponent<Item>().GetResourceAmount(), incomingItem.GetComponent<Item>().index);
                        Transform chestUI = incomingItem.transform.parent.parent.parent.parent;
                        chestUI.GetComponent<ChestResources>().AddResource(-incomingItem.GetComponent<Item>().GetResourceAmount(), incomingItem.GetComponent<Item>().index);
                        PlayerResources.instance.AddResource(-currentChildItem.GetComponent<Item>().GetResourceAmount(), currentChildItem.GetComponent<Item>().index);
                        chestUI.GetComponent<ChestResources>().AddResource(currentChildItem.GetComponent<Item>().GetResourceAmount(), incomingItem.GetComponent<Item>().index);

                        //Debug.Log("pr " + PlayerResources.instance.GetWoodCount());
                        //Debug.Log("cr " + chestUI.GetComponent<ChestResources>().GetWood());


                        //original position and location of item being dropped
                        Transform originalParentSlot = incomingItem.GetComponent<Item>().GetOriginalParent();
                        Vector2 originalLocation = incomingItem.GetComponent<Item>().GetOriginalLocation();

                        //set the item at the location of drop's parent/slot to the dropped item's parent/slot
                        currentChildItem.SetParent(originalParentSlot);
                        //set the dropped item's parent/slot to the parent/slot of the existing item at that slot. 
                        incomingItem.transform.SetParent((parentBackpack.gameObject.GetComponent<BackpackManager>().slots[currentSlotValue]).transform);

                        //set the dropped item's position to the position of the new slot
                        incomingItem.transform.localPosition = Vector3.zero;
                        //set the position of the original item at this slot to the dropped item's old position
                        originalParentSlot.transform.GetChild(0).localPosition = originalLocation;


                        currentChildItem.GetComponent<Item>().FitToSlot(); //?

                    }
                }
                incomingItem.GetComponent<Item>().FitToSlot();

            }
        }


        
    }
}

