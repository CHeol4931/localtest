using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RobbyControl : MonoBehaviour
{
    public Text[] itemText;
    public Text Cost;
    public DelayText delayText;
    bool isNotCost = false;


    void Start()
    {
        if (GameObject.Find("GameData") == null)
        {
            delayText.SetText("GameData가 생성되지 않았습니다. 메인에서부터 실행하세요.");
            return;
        }
        int count = Mathf.Min(GameData.Instance.isBuyItem.Length, itemText.Length);
        for (int i = 0; i < count; i++)
        {
            if (GameData.Instance.isBuyItem[i])
            {
                itemText[i].text = GameData.itemText[i];
            }
            else
            {
                itemText[i].text = GameData.buyItemText;
            }
        }
        Cost.text = GameData.Instance.cost.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            isNotCost = !isNotCost;
        }
    }
    public void BuyItem(int num)
    {
        if (GameData.itemCost[num] <= GameData.Instance.cost)
        {
            GameData.Instance.isBuyItem[num] = false;
            itemText[num].text = GameData.buyItemText;
            GameData.Instance.cost -= GameData.itemCost[num];
            Cost.text = GameData.Instance.cost.ToString();
            if(num < 3)
            {
                if(GameData.Instance.bagO2 <= num + 1)
                {
                    GameData.Instance.bagO2 = num + 1;
                }
            }
            else
            {
                if (GameData.Instance.bagUpgradeNum <= num - 2)
                {
                    GameData.Instance.bagUpgradeNum = num - 2;
                }
            }
        }
        else if (isNotCost)
        {
            GameData.Instance.isBuyItem[num] = false;
            itemText[num].text = GameData.buyItemText;
            GameData.Instance.cost -= GameData.itemCost[num];
            Cost.text = GameData.Instance.cost.ToString();
            if(num < 3)
            {
                if(GameData.Instance.bagO2 <= num + 1)
                {
                    GameData.Instance.bagO2 = num + 1
;                }
            }
            else
            {
                if(GameData.Instance.bagUpgradeNum <= num - 2)
                {
                    GameData.Instance.bagUpgradeNum = num - 2;
                }
            }
        }
        else
        {
            delayText.SetText("구매 불가능");
        }
    }
    public void InGame()
    {
        SceneManager.LoadScene("Game");
    }
}
