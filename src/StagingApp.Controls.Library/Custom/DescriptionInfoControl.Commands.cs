namespace StagingApp.Controls.Library.Custom;
public partial class DescriptionInfoControl : Control
{
    /// <summary>
    /// Sets the DescriptionInfoControl to edit mode: <see cref="DescriptionInfoControl.IsReadOnly"/> = <see langword="false"/>
    /// </summary>
    public static RoutedUICommand Edit { get; } = new RoutedUICommand("Go to Edit Mode", nameof(Edit), typeof(DescriptionInfoControl));

    /// <summary>
    /// Updates the value of the source property <see cref="DescriptionInfoDto.Property"/> with the value received
    /// from the input field <see cref="DescriptionInfoDto.NewValue"./>
    /// </summary>
    public static RoutedUICommand OK { get; } = new RoutedUICommand("Accept changes.", nameof(OK), typeof(DescriptionInfoControl));

    /// <summary>
    /// Returns the value of the input field <see cref="DescriptionDto.NewValue"/>
    /// to the value of the source property <see cref="DescriptionDto.Property"/>
    /// and cancels the edit mode: <see cref="DescriptionControl.IsReadOnly"/> = <see langword="true"/>.
    /// </summary>    
    public static RoutedUICommand Cancel { get; } = new RoutedUICommand("Undo changes and exit edit mode.", nameof(Cancel), typeof(DescriptionInfoControl));

}
