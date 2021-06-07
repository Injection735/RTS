using System.Threading;
using System.Threading.Tasks;

public static class AsyncExtensions
{
	public struct Void { }

	public static async Task<T> WithCancelation<T>(this Task<T> task, CancellationToken cancelationToken)
	{
		var voidTask = new TaskCompletionSource<Void>();

		using (cancelationToken.Register(token => ((TaskCompletionSource<Void>) token).TrySetResult(new Void()), voidTask))
		{
			var any = await Task.WhenAny(task, voidTask.Task);

			if (any == voidTask.Task)
				cancelationToken.ThrowIfCancellationRequested();
		}

		return await task;
	}

	public static Task<TValue> AsTask<TValue>(this IAwaitable<TValue> awaitable)
	{
		return Task.Run(async () => await awaitable);
	}
}