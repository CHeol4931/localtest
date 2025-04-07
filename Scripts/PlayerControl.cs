using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerControl : MonoBehaviour
{
    // 플레이어 이동및 아이템
    public float moveSpeed = 5.0f;
    public float originalSpeed;
    public float speedTimer = 5.0f;
    public float speedTiming = 0f;
    public bool isSpeedUp = false;
    public float dbSpeedTimer = 5.0f;
    public float dbSpeedTiming = 0;
    public bool isDbSpeedUp = false;
    public float recogTimer = 5.0f;
    public float recogTiming = 0;
    public bool isRecog = false;
    public GameObject ChestArrow;
    public GameObject tempChest;
    public GameObject knife;
    // 카메라 이동 및 회전
    public float mouseSpeed;
    float yRotation;
    float xRotation;
    Camera cam;
    public float playerHeight;
    bool grounded;
    public float jumpForce;
    // 산소 및 체력
    public int O2 = 60;
    public int HP = 10;
    public float O2Timer = 10;
    public float O2Timing = 0;
    public bool isO2 = true;
    public Animator animator;
    public Rigidbody rb;

    public GameManager gameManager;
    void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameData.Instance.isMouse)
        {
            Move();
            Rotate();
            Attack();
            SetO2();
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f);

            if (grounded && Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            if (Input.GetKeyDown(KeyCode.F1))
            {
                HP = GameData.MaxHp;
                O2 = GameData.MaxO2[GameData.Instance.bagO2];
            }
            if (Input.GetKeyDown(KeyCode.F3))
            {
                SetGameInit();
                SceneManager.LoadScene("Game");
            }
            if (Input.GetKeyDown(KeyCode.F4))
            {
                int num = SceneManager.sceneCount - 2;
                if (num <= GameData.maxStageNum)
                {
                    SceneManager.LoadScene(SceneManager.sceneCount + 1);
                }
                else
                {
                    SceneManager.LoadScene(2);
                }
            }
            if (Input.GetKeyDown(KeyCode.F5))
            {
                if (Time.timeScale < 1)
                {
                    Time.timeScale = 1;
                }
                else
                {
                    Time.timeScale = 0;
                }

            }
            GameData.Instance.gameTime += Time.deltaTime;
            gameManager.totalTime.text = GameData.Instance.gameTime.ToString("#");
        }


    }
    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal"); // 수평 이동 입력 값
        float v = Input.GetAxisRaw("Vertical");   // 수직 이동 입력 값

        // 입력에 따라 이동 방향 벡터 계산
        Vector3 moveVec = transform.forward * v + transform.right * h;

        // 이동 벡터를 정규화하여 이동 속도와 시간 간격을 곱한 후 현재 위치에 더함
        transform.position += moveVec.normalized * moveSpeed * Time.deltaTime;
    }
    void Rotate()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSpeed * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSpeed * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("isAttack");
        }
    }
    public void SetGameInit()
    {
        GameData.Instance.isGame = false;
        GameData.Instance.gameTime = 0;
        GameData.Instance.bagData.Clear();
        GameData.Instance.bagO2 = 0;
        GameData.Instance.bagUpgradeNum = 0;
        GameData.Instance.cost = 0;

        GameData.Instance.stageNum = 0;
        GameData.Instance.bagUpgradeNum = 0;
        GameData.Instance.bagData.Clear();
        GameData.Instance.chestData.Clear();

        for (int i = 0; i < GameData.Instance.isBuyItem.Length; i++)
        {
            GameData.Instance.isBuyItem[i] = true;
        }
        GameData.Instance.stageNum = 0;

    }
    public void UseItem(int itemNum)
    {
        Debug.Log("ItemNum" + itemNum + " 사용");
        // { "FIND", "HP", "O2", "NOTRECOG", "DOUBLEFAST", "FAST", };
        switch (itemNum)
        {
            /*case 0:
                {
                    Vector3 pos = FindChecst().position;
                    tempChest = Instantiate(ChestArrow, pos, ChestArrow.transform.rotation);
                }
                break;*/
            case 1:
                {
                    HP = GameData.MaxHp;
                    gameManager.HPBar.value = (float)HP / (float)GameData.MaxHp;
                }
                break;
            case 2:
                {
                    O2 = GameData.MaxO2[GameData.Instance.bagO2];
                    gameManager.O2Bar.value = (float)O2 / (float)GameData.MaxO2[GameData.Instance.bagO2];
                }
                break;
            case 3:
                {
                    isRecog = true;
                    recogTiming = 0;
                }
                break;
            case 4:
                {
                    isDbSpeedUp = true;
                    moveSpeed = originalSpeed * 1.4f;
                    speedTiming = 0;
                }
                break;
            case 5:
                {
                    isSpeedUp = true;
                    moveSpeed = originalSpeed * 1.2f;
                    speedTiming = 0;
                }
                break;



        }
    }
    public void SetDamage(int damage)
    {
        if (!isRecog)
        {
            HP -= damage;
            Debug.Log("체력 감소");
            if (HP <= 0)
            {
                gameManager.HPBar.value = 0;
                gameManager.SetGameClear(false);

            }
            else
            {
                gameManager.HPBar.value = (float)HP / (float)GameData.MaxHp;
            }
        }
    }
    public void SetO2()
    {
        if (isO2)
        {
            if (O2Timing > O2Timer)
            {
                O2Timing = 0;
                O2 -= 1;
                if (O2 < 0)
                {
                    gameManager.O2Bar.value = 0;
                    gameManager.SetGameClear(false);
                }
                else
                {
                    gameManager.O2Bar.value = (float)O2 / (float)GameData.MaxO2[GameData.Instance.bagO2];
                }
            }
            else
            {
                O2Timing += Time.deltaTime;
            }

        }
    }
}
