namespace TaskManagement.Commands;

public interface ICommand
{
}

public interface ICommand<TResult> : ICommand
{
}
