using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FashionHub.Commands;
using FashionHub.ViewModels;
using FashionHub.Models;
using FashionHub.Services;
using NavigationService = FashionHub.Services.NavigationService;



namespace FashionHub.Views
{
  /// <summary>
  /// Логика взаимодействия для MainWindow.xaml
  /// </summary>
  public partial class FavoritesPage : Page
  {
    public FavoritesPage()
    {
      InitializeComponent();
      LoadData();
    }

    private void LoadData()
    {
      var repository = ServiceLocator.GetService<IClothingRepository>();
      var navigationService = ServiceLocator.NavigationService;
      DataContext = new FavoritesPageViewModel(repository, navigationService);
    }
  }

}

namespace FashionHub.ViewModels
{
  public partial class FavoritesPageViewModel : RecomendationViewModel
  {
    public FavoritesPageViewModel(IClothingRepository repository, INavigationService navigationService)
        : base(repository, navigationService)
    {
    }

    public FavoritesPageViewModel() : base() { }
  }
}
