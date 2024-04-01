using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.ViewModel;

public partial class ScanElementViewModel : BaseViewModel
{
    public ObservableCollection<Titan> myObservableTitans { get; } = new();
    public List<Titan> allOfMyTitans = new List<Titan>();
    public Boolean dejaPosseder = false;
    public ScanElementViewModel()
    {
 
    }

    [RelayCommand]
    private async Task AddElement()
    {
        IsBusy = true;

        JSONServices MyService = new();

        Titan myRandomTitan = RandomTitan();

        foreach (var mytitan in Globals.myTitans) 
        {
            if (mytitan.Id.Equals(myRandomTitan.Id)) 
            {
                dejaPosseder = true;
            }
        }
        if (dejaPosseder == true)
        {
            await Shell.Current.DisplayAlert("Titan deja posseder", "dommage retente ta chance dans une heure !", "OK");
            dejaPosseder = false;
        } 
        else {

            Globals.myTitans.Add(myRandomTitan);
            myObservableTitans.Add(myRandomTitan);
            await MyService.SetTitans();

        }
        IsBusy = false;

    }

    private Titan RandomTitan() 
    {
        Titan Assaillant = new();
        Assaillant.Id = 1;
        Assaillant.Name = "Titan Assaillant";
        Assaillant.Picture = "assaillant.gif";
        Assaillant.Height = "15m";
        Assaillant.Abilities = "Héritage de la mémoire du futur";
        Assaillant.Current_inheritor = "Eren Jaeger";
        Assaillant.Former_inheritor = "Grisha Jaeger / Eren Kruger";
        Assaillant.Allegiance = "Eldien";
        allOfMyTitans.Add(Assaillant);

        Titan Fondateur = new();
        Fondateur.Id = 2;
        Fondateur.Name = "Titan Fondateur";
        Fondateur.Picture = "funder.gif";
        Fondateur.Height = "350m";
        Fondateur.Abilities = "Création de Titans / Contrôle du comportement des Titans / Manipulation de la mémoire et du corps des sujets d'Ymir / Communication télépathique avec les sujets d'Ymir";
        Fondateur.Current_inheritor = "Eren Jaeger";
        Fondateur.Former_inheritor = "Grisha Jaeger / Frieda Reiss / Uri Reiss / Rod and Uri's father / Karl Fritz / Ymir Fritz";
        Fondateur.Allegiance = "Eldien";
        allOfMyTitans.Add(Fondateur);


        Titan Marteau = new();
        Marteau.Id = 3;
        Marteau.Name = "Titan Marteau";
        Marteau.Picture = "marteau.gif";
        Marteau.Height = "~15m";
        Marteau.Abilities = "Durcissement de la structure";
        Marteau.Current_inheritor = "Eren Jaeger";
        Marteau.Former_inheritor = "Lara Tybur";
        Marteau.Allegiance = "Eldien";
        allOfMyTitans.Add(Marteau);

        Titan Colossal = new();
        Colossal.Id = 4;
        Colossal.Name = "Titan Colossal";
        Colossal.Picture = "colossal.gif";
        Colossal.Height = "60 m";
        Colossal.Abilities = "Transformation explosive / Taille et force immenses / Émission de vapeur";
        Colossal.Current_inheritor = "Armin Arlelt";
        Colossal.Former_inheritor = "Bertholdt Hoover";
        Colossal.Allegiance = "Eldien";
        allOfMyTitans.Add(Colossal);

        Titan Cuirasse = new();
        Cuirasse.Id = 5;
        Cuirasse.Name = "Titan Cuirassé";
        Cuirasse.Picture = "armored.gif";
        Cuirasse.Height = "15m";
        Cuirasse.Abilities = "Peau blindée / Durcissement";
        Cuirasse.Current_inheritor = "Reiner Braun";
        Cuirasse.Former_inheritor = "Inconnu";
        Cuirasse.Allegiance = "Mahr";
        allOfMyTitans.Add(Cuirasse);

        Titan Feminin = new();
        Feminin.Id = 6;
        Feminin.Name = "Titan Féminin";
        Feminin.Picture = "female.gif";
        Feminin.Height = "14m";
        Feminin.Abilities = "Polyvalence / Attraction / Cristallisation";
        Feminin.Current_inheritor = "Annie Leonhart";
        Feminin.Former_inheritor = "Inconnu";
        Feminin.Allegiance = "Mahr";
        allOfMyTitans.Add(Feminin);

        Titan Bestial = new();
        Bestial.Id = 7;
        Bestial.Name = "Titan Bestial";
        Bestial.Picture = "bestial.gif";
        Bestial.Height = "17m";
        Bestial.Abilities = "Lancer puissant et précis / endurcissement / peut transformer les sujets d'Ymir en titans qu'il peut contrôler grossièrement (utilisateurs de sang royal uniquement).";
        Bestial.Current_inheritor = "Sieg Jaeger";
        Bestial.Former_inheritor = "Tom Xaver";
        Bestial.Allegiance = "Inconnu";
        allOfMyTitans.Add(Bestial);

        Titan Machoire = new();
        Machoire.Id = 8;
        Machoire.Name = "Titan Mâchoire";
        Machoire.Picture = "jaw.gif";
        Machoire.Height = "5m";
        Machoire.Abilities = "Mâchoires puissantes / Griffes renforcées / Grande vitesse et agilité";
        Machoire.Current_inheritor = "Falco Grice";
        Machoire.Former_inheritor = "Porco Galliard / Ymir / Marcel Galliard";
        Machoire.Allegiance = "Mahr";
        allOfMyTitans.Add(Machoire);

        Titan Charrette = new();
        Charrette.Id = 9;
        Charrette.Name = "Titan Charrette";
        Charrette.Picture = "cart_titan.jpg";
        Charrette.Height = "4m";
        Charrette.Abilities = "Forme quadrupède / Grande endurance / Grande vitesse";
        Charrette.Current_inheritor = "Pieck Finger";
        Charrette.Former_inheritor = "Inconnu";
        Charrette.Allegiance = "Mahr";
        allOfMyTitans.Add(Charrette);



        return allOfMyTitans[GenererNombreAleatoire()];
    }
    public int GenererNombreAleatoire()
    {
        // Créer une instance de la classe Random
        Random random = new Random();

        // Générer un nombre aléatoire entre 0 (inclus) et 9 (exclus) inclusivement
        int nombre = random.Next(0, 9);
        return nombre;

    }

}


