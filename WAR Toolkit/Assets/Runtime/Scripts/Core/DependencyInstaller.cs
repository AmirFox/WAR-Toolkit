using UnityEngine;
using WarToolkit.Managers;
using WarToolkit.ObjectData;
using WarToolkit.Pathfinding;
using Zenject;

public class DependencyInstaller : MonoInstaller
{
    public MatchData matchData;

    public override void InstallBindings()
    {
        Container.Bind<ITurnManager>().To<TurnManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<ITileQuery>().To<MovementHighlighter>().AsSingle();
        Container.Bind<ITilePathFinder>().To<MovementPathfinder>().AsSingle();
        Container.Bind<IMapController>().To<MapController>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<MatchData>().FromScriptableObject(matchData).AsSingle();
        Container.Bind<IEventManager>().To<EventManager>().AsSingle().Lazy();
        Container.BindIFactory<Player>();
        Container.BindFactory<IFactionData, int, Vector2, int, Player, Player.Factory>();
    }
}
