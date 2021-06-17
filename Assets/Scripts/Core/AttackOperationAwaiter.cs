
using static AttackUnitExecutor;

public class AttackOperationAwaiter : AwaiterBase<AttackOperation, AsyncExtensions.Void>
{
	public AttackOperationAwaiter(AttackOperation value) : base(value)
	{
		this.value.OnComplete += HandleValueChanged;
	}

	protected override void Complete()
	{
		this.value.OnComplete -= HandleValueChanged;
		result = new AsyncExtensions.Void();
	}
}
