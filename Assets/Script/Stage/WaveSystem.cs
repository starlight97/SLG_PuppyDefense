using UnityEngine;

public class WaveSystem : MonoBehaviour
{    
    [SerializeField]
    private float time_WaveStart;
    [SerializeField]
    private float time_WaveReady;
    [SerializeField]
    private ChangeScene changeScene;
    private float time_Current;
    private float time_WaveLimit;
    private bool isWaveReady = false;

    [SerializeField]
    private Wave[] waves;       // ���� ���������� ��� ���̺� ����
    [SerializeField]
    private EnemySpawner enemySpawner;
    private int currentWaveIndex = -1;  // ���� ���̺� �ε���

    // ���̺� ���� ����� ���� Get ������Ƽ (���� ���̺�, �� ���̺�)
    public int CurrentWave => currentWaveIndex + 1; // ������ 0�̱� ������ +1
    public int MaxWave => waves.Length;
    public float Time_WaveLimit => time_WaveLimit;


    private void Start()
    {
        //StartWave();
    }
    private void Update()
    {
        time_Current = Time.deltaTime + time_Current;

        if(enemySpawner.CurrentEnemyCount >= 50)
        {
            changeScene.LoadScene("GameOver");
        }

        if(!isWaveReady)
        {
            time_WaveLimit = time_WaveReady - time_Current;
            if (time_Current > time_WaveReady)
            {
                StartWave();
                time_Current = 0;
                isWaveReady = true;
            }
        }
        else
        {
            time_WaveLimit = time_WaveStart - time_Current;
            if (time_Current > time_WaveStart)
            {
                StartWave();
                time_Current = 0;
            }
        }
        time_WaveLimit = time_WaveLimit % 60;
    }

    public void StartWave()
    {        
        // �ε����� ������ -1�̱� ������ ���̺� �ε��� ������ ���� ������
        currentWaveIndex++;        

        if(currentWaveIndex < 2)
        {
            // EnemySpawner�� StartWave() �Լ� ȣ��, ���� ���̺� ���� ����
            enemySpawner.StartWave(waves[0]);
        }
        else if(currentWaveIndex < 4)
        {
            // EnemySpawner�� StartWave() �Լ� ȣ��, ���� ���̺� ���� ����
            enemySpawner.StartWave(waves[1]);
        }
        else
        {
            enemySpawner.StartWave(waves[2]);
        }

        /*
        // ���� �ʿ� ���� ����, Wave�� ����������
        if (enemySpawner.EnemyList.Count == 0 && currentWaveIndex < waves.Length - 1)
        {
            // �ε����� ������ -1�̱� ������ ���̺� �ε��� ������ ���� ������
            currentWaveIndex++;
            // EnemySpawner�� StartWave() �Լ� ȣ��, ���� ���̺� ���� ����
            enemySpawner.StartWave(waves[currentWaveIndex]);
        }*/
    }
}

[System.Serializable]
public struct Wave
{
    public float spawnTime; // ���� ���̺� �� ���� �ֱ�
    public int maxEnemyCount;   // ���� ���̺� �� ���� ����
    public GameObject[] enemyPrefabs;   // ���� ���̺� �� ���� ����
}