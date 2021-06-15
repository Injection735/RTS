
using UniRx;

public interface IUnitProducer
{
	public IReactiveCollection<IUnitProductionTask> Queue { get; }
}
