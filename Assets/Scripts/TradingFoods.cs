using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradingFoods : MonoBehaviour
{

    public List<GameObject> foodButtons;
    private List<GameObject> availableFoodButtons = new List<GameObject>();

    public List<GameObject> buyingFoodImages;

    public GameObject leftArrow;
    public GameObject rightArrow;
    public Text purchaseQuantityText;
    public GameObject sideMenu;

    public GameObject menu2;

    private List<int> foodCostIndex = new List<int>();
    private List<int> foodCostAmount = new List<int>();

    private static bool toMoveCostImgs;
    private int purchaseQuantity = 1;

    private int purchasedIndex;

    public GameObject radialBar;

    //public void LateUpdate()
    //{
    //    Check();
    //    AddAndOrient();
    //    enabled = false;
    //}

    public void OnEnable()
    {

        ///TEMPORARY

        //PlayerResources.instance.AddResource(10, PlayerResources.instance.GetBlueberryIndex());
        //PlayerResources.instance.AddResourceToInventory(10, PlayerResources.instance.GetBlueberryIndex());
        //PlayerResources.instance.AddResource(10, PlayerResources.instance.GetCarrotIndex());  //what is index
        //PlayerResources.instance.AddResourceToInventory(10, PlayerResources.instance.GetCarrotIndex());
        //PlayerResources.instance.AddResource(10, PlayerResources.instance.GetWheatIndex());  //what is index
        //PlayerResources.instance.AddResourceToInventory(10, PlayerResources.instance.GetWheatIndex());
        //PlayerResources.instance.AddResource(10, PlayerResources.instance.GetBreadIndex());  //what is index
        //PlayerResources.instance.AddResourceToInventory(10, PlayerResources.instance.GetBreadIndex());


        foreach (GameObject f in availableFoodButtons)
        {
            f.SetActive(false);
        }
        availableFoodButtons.Clear();

        Check();
        AddAndOrient();
        leftArrow.SetActive(false);
        rightArrow.SetActive(true);
        sideMenu.SetActive(false);
        purchaseQuantityText.text = "1";
        purchaseQuantity = 1;
        toMoveCostImgs = true;
    }

    public void Check()
    {
        if (PlayerResources.instance.GetBlueberryCount() >= 3 && PlayerResources.instance.GetWheatCount() >= 3) //blueberry pie
        {
            availableFoodButtons.Add(foodButtons[0]);
        }                                                      
        if (PlayerResources.instance.GetWheatCount() >= 5) //bread
        {
            availableFoodButtons.Add(foodButtons[1]);
        }
    }
    //for buttons
    public void AddAndOrient()
    {
        //int count = 0;
        //foreach(GameObject button in availableFoodButtons)
        //{
        //    button.SetActive(true);

        //    RectTransform pos = button.transform as RectTransform;
        //    pos.anchoredPosition = new Vector2(pos.anchoredPosition.x, 120f);
        //    pos.anchoredPosition = new Vector2(pos.anchoredPosition.x, pos.anchoredPosition.y-count);
        //    Debug.Log(count);
        //    count += 50;
        //}
        //count = 0;

        for (int i = 0; i < availableFoodButtons.Count; i++)
        {
            availableFoodButtons[i].SetActive(true);
            RectTransform pos = availableFoodButtons[i].transform as RectTransform;
            pos.anchoredPosition = new Vector2(pos.anchoredPosition.x, 120f);
            pos.anchoredPosition = new Vector2(pos.anchoredPosition.x, pos.anchoredPosition.y - (i*65));
            //Debug.Log(i*50);
        }

    }

    public void FoodButtonClicked(int index)
    {
        ClearCostImages();
        purchaseQuantity = 1;
        purchaseQuantityText.text = purchaseQuantity.ToString();

        sideMenu.SetActive(true);
        foodCostIndex.Clear();
        foodCostAmount.Clear();

        if (index == 0) //blueberry pie
        {
            foodCostIndex.Add(PlayerResources.instance.GetBlueberryIndex());
            foodCostAmount.Add(3);

            foodCostIndex.Add(PlayerResources.instance.GetWheatIndex());
            foodCostAmount.Add(3);

            purchasedIndex = 13;

        } else if (index == 1) //bread
        {
            foodCostIndex.Add(PlayerResources.instance.GetWheatIndex()); //index from PlayerResources.cs
            foodCostAmount.Add(5);

            purchasedIndex = 17;

        }


        toMoveCostImgs = true;
        AlterCostNumbers();
        toMoveCostImgs = false;

        //check alter food purchases amount without arrow button being pressed
        if (purchaseQuantity <= 1)
        {
            leftArrow.SetActive(false);
        }
        else
        {
            leftArrow.SetActive(true);
        }
        bool ra = true;
        for (int i = 0; i < foodCostIndex.Count; i++)
        {
            if (PlayerResources.instance.GetResourceCount(foodCostIndex[i]) < (purchaseQuantity + 1) * foodCostAmount[i])
            {
                ra = false;
                rightArrow.SetActive(false);
            }
        }
        if (ra)
        {
            rightArrow.SetActive(true);
        }
        ////
    }

    public void AlterFoodPurchaseAmount(int difference)
    {
        purchaseQuantity += difference;
        purchaseQuantityText.text = purchaseQuantity.ToString(); 
        if (purchaseQuantity <= 1)
        {
            leftArrow.SetActive(false);
        } else {

            leftArrow.SetActive(true);
        }
        bool ra = true;
        for (int i = 0; i < foodCostIndex.Count; i++)
        {
            if (PlayerResources.instance.GetResourceCount(foodCostIndex[i]) < (purchaseQuantity + 1) * foodCostAmount[i]) 
            {
                ra = false;
                rightArrow.SetActive(false);
            } 
        }
        if (ra)
        {
            rightArrow.SetActive(true);
        }
        
        AlterCostNumbers();
    }

    public void AlterCostNumbers()
    {
        int count = 0;
        for (int i = 0; i < foodCostIndex.Count; i++) //subtract all the costs
        {
            if (foodCostIndex[i] == PlayerResources.instance.GetBlueberryIndex())
            {
                GameObject item = buyingFoodImages[0];
                item.SetActive(true);
                if (toMoveCostImgs) { 
                    RectTransform pos = item.transform as RectTransform;
                    pos.anchoredPosition = new Vector2(pos.anchoredPosition.x, 90 - count);
                }
                Text text = item.transform.GetChild(0).GetComponent<Text>();
                text.text = (foodCostAmount[i] * purchaseQuantity).ToString();

            } else if (foodCostIndex[i] == PlayerResources.instance.GetCarrotIndex())
            {

                GameObject item = buyingFoodImages[1];
                item.SetActive(true);
                if (toMoveCostImgs)
                {
                    RectTransform pos = item.transform as RectTransform;
                    pos.anchoredPosition = new Vector2(pos.anchoredPosition.x, 90 - count);
                }
                Text text = item.transform.GetChild(0).GetComponent<Text>();
                text.text = (foodCostAmount[i] * purchaseQuantity).ToString();
            }
            else if (foodCostIndex[i] == PlayerResources.instance.GetWheatIndex())
            {

                GameObject item = buyingFoodImages[2];
                item.SetActive(true);
                if (toMoveCostImgs)
                {
                    RectTransform pos = item.transform as RectTransform;
                    pos.anchoredPosition = new Vector2(pos.anchoredPosition.x, 90 - count);
                }
                Text text = item.transform.GetChild(0).GetComponent<Text>();
                text.text = (foodCostAmount[i] * purchaseQuantity).ToString();
            }
            count += 20;
                

        }
    }

    public void ClearCostImages()
    {
        foreach(GameObject img in buyingFoodImages)
        {
            img.SetActive(false);
        }
    }

    public void BuyFood()
    {
 
        for (int i = 0; i < foodCostIndex.Count; i++) //subtract all the costs
        {
            PlayerResources.instance.AddResource(-(foodCostAmount[i] * purchaseQuantity), foodCostIndex[i]);
            PlayerResources.instance.AddResourceToInventory(-(foodCostAmount[i] * purchaseQuantity), foodCostIndex[i]);
        }


        sideMenu.SetActive(false);

        //set timer
        radialBar.SetActive(true);
        radialBar.transform.GetChild(0).GetComponent<RadialProgressBar>().SetRun(true);
        radialBar.transform.GetChild(0).GetComponent<RadialProgressBar>().SetTimeIncrementValue(0.6f);
        FreezePlayer.instance.SetFrozen(false);
        //RadialProgressBar.instance.SetRun(true);
        //RadialProgressBar.instance.SetTimeIncrementValue(0.2f);

        menu2.GetComponent<CollectFood>().SetStats(purchaseQuantity, purchasedIndex);
        menu2.GetComponent<CollectFood>().SetCookingFood(false);

        gameObject.SetActive(false);
    }

    //IEnumerator FlashPurchasedText()
    //{
    //    boughtSymbol.SetActive(true);
    //    yield return new WaitForSeconds(1.0f);
    //    boughtSymbol.SetActive(false);
    //}

    //IEnumerator FlashNoFundsText()
    //{
    //    insufficientFunds.enabled = true;
    //    yield return new WaitForSeconds(1.0f);
    //    insufficientFunds.enabled = false;
    //}

    //public bool CheckIfPurchasable(int quantityOfPurchase)
    //{
    //    bool affordable = false; 
    //    for (int i = 0; i < foodCostIndex.Count; i++)
    //    {
    //        if (foodCostAmount[i] * quantityOfPurchase > PlayerResources.instance.GetResourceCount(foodCostIndex[i]))
    //        {

    //        }
    //    }
    //    return affordable;
    //}

}
