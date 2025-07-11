using Microsoft.UI.Xaml.Media;

namespace P42.Uno.Markup;

public static class SystemMenuFlyoutBrushes
{
    public static Brush ItemFocusedBackground => ColorExtensions.AppBrush("MenuFlyoutItemFocusedBackgroundThemeBrush");
    public static Brush ItemFocusedForeground => ColorExtensions.AppBrush("MenuFlyoutItemFocusedForegroundThemeBrush");
    public static Brush ItemDisabledForeground => ColorExtensions.AppBrush("MenuFlyoutItemDisabledForegroundThemeBrush");
    public static Brush ItemPointerOverBackground => ColorExtensions.AppBrush("MenuFlyoutItemPointerOverBackgroundThemeBrush");
    public static Brush ItemPointerOverForeground => ColorExtensions.AppBrush("MenuFlyoutItemPointerOverForegroundThemeBrush");
    public static Brush ItemPressedBackground => ColorExtensions.AppBrush("MenuFlyoutItemPressedBackgroundThemeBrush");
    public static Brush ItemPressedForeground => ColorExtensions.AppBrush("MenuFlyoutItemPressedForegroundThemeBrush");
    public static Brush Separator => ColorExtensions.AppBrush("MenuFlyoutSeparatorThemeBrush");
}