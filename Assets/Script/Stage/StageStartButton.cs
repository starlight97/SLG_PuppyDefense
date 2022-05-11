using UnityEngine;
using UnityEngine.SceneManagement;

public class StageStartButton : MonoBehaviour
{
    public void StageStart()
    {
        if (SelectTower.instance.GetReady() == true)
        {
            SceneManager.LoadScene("Stage");
        }
        else
        {
            Debug.Log("타워 3개 선택해야함");
        }

    }

    private bool CheckReady()
    {
        //SelectTower.instance.GetSelectTowerList()
        return true;
    }
}
