using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class MoveUnitExecutor : CommandExecutorBase<IMoveCommand>
{
	private const int DISABLE_CHECK_MAX_COUNT = 3;
	private const float REMAINING_DISTANCE_DELTA = 0.05f;

	[SerializeField] private NavMeshAgent _agent;

	private int _disableCheckCount = 0;

	private float _lastRemainingDistance = 0;

	public void Stop()
	{
		_agent.isStopped = true;
		_agent.ResetPath();
	}

	protected override async Task ExecuteConcreteCommand(IMoveCommand command)
	{
		_agent.SetDestination(command.Position);
		await WaitForComplete();
		_disableCheckCount = 0;
	}

	private bool IsFinishMoving() => !_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance && (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f);

	private void TryIncreaseFailureAttempt()
	{
		if (Math.Abs(_agent.remainingDistance - _lastRemainingDistance) < REMAINING_DISTANCE_DELTA)
			_disableCheckCount++;
		else 
			_disableCheckCount = 0;

		_lastRemainingDistance = _agent.remainingDistance;
		Debug.Log("CURRENT CHECK COUNT " + _disableCheckCount);
	}

	private async Task WaitForComplete()
	{
		while (!IsFinishMoving())
		{
			await Task.Delay(100); // TODO придумать нормальное решение
			TryIncreaseFailureAttempt();

			if (_disableCheckCount < DISABLE_CHECK_MAX_COUNT)
				continue;

			Stop();
			Debug.Log("COMMAND IS STOPED");
			break;
		}

		await Task.CompletedTask;
	}
}