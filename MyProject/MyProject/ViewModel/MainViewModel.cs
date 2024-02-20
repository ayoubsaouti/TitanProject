using IntelliJ.Lang.Annotations;
using System;
using System.Collections.Generic;
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
    private async Task IncButton()
    {
        IsBusy = true;

        JSONServices MyServices = new();

        await MyServices.GetTitans();


        //REDIRECTION VERS UNE NOUVELLE PAGE A PARTIR DU BUTTON
        await Shell.Current.GoToAsync("NewPage", true);

        await MyServices.SetTitans();
        IsBusy = false;
    }
}