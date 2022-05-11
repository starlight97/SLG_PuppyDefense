using UnityEngine;
using TMPro;

public class TextTMPViewer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textPlayerHP;   // Text - TextMeshPro UI [플레이어 체력]
    [SerializeField]
    private TextMeshProUGUI textPlayerGold;   // Text - TextMeshPro UI [플레이어 골드]
    [SerializeField]
    private TextMeshProUGUI textWave;   // Text - TextMeshPro UI [현재 웨이브]
    [SerializeField]
    private TextMeshProUGUI textEnemyCount;   // Text - TextMeshPro UI [현재 적 숫자]
    [SerializeField]
    private TextMeshProUGUI textTime;   // Text - TextMeshPro UI [남은 시간]


    [SerializeField]
    private PlayerStats playerStats;  // 플레이어 정보(체력 ,골드)
    [SerializeField]
    private WaveSystem waveSystem;  // 웨이브 정보
    [SerializeField]
    private EnemySpawner enemySpawner;  // 적 정보


    void Update()
    {
        textPlayerHP.text = "HP : " + playerStats.CurrentHP;
        textPlayerGold.text = "Gold : " + playerStats.CurrentGold;
        textWave.text = "Wave : " + waveSystem.CurrentWave;
        textEnemyCount.text = "Enemy : " + enemySpawner.CurrentEnemyCount;
        textTime.text = "NextWave : " + (int)waveSystem.Time_WaveLimit;
    }
}
