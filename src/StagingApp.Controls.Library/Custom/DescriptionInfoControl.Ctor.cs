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

		CommandBinding cancelCommand = new() { Command = CancelEdit };
		cancelCommand.CanExecute += OnCanExecute;
		cancelCommand.Executed += OnExecuted;

		CommandBinding okCommand = new() { Command = OkEdit };
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
		else if (e.Command == CancelEdit || e.Command == OkEdit)
		{
			if (e.Command == CancelEdit)
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
		else if (e.Command == CancelEdit || e.Command == OkEdit)
		{
			e.CanExecute = !ProtectedIsReadOnly;
		}
	}
}
