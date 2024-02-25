using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Xaml.Interactivity;

namespace CloudSculpt.Behaviors;

public class SelectionChangedCommandBehavior : AvaloniaObject, IAction
{
    public static readonly StyledProperty<ICommand> CommandProperty =
        AvaloniaProperty.Register<SelectionChangedCommandBehavior, ICommand>(nameof(Command));

    public ICommand Command
    {
        get => GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public object Execute(object? sender, object? parameter)
    {
        
        if (parameter is not SelectionChangedEventArgs ||Command == null) return false;
        Command.Execute(sender);
        return true;
    }
}