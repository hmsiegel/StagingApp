using System.Windows.Input;

namespace StagingApp.Controls.Library.Custom;

public partial class DescriptionControl : Control
{
    /// <summary>Sets the DescriptionControl to edit mode: <see cref="DescriptionControl.IsReadOnly"/> = <see langword="false"/>.</summary>
    public static RoutedUICommand Edit { get; } = new RoutedUICommand("Go to edit mode.", nameof(Edit), typeof(DescriptionControl));

    /// <summary>Updates the value of the source property <see cref="DescriptionDto.Property"/> with the value received
    /// from the input field <see cref="DescriptionDto.NewValue"/>.</summary>
    public static RoutedUICommand OK { get; } = new RoutedUICommand("Accept changes.", nameof(OK), typeof(DescriptionControl));

    /// <summary>Returns the value of the input field <see cref="DescriptionDto.NewValue"/>
    /// to the value of the source property <see cref="DescriptionDto.Property"/>
    /// and cancels the edit mode: <see cref="DescriptionControl.IsReadOnly"/> = <see langword="true"/>.</summary>
    public static RoutedUICommand Cancel { get; } = new RoutedUICommand("Undo changes and edit mode.", nameof(Cancel), typeof(DescriptionControl));
}
