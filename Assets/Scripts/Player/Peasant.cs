using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peasant : PlayerController
{
    public enum WeaponType
    {
        Axe, Hammer, Pick
    }
    public enum PackageType
    {
        Wood, Bag, Package, None
    }
    public GameObject[] weapons;
    public GameObject[] packages;
    public CutTree cutTree => skills[SkillId.CutTree] as CutTree;
    public Build build => skills[SkillId.Build] as Build;
    public override void Select(GameObject highlightingPrefab)
    {
        base.Select(highlightingPrefab);
    }
    public override void UnSelect()
    {
        base.UnSelect();
    }

    private void ChangeWeapon(WeaponType weaponType)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if ((int)weaponType == i)
            {
                weapons[i].SetActive(true);
                continue;
            }
            weapons[i].SetActive(false);
        }
    }
    private void ShowPackage(PackageType packageType)
    {
        for (int i = 0; i < packages.Length; i++)
        {
            if (packageType != PackageType.None && (int)packageType == i)
            {
                packages[i].SetActive(true);
                continue;
            }
            packages[i].SetActive(false);
        }
    }

    public void AddWood()
    {
        cutTree.canUse = false;
        ShowPackage(PackageType.Wood);
    }
    public void CutTree(IDamage defender)
    {
        ChangeWeapon(WeaponType.Axe);
        cutTree.Excute(defender);
    }

    public void Build(Building building)
    {
        ChangeWeapon(WeaponType.Hammer);
        build.Excute(building);
    }

    public override void Move(Vector3 target)
    {
        cutTree.Interrupt();
        build.Interrupt();
        base.Move(target);
    }
}
