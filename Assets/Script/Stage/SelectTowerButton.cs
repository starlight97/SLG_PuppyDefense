using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTowerButton : MonoBehaviour
{
    // Ÿ�� ��ư�� Ŭ�� �Ǿ����� �˻��ϴ� ����
    public bool IsSelectBtn { set; get; }
    public int TowerIndex { set; get; }

    private void Awake()
    {
        IsSelectBtn = false;
        TowerIndex = 0;
    }
}
