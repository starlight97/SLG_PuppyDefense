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
            Debug.Log("Ÿ�� 3�� �����ؾ���");
        }

    }

    private bool CheckReady()
    {
        //SelectTower.instance.GetSelectTowerList()
        return true;
    }
}
