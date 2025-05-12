using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Metadata;
using Avalonia.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mimeo.DynamicUI.Avalonia.Controls
{
    public class TranslatedLabel : Label
    {
        protected override void OnInitialized()
        {
            base.OnInitialized();

            var serviceProvider = (IServiceProvider)this.FindResource(typeof(IServiceProvider))!;
            stringLocalizer = serviceProvider.GetRequiredService<IStringLocalizer>();
            Translate();
        }

        private IStringLocalizer? stringLocalizer;

        protected override Type StyleKeyOverride => typeof(Label);

        public static readonly DirectProperty<TranslatedLabel, string?> LanguageKeyProperty =
            AvaloniaProperty.RegisterDirect<TranslatedLabel, string?>(
                nameof(LanguageKey),
                o => o.LanguageKey,
                (o, v) => o.LanguageKey = v);

        public static readonly DirectProperty<TranslatedLabel, bool> HideIfUndefinedProperty =
            AvaloniaProperty.RegisterDirect<TranslatedLabel, bool>(
                nameof(HideIfUndefined),
                o => o.HideIfUndefined,
                (o, v) => o.HideIfUndefined = v);

        public string? LanguageKey
        {
            get { return _languageKey; }
            set 
            { 
                _languageKey = value;
                Translate();
            }
        }
        private string? _languageKey;

        private void Translate()
        {
            if (stringLocalizer != null && LanguageKey is string stringValue)
            {
                Content = stringLocalizer[stringValue];
                if (HideIfUndefined && stringLocalizer[stringValue] == stringValue)
                {
                    IsVisible = false;
                }
                else
                {
                    IsVisible = true;
                }
            }
        }

        public bool HideIfUndefined
        {
            get { return _hideIfUndefined; }
            set { SetAndRaise(HideIfUndefinedProperty, ref _hideIfUndefined, value); }
        }

        private bool _hideIfUndefined;
    }
}
