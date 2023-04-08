using WarToolkit.ObjectData;
using WarToolkit.Pathfinding;
using Zenject;

public class DependencyInstaller : MonoInstaller
{
    public MapData mapData;

    public override void InstallBindings()
    {
        Container.Bind<ITileQuery>().To<MovementHighlighter>().AsSingle();
        Container.Bind<ITilePathFinder>().To<MovementPathfinder>().AsSingle();
        Container.Bind<IMapController>().To<MapController>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<IMapData<DataTile>>().To<MapData>().FromScriptableObject(mapData).AsSingle();
        Container.Bind<IEventManager>().To<EventManager>().AsSingle();
        Container.Bind<DebugSelectionListener>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
    }
}
