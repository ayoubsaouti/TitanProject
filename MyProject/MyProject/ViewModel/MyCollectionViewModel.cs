using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SkiaSharp;

namespace MyProject.ViewModel;


public partial class MyCollectionViewModel : BaseViewModel
{
    public ObservableCollection<Titan> myObservableTitans { get; } = new();
    private CSVExportServices csvExportServices;
    private List<string> selectedAttributes;
    private bool _isNameChecked;
    private bool _isAbilitiesChecked;
    private bool _isCurrentInheritorChecked;
    private bool _isHeightChecked;
    private bool _isFormerInheritorChecked;
    private bool _isAllegianceChecked;

    DataAccessService MyDBService;



    public MyCollectionViewModel(DataAccessService MyDBService) {

        /* AFFICHAGE POUR LE JSON 
        foreach (var titan in Globals.myTitans)
        {
            myObservableTitans.Add(titan);
        } */
        this.MyDBService = MyDBService;
        afficheFromDb();
        csvExportServices = new CSVExportServices();
        selectedAttributes = new List<string>();

        
    }
    public async Task afficheFromDb() {
        IsBusy = true;

        try
        {
            // Récupere les titans qui apparatien a l'utilisateur
            var titans = MyDBService.Titans.Where(e => e.IdUser == Globals.idUserConected).ToList();

            foreach (var titan in titans)
            {
                // Ajout dans la liste observable
                myObservableTitans.Add(titan);              
            }
            
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Erreur", "Une erreur est survenue lors de la tentative de connexion", "OK");
        }
    

    IsBusy = false;
    }


    [RelayCommand]
    private async Task ExportToCSV()
    {
        if (selectedAttributes.Count == 0) {
            string[] defaultAttributes = ["Name", "Height", "Abilities", "Current_inheritor", "Former_inheritor", "Allegiance"];
            selectedAttributes.AddRange(defaultAttributes);
        }

        IsBusy = true;
        try
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var filePath = Path.Combine(desktopPath, "CSVTitan.csv");

            //LOGIQUE DEXPORT
            csvExportServices.ExportToCSV(myObservableTitans, filePath, selectedAttributes);

            //MESSAGE DAFFICHAGE
            await Shell.Current.DisplayAlert("Export en fichier CSV ", "Exportation reussis !", "OK");
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlert("Export en fichier CSV ", "Echec de l'exportation : " + e, "OK");
        }

        IsBusy = false;
        selectedAttributes.Clear();
        UncheckAll();
    }

    private void UncheckAll(){
        IsNameChecked = false;
        IsAbilitiesChecked = false;
        IsCurrentInheritorChecked = false;
        IsHeightChecked = false;
        IsFormerInheritorChecked = false;
        IsAllegianceChecked = false;
    }

    
    public bool IsNameChecked
    {
        get { return _isNameChecked; }
        set
        {
            if (_isNameChecked != value)
            {
                _isNameChecked = value;
                OnPropertyChanged(nameof(IsNameChecked));

                // Ajoutez ou supprimez la chaîne associée en fonction de l'état de la case à cocher
                if (_isNameChecked)
                {
                    selectedAttributes.Add("Name"); // Ajoutez "Name" à votre liste
                }
                else
                {
                    selectedAttributes.Remove("Name"); // Supprimez "Name" de votre liste
                }
            }
        }
    }
    public bool IsAbilitiesChecked
    {
        get { return _isAbilitiesChecked; }
        set
        {
            if (_isAbilitiesChecked != value)
            {
                _isAbilitiesChecked = value;
                OnPropertyChanged(nameof(IsAbilitiesChecked));

                // Ajoutez ou supprimez la chaîne associée en fonction de l'état de la case à cocher
                if (_isAbilitiesChecked)
                {
                    selectedAttributes.Add("Abilities"); // Ajoutez "Abilities" à votre liste
                }
                else
                {
                    selectedAttributes.Remove("Abilities"); // Supprimez "Abilities" de votre liste
                }
            }
        }
    }
    public bool IsCurrentInheritorChecked
    {
        get { return _isCurrentInheritorChecked; }
        set
        {
            if (_isCurrentInheritorChecked != value)
            {
                _isCurrentInheritorChecked = value;
                OnPropertyChanged(nameof(IsCurrentInheritorChecked));

                // Ajoutez ou supprimez la chaîne associée en fonction de l'état de la case à cocher
                if (_isCurrentInheritorChecked)
                {
                    selectedAttributes.Add("Current_inheritor"); // Ajoutez "Current_inheritor" à votre liste
                }
                else
                {
                    selectedAttributes.Remove("Current_inheritor"); // Supprimez "Current_inheritor" de votre liste
                }
            }
        }
    }
    public bool IsHeightChecked
    {
        get { return _isHeightChecked; }
        set
        {
            if (_isHeightChecked != value)
            {
                _isHeightChecked = value;
                OnPropertyChanged(nameof(IsHeightChecked));

                // Ajoutez ou supprimez la chaîne associée en fonction de l'état de la case à cocher
                if (_isHeightChecked)
                {
                    selectedAttributes.Add("Height"); // Ajoutez "Height" à votre liste
                }
                else
                {
                    selectedAttributes.Remove("Height"); // Supprimez "Height" de votre liste
                }
            }
        }
    }
    public bool IsFormerInheritorChecked
    {
        get { return _isFormerInheritorChecked; }
        set
        {
            if (_isFormerInheritorChecked != value)
            {
                _isFormerInheritorChecked = value;
                OnPropertyChanged(nameof(IsFormerInheritorChecked));

                // Ajoutez ou supprimez la chaîne associée en fonction de l'état de la case à cocher
                if (_isFormerInheritorChecked)
                {
                    selectedAttributes.Add("Former_inheritor"); // Ajoutez "Former_inheritor" à votre liste
                }
                else
                {
                    selectedAttributes.Remove("Former_inheritor"); // Supprimez "Former_inheritor" de votre liste
                }
            }
        }
    }
    public bool IsAllegianceChecked
    {
        get { return _isAllegianceChecked; }
        set
        {
            if (_isAllegianceChecked != value)
            {
                _isAllegianceChecked = value;
                OnPropertyChanged(nameof(IsAllegianceChecked));

                // Ajoutez ou supprimez la chaîne associée en fonction de l'état de la case à cocher
                if (_isAllegianceChecked)
                {
                    selectedAttributes.Add("Allegiance"); // Ajoutez "Allegiance" à votre liste
                }
                else
                {
                    selectedAttributes.Remove("Allegiance"); // Supprimez "Allegiance" de votre liste
                }
            }
        }
    }
}
