using StagingApp.Controls.Library.Models;
using System.Windows;

namespace StagingApp.Presentation.ViewModels.Base;
public class BaseViewModel : ObservableObject, INotifyPropertyChanged
{
}

public partial class OkCancelCommandsVM : BaseViewModel
{
    [RelayCommand]
    private void Ok(IReadOnlyList<DescriptionDto> descriptions)
    {
        MessageBox.Show(this + Environment.NewLine +
            string.Join(Environment.NewLine, descriptions.Select(desс => $"{desс.Property.Name}: \"{desс.NewValue}\"")));
    }

    [RelayCommand]
    private void Cancel(Action cancel)
    {
        cancel?.Invoke();
    }

}
