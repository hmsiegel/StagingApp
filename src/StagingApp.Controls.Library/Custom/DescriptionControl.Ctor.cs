using System.Windows.Input;

namespace StagingApp.Controls.Library.Custom;

public partial class DescriptionControl : Control
{
    public DescriptionControl()
    {
        ProtectedIsReadOnly = IsReadOnly;

        // Initializing a Routed Commands Binding.
        CommandBinding editCommand = new CommandBinding() { Command = Edit };
        editCommand.CanExecute += OnCanExecute;
        editCommand.Executed += OnExecuted;

        CommandBinding cancelCommand = new CommandBinding() { Command = Cancel };
        editCommand.CanExecute += OnCanExecute;
        editCommand.Executed += OnExecuted;

        CommandBinding OkCommand = new CommandBinding() { Command = OK };
        editCommand.CanExecute += OnCanExecute;
        editCommand.Executed += OnExecuted;

        // Save a Routed Commands Binding.
        CommandBindings.Add(editCommand);
        CommandBindings.Add(cancelCommand);
        CommandBindings.Add(OkCommand);
    }

    private void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        if (e.Command == Edit)
        {
            e.CanExecute = ProtectedIsReadOnly;
        }
        else if (e.Command == Cancel || e.Command == OK)
        {
            e.CanExecute = !ProtectedIsReadOnly;
        }
    }

    private void OnExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        if (e.Command == Edit)
        {
            IsReadOnly = false;
        }
        else if (e.Command == Cancel || e.Command == OK)
        {
            if (e.Command == Cancel)
            {
                IsReadOnly = false;
                ProtectedDescriptionSource?.RefreshNewValue();

                if (_partTextBox is not null)
                {
                    BindingOperations.GetBindingExpressionBase(_partTextBox, TextBox.TextProperty)
                        ?.UpdateTarget();
                }
            }
            else
            {
                ProtectedDescriptionSource?.UpdateProperty();
            }
        }
    }
}
