using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagData
{
    public string name;
    public int num;
    public int weight;
    public int bagNum;
}


public class GameData : MonoBehaviour
{
    public int stageNum = 0;

    public int bagUpgradeNum = 0;

    public bool isGame = true;

    public int stageSize = 0;

    public List<Item> bagData = new List<Item>();

    public List<bool> chestData = new List<bool>();

    public float gameTime = 0f;

    public int cost = 0;

    public bool[] isBuyItem = { true, true, true, true, true };

    public bool isMouse = true;

    public int bagO2 = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static GameData Instance;

    public static int[] stageDeO2 = { 1, 1, 2, 2, 4 };

    public static int[] itemCost = { 10, 100, 1000, 100, 1000 };

    public static string[] itemText =
    {
        "���п� �����\n$10",
        "�߾п� �����\n$100",
        "��п� �����\n$1000",
        "���� ����\n$100",
        "�ʴ��� ����\n$1000"
    };
    public static string buyItemText = "���� �Ϸ�";
    public static string buyFileText = "���� �Ұ���";
    public static string[] sName = { "FIND", "HP", "O2", "NOTRECOG", "DOUBLEFAST", "FAST", };
    public static int[] iWeight = { 10, 10, 10, 10, 10, 10 };
    public static int[] iBagSize = { 4, 6, 8 };
    public static int MaxHp = 10;
    public static int[] MaxO2 = { 60, 70, 80, 100 };
    public static int[] chestCost = { 100, 10, 100, 500, 1000 };
    public static int maxStageNum = 0;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

}
