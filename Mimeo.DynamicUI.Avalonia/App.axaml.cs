using Avalonia;
using Avalonia.Markup.Xaml;

namespace Mimeo.DynamicUI.Avalonia;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
