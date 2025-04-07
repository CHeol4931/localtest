using UnityEngine;

public class BarrelItemDrop : MonoBehaviour
{
    public PlayerControl player; // 플레이어 참조

    void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerControl>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 칼에 닿았는지 확인 (태그로 확인하거나 이름으로 비교)
        if (other.gameObject.name.Contains("knife"))
        {
            Debug.Log(" Barrel이 칼에 맞음!");

            // 랜덤 아이템 선택 (1~5)
            int randItem = Random.Range(1, 6); // HP~FAST (1~5)

            // 아이템 사용
            player.UseItem(randItem);

            // Barrel 제거
            Destroy(gameObject);
        }
    }
}