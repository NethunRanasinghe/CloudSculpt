using System;

namespace CloudSculpt.Commands;

public class ServiceElementCanvasDeleteCommand : CommandBase
{
    public override void Execute(object? parameter)
    {
        Console.WriteLine("Delete");
    }
}