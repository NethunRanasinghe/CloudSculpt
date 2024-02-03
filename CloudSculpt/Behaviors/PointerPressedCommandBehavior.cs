using System.Windows.Input;
using Avalonia;
using Avalonia.Input;
using Avalonia.Xaml.Interactivity;

namespace CloudSculpt.Behaviors;

public class PointerPressedCommandBehavior : AvaloniaObject, IAction
{
    public static readonly StyledProperty<ICommand> CommandProperty =
        AvaloniaProperty.Register<PointerPressedCommandBehavior, ICommand>(nameof(Command));

    public ICommand Command
    {
        get => GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public object Execute(object? sender, object? parameter)
    {
        if (parameter is not PointerPressedEventArgs e || Command == null) return false;
        Command.Execute(e);
        return true;
    }
}