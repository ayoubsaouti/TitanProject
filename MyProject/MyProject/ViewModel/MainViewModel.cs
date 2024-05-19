using Azure.Identity;
using Microsoft.IdentityModel.Tokens;
using MyProject.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyProject.ViewModel;

public partial class MainViewModel : BaseViewModel

{

    public MainViewModel()
    {
        
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
    private async Task GoToChart()
    {
        IsBusy = true;
        //REDIRECTION VERS UNE NOUVELLE PAGE A PARTIR DU BUTTON
        await Shell.Current.GoToAsync("RepresentationPage", true);
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