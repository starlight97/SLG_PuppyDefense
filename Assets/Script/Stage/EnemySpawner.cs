using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private WaveSystem waveSystem; // ���̺� ����
    [SerializeField]
    private GameObject enemyHPSliderPrefab; // �� ü���� ��Ÿ���� Slider UI ������
    [SerializeField]
    private Transform canvasTransform;  // UI�� ǥ���ϴ� Canvas ������Ʈ�� Transform
    [SerializeField]
    private Transform[] wayPoints;  // ���� ���������� �̵� ���

    [SerializeField]
    private PlayerStats playerStats;  // �÷��̾��� ���� ������Ʈ
    private Wave currentWave;   // ���� ���̺� ����
    private int currentEnemyCount;  // ���� ���̺꿡 �����ִ� �� ���� (���̺� ���۽� max�� ����, �� ��� �� -1)
    private List<Enemy> enemyList; // ���� �ʿ� �����ϴ� ��� ���� ����

    // ���� ������ ������ EnemySpawner���� �ϱ� ������ Set�� �ʿ� ����.
    public List<Enemy> EnemyList => enemyList;
    // ���� ���̺��� �����ִ� ��, �ִ� �� ����
    public int CurrentEnemyCount => currentEnemyCount;
    public int MaxEnemyCount => currentWave.maxEnemyCount;

    private void Awake()
    {
        // �� ����Ʈ �޸� �Ҵ�
        enemyList = new List<Enemy>();
        // �� ���� �ڷ�ƾ �Լ� ȣ��
        StartCoroutine("SpawnEnemy");
    }
    public void StartWave(Wave wave)
    {
        // �Ű������� �޾ƿ� ���̺� ���� ����
        currentWave = wave;

        // ���� ���̺� ����
        StartCoroutine("SpawnEnemy");
    }


    private IEnumerator SpawnEnemy()
    {
        // ���� ���̺꿡�� ������ �� ����
        int spawnEnemyCount = 0;

        // ���� ���̺꿡�� �����Ǿ�� �ϴ� ���� ���ڸ�ŭ ���� �����ϰ� �ڷ�ƾ ����
        while (spawnEnemyCount < currentWave.maxEnemyCount)
        {
            //GameObject clone = Instantiate(enemyPrefab);    // �� ������Ʈ ����
            // ���̺꿡 �����ϴ� ���� ������ ���� ������ �� ������ ���� �����ϵ��� �����ϰ�, �� ������Ʈ ����
            int enemyIndex = Random.Range(0, currentWave.enemyPrefabs.Length);
            GameObject clone = Instantiate(currentWave.enemyPrefabs[enemyIndex]);
            Enemy enemy = clone.GetComponent<Enemy>();  // ��� ������ ���� Enemy ������Ʈ
            EnemyHP enemyHP = clone.GetComponent<EnemyHP>();
            enemyHP.Setup(waveSystem);
            enemy.Setup(this, wayPoints);     // wayPoint ������ �Ű������� Setup() ȣ��
            EnemyList.Add(enemy);

            currentEnemyCount = EnemyList.Count;

            SpawnEnemyHPSlider(clone);

            // ���� ���̺꿡�� ������ ���� ���� +1
            spawnEnemyCount++;

            //yield return new WaitForSeconds(spawnTime); // spawnTime �ð� ���� ���
            // �� ���̺긶�� spawnTime�� �ٸ� �� �ֱ� ������ ���� ���̺�(currentWave)�� spawnTime ���
            yield return new WaitForSeconds(currentWave.spawnTime); // spawnTime �ð� ���� ���

        }
    }

    public void DestroyEnemy(EnemyDestroyType type, Enemy enemy, int gold)
    {
        // ���� ��ǥ�������� �������� ��
        if (type == EnemyDestroyType.Arrive)
        {
            // �÷��̾��� ü�� -1
            playerStats.TakeDamage(1);
        }
        // ���� �÷��̾��� �߻�ü���� ������� ��
        else if (type == EnemyDestroyType.Kill)
        {
            // ���� ������ ���� ����� ��� ȹ��
            playerStats.CurrentGold += gold;
        }
        // ����Ʈ���� ����ϴ� �� ���� ����
        EnemyList.Remove(enemy);
        // �� ������Ʈ ����
        Destroy(enemy.gameObject);
        currentEnemyCount = EnemyList.Count;
    }

    private void SpawnEnemyHPSlider(GameObject enemy)
    {
        
        // �� ü���� ��Ÿ���� Slider UI ����
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);
        // Slider UI ������Ʈ�� parent("Canvas" ������Ʈ)�� �ڽ����� ����
        // Tip. UI�� ĵ������ �ڽĿ�����Ʈ�� �����Ǿ� �־�� ȭ�鿡 ���δ�.
        sliderClone.transform.SetParent(canvasTransform);
        // ���� �������� �ٲ� ũ�⸦ �ٽ� (1,1,1)�� ����
        sliderClone.transform.localScale = Vector3.one;

        // Slider UI�� �i�ƴٴ� ����� �������� ����
        sliderClone.GetComponent<EnemySliderPositionAutoSetter>().Setup(enemy.transform);
        // Slider UI�� �ڽ��� ü�� ������ ǥ���ϵ��� ����
        sliderClone.GetComponent<EnemyHPViewer>().Setup(enemy.GetComponent<EnemyHP>());
        

    }
    
}
