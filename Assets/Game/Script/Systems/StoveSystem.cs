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
    private PickableService _pickableService;

    public StoveSystem(RecipeService recipeService, PickableService pickableService)
    {
        _recipeService = recipeService;
        _pickableService = pickableService;
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

            if (holder.Item == null)
            {
                Debug.Log("Item type is None");
                continue;
            }

            if (!_recipeService.TryGetRecipe(holder.Item, works.workstationType.GetType(), out var recipe))
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
            _baseAspect.TimerCompletedPool.Add(stoveEntity);
            _viewAspect.ProgressBarPool.Get(stoveEntity).HideComponent();
        }

        foreach (var stoveEntity in _processingIterator)
        {
            ref var holder = ref _playerAspect.HolderPool.Get(stoveEntity);
            ref var timer = ref _baseAspect.TimerPool.Get(stoveEntity);

            if (!timer.Completed) continue;

            ref var works = ref _workstationsAspect.WorkstationsTypePool.Get(stoveEntity);
            if (!_recipeService.TryGetRecipe(holder.Item, works.workstationType.GetType(), out var recipe)) continue;

            holder.Item = recipe.outputItemType.GetType();
            
            _pickableService.TryGetPickable(recipe.outputItemType.GetType(), out var pickable);
            
            holder.SpriteRenderer.sprite = pickable.PickupItemSprite;
            _viewAspect.ProgressBarPool.Get(stoveEntity).HideComponent();
        }
    }
}