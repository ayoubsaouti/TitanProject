using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.ViewModel;

public partial class NewViewModel : BaseViewModel
{
    public ObservableCollection<Titan> myObservableTitans { get; } = new();
    public NewViewModel()
    {
 
    }
    [RelayCommand]
    private async Task AddElement()
    {
        IsBusy = true;

        JSONServices MyService = new();

        Titan NewTitan = new();
        NewTitan.Name = "Assailliant";
        NewTitan.Height = "15M";
        NewTitan.Abilities = "A Acces au souvenir des precedents detenteur";
        NewTitan.Picture = "assaillant_eren.jpg";

        Globals.myTitans.Add(NewTitan);
        myObservableTitans.Add(NewTitan);

        await MyService.SetTitans();

        IsBusy = false;
    }

}
