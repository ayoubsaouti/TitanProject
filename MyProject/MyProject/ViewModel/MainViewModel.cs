using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.ViewModel;

public partial class MainViewModel : BaseViewModel
{
    public ObservableCollection<Titan> myObservableTitans { get; } = new();

    public MainViewModel()
    {


    }

    [RelayCommand]
    private async Task GoToScanPage()
    {
        IsBusy = true;
        //REDIRECTION VERS UNE NOUVELLE PAGE A PARTIR DU BUTTON
        await Shell.Current.GoToAsync("NewPage", true);
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
    private async Task LoadJSON()
    {
        IsBusy = true;
        JSONServices MyService = new();

        await MyService.GetTitans();

        foreach (var titan in Globals.myTitans)
        {
            myObservableTitans.Add(titan);
        }

        IsBusy = false;
    }


}