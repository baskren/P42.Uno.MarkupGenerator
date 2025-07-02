using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace P42.Uno.Markup.Target;
public sealed partial class MyUserControl : UserControl
{
    #region TestBrush Property
    public static readonly DependencyProperty TestBrushProperty = DependencyProperty.Register(
        nameof(TestBrush),
        typeof(Brush),
        typeof(MyUserControl),
        new PropertyMetadata(default(Brush))
    );
    public Brush TestBrush
    {
        get => (Brush)GetValue(TestBrushProperty);
        set => SetValue(TestBrushProperty, value);
    }
    #endregion TestBrush Property



    public MyUserControl()
    {
        this.InitializeComponent();
    }

}
