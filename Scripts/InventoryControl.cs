using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class InventoryControl : MonoBehaviour
{
    public GameObject baseInv;
    public List<Transform> Inventory = new List<Transform>();
    public PlayerControl playerControl;
    public GameManager gameManager;


    void Start()
    {

        playerControl = this.GetComponent<PlayerControl>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        baseInv = GameObject.Find("Inven");

        for(int i= 0; i <baseInv.transform.childCount; i++)
        {
            Inventory.Add(baseInv.transform.GetChild(i));
        }
        for(int i = 0; i< Inventory.Count; i++)
        {
            if(i < GameData.iBagSize[GameData.Instance.bagUpgradeNum])
            {

                Inventory[i].GetChild(0).gameObject.SetActive(false);
                Inventory[i].GetChild(1).GetComponent<Text>().text = "";
                Inventory[i].GetChild(2).GetComponent<Text>().text = "";
            }
            else
            {
                Inventory[i].GetChild(0).gameObject.SetActive(true);
                Inventory[i].GetChild(1).GetComponent<Text>().text = "";
                Inventory[i].GetChild(2).GetComponent<Text>().text = "";
            }
        }
    }
    public void UpgradeInven()
    {
        for(int i = 0;i < Inventory.Count; i++)
        {
            if(i < GameData.iBagSize[GameData.Instance.bagUpgradeNum])
            {
                Inventory[i].GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                Inventory[i].GetChild(0).gameObject.SetActive(true);
            }
        }
    }
    public void UseItem(int itemNum)
    {
        playerControl.UseItem(itemNum);
    }
    public bool GetInventoryEmpty(Item item)
    {
        int bagSize = GameData.iBagSize[GameData.Instance.bagUpgradeNum];
        if(GameData.Instance.bagData.Count < bagSize)
        {
            if(GameData.Instance.bagData.Count > 0)
            {
                for(int i =0;i < GameData.Instance.bagData.Count; i++)
                {
                    if (GameData.Instance.bagData[i].itemNum == item.itemNum)
                    {
                        GameData.Instance.bagData[i].num++;
                        GameData.Instance.bagData[i].weight += GameData.iWeight[item.itemNum];
                        Inventory[i].GetChild(1).GetComponent<Text>().text = item.itemName;
                        Inventory[i].GetChild(2).GetComponent<Text>().text = GameData.Instance.bagData[i].num.ToString();
                    }
                }
                GameData.Instance.bagData.Add(item);
                Inventory[GameData.Instance.bagData.Count - 1].GetChild(1).GetComponent<Text>().text = item.itemName;
                Inventory[GameData.Instance.bagData.Count - 1].GetChild(2).GetComponent<Text>().text = item.num.ToString();
            }
            else
            {
                GameData.Instance.bagData.Add(item);
                Inventory[0].GetChild(1).GetComponent<Text>().text = item.itemName;
                Inventory[0].GetChild(2).GetComponent<Text>().text = item.num.ToString();
            }
            return true;
        }
        else if(GameData.Instance.bagData.Count == bagSize)
        {
            for(int i = 0; i < GameData.Instance.bagData.Count; i++)
            {
                if (GameData.Instance.bagData[i].itemNum == item.itemNum)
                {
                    GameData.Instance.bagData[i].num++;
                    GameData.Instance.bagData[i].weight += GameData.iWeight[item.itemNum];
                    Inventory[i].GetChild(1).GetComponent<Text>().text = item.itemName;
                    Inventory[i].GetChild(2).GetComponent<Text>().text = GameData.Instance.bagData[i].num.ToString();
                    return true;
                }
            }
        }
        gameManager.delayText.SetText("아이템이 가득찼습니다.");
        return false;
    }
    void Update()
    {
        int InvenNum = -1;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            InvenNum = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            InvenNum = 1;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3)) { InvenNum = 2; }
        else if(Input.GetKeyDown(KeyCode.Alpha4)) { InvenNum = 3; }
        else if(Input.GetKeyDown(KeyCode.Alpha5)) { InvenNum = 4; }
        else if(Input.GetKeyDown(KeyCode.Alpha6)) { InvenNum = 5; }
        else if(Input.GetKeyDown(KeyCode.Alpha7)) { InvenNum = 6; }
        else if(Input.GetKeyDown(KeyCode.Alpha8)) { InvenNum = 7; }

        if(InvenNum > -1)
        {
            if(GameData.Instance.bagData.Count > InvenNum)
            {
                GameData.Instance.bagData[InvenNum].num--;
                UseItem(GameData.Instance.bagData[InvenNum].itemNum);
                if (GameData.Instance.bagData[InvenNum].num <= 0)
                {
                    GameData.Instance.bagData.Remove(GameData.Instance.bagData[InvenNum]);
                }
                SetInventory();
            }
            else
            {
                gameManager.delayText.SetText("아이템이 없습니다.");
            }
        }
    }
    public void SetInventory()
    {
        for(int i = 0; i< GameData.iBagSize[GameData.Instance.bagUpgradeNum]; i++)
        {
            if (i < GameData.Instance.bagData.Count)
            {
                Inventory[i].GetChild(1).GetComponent<Text>().text = GameData.Instance.bagData[i].itemName;
                Inventory[i].GetChild(2).GetComponent<Text>().text = GameData.Instance.bagData[i].num.ToString();
            }
            else
            {
                Inventory[i].GetChild(1).GetComponent<Text>().text = "";
                Inventory[i].GetChild(2).GetComponent<Text>().text = "";
            }
        }
    }
}
