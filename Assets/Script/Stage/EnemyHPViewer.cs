using UnityEngine;
using UnityEngine.UI;

public class EnemyHPViewer : MonoBehaviour
{
    private EnemyHP enemy;
    private Slider hpSlider;

    public void Setup(EnemyHP enemy)
    {
        this.enemy = enemy;
        hpSlider = GetComponent<Slider>();
    }

    void Update()
    {
        hpSlider.value = enemy.CurrentHP / enemy.MaxHP;
    }
}
