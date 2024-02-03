using System.Windows.Input;
using Avalonia;
using Avalonia.Input;
using Avalonia.Xaml.Interactivity;

namespace CloudSculpt.Behaviors;

public class PointerReleasedCommandBehavior : AvaloniaObject, IAction
{
    public static readonly StyledProperty<ICommand> CommandProperty =
        AvaloniaProperty.Register<PointerReleasedCommandBehavior, ICommand>(nameof(Command));

    public ICommand Command
    {
        get => GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public object Execute(object? sender, object? parameter)
    {
        if (parameter is not PointerReleasedEventArgs e || Command == null) return false;
        Command.Execute(e);
        return true;
    }
}