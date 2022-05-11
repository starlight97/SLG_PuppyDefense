using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Data;
using Mono.Data.SqliteClient;
using System.Runtime.Serialization.Formatters.Binary;




public class DataManager : MonoBehaviour
{
    [SerializeField]
    private TowerTemplateList towerTemplateList;

    public UserTowerData userTowerData;

    // 유저가 보유하고있는 타워 인덱스들 반환
    // ex(Tower01_Water : 0, Tower01_Fire : 1,Tower01_Wind : 2)
    public List<int> GetUserTowerList()
    {
        List<int> userTowerList = new List<int>();
        string result = "";
        //string _constr = "URI=file:" + Application.dataPath + "/20220413DefenseDB.db";
        //string _constr = "URI=file:" + Application.streamingAssetsPath + "/20220413DefenseDB.db";
        string _constr = "URI=file:" + Application.dataPath + "/StreamingAssets/20220413DefenseDB.db";
        Debug.Log(_constr);
        IDbConnection dbc;
        IDbCommand dbcm;
        IDataReader dbr;

        dbc = new SqliteConnection(_constr);
        dbc.Open();
        dbcm = dbc.CreateCommand();
        dbcm.CommandText = "SELECT TOWERINDEX FROM USERTOWER "; // mymytable 테이블의 txt 칼럼의 값을 가져오는데, 조건은 id 칼럼의 값이 4이다.
        dbr = dbcm.ExecuteReader();

        while (dbr.Read())
        {
            //result = _dbr.GetInt16(0);
            result = dbr.GetString(0); // 이제 현재 row의 0번째 칼럼의 값 결과물을 문자열로 받아온 것을 result 에 넣어준다.
            userTowerList.Add(int.Parse(result));
        }

        //_dbr.NextResult(); // 만일 결과 row가 여러 줄일 때는 .NextResult() 메서드를 사용하여 다음 row를 다루도록 하자.



        //+ 추가 (DB 닫는 용도)
        dbr.Close();
        dbr = null;
        dbcm.Dispose();
        dbcm = null;
        dbc.Close();
        dbc = null;

        return userTowerList;
    }

    // 유저가 보유하고 있는 타워들 이미지스프라이트 반환
    /*
    public List<Sprite> GetUserTowerSprite(int towerIndex)
    {
        List<Sprite> userTowerSpriteList = new List<Sprite>();
        List<int> userTowerList = new List<int>();
        userTowerList = GetUserTowerList();
        for (int i = 0; i < userTowerList.Count; i++)
        {
            Sprite towerSprite = towerTemplateList.towerTemplate[userTowerList[i]].weapon[0].sprite;
            userTowerSpriteList.Add(towerSprite);
        }

        return userTowerSpriteList;
    }*/
    public Sprite GetUserTowerSprite(int towerIndex)
    {
        return towerTemplateList.towerTemplate[towerIndex].weapon[0].sprite;
    }

    /*
    IEnumerator DBCreate()
    {
        string filepath = string.Empty; // 파일 경로

        // 플랫폼이 안드로이드라면
        if(Application.platform == RuntimePlatform.Android)
        {

        }
        // 기타 플랫폼이라면(ex : PC)
        else
        {
            filepath = Application.dataPath + "/20220413DefenseDB.db";
            // 파일이 없을때
            if (!File.Exists(filepath))  
            {
                File.Copy(Application.streamingAssetsPath + "/20220413DefenseDB.db", filepath);
            }
        }
        Debug.Log("DB생성 완료");
    }
    */

    public void Setup()
    {
        testDataCreate();
        SaveUserTowerData();
        LoadUserData();
    }
    private void testDataCreate()
    {
        List<int> towerIndexList = new List<int>();
        List<int> towerGradeList = new List<int>();
        List<string> towerNameList = new List<string>();
        towerIndexList.Add(0);
        towerIndexList.Add(1);
        towerIndexList.Add(2);
        towerIndexList.Add(3);
        towerIndexList.Add(4);
        towerIndexList.Add(5);
        towerGradeList.Add(0);
        towerGradeList.Add(1);
        towerGradeList.Add(2);
        towerGradeList.Add(3);
        towerGradeList.Add(4);
        towerGradeList.Add(5);
        towerNameList.Add("Tower01_Fire");
        towerNameList.Add("Tower01_Water");
        towerNameList.Add("Tower01_Wind");
        towerNameList.Add("Tower02_Fire");
        towerNameList.Add("Tower02_Water");
        towerNameList.Add("Tower02_Wind");
        userTowerData = new UserTowerData();
        userTowerData.towerIndexList = towerIndexList;
        userTowerData.towerGradeList = towerGradeList;
        userTowerData.towerNameList = towerNameList;
    }

    public void SaveUserTowerData()
    {
        FileStream file = new FileStream(Application.persistentDataPath + "/userTowerData.dat", FileMode.Create);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(file, userTowerData);
        file.Close();
    }

    private void LoadUserData()
    {
        if (File.Exists(Application.persistentDataPath + "/userTowerData.dat"))
        {
            FileStream file = new FileStream(Application.persistentDataPath + "/userTowerData.dat", FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            userTowerData = (UserTowerData)binaryFormatter.Deserialize(file);
            file.Close();
        }
        else
        {
            userTowerData = new UserTowerData();
        }
    }

}


[Serializable]
public class UserTowerData
{
    public List<int> towerIndexList;
    public List<int> towerGradeList;
    public List<string> towerNameList;

}