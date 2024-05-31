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
    public async Task afficheFromDb()
    {
        IsBusy = true;
        try
        {
            var titans = await MyDBService.Titans.Where(e => e.IdUser == Globals.idUserConected).ToListAsync();
            foreach (var titan in titans)
            {
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
        if (selectedAttributes.Count == 0)
        {
            string[] defaultAttributes = { "Name", "Height", "Abilities", "Current_inheritor", "Former_inheritor", "Allegiance" };
            selectedAttributes.AddRange(defaultAttributes);
        }

        IsBusy = true;
        try
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Select a folder",
            });

            if (result != null)
            {
                string folderPath = result.FullPath;

                csvExportServices.ExportToCSV(myObservableTitans, folderPath, selectedAttributes);
                await Shell.Current.DisplayAlert("Export en fichier CSV", "Exportation réussie !", "OK");
            }
            else
            {
                await Shell.Current.DisplayAlert("Erreur", "Aucun dossier sélectionné", "OK");
            }
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlert("Erreur", "Échec de l'exportation CSV : " + e.Message, "OK");
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


    [ObservableProperty]
    private bool isNameChecked;

    partial void OnIsNameCheckedChanged(bool value)
    {
        if (value)
        {
            selectedAttributes.Add("Name");
        }
        else
        {
            selectedAttributes.Remove("Name");
        }
    }

    [ObservableProperty]
    private bool isAbilitiesChecked;

    partial void OnIsAbilitiesCheckedChanged(bool value)
    {
        if (value)
        {
            selectedAttributes.Add("Abilities");
        }
        else
        {
            selectedAttributes.Remove("Abilities");
        }
    }

    [ObservableProperty]
    private bool isCurrentInheritorChecked;

    partial void OnIsCurrentInheritorCheckedChanged(bool value)
    {
        if (value)
        {
            selectedAttributes.Add("Current_inheritor");
        }
        else
        {
            selectedAttributes.Remove("Current_inheritor");
        }
    }

    [ObservableProperty]
    private bool isHeightChecked;

    partial void OnIsHeightCheckedChanged(bool value)
    {
        if (value)
        {
            selectedAttributes.Add("Height");
        }
        else
        {
            selectedAttributes.Remove("Height");
        }
    }

    [ObservableProperty]
    private bool isFormerInheritorChecked;

    partial void OnIsFormerInheritorCheckedChanged(bool value)
    {
        if (value)
        {
            selectedAttributes.Add("Former_inheritor");
        }
        else
        {
            selectedAttributes.Remove("Former_inheritor");
        }
    }

    [ObservableProperty]
    private bool isAllegianceChecked;

    partial void OnIsAllegianceCheckedChanged(bool value)
    {
        if (value)
        {
            selectedAttributes.Add("Allegiance");
        }
        else
        {
            selectedAttributes.Remove("Allegiance");
        }
    }
}
