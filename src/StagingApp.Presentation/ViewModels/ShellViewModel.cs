using StagingApp.Presentation.ViewModels.ConfigureViewModels;

namespace StagingApp.Presentation.ViewModels;
public sealed class ShellViewModel : Conductor<object>
{
	public ShellViewModel()
	{
	}

	protected override async void OnViewLoaded(object view)
	{
		base.OnViewLoaded(view);
		var viewModel = IoC.Get<KitchenConfigureViewModel>();
		await ActivateItemAsync(viewModel, new CancellationToken());
	}
}
