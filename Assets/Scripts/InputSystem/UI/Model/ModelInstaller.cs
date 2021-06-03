using Zenject;
using Unity;
using UnityEngine;

public class ModelInstaller : MonoInstaller
{
	[SerializeField] private Vector3Value _currentGroundPosition;
	[SerializeField] private AssetStorage _context;

	public override void InstallBindings()
	{
		Container.Bind<AssetStorage>().FromInstance(_context).AsSingle();

		Container.Bind<CommandCreatorBase<IProductionCommand>>().To<ProduceUnitCommandCreator>().AsTransient();
		Container.Bind<CommandCreatorBase<IMoveCommand>>().To<MoveCommandCreator>().AsTransient();

		Container.Bind<ButtonPanel>().AsTransient();

		Container.Bind<Vector3Value>().FromInstance(_currentGroundPosition).AsSingle();
	}
}
