using Microsoft.UI.Xaml.Media;

namespace P42.Uno.Markup;

// XAML theme resources
// The XAML color ramp and theme-dependent brushes
// Light and Dark theme colors
// https://github.com/microsoft/microsoft-ui-xaml/blob/main/dev/CommonStyles/Button_themeresources.xaml
public static class SystemBrushes
{
    #region Accent
    // Light: 0xFFFFFFFF Dark: 0xFF000000
    public static Brush AccentFill
        => ColorExtensions.AppBrush("AccentFillColorDefaultBrush");

    public static Brush AccentFillSecondary
        => ColorExtensions.AppBrush("AccentFillColorSecondaryBrush");

    public static Brush AccentFillTertiary
        => ColorExtensions.AppBrush("AccentFillColorTertiaryBrush");

    public static Brush AccentFillDisabled
        => ColorExtensions.AppBrush("AccentFillColorDisabledBrush");
    #endregion


    #region Text on Accent
    public static Brush TextOnAccentFillPrimary
        => ColorExtensions.AppBrush("TextOnAccentFillColorPrimaryBrush");

    public static Brush TextOnAccentFillSecondary
        => ColorExtensions.AppBrush("TextOnAccentFillColorSecondaryBrush");

    public static Brush TextOnAccentFillDisabled
        => ColorExtensions.AppBrush("TextOnAccentFillColorDisabled");
    #endregion


    #region Control
    public static Brush AccentControlElevationBorder
        => ColorExtensions.AppBrush("AccentControlElevationBorderBrush");

    public static Brush ControlFillTransparent
        => ColorExtensions.AppBrush("ControlFillColorTransparentBrush");

    public static Brush ControlFillDefault
        => ColorExtensions.AppBrush("ControlFillColorDefaultBrush");

    public static Brush ControlFillSecondary
        => ColorExtensions.AppBrush("ControlFillColorSecondaryBrush");

    public static Brush ControlFillTertiary
        => ColorExtensions.AppBrush("ControlFillColorTertiaryBrush");

    public static Brush ControlFillDisabled
        => ColorExtensions.AppBrush("ControlFillColorDisabledBrush");

    public static Brush ControlElevationBorder
        => ColorExtensions.AppBrush("ControlElevationBorderBrush");

    public static Brush ControlStrokeDefault
        => ColorExtensions.AppBrush("ControlStrokeColorDefaultBrush");
    #endregion


    #region Text
    public static Brush TextFillPrimary
        => ColorExtensions.AppBrush("TextFillColorPrimaryBrush");

    public static Brush TextFillSecondary
        => ColorExtensions.AppBrush("TextFillColorSecondaryBrush");

    public static Brush TextFillDisabled
        => ColorExtensions.AppBrush("TextFillColorDisabledBrush");

    public static Brush DefaultTextForeground
        => ColorExtensions.AppBrush("DefaultTextForegroundThemeBrush");
    #endregion


}