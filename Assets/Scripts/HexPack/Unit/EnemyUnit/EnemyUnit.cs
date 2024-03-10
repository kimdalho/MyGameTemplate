using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyUnit : Unit
{
    public UnitStat stat;
    public int modelId;
    public int grad;

    [Header("소재 아이템")]
    public List<GameMaterial> materials;

    public Node parent;

    public override void SetData(UnitItem item, bool isFront, Node parent)
    {
        base.SetData(item, isFront, parent);

        this.parent = parent;
        this.parent.unit = this;
        this.parent.isWall = true;

        stat = new UnitStat();
        var model = item.baseStat;
        stat.curHp = model.curHp;
        stat.maxHp = model.maxHp;
        stat.atk = model.atk;
        stat.move = model.move;
        stat.maxHp = model.maxHp;

        hud = GetComponent<EntryHud>();
        hud.Draw(item, stat);
        modelId = item.id;
        grad = item.grad;

        materials = new List<GameMaterial>();
        for (int i = 0; i < item.dropitemIds.Length; i++)
        {
            int itemId = item.dropitemIds[i];
            GameMaterial itemModel = UnitManager.Instance.GetGameMaterial(itemId);
            materials.Add(itemModel);
        }
    }

    public override int Hit(Unit AttackUnit)
    {
        var oldhp = stat.curHp;
        stat.curHp = Math.Clamp(stat.curHp - AttackUnit.GetAttack(), 0, stat.maxHp);

        if (stat.curHp == 0)
        {
            SkillManager.Instance.WhenPlayerAttack(oldhp);
        }
        else
        {
            SkillManager.Instance.WhenPlayerAttack(AttackUnit.GetAttack());
        }


        return stat.curHp;
    }

    public override int GetAttack()
    {
        return stat.atk;
    }

    public override int GetMaxHp()
    {
        return stat.maxHp;
    }

    public override int GetCurrentHp()
    {
        return stat.curHp;
    }

}
