public class ValueChangeNotifier<TValue> : AwaiterBase<SubscribeValue<TValue>, TValue>
{
	public ValueChangeNotifier(SubscribeValue<TValue> value) : base(value)
	{
		this.value.OnChanged += HandleValueChanged;
	}

	protected override void Complete()
	{
		this.value.OnChanged -= HandleValueChanged;
		this.result = this.value.Value;
	}
}