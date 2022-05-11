using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower01Bullet : MonoBehaviour
{
    private Movement2D movement2D;
    private Transform target;
    private float damage;

    public void Setup(Transform target, float damage)
    {
        movement2D = GetComponent<Movement2D>();
        this.target = target;
        this.damage = damage;
    }


    void Update()
    {
        if (target != null)  // Ÿ���� �����ϸ�
        {
            // �߻�ü�� target�� ��ġ�� �̵�
            Vector3 direction = (target.position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        else
        {
            // �߻�ü ������Ʈ ����
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return; // ���� �ƴ� ���� �ε�����
        if (collision.transform != target) return;  // ���� target�� ���� �ƴ� ��

        //collision.GetComponent<Enemy>().OnDie();    // �� ��� �Լ� ȣ��
        collision.GetComponent<EnemyHP>().TakeDamage(damage);   // �� ü���� damage��ŭ ����
        Destroy(gameObject);    // �߻�ü ������Ʈ ����

    }
}
