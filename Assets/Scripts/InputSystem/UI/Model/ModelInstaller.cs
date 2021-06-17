using Zenject;
using Unity;
using UnityEngine;

public class ModelInstaller : MonoInstaller
{
	[SerializeField] private Vector3Value _collectionPosition;
	[SerializeField] private Vector3Value _currentGroundPosition;
	[SerializeField] private Vector3Collection _patrolPoints;
	[SerializeField] private AttackableValue _enemyValue;
	[SerializeField] private AssetStorage _context;
	[SerializeField] private HoldValue _holdValue;

	public override void InstallBindings()
	{
		Container.Bind<AssetStorage>().FromInstance(_context).AsSingle();

		Container.Bind<CommandCreatorBase<IProductionCommandEllen>>().To<ProduceEllenCommandCreator>().AsTransient();
		Container.Bind<CommandCreatorBase<IProductionCommandChomper>>().To<ProduceChomperCommandCreator>().AsTransient();

		Container.Bind<CommandCreatorBase<IMoveCommand>>().To<MoveCommandCreator>().AsTransient();
		Container.Bind<CommandCreatorBase<IAttackCommand>>().To<AttackCommandCreator>().AsTransient();
		Container.Bind<CommandCreatorBase<IPatrolCommand>>().To<PatrolCommandCreator>().AsTransient();
		Container.Bind<CommandCreatorBase<IStopCommand>>().To<StopCommandCreator>().AsTransient();
		Container.Bind<CommandCreatorBase<ISetCollectionPointCommand>>().To<SetCollectionCommandCreator>().AsTransient();

		Container.Bind<IAwaitable<Vector3>>().FromInstance(_currentGroundPosition);
		Container.Bind<Vector3Value>().FromInstance(_collectionPosition);
		Container.Bind<IAwaitable<bool>>().FromInstance(_holdValue).AsSingle();
		Container.Bind<Vector3Collection>().FromInstance(_patrolPoints).AsSingle();
		Container.Bind<AttackableValue>().FromInstance(_enemyValue).AsSingle();
	
		Container.Bind<ButtonPanel>().AsTransient();
		Container.Bind<UnitProductionPanel>().AsTransient();

		SetUnitInfo();
	}

	private void SetUnitInfo()
	{

	}
}
