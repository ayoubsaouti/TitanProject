global using System;

global using CommunityToolkit.Maui;
global using CommunityToolkit.Mvvm.Input;
global using CommunityToolkit.Mvvm.ComponentModel;

global using MyProject.View;
global using MyProject.ViewModel;
global using MyProject.Model;
global using MyProject.Services;

global using Microcharts;
global using Microcharts.Maui;

internal class Globals
{
    public static List<Titan> myTitans = new();
    public static string userConnected ;
    public static string idUserConected;
    public static string portConnected;

}