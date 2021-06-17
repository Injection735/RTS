
using Zenject;

namespace Core.GameObjectInstallers
{
	public class ChomperPrefabInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<float>().WithId("Distance").FromInstance(1.0f);
			Container.Bind<float>().WithId("AttackSpeed").FromInstance(0.3f);
			Container.Bind<float>().WithId("AttackDamage").FromInstance(0.3f);
		}
	}
}
