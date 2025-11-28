using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using UnityEngine;

internal class StoveSystem : IProtoInitSystem, IProtoRunSystem
{
    [DI] readonly PlayerAspect _playerAspect;
    [DI] readonly WorkstationsAspect _workstationsAspect;
    [DI] readonly ItemAspect _itemAspect;
    [DI] readonly BaseAspect _baseAspect;
    [DI] readonly ViewAspect _viewAspect;
    [DI] readonly ProtoWorld _world;

    private ProtoIt _placeIterator;
    private ProtoIt _processingIterator;
    private ProtoIt _abortIterator;
    private RecipeService _recipeService;

    public StoveSystem(RecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    public void Init(IProtoSystems systems)
    {
        _placeIterator = new(new[]
        {
            typeof(WorkstationsTypeComponent),
            typeof(InteractableComponent),
            typeof(HolderComponent),
            typeof(StoveComponent),
            typeof(ItemPlaceEvent),
        });
        _processingIterator = new(new[]
        {
            typeof(WorkstationsTypeComponent),
            typeof(InteractableComponent),
            typeof(StoveComponent),
            typeof(TimerComponent),
        });
        _abortIterator = new(new[]
        {
            typeof(WorkstationsTypeComponent),
            typeof(InteractableComponent),
            typeof(StoveComponent),
            typeof(ItemPickEvent),
        });
        _abortIterator.Init(_world);
        _placeIterator.Init(_world);
        _processingIterator.Init(_world);
    }

    public void Run()
    {
        foreach (var stoveEntity in _placeIterator)
        {
            ref var works = ref _workstationsAspect.WorkstationsTypePool.Get(stoveEntity);
            ref var holder = ref _playerAspect.HolderPool.Get(stoveEntity);

            if (holder.ItemType == PickupItemType.None)
            {
                Debug.Log("Item type is None");
                continue;
            }

            if (!_recipeService.TryGetRecipe(holder.ItemType, works.WorkstationType, out var recipe))
            {
                Debug.Log("Recipe not found");
                continue;
            }

            ref var timer = ref _baseAspect.TimerPool.Add(stoveEntity);
            timer.Duration = recipe.Duration;

            _viewAspect.ProgressBarPool.Get(stoveEntity).ShowComponent();
        }

        foreach (var stoveEntity in _abortIterator)
        {
            _baseAspect.TimerPool.DelIfExists(stoveEntity);
            _viewAspect.ProgressBarPool.Get(stoveEntity).HideComponent();
        }

        foreach (var stoveEntity in _processingIterator)
        {
            ref var holder = ref _playerAspect.HolderPool.Get(stoveEntity);
            ref var timer = ref _baseAspect.TimerPool.Get(stoveEntity);

            if (!timer.Completed) continue;

            ref var works = ref _workstationsAspect.WorkstationsTypePool.Get(stoveEntity);
            if (!_recipeService.TryGetRecipe(holder.ItemType, works.WorkstationType, out var recipe)) continue;

            holder.ItemType = recipe.outputItemType;
            holder.SpriteRenderer.sprite = recipe.outputItemSprite;
            _baseAspect.TimerPool.DelIfExists(stoveEntity);
            _viewAspect.ProgressBarPool.Get(stoveEntity).HideComponent();
        }
    }
}