using System.Windows.Input;
using Avalonia;
using Avalonia.Xaml.Interactivity;

namespace CloudSculpt.Behaviors;

public class WindowLoadedBehavior  : AvaloniaObject, IAction
{
    public static readonly StyledProperty<ICommand> CommandProperty =
        AvaloniaProperty.Register<WindowLoadedBehavior, ICommand>(nameof(Command));

    public ICommand Command
    {
        get => GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public object Execute(object? sender, object? parameter)
    {
        if (Command == null) return false;
        Command.Execute(true);
        return true;
    }
}