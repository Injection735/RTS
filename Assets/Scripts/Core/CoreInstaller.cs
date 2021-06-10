using Abstractions;
using Core;
using Zenject;

public class CoreInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
		Container.BindInterfacesAndSelfTo<TimeModel>().AsSingle();
	}
}
