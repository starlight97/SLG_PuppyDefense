using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private int currentGold = 100;

    [SerializeField]
    private Image imageScreen;    // ��üȭ���� ���� ������ �̹���
    [SerializeField]
    private float maxHP = 20;   // �ִ� ü��
    private float currentHP;    // ���� ü��

    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;

    public int CurrentGold
    {
        set => currentGold = Mathf.Max(0, value);
        get => currentGold;
    }

    private void Awake()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(float damage)
    {
        // ���� ü���� damage��ŭ ����
        currentHP -= damage;

        //StopCoroutine("HitAlphaAnimation");
        //StartCoroutine("HitAlphaAnimation");

        // ü���� 0�� �Ǹ� ���� ����
        if (currentHP <= 0)
        {

        }
    }

    private IEnumerator HitAlphaAnimation()
    {
        // ��üȭ�� ũ��� ��ġ�� imageScreen�� ������ color ������ ����
        // imageScreen�� ������ 40%�� ����
        Color color = imageScreen.color;
        color.a = 0.4f;
        imageScreen.color = color;

        // ������ 0%�� �ɶ����� ����
        while (color.a >= 0.0f)
        {
            color.a -= Time.deltaTime;
            imageScreen.color = color;
            yield return null;
        }
    }
}
