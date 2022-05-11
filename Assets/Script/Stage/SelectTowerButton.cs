using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTowerButton : MonoBehaviour
{
    // 타워 버튼이 클릭 되었는지 검사하는 변수
    public bool IsSelectBtn { set; get; }
    public int TowerIndex { set; get; }

    private void Awake()
    {
        IsSelectBtn = false;
        TowerIndex = 0;
    }
}
