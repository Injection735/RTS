
public interface IAwaitable<TValue>
{
	IAwaiter<TValue> GetAwaiter();
}
