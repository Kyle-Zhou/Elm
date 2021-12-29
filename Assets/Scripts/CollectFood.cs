using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectFood : MonoBehaviour
{

    //public GameObject radialProgressBar2;
    public GameObject collectButton;
    private int purchaseQuantity, purchasedIndex;

    bool foodCollected = true;

    public void Start()
    {
    }

    public void SetCookingFood(bool cf)
    {
        foodCollected = cf;
    }

    public void EnableCollectButton()
    {
        collectButton.SetActive(true); 
    }

    public bool GetFoodCollected()
    {
        return foodCollected;
    }

    public void SetStats(int pq, int pi)
    {
        purchaseQuantity = pq;
        purchasedIndex = pi;
    }

    public void BuyFood()
    {
        PlayerResources.instance.AddResource(purchaseQuantity, purchasedIndex);  
        PlayerResources.instance.AddResourceToInventory(purchaseQuantity, purchasedIndex);
        foodCollected = true;
        //switch menu
        collectButton.SetActive(false);
    }

}
