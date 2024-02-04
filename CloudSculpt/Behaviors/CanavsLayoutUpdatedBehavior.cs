using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Xaml.Interactivity;

namespace CloudSculpt.Behaviors;

public class CanvasLayoutUpdatedBehavior : AvaloniaObject, IAction
{
    public static readonly StyledProperty<ICommand> CommandProperty =
        AvaloniaProperty.Register<CanvasLayoutUpdatedBehavior, ICommand>(nameof(Command));

    public ICommand Command
    {
        get => GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public object Execute(object? sender, object? parameter)
    {
        if (Command == null || sender is not Canvas canvas) return false;
        Command.Execute(canvas);
        return true;
    }
}