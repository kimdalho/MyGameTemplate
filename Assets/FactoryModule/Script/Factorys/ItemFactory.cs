using UnityEngine;
using UnityFactory;

public class ItemFactory : BaseFactory<ItemInfo, ItemObject>
{
    public override ItemObject Create(ItemInfo info, Transform parent)
    {
        return base.Create(info, parent);
    }

    public ItemObject CreateRandomItem(Transform parent)
    {
        System.Random rnd = new System.Random();
        var config = configSOs[rnd.Next(configSOs.Count)];
        return Create(config, parent);
    }

}

