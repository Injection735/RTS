using Abstractions;
using Core;
using Zenject;

public class CoreInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
		Container.BindInterfacesAndSelfTo<TimeModel>().AsSingle();
		Container.BindInterfacesAndSelfTo<ProduceUnitExecutorEllen>().FromComponentInHierarchy().AsTransient();
		Container.BindInterfacesAndSelfTo<ProduceUnitExecutorChomper>().FromComponentInHierarchy().AsTransient();
		Container.BindInterfacesAndSelfTo<AttackUnitExecutor>().FromComponentInHierarchy().AsTransient();

		Container.Bind<BuildingsData>().AsSingle();
		Container.Bind<BalancesData>().AsSingle();
	}
}
