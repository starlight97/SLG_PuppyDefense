using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerData : MonoBehaviour
{
    [SerializeField]
    private TowerTemplateList towerTemplateList;

    public TowerTemplate GetTowerTemplate(int towerIndex)
    {
        return towerTemplateList.towerTemplate[towerIndex];
    }

    public Sprite GetTowerSprite(int towerIndex)
    {
        return towerTemplateList.towerTemplate[towerIndex].weapon[0].sprite;
    }


    public WeaponProperty GetTowerProperty(int towerIndex)
    {
        return towerTemplateList.towerTemplate[towerIndex].weaponProperty;
    }
}
