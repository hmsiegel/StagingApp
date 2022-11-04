using StagingApp.Presentation.ViewModels.ConfigureViewModels;
using StagingApp.Presentation.ViewModels.InfoViewModels;

namespace StagingApp.Presentation.ViewModels;
public sealed class ShellViewModel : Conductor<object>
{
	public ShellViewModel()
	{
	}

	protected override async void OnViewLoaded(object view)
	{
		base.OnViewLoaded(view);
		var viewModel = IoC.Get<KitchenInfoViewModel>();
		await ActivateItemAsync(viewModel, new CancellationToken());
	}
}
