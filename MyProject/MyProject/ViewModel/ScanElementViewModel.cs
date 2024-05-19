using MyProject.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyProject.ViewModel;

public partial class ScanElementViewModel : BaseViewModel
{
    public ObservableCollection<Titan> myObservableTitans { get; } = new();
    public List<Titan> allOfMyTitans = new List<Titan>();
    public Boolean dejaPosseder = false;

    [ObservableProperty]
    DeviceOrientationService myScanner;
    private String selectedImageSource;

    [ObservableProperty]
    string? codeBar;


    [ObservableProperty]
    private String id;
    [ObservableProperty]
    private String name;
    [ObservableProperty]
    private String height;
    [ObservableProperty]
    private String abilities;
    [ObservableProperty]
    private String current_inheritor;
    [ObservableProperty]
    private String former_inheritor;
    [ObservableProperty]
    private String allegiance;

    DataAccessService MyDBService;


    public string SelectedImageSource
    {
        get => selectedImageSource;
        set
        {
            selectedImageSource = value;
            OnPropertyChanged(nameof(SelectedImageSource));
        }
    }

    public ICommand SelectImageCommand { get; }
    public ICommand AddTitanCommand { get; set; }


    public ScanElementViewModel(DataAccessService MyDBService)
    {
        this.MyDBService = MyDBService;
        SelectImageCommand = new Command(async () => await SelectImage());

        this.myScanner = new();
        myScanner.ConfigureScanner();
        myScanner.SerialBuffer.Changed += OnBarCodeScanned;
        AddTitanCommand = new Command(async () => await AddElement());

    }

    private void OnBarCodeScanned(object sender, EventArgs arg)
    {
        DeviceOrientationService.QueueBuffer MyLocalBuffer = (DeviceOrientationService.QueueBuffer)sender;

        CodeBar = MyLocalBuffer.Dequeue().ToString();
    }

    private async Task SelectImage()
    {
        try
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Choisir une image"
            });

            if (result != null)
            {
                SelectedImageSource = result.FileName;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }



    private async Task AddElement()
    {

        if (id != null && name != null && selectedImageSource != null && height != null && abilities != null 
            && current_inheritor != null && former_inheritor != null && allegiance != null)
        {
            IsBusy = true;
            JSONServices MyService = new();

            Titan newTitan = new Titan(id, name, selectedImageSource, height, abilities, current_inheritor, former_inheritor, allegiance, Globals.idUserConected);
            
            foreach (var mytitan in Globals.myTitans)
            {
                if (mytitan.IdTitan.Equals(newTitan.IdTitan))
                {
                    dejaPosseder = true;
                }
            }
            if (dejaPosseder == true)
            {
                await Shell.Current.DisplayAlert("Titan deja posseder", "L'id existe déjà dans la collection", "OK");
                dejaPosseder = false;
            }
            else
            {

                Globals.myTitans.Add(newTitan);
                myObservableTitans.Add(newTitan);
                await MyService.SetTitans();
                //ajouter a la db
                try
                {
                    await MyDBService.Titans.AddAsync(newTitan);
                    await MyDBService.SaveChangesAsync();
                    
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Erreur", "Une erreur s'est produite lors de l'ajout du titan à la base de données", "OK");
                }

            }
            IsBusy = false;
        }
        else {
            await Shell.Current.DisplayAlert("Erreur", "Un ou plusieurs champ(s) vide(s)", "OK");

        }

       
    }
}