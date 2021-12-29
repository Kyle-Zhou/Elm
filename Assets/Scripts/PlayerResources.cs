using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResources : MonoBehaviour
{
    public static PlayerResources instance;

    private int[] quantities = new int[18];

    //public Text[] texts = new Text[11];
    public List<GameObject> items;

    public GameObject matsBackpack;


    //public GameObject foodBackpack;
    //public GameObject renewableBackpack;


    public GameObject[] slots;


    private void Start()
    {
        instance = this;

        //for (int i = 0; i < matsBackpack.GetComponent<BackpackManager>().slots.Length-1; i++)
        //{
        //    slots[i] = matsBackpack.GetComponent<BackpackManager>().slots[i];
        //}
        slots = matsBackpack.GetComponent<BackpackManager>().slots;


        //test sword upgrade
        //PlayerResources.instance.AddResource(30, PlayerResources.instance.GetPlatIndex());
        //PlayerResources.instance.AddResource(30, PlayerResources.instance.GetWoodIndex());
        //PlayerResources.instance.AddResource(30, PlayerResources.instance.GetSlimeIndex());
        //PlayerResources.instance.AddResourceToInventory(30, PlayerResources.instance.GetPlatIndex());
        //PlayerResources.instance.AddResourceToInventory(30, PlayerResources.instance.GetWoodIndex());
        //PlayerResources.instance.AddResourceToInventory(30, PlayerResources.instance.GetSlimeIndex());



    }

    public void AddResource(int value, int index){
        this.quantities[index] += value;
    }

    public void AddResourceToInventory(int value, int index)
    {

        GameObject resourceItem = Instantiate(items[index]);
      

        for (int i = 0; i < slots.Length - 1; i++)
        {
            if (slots[i].transform.childCount > 0)
            {
                if (slots[i].transform.GetChild(0).name == resourceItem.name){
                    Debug.Log(1);
                    slots[i].transform.GetChild(0).GetComponent<Item>().AddResourceAmount(value);
                    Destroy(resourceItem);
                    return;
                } 
            }
        }

        RectTransform rt = resourceItem.GetComponent<RectTransform>();

        resourceItem.GetComponent<Item>().AddResourceAmount(value);

        if (resourceItem.GetComponent<Item>().GetResourceAmount() > 0)
        {
            matsBackpack.GetComponent<BackpackManager>().SetSlot(resourceItem);
        }
        else
        {
            matsBackpack.GetComponent<BackpackManager>().RemoveSlot(resourceItem);
        }


        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(1, 1);
        rt.pivot = new Vector2(0.5f, 0.5f);


    }

    //public void AddResourceToHotbar(int value, int index)
    //{

    //    GameObject resourceItem = Instantiate(items[index]);

    //    for (int i = 0; i < hotbarSlots.Length - 1; i++)
    //    {
    //        if (hotbarSlots[i].transform.childCount > 0)
    //        {
    //            if (hotbarSlots[i].transform.GetChild(0).name == resourceItem.name)
    //            {
    //                Debug.Log(1);
    //                hotbarSlots[i].transform.GetChild(0).GetComponent<Item>().AddResourceAmount(value);
    //                Destroy(resourceItem);
    //                return;
    //            }
    //        }
    //    }

    //    resourceItem.GetComponent<Item>().AddResourceAmount(value);

    //    if (resourceItem.GetComponent<Item>().GetResourceAmount() > 0)
    //    {
    //        hotbar.GetComponent<BackpackManager>().SetSlot(resourceItem);
    //    }
    //    else
    //    {
    //        hotbar.GetComponent<BackpackManager>().RemoveSlot(resourceItem);
    //    }

    //}

    public int GetWoodCount()
    {
        return quantities[0];
    }
    public int GetRockCount()
    {
        return quantities[1];
    }
    public int GetPlatCount()
    {
        return quantities[2];
    }
    public int GetGravelCount()
    {
        return quantities[3];
    }
    public int GetGoldCount()
    {
        return quantities[4];
    }
    public int GetElixirCount()
    {
        return quantities[5];
    }
    public int GetCrystalCount()
    {
        return quantities[6];
    }
    public int GetCoalCount()
    {
        return quantities[7];
    }
    public int GetBrickCount()
    {
        return quantities[8];
    }
    public int GetBlueBerrySaplingCount()
    {
        return quantities[9];
    }
    public int GetTreeSapingCount()
    {
        return quantities[10];
    }
    public int GetBlueberryCount()
    {
        return quantities[11];
    }
    public int GetCarrotCount()
    {
        return quantities[12];
    }
    public int GetBlueberryPieCount()
    {
        return quantities[13];
    }
    public int GetWheatCount()
    {
        return quantities[14];
    }
    public int GetSlimeCount()
    {
        return quantities[15];
    }
    public int GetHoneyCount()
    {
        return quantities[16];
    }
    public int GetBreadCount()
    {
        return quantities[17];
    }

    public int GetResourceCount(int index)
    {
        return quantities[index];
    }


    public int GetWoodIndex()
    {
        return 0;
    }
    public int GetRockIndex()
    {
        return 1;
    }
    public int GetPlatIndex()
    {
        return 2;
    }
    public int GetGravelIndex()
    {
        return 3;
    }
    public int GetGoldIndex()
    {
        return 4;
    }
    public int GetElixirIndex()
    {
        return 5;
    }
    public int GetCrystalIndex()
    {
        return 6;
    }
    public int GetCoalIndex()
    {
        return 7;
    }
    public int GetBrickIndex()
    {
        return 8;
    }
    public int GetBlueberrySaplingIndex()
    {
        return 9;
    }
    public int GetTreeSaplingIndex()
    {
        return 10;
    }
    public int GetBlueberryIndex()
    {
        return 11;
    }
    public int GetCarrotIndex()
    {
        return 12;
    }
    public int GetBlueberryPieIndex()
    {
        return 13;
    }
    public int GetWheatIndex()
    {
        return 14;
    }
    public int GetSlimeIndex()
    {
        return 15;
    }
    public int GetHoneyIndex()
    {
        return 16;
    }
    public int GetBreadIndex()
    {
        return 17;
    }
    //public int GetRockCount()
    //{
    //    return quantities[1];
    //}

    //public void AddRocks(int value)
    //{
    //    this.quantities[1] += value;
    //    if (this.quantities[1] > 0)
    //    {
    //        texts[1].text = "x" + quantities[1].ToString();
    //        items[1].SetActive(true);
    //        //BackpackManager.instance.SetSlot(items[1]);
    //        matsBackpack.GetComponent<BackpackManager>().SetSlot(items[1]);
    //    }
    //    else
    //    {
    //        items[1].SetActive(false);
    //        //BackpackManager.instance.RemoveSlot(items[1]);
    //        matsBackpack.GetComponent<BackpackManager>().RemoveSlot(items[1]);
    //    }
    //}


    //public int GetGoldCount()
    //{
    //    return quantities[2];
    //}

    //public void AddGold(int value) //Pending rename
    //{
    //    this.quantities[2] += value;
    //    if (this.quantities[2] > 0)
    //    {
    //        texts[2].text = "x" + quantities[2].ToString();
    //        items[2].SetActive(true);
    //        //BackpackManager.instance.SetSlot(items[2]);
    //        matsBackpack.GetComponent<BackpackManager>().SetSlot(items[2]);
    //    }
    //    else
    //    {
    //        items[2].SetActive(false);
    //        //BackpackManager.instance.RemoveSlot(items[2]);
    //        matsBackpack.GetComponent<BackpackManager>().RemoveSlot(items[2]);
    //    }
    //}


    //public int GetBlueberryCount()
    //{
    //    return quantities[3];
    //}

    //public void AddBlueberries(int value)
    //{
    //    this.quantities[3] += value;
    //    if (this.quantities[3] > 0)
    //    {
    //        texts[3].text = "x" + quantities[3].ToString();
    //        items[3].SetActive(true);
    //        //BackpackManager.instance.SetSlot(items[3]);
    //        foodBackpack.GetComponent<BackpackManager>().SetSlot(items[3]);
    //    }
    //    else
    //    {
    //        items[3].SetActive(false);
    //        //BackpackManager.instance.RemoveSlot(items[3]);
    //        foodBackpack.GetComponent<BackpackManager>().RemoveSlot(items[3]);
    //    }
    //}


    //public int GetTreeSaplingCount()
    //{
    //    return quantities[4];
    //}

    //public void AddTreeSaplings(int value)
    //{
    //    this.quantities[4] += value;
    //    if (this.quantities[4] > 0)
    //    {
    //        texts[4].text = "x" + quantities[4].ToString();
    //        items[4].SetActive(true);
    //        //BackpackManager.instance.SetSlot(items[4]);
    //        renewableBackpack.GetComponent<BackpackManager>().SetSlot(items[4]);
    //    }
    //    else
    //    {
    //        items[2].SetActive(false);
    //        //BackpackManager.instance.RemoveSlot(items[2]);
    //        renewableBackpack.GetComponent<BackpackManager>().RemoveSlot(items[4]);
    //    }
    //}


    //public int GetCoalCount()
    //{
    //    return quantities[5];
    //}

    //public void AddCoal(int value)
    //{
    //    this.quantities[5] += value;
    //    if (this.quantities[5] > 0)
    //    {
    //        texts[5].text = "x" + quantities[5].ToString();
    //        items[5].SetActive(true);
    //        //BackpackManager.instance.SetSlot(items[5]);
    //        matsBackpack.GetComponent<BackpackManager>().SetSlot(items[5]);
    //    }
    //    else
    //    {
    //        items[5].SetActive(false);
    //        //BackpackManager.instance.RemoveSlot(items[5]);
    //        matsBackpack.GetComponent<BackpackManager>().RemoveSlot(items[5]);
    //    }
    //}

    //public int GetBrickCount()
    //{
    //    return quantities[6];
    //}

    //public void AddBricks(int value)
    //{
    //    this.quantities[6] += value;
    //    if (this.quantities[6] > 0)
    //    {
    //        texts[6].text = "x" + quantities[6].ToString();
    //        items[6].SetActive(true);
    //        //BackpackManager.instance.SetSlot(items[6]);
    //        matsBackpack.GetComponent<BackpackManager>().SetSlot(items[6]);
    //    }
    //    else
    //    {
    //        items[6].SetActive(false);
    //        //BackpackManager.instance.RemoveSlot(items[6]);
    //        matsBackpack.GetComponent<BackpackManager>().RemoveSlot(items[6]);
    //    }
    //}

    //public int GetGravelCount()
    //{
    //    return quantities[7];
    //}

    //public void AddGravel(int value)
    //{
    //    this.quantities[7] += value;
    //    if (this.quantities[7] > 0)
    //    {
    //        texts[7].text = "x" + quantities[7].ToString();
    //        items[7].SetActive(true);
    //        //BackpackManager.instance.SetSlot(items[7]);
    //        matsBackpack.GetComponent<BackpackManager>().SetSlot(items[7]);
    //    }
    //    else
    //    {
    //        items[7].SetActive(false);
    //        //BackpackManager.instance.RemoveSlot(items[7]);
    //        matsBackpack.GetComponent<BackpackManager>().RemoveSlot(items[7]);
    //    }
    //}

    //public int GetPlatinumCount()
    //{
    //    return quantities[8];
    //}

    //public void AddPlatinum(int value)
    //{
    //    this.quantities[8] += value;
    //    if (this.quantities[8] > 0)
    //    {
    //        texts[8].text = "x" + quantities[8].ToString();
    //        items[8].SetActive(true);
    //        //BackpackManager.instance.SetSlot(items[8]);
    //        matsBackpack.GetComponent<BackpackManager>().SetSlot(items[8]);
    //    }
    //    else
    //    {
    //        items[8].SetActive(false);
    //        //BackpackManager.instance.RemoveSlot(items[8]);
    //        matsBackpack.GetComponent<BackpackManager>().RemoveSlot(items[8]);
    //    }
    //}

    //public int GetElixirCount()
    //{
    //    return quantities[9];
    //}

    //public void AddElixir(int value)
    //{
    //    this.quantities[9] += value;
    //    if (this.quantities[9] > 0)
    //    {
    //        texts[9].text = "x" + quantities[9].ToString();
    //        items[9].SetActive(true);
    //        //BackpackManager.instance.SetSlot(items[9]);
    //        matsBackpack.GetComponent<BackpackManager>().SetSlot(items[9]);
    //    }
    //    else
    //    {
    //        items[9].SetActive(false);
    //        //BackpackManager.instance.RemoveSlot(items[9]);
    //        matsBackpack.GetComponent<BackpackManager>().RemoveSlot(items[9]);
    //    }
    //}


    //public int GetCrystalCount()
    //{
    //    return quantities[10];
    //}

    //public void AddCrystals(int value)
    //{
    //    this.quantities[10] += value;

    //    if (this.quantities[10] > 0)
    //    {
    //        texts[10].text = "x" + quantities[10].ToString();
    //        genCrystalText.text = "x" + quantities[10].ToString();
    //        items[10].SetActive(true);
    //        //BackpackManager.instance.SetSlot(items[10]);
    //        matsBackpack.GetComponent<BackpackManager>().SetSlot(items[10]);
    //    }
    //    else
    //    {
    //        genCrystalText.text = "x0";
    //        items[10].SetActive(false);
    //        //BackpackManager.instance.RemoveSlot(items[10]);
    //        matsBackpack.GetComponent<BackpackManager>().RemoveSlot(items[10]);
    //    }


    //}

}
