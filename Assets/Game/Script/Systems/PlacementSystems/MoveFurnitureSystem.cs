using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class MoveFurnitureSystem : IProtoInitSystem, IProtoRunSystem, IProtoDestroySystem
{
    private ProtoIt _iteratorMoveFurniture;
    private ProtoIt _iteratorPlacement;
    private PlayerAspect _playerAspect;
    private PlacementAspect _placementAspect;
    private PhysicsAspect _physicsAspect;
    private ProtoWorld _world;

    public void Init(IProtoSystems systems)
    {
        _world = systems.World();
        _playerAspect = (PlayerAspect)_world.Aspect(typeof(PlayerAspect));
        _placementAspect = (PlacementAspect)_world.Aspect(typeof(PlacementAspect));
        _physicsAspect = (PhysicsAspect)_world.Aspect(typeof(PhysicsAspect));

        _iteratorMoveFurniture = new(new[] { typeof(MoveStateComponent), typeof(InteractableComponent), typeof(Rigidbody2DComponent) });
        _iteratorPlacement = new(new[] { typeof(PlacementGridComponent) });

        _iteratorMoveFurniture.Init(_world);
        _iteratorPlacement.Init(_world);
    }

    public void Run()
    {
        foreach (var furn in _iteratorMoveFurniture)
        {
            ref var moveComponent = ref _placementAspect.MoveStatePool.Get(furn);
            //получаем игрока
            moveComponent.Invoker.TryUnpack(out _, out var playerEntity);
            ref var playerInput = ref _playerAspect.InputRawPool.Get(playerEntity);

            ref var interactComponent = ref _playerAspect.InteractablePool.Get(furn);

            ref var furnComponent = ref _placementAspect.FurniturePool.Get(furn);

            ref var playerPosition = ref _physicsAspect.PositionPool.Get(playerEntity);

            ref var furnRigidBody = ref _physicsAspect.Rigidbody2DPool.Get(furn);

            var playerCollider = _physicsAspect.Rigidbody2DPool.Get(playerEntity).Rigidbody2D.gameObject.GetComponent<Collider2D>();

            if (playerInput.MoveFurniturePressed)
            {
                if (playerInput.IsInMoveState)
                {
                    playerInput.IsInMoveState = false;
                    interactComponent.SpriteRenderer.color = Color.white;
                    _placementAspect.MoveStatePool.DelIfExists(furn);
                    continue;
                }
                playerInput.IsInMoveState = true;
            }

            var moveColor = Color.violetRed; //заменить

            interactComponent.SpriteRenderer.color = moveColor;

            foreach (var gridEntity in _iteratorPlacement)
            {
                ref var gridComponent = ref _placementAspect.PlacementGridPool.Get(gridEntity);
                var newFurnPosition = GetNearestGridViewedCell(gridComponent,ref playerInput, playerPosition, furnComponent.PositionInGrid);

                //забыть компонент на старом месте
                gridComponent.GridData[furnComponent.PositionInGrid.x][furnComponent.PositionInGrid.y] = new FurnitureComponent() 
                { 
                    Type = FurnitureType.None 
                };

                furnComponent.PositionInGrid = newFurnPosition;
                var furnTrans = furnComponent.ThisGameObject.transform;

                furnTrans.position = gridComponent.GridStartPosition + 
                    gridComponent.CellSize * new Vector3(newFurnPosition.x, newFurnPosition.y, 0);

                //запомнить на новой позиции
                gridComponent.GridData[furnComponent.PositionInGrid.x][furnComponent.PositionInGrid.y] = furnComponent;
            }
        }
    }

    private Vector2Int GetNearestGridViewedCell(PlacementGridComponent grid,ref PlayerInputComponent input, 
        PositionComponent pos, Vector2Int defaultVector)
    {
        if (pos.Position.x > grid.GridStartPosition.x && pos.Position.x < grid.GridStartPosition.x + grid.GridSize.x * grid.CellSize)
        {
            if (pos.Position.y > grid.GridStartPosition.y && pos.Position.y < grid.GridStartPosition.y + grid.GridSize.y * grid.CellSize)
            {
                var gridStart = new Vector2(grid.GridStartPosition.x,grid.GridStartPosition.y);
                var point = (pos.Position + input.LookDirection * grid.CellSize / 2 - gridStart) / grid.CellSize;
                var foundPos = new Vector2Int(Mathf.RoundToInt(point.x),Mathf.RoundToInt(point.y));
                var angle = Vector2.SignedAngle(new Vector2(1, 0), input.LookDirection);
                var diff = SwitchLookAngle(angle);
                var res = foundPos + diff;
                if (res.x >= 0 && res.x < grid.GridSize.x)
                    if (res.y >= 0 && res.y < grid.GridSize.y)
                        return res;
            }
        }
        return defaultVector;
    }
    private Vector2Int SwitchLookAngle(float angle)
    {
        if (angle > -45f / 2 && angle <= 45f / 2)
            return new Vector2Int(1, 0);
        if (angle > 45f / 2 && angle <= 45 + 45f / 2)
            return new Vector2Int(1, 1);
        if (angle > 45 + 45f / 2 && angle <= 90 + 45f / 2)
            return new Vector2Int(0, 1);
        if (angle > 90 + 45f / 2 && angle <= 135 + 45f / 2)
            return new Vector2Int(-1, 1);
        if (angle > -90 + 45f / 2 && angle <= -45f / 2)
            return new Vector2Int(1, -1);
        if (angle > -135 + 45f / 2 && angle <= -90 + 45f / 2)
            return new Vector2Int(0, -1);
        if (angle > -180 + 45f / 2 && angle <= -135 + 45f / 2)
            return new Vector2Int(-1, -1);
        return new Vector2Int(-1, 0);
    }

    public void Destroy()
    {
        throw new System.NotImplementedException();
    }
}
