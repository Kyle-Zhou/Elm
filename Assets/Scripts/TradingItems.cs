using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradingItems : MonoBehaviour
{
    public GameObject purchaseButtonGameObject;
    public Button purchaseButton;

    public GameObject sideMenu;
    public GameObject leftButton, rightButton;

    private int amount;
    public Text amountText;
    private int indexGlobal;

    public List<GameObject> recipes;



    public void ItemPressed(int index)
    {

        //PlayerResources.instance.AddResource(7, PlayerResources.instance.GetRockIndex());
        //PlayerResources.instance.AddResourceToInventory(7, PlayerResources.instance.GetRockIndex());

        for (int i = 0; i < recipes.Count; i++)
        {
            if (i == index)
            {
                recipes[i].SetActive(true);
            } else
            {
                recipes[i].SetActive(false);
            }
        }

        indexGlobal = index;
        amount = 0;
        ChangeAddAmount(1);
    }

    public void ChangeAddAmount(int addAmount)
    {
        purchaseButton.onClick.RemoveAllListeners();

        amount += addAmount;

        amountText.text = amount.ToString();

        CheckArrows(indexGlobal);

        switch (indexGlobal)
        {
            case 0:
                if (CheckGravel(amount)) {
                    purchaseButtonGameObject.SetActive(true);
                    Debug.Log(amount);
                    purchaseButton.onClick.AddListener(() => BuyGravel(amount));
                } else
                {
                    purchaseButtonGameObject.SetActive(false);
                }
                break;
            case 1:
                if (CheckPlatinum(amount))
                {
                    purchaseButtonGameObject.SetActive(true);
                    Debug.Log(amount);
                    purchaseButton.onClick.AddListener(() => BuyPlatinum(amount));
                }
                else
                {
                    purchaseButtonGameObject.SetActive(false);
                }
                break;
        }
    }

    public void CheckArrows(int index)
    {
        //left arrow
        if (amount == 1)
        {
            leftButton.SetActive(false);
        }
        else
        {
            leftButton.SetActive(true);
        }

        //right arrow
        if (ChooseItemToCheck(index, amount + 1))
        {
            rightButton.SetActive(true);
        } else
        {
            rightButton.SetActive(false);
        }
    }

    //this is all right arrow checking--------------
    public bool ChooseItemToCheck(int index, int amountPlusOne)
    {
        bool returnValue = false;
        switch (index)
        {
            case 0:
                returnValue = CheckGravel(amountPlusOne);
                break;
            case 1:
                returnValue = CheckPlatinum(amountPlusOne);
                break;
        }

        return returnValue;
    }
    public bool CheckGravel(int am)
    {
        int rockCost = 3;

        recipes[indexGlobal].SetActive(true);
        foreach (Transform child in recipes[indexGlobal].transform)
        {
            Text t = child.transform.GetChild(0).GetComponent<Text>();
            t.text = ((am) * rockCost).ToString();
        }

        if (PlayerResources.instance.GetRockCount() >= rockCost * am)
        {
            return true;
        }
        return false;
    }

    public bool CheckPlatinum(int am)
    {
        int gravelCost = 2;

        recipes[indexGlobal].SetActive(true);
        foreach (Transform child in recipes[indexGlobal].transform)
        {
            Text t = child.transform.GetChild(0).GetComponent<Text>();
            t.text = ((am) * gravelCost).ToString();
        }

        if (PlayerResources.instance.GetGravelCount() >= gravelCost * am)
        {
            return true;
        }
        return false;
    }

    //right arrow checking--------------


    public void BuyGravel(int addAmount)
    {
        int gravelIndex = PlayerResources.instance.GetGravelIndex();
        int rockIndex = PlayerResources.instance.GetRockIndex();
        int rockCost = 3;

        Debug.Log("addamount " + addAmount);

            //add gravel
            PlayerResources.instance.AddResource(addAmount, gravelIndex);
            PlayerResources.instance.AddResourceToInventory(addAmount, gravelIndex);

            //take away rocks
            PlayerResources.instance.AddResource(-rockCost * addAmount, rockIndex);
            PlayerResources.instance.AddResourceToInventory(-rockCost * addAmount, rockIndex);
        

    }

    public void BuyPlatinum(int addAmount)
    {
        int gravelIndex = PlayerResources.instance.GetGravelIndex();
        int platIndex = PlayerResources.instance.GetPlatIndex();
        int gravelCost = 2;

        Debug.Log("addamount " + addAmount);

        //add plat
        PlayerResources.instance.AddResource(addAmount, platIndex);
        PlayerResources.instance.AddResourceToInventory(addAmount, platIndex);

        //take away gravel
        PlayerResources.instance.AddResource(-gravelCost * addAmount, gravelIndex);
        PlayerResources.instance.AddResourceToInventory(-gravelCost * addAmount, gravelIndex);


    }



}
