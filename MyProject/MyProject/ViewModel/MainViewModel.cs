using MyProject.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyProject.ViewModel;

public partial class MainViewModel : BaseViewModel

{
    [ObservableProperty]
    DeviceOrientationService myScanner;

    [ObservableProperty]
    string? codeBar;
    

    public MainViewModel()
    {
        this.myScanner = new();
        myScanner.ConfigureScanner();
        myScanner.SerialBuffer.Changed += OnBarCodeScanned;
    }
    private void OnBarCodeScanned(object sender, EventArgs arg)
    {
        DeviceOrientationService.QueueBuffer MyLocalBuffer = (DeviceOrientationService.QueueBuffer)sender;

        CodeBar = MyLocalBuffer.Dequeue().ToString();
    }

    [RelayCommand]
    private async Task GoToScanPage()
    {
        IsBusy = true;
        //REDIRECTION VERS UNE NOUVELLE PAGE A PARTIR DU BUTTON
        await Shell.Current.GoToAsync("ScanElement", true);
        IsBusy = false;
    }

    [RelayCommand]
    private async Task GoToCollectionPage()
    {
        IsBusy = true;
        //REDIRECTION VERS UNE NOUVELLE PAGE A PARTIR DU BUTTON
        await Shell.Current.GoToAsync("MyCollectionPage", true);
        IsBusy = false;
    }
    [RelayCommand]
    private async Task GoToConnectPage()
    {
        IsBusy = true;
        //REDIRECTION VERS UNE NOUVELLE PAGE A PARTIR DU BUTTON
        await Shell.Current.GoToAsync("ConnectPage", true);
        IsBusy = false;
    }
    [RelayCommand]
    private async Task GoToRegisterPage()
    {
        IsBusy = true;
        //REDIRECTION VERS UNE NOUVELLE PAGE A PARTIR DU BUTTON
        await Shell.Current.GoToAsync("RegisterPage", true);
        IsBusy = false;
    }


    [RelayCommand]
    private async Task LoadJSON()
    {
        IsBusy = true;
        JSONServices MyService = new();

        await MyService.GetTitans();

        IsBusy = false;
    }


}