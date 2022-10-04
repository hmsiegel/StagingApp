namespace StagingApp.Presentation.ViewModels.Common
{
    public partial class DeviceConfigureTextRowViewModel : BaseViewModel
    {
		private string? _configureLabelText;

		public string? ConfigureLabelText
		{
			get => _configureLabelText;
			set
			{
				_configureLabelText = value;
				OnPropertyChanged();
			}
		}

		private string? _configureTextBox;

		public string? ConfigureTextBox
		{
			get => _configureTextBox;
			set
			{
				_configureTextBox = value;
				OnPropertyChanged();
			}
		}

		private bool? _bool = null;

		public bool? IsValid
		{
			get => _bool;
			set
			{
				_bool = value;
				OnPropertyChanged();
			}
		}

		public void CheckValidation()
		{
			IsValid = true;
		}

	}
}
