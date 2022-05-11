using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AddTowerBuildBtn : MonoBehaviour
{
    [SerializeField]
    private Button[] btnList;
    [SerializeField]
    private TowerSpawner towerSpawner;
    [SerializeField]
    private TowerData towerData;

    [SerializeField]
    private Image[] towerBuildBtnImageList;

    private void Start()
    {
        Setup();
    }

    
    private void Setup()
    {
        List<int> towerIndexList = new List<int>();
        towerIndexList = SelectTower.instance.GetSelectTowerList();

        // Ÿ�� ��ư ����             
        for (int i = 0; i < towerIndexList.Count; ++i)
        {
            int towerIndex = i;
            btnList[i].gameObject.SetActive(true);
            btnList[i].onClick.AddListener(delegate { AddTowerBtn(towerIndexList[towerIndex]); });
            towerBuildBtnImageList[i].sprite = towerData.GetTowerSprite(towerIndexList[towerIndex]);

        }       
        
        
    }

    private void AddTowerBtn(int towerIndex)
    {
        towerSpawner.ReadyToSpawnTower(towerIndex);
    }

}
