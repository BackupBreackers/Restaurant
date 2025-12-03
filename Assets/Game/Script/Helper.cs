using Game.Script;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

public static class Helper
{
    public static void TransferItem(ProtoEntity from, ProtoEntity to, ref HolderComponent fromHolder,
        ref HolderComponent toHolder, PlayerAspect playerAspect)
    {
        // 1. Перекидываем ссылку на Entity предмета
        //toHolder.Entity = fromHolder.Entity;
        toHolder.SpriteRenderer.sprite = fromHolder.SpriteRenderer.sprite;
        toHolder.Item = fromHolder.Item;

        //fromHolder.Entity = default;
        fromHolder.SpriteRenderer.sprite = null;
        fromHolder.Item = null;

        // 2. Обновляем теги
        playerAspect.HasItemTagPool.Add(to);
        playerAspect.HasItemTagPool.DelIfExists(from);

        // Примечание: В ECS часто удобнее сразу тут отправить событие или обновить компонент View,
        // чтобы модель синхронизировалась с Unity GameObject (трансформом предмета).
    }

    public static void EatItem(ProtoEntity guestEntity, ref HolderComponent guestHolder, PlayerAspect playerAspect)
    {
        guestHolder.Clear();
        playerAspect.HasItemTagPool.DelIfExists(guestEntity);
    }
    public static void CreateItem(ProtoEntity playerEntity, ref HolderComponent playerHolder, PlayerAspect playerAspect, PickableItem itemPick)
    {
        playerHolder.SpriteRenderer.sprite = itemPick.PickupItemSprite;
        playerHolder.Item = itemPick.GetType();
        
        playerAspect.HasItemTagPool.GetOrAdd(playerEntity);
        
    }
}