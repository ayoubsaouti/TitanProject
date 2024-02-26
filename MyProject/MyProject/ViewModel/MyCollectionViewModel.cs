using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.ViewModel;


public partial class MyCollectionViewModel : BaseViewModel
{
    public ObservableCollection<Titan> myObservableTitans { get; } = new();

    public MyCollectionViewModel(){

        foreach (var titan in Globals.myTitans)
        {
            myObservableTitans.Add(titan);
        }
    }


 
}
