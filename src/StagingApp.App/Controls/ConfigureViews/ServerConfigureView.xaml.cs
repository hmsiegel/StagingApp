﻿namespace StagingApp.Main.Controls.ConfigureViews;
/// <summary>
/// Interaction logic for ServerConfigureView.xaml
/// </summary>
public partial class ServerConfigureView : UserControl
{
    public ServerConfigureView()
    {
        InitializeComponent();
        DataContext = new ServerConfigureViewModel();
    }
}
