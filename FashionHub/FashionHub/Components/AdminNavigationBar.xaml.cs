using FashionHub.Services;
using FashionHub.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace FashionHub.Components
{
  /// <summary>
  /// Логика взаимодействия для AdminNavigationBar.xaml
  /// </summary>
  public partial class AdminNavigationBar : UserControl
  {
    public AdminNavigationBar()
    {
      InitializeComponent();
      LoadData();
    }

    private void LoadData()
    {
      var repository = ServiceLocator.GetService<IClothingRepository>(); // Получаем репозиторий
      var navigationService = ServiceLocator.NavigationService;
      DataContext = new AdminNavigationBarViewModel(repository, navigationService);
    }
  }
}

  namespace FashionHub.ViewModels
  {
    public class AdminNavigationBarViewModel : NavigationServiceViewModel
    {
      private readonly IClothingRepository _repository;

      public AdminNavigationBarViewModel(IClothingRepository repository, INavigationService navigationService)
          : base(repository, navigationService)
      {
        _repository = repository;
        CurrentUserService.UserStatusChanged += OnUserStatusChanged;
      }

      public AdminNavigationBarViewModel() : base() { }

      private void OnUserStatusChanged()
      {
        OnPropertyChanged(nameof(IsAdmin));
        OnPropertyChanged(nameof(IsManager));
      }
    }

  }