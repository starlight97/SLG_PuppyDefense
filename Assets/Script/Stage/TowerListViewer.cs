using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


public class TowerListViewer : MonoBehaviour
{
    [SerializeField]
    private Transform towerListView;
    [SerializeField]
    private DataManager dataManager;
    [SerializeField]
    private Button btn_TowerSelect;

    private int towerSelectCount = 0;
    private List<Button> btn_TowerSelectList;
    private List<int> selectTowerIndexList;


    void Start()
    {
        //List<int> userTowerList = dataManager.GetUserTowerList();
        dataManager.Setup();
        List<int> userTowerList = dataManager.userTowerData.towerIndexList;
        SelectTower.instance.SetSelectTowerList(userTowerList);

        btn_TowerSelectList = new List<Button>();
        selectTowerIndexList = new List<int>();

        for (int i=0; i< userTowerList.Count; i++)
        {
            Button btnTower = (Button)Instantiate(btn_TowerSelect);
            btn_TowerSelectList.Add(btnTower);
            Transform transform = btnTower.transform;
            SelectTowerButton selectTowerButton = transform.GetComponent<SelectTowerButton>();
            selectTowerButton.TowerIndex = userTowerList[i];

            transform.GetChild(0).GetComponent<Image>().sprite = dataManager.GetUserTowerSprite(userTowerList[i]);            
            //btnTower.GetComponentInChildren<Image>().sprite = dataManager.GetUserTowerSprite(userTowerList[i]);
            //btnTower.Getchild(int).getcomponent<image>();
            btnTower.GetComponentInChildren<TextMeshProUGUI>().text = "btn : " + i.ToString();
            btnTower.onClick.AddListener(delegate
            {
                TowerSelect(btnTower);
            }) ;
            btnTower.transform.SetParent(towerListView);
        }
    }



    // 타워 선택 버튼을 누르면 투명도가 40%로 바뀌며 선택한 버튼 확인 가능
    private void TowerSelect(Button btnTowerSelect)
    {
        SelectTowerButton selectTowerBtn = btnTowerSelect.GetComponent<SelectTowerButton>();

        Color color = btnTowerSelect.GetComponentInChildren<Image>().color;
        if (selectTowerBtn.IsSelectBtn == false && GetTowerSelectCount() < 3)
        {
            selectTowerBtn.IsSelectBtn = true;
            color.a = 0.4f;
            selectTowerIndexList.Add(selectTowerBtn.TowerIndex);
        }
        else
        {
            selectTowerBtn.IsSelectBtn = false;
            color.a = 1f;
            selectTowerIndexList.Remove(selectTowerBtn.TowerIndex);
        }

        btnTowerSelect.GetComponentInChildren<Image>().color = color;
        SelectTower.instance.SetSelectTowerList(selectTowerIndexList);
    }

    private int GetTowerSelectCount()
    {        
        towerSelectCount = 0;
        
        for (int i = 0; i < btn_TowerSelectList.Count; i++)
        {
            SelectTowerButton selectTowerBtn = btn_TowerSelectList[i].transform.GetComponent<SelectTowerButton>();
            if (selectTowerBtn.IsSelectBtn == true)
            {                              
                towerSelectCount++;
            }
        }

        return towerSelectCount;
    }
    /*
    private IEnumerator HitAlphaAnimation()
    {
        // 현재 적의 색상을 color 변수에 저장
        Color color = spriteRenderer.color;

        // 적의 투명도를 40%로 설정
        color.a = 0.4f;
        spriteRenderer.color = color;

        // 0.05초 동안 대기
        yield return new WaitForSeconds(0.05f);

        // 적의 투명도를 100%로 설정
        color.a = 1.0f;
        spriteRenderer.color = color;
    }
    */

}
