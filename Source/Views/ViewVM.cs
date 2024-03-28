using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthPing.Views;

public partial class ViewVM : ObservableObject
{
    [ObservableProperty]
    string name;

    [ObservableProperty]
    Type viewModelType;
}
