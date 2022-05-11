using UnityEngine;

[CreateAssetMenu]
public class TowerTemplate : ScriptableObject
{
    public GameObject towerPrefab;  // Ÿ�� ������ ���� ������
    public GameObject followTowerPrefab;    // �ӽ� Ÿ�� ������
    public WeaponProperty weaponProperty;   // Ÿ�� �Ӽ�
    public string towerName;                // Ÿ�� �̸�
    public Weapon[] weapon; // ������ Ÿ��(����) ����

    [System.Serializable]
    public struct Weapon
    {
        public Sprite sprite;   // �������� Ÿ�� �̹���(UI)
        public float damage;    // ���ݷ�
        public float slow;  // ���� �ۼ�Ʈ (0.2 = 20%)
        public float buff;  // ���ݷ� ������ (0.2 = 20%)
        public float rate;  // ���� �ӵ�
        public float range; // ���� ����
        public int cost;    // �ʿ� ��� (0���� : �Ǽ�, 1~���� : ���׷��̵�)
        public int sell;    // Ÿ�� �Ǹ� �� ȹ�� ���

    }

}