using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;

namespace Mimeo.DynamicUI.Avalonia.Forms;

public partial class DynamicTabControlEditForm : UserControl
{
    public DynamicTabControlEditForm()
    {
        InitializeComponent();
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        var serviceProvider = (IServiceProvider)this.FindResource(typeof(IServiceProvider))!;
        var stringLocalizer = serviceProvider.GetRequiredService<IStringLocalizer>();
        this.Resources["StringLocalizer"] = new LocalizeConverter(stringLocalizer);
    }
}