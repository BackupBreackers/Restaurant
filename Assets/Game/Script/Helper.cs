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
}