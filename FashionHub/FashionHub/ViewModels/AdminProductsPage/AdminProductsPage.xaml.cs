using FashionHub.Commands;
using FashionHub.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using FashionHub.Views;
using FashionHub.ViewModels;
using FashionHub.Services;
using FashionHub;
using NavigationService = FashionHub.Services.NavigationService;

namespace FashionHub.Views
{
  /// <summary>
  /// Логика взаимодействия для AdminPage.xaml
  /// </summary>
  public partial class AdminProductsPage : Page
  {
    public AdminProductsPage()
    {
      InitializeComponent();
      LoadData();
      Loaded += Page_Loaded;
    }

    private void LoadData()
    {
      var repository = ServiceLocator.GetService<IClothingRepository>();
      var navigationService = ServiceLocator.NavigationService;
      DataContext = new AdminProductsPageViewModel(repository, navigationService);
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
      if (DataContext is AdminProductsPageViewModel vm)
        await vm.InitializeAsync();
    }

  }
}


namespace FashionHub.ViewModels
{
  public class AdminProductsPageViewModel : NavigationBarViewModel
  {
    private readonly UndoRedoManager _undoRedoManager;

    public ICommand EditCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand AddProductCommand { get; }
    public ICommand AddCommentCommand { get; }

    public ICommand UndoCommand { get; }
    public ICommand RedoCommand { get; }


    public AdminProductsPageViewModel(IClothingRepository repository, INavigationService navigationService) : base(repository, navigationService)
    {
      _undoRedoManager = ServiceLocator.UndoRedoManager;

      ClothingItems = new ObservableCollection<ClothingItem>();

      EditCommand = new RelayCommand(EditItem);
      DeleteCommand = new RelayCommand(DeleteItem);
      AddProductCommand = new RelayCommand(AddProduct);
      AddCommentCommand = new RelayCommand(AddComment);

      UndoCommand = new RelayCommand(async _ =>
      {
        await _undoRedoManager.UndoAsync();
        await ReloadClothingItemsAsync();
      });

      RedoCommand = new RelayCommand(async _ =>
      {
        await _undoRedoManager.RedoAsync();
        await ReloadClothingItemsAsync();
      });


    }

    public AdminProductsPageViewModel() : base() { }

    private async void DeleteItem(object parameter)
    {
      if (parameter is ClothingItem item)
      {
        var result = CustomMessageBox.Show("Подтверждение", "Вы уверены, что хотите удалить этот товар?", true);
        if (result == true)
        {
          // Выполнить команду через UndoRedoManager
          var command = new DeleteProductCommand(_repository, item);
          await ServiceLocator.UndoRedoManager.ExecuteCommandAsync(command);

          // Обновить коллекцию в UI
          var items = await _repository.GetClothingItemsAsync();
          ClothingItems = new ObservableCollection<ClothingItem>(items);
          OnPropertyChanged(nameof(ClothingItems));
        }
      }
    }


    public async Task InitializeAsync()
    {
      var items = await _repository.GetClothingItemsAsync();
      ClothingItems = new ObservableCollection<ClothingItem>(items);
      OnPropertyChanged(nameof(ClothingItems));
      await ReloadClothingItemsAsync();
    }


    private void EditItem(object parameter)
    {
      if (parameter is ClothingItem)
      {
        IsCatalogPage = false;
        _navigationService.NavigateTo(typeof(AddingProductPage), parameter);
      }
    }


    private void AddProduct(object parameter)
    {
      IsCatalogPage = false;
      _navigationService.NavigateTo(typeof(AddingProductPage), null);
    }

    private void AddComment(object parameter)
    {
      if (parameter is ClothingItem)
      {
        IsCatalogPage = false;
        _navigationService.NavigateTo(typeof(AddingCommentPage), parameter);
      }
    }

    private async Task ReloadClothingItemsAsync()
    {
      var items = await _repository.GetClothingItemsAsync();
      ClothingItems = new ObservableCollection<ClothingItem>(items);
      OnPropertyChanged(nameof(ClothingItems));
    }

  }
}

namespace FashionHub
{
  public interface IUndoableCommand
  {
    Task ExecuteAsync();
    Task UndoAsync();
  }

  public class UndoRedoManager
  {
    private readonly Stack<IUndoableCommand> undoStack = new Stack<IUndoableCommand>();
    private readonly Stack<IUndoableCommand> redoStack = new Stack<IUndoableCommand>();

    public async Task ExecuteCommandAsync(IUndoableCommand command)
    {
      await command.ExecuteAsync();
      undoStack.Push(command);
      redoStack.Clear();
    }

    public async Task UndoAsync()
    {
      if (undoStack.Any())
      {
        var command = undoStack.Pop();
        await command.UndoAsync();
        redoStack.Push(command);
      }
    }

    public async Task RedoAsync()
    {
      if (redoStack.Any())
      {
        var command = redoStack.Pop();
        await command.ExecuteAsync();
        undoStack.Push(command);
      }
    }
  }



  public class AddProductCommand : IUndoableCommand
  {
    private readonly IClothingRepository repository;
    private readonly ClothingItem item;

    public AddProductCommand(IClothingRepository repository, ClothingItem item)
    {
      this.repository = repository;
      this.item = item;
    }

    public Task ExecuteAsync()
    {
      repository.AddOrUpdateClothingItemAsync(item);
      return Task.CompletedTask;
    }

    public Task UndoAsync()
    {
      repository.RemoveClothingItemAsync(item.ProductId);
      return Task.CompletedTask;
    }
  }

  public class DeleteProductCommand : IUndoableCommand
  {
    private readonly IClothingRepository repository;
    private readonly ClothingItem item;

    public DeleteProductCommand(IClothingRepository repository, ClothingItem item)
    {
      this.repository = repository;
      this.item = item;
    }

    public Task ExecuteAsync()
    {
      repository.RemoveClothingItemAsync(item.ProductId);
      return Task.CompletedTask;
    }

    public Task UndoAsync()
    {
      repository.AddOrUpdateClothingItemAsync(item);
      return Task.CompletedTask;
    }
  }

  public class EditProductCommand : IUndoableCommand
  {
    private readonly IClothingRepository repository;
    private readonly ClothingItem oldItem;
    private readonly ClothingItem newItem;

    public EditProductCommand(IClothingRepository repository, ClothingItem oldItem, ClothingItem newItem)
    {
      this.repository = repository;
      this.oldItem = oldItem;
      this.newItem = newItem;
    }

    public Task ExecuteAsync()
    {
      repository.AddOrUpdateClothingItemAsync(newItem);
      return Task.CompletedTask;
    }

    public Task UndoAsync()
    {
      repository.AddOrUpdateClothingItemAsync(oldItem);
      return Task.CompletedTask;
    }
  }




}