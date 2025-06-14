using FashionHub.Commands;
using FashionHub.Models;
using FashionHub.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO.Packaging;
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
using FashionHub.ViewModels;
using FashionHub.Views;
using FashionHub;
using NavigationService = FashionHub.Services.NavigationService;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace FashionHub.Views
{
  /// <summary>
  /// Логика взаимодействия для AddingProductPage.xaml
  /// </summary>
  public partial class AddingProductPage : Page
  {
    public AddingProductPage(ClothingItem item)
    {
      InitializeComponent();
      LoadData(item);
    }

    public AddingProductPage()
    {
      InitializeComponent();
      LoadData(null);
    }

    private void LoadData(ClothingItem item)
    {

      var repository = ServiceLocator.GetService<IClothingRepository>(); 
      var navigationService = ServiceLocator.NavigationService;
      if (item != null)
      {
        DataContext = new AddingProductPageViewModel(repository, navigationService, item);
      }
      else
      {
        DataContext = new AddingProductPageViewModel(repository, navigationService);
      }
    }

    private void ImageBorder_DragOver(object sender, DragEventArgs e)
    {
      if (e.Data.GetDataPresent(DataFormats.FileDrop))
      {
        string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
        e.Effects = files.Any(f => IsImageFile(f)) ? DragDropEffects.Copy : DragDropEffects.None;
      }
      else
      {
        e.Effects = DragDropEffects.None;
      }

      e.Handled = true;
    }

    private void ImageBorder_Drop(object sender, DragEventArgs e)
    {
      if (e.Data.GetDataPresent(DataFormats.FileDrop))
      {
        string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
        var imageFiles = files.Where(IsImageFile).ToList();

        if (DataContext is AddingProductPageViewModel vm)
        {
          foreach (var file in imageFiles)
          {
            if (vm.ClothingItem.ImagePaths.Count < 4 && !vm.ClothingItem.ImagePaths.Contains(file))
            {
              vm.ClothingItem.ImagePaths.Add(file);
            }
          }
        }
      }
    }

    private bool IsImageFile(string path)
    {
      string extension = System.IO.Path.GetExtension(path).ToLower();
      return extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".bmp" || extension == ".gif";
    }
  }
  }

namespace FashionHub.ViewModels
{
  public class AddingProductPageViewModel : NavigationBarViewModel
  {
    private readonly PageState pageState;

    private ClothingItem clothingItem;
    public ClothingItem ClothingItem
    {
      get => clothingItem;
      set
      {
        clothingItem = value;
        OnPropertyChanged(nameof(ClothingItem));
      }
    }


    private string buttonText;
    public string ButtonText
    {
      get => buttonText;
      set
      {
        buttonText = value;
        OnPropertyChanged(nameof(ButtonText));
      }
    }

    public ObservableCollection<string> Categories { get; set; }

    private string _selectedCategory;
    public string SelectedCategory
    {
      get => _selectedCategory;
      set
      {
        _selectedCategory = value;
        OnPropertyChanged(nameof(Categories));
      }
    }

    private string titleText;
    public string TitleText
    {
      get => titleText;
      set
      {
        titleText = value;
        OnPropertyChanged(nameof(TitleText));
      }
    }

    public Dictionary<string, string> ClothingItemErrors { get; } = new Dictionary<string, string>();

    public ICommand AddCommand { get; }
    public ICommand AddImageCommand { get; }
    public ICommand FinishCommand { get; }
    public ICommand RemoveImageCommand { get; }


    public AddingProductPageViewModel(IClothingRepository repository ,INavigationService navigationService, ClothingItem item) : base(repository, navigationService) 
    {
      FinishCommand = new RelayCommand(FinishProduct);
      AddImageCommand = new RelayCommand(AddImage);
      RemoveImageCommand = new RelayCommand(RemoveImage);

      Categories = new ObservableCollection<string>
{
    CategoryFilterService.AllCategory,
    CategoryFilterService.ManCategory,
    CategoryFilterService.WomanCategory,
    CategoryFilterService.KidsCategory
};

      ButtonText = "Изменить товар";
      TitleText = "Редактирование товара";
      clothingItem = item;
      pageState = PageState.EDITING;
      SelectedCategory = Categories.FirstOrDefault(c => c == item.Category);
    }

    public AddingProductPageViewModel(IClothingRepository repository, INavigationService navigationService) : base(repository, navigationService)
    {
      AddImageCommand = new RelayCommand(AddImage);
      FinishCommand = new RelayCommand(FinishProduct);
      RemoveImageCommand = new RelayCommand(RemoveImage);

      Categories = new ObservableCollection<string>
{
    CategoryFilterService.AllCategory,
    CategoryFilterService.ManCategory,
    CategoryFilterService.WomanCategory,
    CategoryFilterService.KidsCategory
};

      ButtonText = "Добавить товар";
      TitleText = "Добавление товара";
      clothingItem = new ClothingItem();
      pageState = PageState.ADDITION;
    }

    public AddingProductPageViewModel() : base() { }

    private enum PageState
    {
      EDITING,
      ADDITION,
    }


    private void RemoveImage(object parameter)
    {
      if (parameter is string path && ClothingItem.ImagePaths.Contains(path))
      {
        ClothingItem.ImagePaths.Remove(path);
      }
    }


    private async void FinishProduct(object parameter)
    {
      if (pageState == PageState.ADDITION)
      {
        await AddProduct();
      }
      else if (pageState == PageState.EDITING)
      {
        await EditProduct();
      }
    }

    private async Task AddProduct()
    {
      try
      {
        ClearValidationErrors();

        var context = new ValidationContext(ClothingItem);
        var validationResults = new List<ValidationResult>();

        ClothingItem.Category = SelectedCategory;

        if (!Validator.TryValidateObject(ClothingItem, context, validationResults, true))
        {
          SetValidationErrors(validationResults);
          return;
        }

        // Выполнить команду добавления
        var command = new AddProductCommand(_repository, ClothingItem);
        await ServiceLocator.UndoRedoManager.ExecuteCommandAsync(command);

        CustomMessageBox.Show("Успех", "Товар успешно добавлен!");
        _navigationService.NavigateTo(typeof(AdminProductsPage), null);
      }
      catch (Exception ex)
      {
        CustomMessageBox.Show("Ошибка", $"Произошла ошибка при добавлении товара: {ex.Message}");
      }
    }


    private async Task EditProduct()
    {
      try
      {
        ClearValidationErrors();

        var context = new ValidationContext(ClothingItem);
        var validationResults = new List<ValidationResult>();

        ClothingItem.Category = SelectedCategory;

        if (!Validator.TryValidateObject(ClothingItem, context, validationResults, true))
        {
          SetValidationErrors(validationResults);
          return;
        }

        // Получаем копию старого товара
        var oldItem = _repository.GetClothingItemById(ClothingItem.ProductId);

        // Выполнить команду обновления
        var command = new EditProductCommand(_repository, oldItem, ClothingItem);
        await ServiceLocator.UndoRedoManager.ExecuteCommandAsync(command);

        CustomMessageBox.Show("Успех", "Товар успешно обновлён!");
        _navigationService.NavigateTo(typeof(AdminProductsPage), null);
      }
      catch (Exception ex)
      {
        CustomMessageBox.Show("Ошибка", $"Произошла ошибка при обновлении товара: {ex.Message}");
      }
    }





    private void AddImage(object parameter)
    {
      var openFileDialog = new OpenFileDialog
      {
        Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif",
        Title = "Выберите изображения товара",
        Multiselect = true
      };

      if (openFileDialog.ShowDialog() == true)
      {
        foreach (var path in openFileDialog.FileNames)
        {
          if (ClothingItem.ImagePaths.Count <= 4)
          {
            ClothingItem.ImagePaths.Add(path);
          }
          else
          {
            CustomMessageBox.Show("Лимит изображений", "Можно добавить не более 4 изображений.");
            break;
          }
        }
      }
    }





    private void ClearValidationErrors()
    {
      ClothingItemErrors.Clear();
      OnPropertyChanged(nameof(ClothingItemErrors));
    }

    private void SetValidationErrors(List<ValidationResult> results)
    {
      foreach (var error in results)
      {
        foreach (var member in error.MemberNames)
        {
          ClothingItemErrors[member] = error.ErrorMessage;
        }
      }
      OnPropertyChanged(nameof(ClothingItemErrors));
    }



  }
}
