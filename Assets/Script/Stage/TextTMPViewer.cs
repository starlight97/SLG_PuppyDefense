using UnityEngine;
using TMPro;

public class TextTMPViewer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textPlayerHP;   // Text - TextMeshPro UI [�÷��̾� ü��]
    [SerializeField]
    private TextMeshProUGUI textPlayerGold;   // Text - TextMeshPro UI [�÷��̾� ���]
    [SerializeField]
    private TextMeshProUGUI textWave;   // Text - TextMeshPro UI [���� ���̺�]
    [SerializeField]
    private TextMeshProUGUI textEnemyCount;   // Text - TextMeshPro UI [���� �� ����]
    [SerializeField]
    private TextMeshProUGUI textTime;   // Text - TextMeshPro UI [���� �ð�]


    [SerializeField]
    private PlayerStats playerStats;  // �÷��̾� ����(ü�� ,���)
    [SerializeField]
    private WaveSystem waveSystem;  // ���̺� ����
    [SerializeField]
    private EnemySpawner enemySpawner;  // �� ����


    void Update()
    {
        textPlayerHP.text = "HP : " + playerStats.CurrentHP;
        textPlayerGold.text = "Gold : " + playerStats.CurrentGold;
        textWave.text = "Wave : " + waveSystem.CurrentWave;
        textEnemyCount.text = "Enemy : " + enemySpawner.CurrentEnemyCount;
        textTime.text = "NextWave : " + (int)waveSystem.Time_WaveLimit;
    }
}
