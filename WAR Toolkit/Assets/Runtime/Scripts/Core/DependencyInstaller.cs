using WarToolkit.ObjectData;
using WarToolkit.Pathfinding;
using Zenject;

public class DependencyInstaller : MonoInstaller
{
    public MapData mapData;

    public override void InstallBindings()
    {
        Container.Bind<ITileQuery<DataTile>>().To<MovementHighlighter<DataTile>>().AsSingle();
        Container.Bind<ITilePathFinder<DataTile>>().To<MovementPathfinder<DataTile>>().AsSingle();
        Container.Bind<IMapController<DataTile>>().To<MapController>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<IMapData<DataTile>>().To<MapData>().FromScriptableObject(mapData).AsSingle();
        Container.Bind<IEventManager>().To<EventManager>().AsSingle();
        Container.Bind<DebugSelectionListener>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
    }
}
