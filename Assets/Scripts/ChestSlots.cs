using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ChestSlots : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    public GameObject mark;
    private Transform parentChest; //container/
    private GameObject incomingItem;

    private Transform currentChildItem;

    private Transform incomingParentBackpack;
    private int currentSlotValue;

    public void Start()
    {
        parentChest = transform.parent.parent.parent;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.PlaySound(SoundManager.Sound.click);
        mark.SetActive(true);
        mark.transform.position = gameObject.transform.position;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mark.SetActive(false);
    }


    public void OnDrop(PointerEventData eventData)
    {
        incomingItem = eventData.pointerDrag.gameObject;
        incomingParentBackpack = incomingItem.transform.parent.parent.parent;
        currentSlotValue = parentChest.GetComponent<ChestManager>().DroppedOnSlot(gameObject);

        incomingItem.GetComponent<Item>().CentreFit();


        if (incomingItem.transform.parent.name == gameObject.name) //checks if incoming item's former parent is the same as the current slot
        {
            incomingItem.GetComponent<Item>().FitToSlot();
            return;
        }
        else
        {
            //backpack to chest
            if (incomingParentBackpack.name == "BackpackMats" || incomingParentBackpack.name == "SlotsHotbar")
            {

                if (gameObject.transform.childCount <= 0)
                {
                    //gets the index of this slot (the one being dropped on)

                    //set the parent/slot of the dragged item to this one
                    incomingItem.transform.SetParent((parentChest.gameObject.GetComponent<ChestManager>().slots[currentSlotValue]).transform);
                    //change the position accordingly
                    incomingItem.transform.localPosition = Vector3.zero;


                    PlayerResources.instance.AddResource(-incomingItem.GetComponent<Item>().GetResourceAmount(), incomingItem.GetComponent<Item>().index);
                    parentChest.GetComponent<ChestResources>().AddResource(incomingItem.GetComponent<Item>().GetResourceAmount(), incomingItem.GetComponent<Item>().index);

                    //Debug.Log("pr " + PlayerResources.instance.GetRockCount());
                    //Debug.Log("cr " + parentChest.GetComponent<ChestResources>().GetRocks());


                } else if (gameObject.transform.childCount >= 1)
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
                        PlayerResources.instance.AddResource(-incomingItem.GetComponent<Item>().GetResourceAmount(), incomingItem.GetComponent<Item>().index);
                        parentChest.GetComponent<ChestResources>().AddResource(incomingItem.GetComponent<Item>().GetResourceAmount(), incomingItem.GetComponent<Item>().index);
                        ////
                        Debug.Log(currentChildItem.GetComponent<Item>().GetResourceAmount());
                        Debug.Log(currentChildItem.GetComponent<Item>().index);
                        ////
                        PlayerResources.instance.AddResource(currentChildItem.GetComponent<Item>().GetResourceAmount(), currentChildItem.GetComponent<Item>().index);
                        parentChest.GetComponent<ChestResources>().AddResource(currentChildItem.GetComponent<Item>().GetResourceAmount(), incomingItem.GetComponent<Item>().index);

                        //Debug.Log("pr " + PlayerResources.instance.GetWoodCount());
                        //Debug.Log("cr " + parentChest.GetComponent<ChestResources>().GetWood());


                        //original position and location of item being dropped
                        Transform originalParent = incomingItem.GetComponent<Item>().GetOriginalParent();
                        Vector2 originalLocation = incomingItem.GetComponent<Item>().GetOriginalLocation();

                        //set the item at the location of drop's parent/slot to the dropped item's parent/slot
                        parentChest.gameObject.GetComponent<ChestManager>().slots[currentSlotValue].transform.GetChild(0).SetParent(originalParent);
                        //set the dropped item's parent/slot to the parent/slot of the existing item at that slot. 
                        incomingItem.transform.SetParent((parentChest.gameObject.GetComponent<ChestManager>().slots[currentSlotValue]).transform);

                        //set the dropped item's position to the position of the new slot
                        incomingItem.transform.localPosition = Vector3.zero;
                        //set the position of the original item at this slot to the dropped item's old position
                        originalParent.transform.GetChild(0).localPosition = originalLocation;

                        currentChildItem.GetComponent<Item>().FitToSlot(); // does this work?


                    }
                }
                incomingItem.GetComponent<Item>().FitToSlot();


            }
            else { //chest to chest

                //checks if slot is filled
                if (gameObject.transform.childCount <= 0)
                {
                    //set the parent/slot of the dragged item to this one
                    incomingItem.transform.SetParent((parentChest.gameObject.GetComponent<ChestManager>().slots[currentSlotValue]).transform);
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

                        //original position and location of item being dropped
                        Transform originalParent = incomingItem.GetComponent<Item>().GetOriginalParent();
                        Vector2 originalLocation = incomingItem.GetComponent<Item>().GetOriginalLocation();

                        //set the item at the location of drop's parent/slot to the dropped item's parent/slot
                        parentChest.gameObject.GetComponent<ChestManager>().slots[currentSlotValue].transform.GetChild(0).SetParent(originalParent);
                        //set the dropped item's parent/slot to the parent/slot of the existing item at that slot. 
                        incomingItem.transform.SetParent((parentChest.gameObject.GetComponent<ChestManager>().slots[currentSlotValue]).transform);

                        //set the dropped item's position to the position of the new slot
                        incomingItem.transform.localPosition = Vector3.zero;
                        //set the position of the original item at this slot to the dropped item's old position
                        originalParent.transform.GetChild(0).localPosition = originalLocation;
                        currentChildItem.GetComponent<Item>().FitToSlot(); //?

                    }
                }

                incomingItem.GetComponent<Item>().FitToSlot();


            }




        }

    }
}
