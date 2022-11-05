using StagingApp.Domain.EventModels;
using StagingApp.Presentation.ViewModels.ConfigureViewModels;
using StagingApp.Presentation.ViewModels.InfoViewModels;

namespace StagingApp.Presentation.ViewModels;
public sealed class ShellViewModel : Conductor<object>, IHandle<CloseApplicationEvent>
{
	public ShellViewModel()
	{
	}

	public Task HandleAsync(CloseApplicationEvent message, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	//protected override async void OnViewLoaded(object view)
	//{
	//	base.OnViewLoaded(view);
	//	var viewModel = IoC.Get<KitchenInfoViewModel>();
	//	await ActivateItemAsync(viewModel, new CancellationToken());
	//}
}
