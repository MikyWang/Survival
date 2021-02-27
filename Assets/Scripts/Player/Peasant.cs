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
    public override void Select(GameObject highlightingPrefab)
    {
        base.Select(highlightingPrefab);
        MouseManager.Instance.OnTreeClicked += CutTree;
    }
    public override void UnSelect()
    {
        base.UnSelect();
        MouseManager.Instance.OnTreeClicked -= CutTree;
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
    private void CutTree(IDamage defender)
    {
        cutTree.Interrupt();
        ChangeWeapon(WeaponType.Axe);
        cutTree.Excute(defender);
    }

    public override void Move(Vector3 target)
    {
        cutTree.Interrupt();
        base.Move(target);
    }
}
