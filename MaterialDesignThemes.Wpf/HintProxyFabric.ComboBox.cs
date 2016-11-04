using System;
using System.Windows;
using System.Windows.Controls;

namespace MaterialDesignThemes.Wpf
{
    public static partial class HintProxyFabric
    {
        private sealed class ComboBoxHintProxy : IHintProxy
        {
            private readonly ComboBox _comboBox;
            private readonly DependencyPropertyListener _textChangedListener;

            public object Content => _comboBox.Text;
            public bool IsLoaded => _comboBox.IsLoaded;
            public bool IsVisible => _comboBox.IsVisible;

            public event EventHandler ContentChanged;
            public event EventHandler IsVisibleChanged;
            public event EventHandler Loaded;

            public ComboBoxHintProxy(ComboBox comboBox)
            {
                if (comboBox == null) throw new ArgumentNullException(nameof(comboBox));

                _comboBox = comboBox;
                _textChangedListener = new DependencyPropertyListener(comboBox, ComboBox.TextProperty, e => ContentChanged?.Invoke(comboBox, EventArgs.Empty));
                _comboBox.Loaded += ComboBoxLoaded;
                _comboBox.IsVisibleChanged += ComboBoxIsVisibleChanged;
            }

            private void ComboBoxIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
            {
                IsVisibleChanged?.Invoke(sender, EventArgs.Empty);
            }

            private void ComboBoxLoaded(object sender, RoutedEventArgs e)
            {
                Loaded?.Invoke(sender, EventArgs.Empty);
            }

            public void Dispose()
            {
                _comboBox.Loaded -= ComboBoxLoaded;
                _comboBox.IsVisibleChanged -= ComboBoxIsVisibleChanged;
                _textChangedListener.Dispose();
            }
        }
    }
}
