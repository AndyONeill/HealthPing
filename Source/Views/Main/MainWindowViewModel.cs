using CommunityToolkit.Mvvm.ComponentModel;
using HealthPing.Interfaces;
using HealthPing.Views.EditPingList;
using HealthPing.Views.ViewPingList;
using System.Windows;

namespace HealthPing.Views.Main;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    List<ViewVM> views = new List<ViewVM> {
            new ViewVM{ViewModelType=typeof(ViewPingListViewModel), Name="Ping List" },
            new ViewVM{ViewModelType=typeof(EditPingListViewModel), Name="Edit List" }
            };

    [ObservableProperty]
    Object? currentViewModel;

    [ObservableProperty]
    ViewVM? selectedView;

    partial void OnSelectedViewChanged(ViewVM? newView)
    {
        if (newView?.ViewModelType != null)
        {
            CurrentViewModel = Activator.CreateInstance(newView.ViewModelType);
            if (CurrentViewModel != null && CurrentViewModel is IInitialised)
            {
                Application.Current.Dispatcher.InvokeAsync(new Action(async () =>
                {
                    await Task.Run(() => ((IInitialised)CurrentViewModel).Initialise());
                }));
            }
        }
    }

    public MainWindowViewModel() 
    { 
        Application.Current.Dispatcher.InvokeAsync(() => { SelectedView = views[0]; });
    }
}