using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peasant : PlayerController
{
    public enum WeaponType
    {
        axe, hammer, pick
    }
    public GameObject[] weapons;
    private CutTree cutTree;
    public bool isCutting => cutTree.isCutting;
    protected override void Awake()
    {
        base.Awake();
        cutTree = GetComponent<CutTree>();
    }

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

    private void CutTree(IDamage defender)
    {
        StartCoroutine(CutTreeIntern(defender));
    }

    //TODO:修复砍树BUG
    private IEnumerator CutTreeIntern(IDamage defender)
    {
        while (isCutting)
        {
            cutTree.Interrupt();
            yield return new WaitForEndOfFrame();
        }
        ChangeWeapon(WeaponType.axe);
        cutTree.Excute(defender);
    }

    public override void Move(Vector3 target)
    {
        cutTree.Interrupt();
        base.Move(target);
    }
}
