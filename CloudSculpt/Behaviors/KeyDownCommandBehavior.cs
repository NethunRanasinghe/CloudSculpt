using System.Windows.Input;
using Avalonia;
using Avalonia.Input;
using Avalonia.Xaml.Interactivity;

namespace CloudSculpt.Behaviors;

public class KeyDownCommandBehavior : AvaloniaObject, IAction
{
    public static readonly StyledProperty<ICommand> CommandProperty =
        AvaloniaProperty.Register<KeyDownCommandBehavior, ICommand>(nameof(Command));

    public ICommand Command
    {
        get => GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
    
    public object? Execute(object? sender, object? parameter)
    {
        if (parameter is not KeyEventArgs e || Command == null) return false;
        if (e.Key != Key.Return) return false;
        Command.Execute(sender);
        return true;
    }
}