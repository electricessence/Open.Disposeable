/*!
 * @author electricessence / https://github.com/electricessence/
 * Licensing: MIThttps://github.com/electricessence/Open.Disposable/blob/master/LISCENSE.md
 */

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Open.Disposable
{
	public class AsyncDisposeHandler : AsyncDisposableBase
	{
		public AsyncDisposeHandler(Func<ValueTask> action)
		{
			_action = action ?? throw new ArgumentNullException(nameof(action));
		}

		Func<ValueTask> _action;

		protected override ValueTask OnDisposeAsync(AsyncDisposeMode mode)
			=> Nullify(ref _action).Invoke();
	}

	public class AsyncDisposeHandler<T> : AsyncDisposeHandler
	{
		public AsyncDisposeHandler(T value, Func<ValueTask> action) : base(action)
		{
			Value = value;
		}

		public T Value { get; private set; }

		protected override ValueTask OnDisposeAsync(AsyncDisposeMode mode)
		{
			Value = default;
			return base.OnDisposeAsync(mode);
		}
	}
}
