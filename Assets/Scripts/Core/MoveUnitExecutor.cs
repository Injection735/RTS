

public class MoveUnitExecutor : CommandExecutorBase<IMoveCommand>
{
	protected override void ExecuteConcreteCommand(IMoveCommand command)
	{
		// TODO плавное перемещение
		transform.position = command.Position;
	}
}