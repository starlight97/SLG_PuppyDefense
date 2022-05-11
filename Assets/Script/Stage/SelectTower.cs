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

//  StageReady Sceen 에서 선택한 타워에 대한 인덱스 정보를 가지고 있는 싱글톤 오브젝트
//  2022-04-17 
//  작성자 : Starlight