namespace StagingApp.Main.Views;
/// <summary>
/// Interaction logic for ShellView.xaml
/// </summary>
public partial class ShellView : Window
{
    //private readonly bool _allowClosing = false;

    //[DllImport("user32.dll")]
    //private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

    //[DllImport("user32.dll")]
    //private static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

    //private const uint _mF_BYCOMMAND = 0x00000000;
    //private const uint _mF_GRAYED = 0x00000001;

    //private const uint _sC_CLOSE = 0xF060;

    //private const int _wM_SHOWWINDOW = 0x00000018;
    //private const int _wM_CLOSE = 0x0010;

    public ShellView()
    {
        InitializeComponent();
    }

    //protected override void OnSourceInitialized(EventArgs e)
    //{
    //    base.OnSourceInitialized(e);

    //    if (PresentationSource.FromVisual(this) is HwndSource hwndSource)
    //    {
    //        hwndSource.AddHook(HwndSourceHook);
    //    }
    //}

    //private IntPtr HwndSourceHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    //{
    //    switch (msg)
    //    {
    //        case _wM_SHOWWINDOW:
    //            IntPtr hMenu = GetSystemMenu(hwnd, false);
    //            if (hMenu != IntPtr.Zero)
    //            {
    //                EnableMenuItem(hMenu, _sC_CLOSE, _mF_BYCOMMAND | _mF_GRAYED);
    //            }
    //            break;
    //        case _wM_CLOSE:
    //            if (!_allowClosing)
    //            {
    //                handled = true;
    //            }
    //            break;
    //    }
    //    return IntPtr.Zero;
    //}
}
