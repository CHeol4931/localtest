using UnityEngine;

public class BarrelItemDrop : MonoBehaviour
{
    public PlayerControl player; // �÷��̾� ����

    void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerControl>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Į�� ��Ҵ��� Ȯ�� (�±׷� Ȯ���ϰų� �̸����� ��)
        if (other.gameObject.name.Contains("knife"))
        {
            Debug.Log(" Barrel�� Į�� ����!");

            // ���� ������ ���� (1~5)
            int randItem = Random.Range(1, 6); // HP~FAST (1~5)

            // ������ ���
            player.UseItem(randItem);

            // Barrel ����
            Destroy(gameObject);
        }
    }
}