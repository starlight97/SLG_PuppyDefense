using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTower : MonoBehaviour
{    
    public static SelectTower instance;
    private bool isReady;

    private List<int> selectTowerListIndex;

    private void Awake()
    {
        isReady = false;
        selectTowerListIndex = new List<int>();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    public void SetSelectTowerList(List<int> selectTowerList)
    {
        selectTowerListIndex = selectTowerList;
    }

    public List<int> GetSelectTowerList()
    {
        if (selectTowerListIndex == null)
            return null;
        else
            return selectTowerListIndex;
    }

    public bool GetReady()
    {
        if (selectTowerListIndex.Count == 3)
            isReady = true;
        else
            isReady = false;

        return isReady;
    }

}

//  StageReady Sceen ���� ������ Ÿ���� ���� �ε��� ������ ������ �ִ� �̱��� ������Ʈ
//  2022-04-17 
//  �ۼ��� : Starlight