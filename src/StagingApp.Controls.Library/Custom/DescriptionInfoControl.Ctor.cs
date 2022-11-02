namespace StagingApp.Controls.Library.Custom;
public partial class DescriptionInfoControl : Control
{
	public DescriptionInfoControl()
	{
		ProtectedIsReadOnly = IsReadOnly;

		// Initializing a Routed Commands Binding
		CommandBinding editCommand = new() { Command = Edit };
		editCommand.CanExecute += OnCanExecute;
		editCommand.Executed += OnExecuted;

		CommandBinding cancelCommand = new() { Command = Cancel };
		cancelCommand.CanExecute += OnCanExecute;
		cancelCommand.Executed += OnExecuted;

		CommandBinding okCommand = new() { Command = OK };
		okCommand.CanExecute += OnCanExecute;
		okCommand.Executed += OnExecuted;

		// Save a Routed Commands Binding
		CommandBindings.Add(editCommand);
		CommandBindings.Add(cancelCommand);
		CommandBindings.Add(okCommand);
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
}
