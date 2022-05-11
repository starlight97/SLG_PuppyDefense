using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField]
    private TowerTemplateList towerTemplateList;
    [SerializeField]
    private EnemySpawner enemySpawner; // ���� �ʿ� �����ϴ� �� ����Ʈ ������ ��� ����
    [SerializeField]
    private PlayerStats playerStats;  // Ÿ�� �Ǽ��� ��� ���Ҹ� ����
    //[SerializeField]
    //private SystemTextViewer systemTextViewer;  // �� ����, �Ǽ� �Ұ��� ���� �ý��� �޼��� ���
    private bool isOnTowerButton = false;   // Ÿ�� �Ǽ� ��ư�� �������� üũ
    private bool isOnTowerMoveButton = false;   // Ÿ�� �̵� ��ư�� �������� üũ
    private GameObject followTowerClone = null; // �ӽ� Ÿ�� ��� �Ϸ� �� ������ ���� �����ϴ� ����
    public Transform currentSelectTower;    // ���� �������� Ÿ��
    private int towerIndex;  // Ÿ�� ��ȣ

    public void ReadyToSpawnTower(int index)
    {
        towerIndex = index;

        // ��ư �ߺ�Ŭ�� ����
        if (isOnTowerButton == true)
        {
            return;
        }

        // Ÿ�� �Ǽ� ���� ���� Ȯ��
        // Ÿ���� �Ǽ��� ��ŭ ���� ������ Ÿ�� �Ǽ� X 
        if (towerTemplateList.towerTemplate[towerIndex].weapon[0].cost > playerStats.CurrentGold)
        {
            // ��尡 �����ؼ� Ÿ�� �Ǽ��� �Ұ��� �ϴٰ� ���
            //systemTextViewer.PrintText(SystemType.Money);
            return;
        }

        // Ÿ�� �Ǽ� ��ư�� �����ٰ� ����
        isOnTowerButton = true;
        // ���콺�� ����ٴϴ� �ӽ� Ÿ�� ����
        followTowerClone = Instantiate(towerTemplateList.towerTemplate[towerIndex].followTowerPrefab);
        // Ÿ�� �Ǽ��� ����� �� �ִ� �ڷ�ƾ �Լ� ����
        StartCoroutine("OnTowerCancelSystem");
    }

    public void ReadyToMoveTower()
    {
        // ��ư �ߺ�Ŭ�� ����
        if (isOnTowerButton == true)
        {
            return;
        }

        // Ÿ�� �Ǽ� ���� ���� Ȯ��

        // Ÿ�� �Ǽ� ��ư�� �����ٰ� ����
        isOnTowerMoveButton = true;
        // ���콺�� ����ٴϴ� �ӽ� Ÿ�� ����
        followTowerClone = Instantiate(towerTemplateList.towerTemplate[0].followTowerPrefab);
        // Ÿ�� �̵��� ����� �� �ִ� �ڷ�ƾ �Լ� ����
        StartCoroutine("OnTowerMoveCancelSystem");
    }

    public void SpawnTower(Transform tileTransform)
    {
        // Ÿ�� �Ǽ� ��ư�� ������ ���� Ÿ�� �Ǽ� ����
        if (isOnTowerButton == false)
        {
            return;
        }
        //Ÿ�� �Ǽ� ���� ���� Ȯ��
        /*
        // 1. Ÿ���� �Ǽ��� ��ŭ ���� ������ Ÿ�� �Ǽ� x
        if(towerTemplate.weapon[0].cost > playerGold.CurrentGold)
        {
            // ��尡 �����ؼ� Ÿ�� �Ǽ��� �Ұ����ϴٰ� ���
            systemTextViewer.PrintText(SystemType.Money);
            return;
        }
        */
        Tile tile = tileTransform.GetComponent<Tile>();

        // 2. ���� Ÿ���� ��ġ�� �̹� Ÿ���� �Ǽ��Ǿ� ������ Ÿ�� �Ǽ� X
        if (tile.IsBuildTower == true)
        {
            //systemTextViewer.PrintText(SystemType.Build);
            return;
        }

        // �ٽ� Ÿ�� �Ǽ� ��ư�� ������ Ÿ���� �Ǽ��ϵ��� ���� ����
        isOnTowerButton = false;
        // Ÿ���� �Ǽ��Ǿ� ���� ���� ����
        tile.IsBuildTower = true;
        // Ÿ�� �Ǽ��� �ʿ��� ��常ŭ ����
        playerStats.CurrentGold -= towerTemplateList.towerTemplate[towerIndex].weapon[0].cost;
        // ������ Ÿ���� ��ġ�� Ÿ�� �Ǽ�(Ÿ�Ϻ��� z�� -1�� ��ġ�� ��ġ)
        Vector3 position = tileTransform.position + Vector3.back;
        GameObject clone = Instantiate(towerTemplateList.towerTemplate[towerIndex].towerPrefab, position, Quaternion.identity);
        // Ÿ�� ���⿡ enemySpawner ���� ����
        clone.GetComponent<TowerWeapon>().Setup(this, enemySpawner, playerStats, tile);

        // ���� ��ġ�Ǵ� Ÿ���� ���� Ÿ �� �ֺ��� ��ġ�� ���
        // ���� ȿ���� ���� �� �ֵ��� ��� ���� Ÿ���� ���� ȿ�� ����
        OnBuffAllBuffTowers();

        // Ÿ���� ��ġ�߱� ������ ���콺�� ����ٴϴ� �ӽ� Ÿ�� ����
        Destroy(followTowerClone);
        // Ÿ�� �Ǽ��� ����� �� �ִ� �ڷ�ƾ �Լ� ����
        StopCoroutine("OnTowerCancelSystem");
    }
    public void MoveTower(Transform tileTransform)
    {
        // Ÿ�� �̵� ��ư�� ������ ���� Ÿ�� �̵� ����
        if (isOnTowerMoveButton == false)
        {
            return;
        }
        
        Tile tile = tileTransform.GetComponent<Tile>();
        // 2. ���� Ÿ���� ��ġ�� �̹� Ÿ���� �Ǽ��Ǿ� ������ Ÿ�� �Ǽ� X
        if (tile.IsBuildTower == true)
        {
            //systemTextViewer.PrintText(SystemType.Build);
            return;
        }

        // �ٽ� Ÿ�� �Ǽ� ��ư�� ������ Ÿ���� �Ǽ��ϵ��� ���� ����
        isOnTowerMoveButton = false;
        // Ÿ���� �Ǽ��Ǿ� ���� ���� ����
        tile.IsBuildTower = true;
        // ������ Ÿ���� ��ġ�� Ÿ�� �Ǽ�(Ÿ�Ϻ��� z�� -1�� ��ġ�� ��ġ)
        Vector3 position = tileTransform.position + Vector3.back;

        currentSelectTower.GetComponent<TowerWeapon>().TowerMove(position, tile);
        // Ÿ���� ��ġ�߱� ������ ���콺�� ����ٴϴ� �ӽ� Ÿ�� ����
        Destroy(followTowerClone);
        // Ÿ�� �Ǽ��� ����� �� �ִ� �ڷ�ƾ �Լ� ����
        StopCoroutine("OnTowerMoveCancelSystem");
    }

    private IEnumerator OnTowerCancelSystem()
    {
        while (true)
        {
            // ESCŰ �Ǵ� ���콺 ������ ��ư�� ������ �� Ÿ�� �Ǽ� ���
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                isOnTowerButton = false;
                // ���콺�� ����ٴϴ� �ӽ� Ÿ�� ����
                Destroy(followTowerClone);
                break;
            }
            yield return null;
        }
    }

    private IEnumerator OnTowerMoveCancelSystem()
    {
        while (true)
        {
            // ESCŰ �Ǵ� ���콺 ������ ��ư�� ������ �� Ÿ�� �̵� ���
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                isOnTowerMoveButton = false;
                // ���콺�� ����ٴϴ� �ӽ� Ÿ�� ����
                Destroy(followTowerClone);
                break;
            }
            yield return null;
        }
    }

    public void OnBuffAllBuffTowers()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");

        for (int i = 0; i < towers.Length; ++i)
        {
            TowerWeapon weapon = towers[i].GetComponent<TowerWeapon>();

            if (weapon.WeaponType == WeaponType.Buff)
            {
                weapon.OnBuffArroundTower();
            }
        }
    }


}
