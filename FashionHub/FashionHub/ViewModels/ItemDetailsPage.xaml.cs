using FashionHub.Commands;
using FashionHub.Models;
using FashionHub.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
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
using System.Windows.Shapes;
using System.Xml.Linq;
using FashionHub.Views;
using FashionHub.Services;


namespace FashionHub.Views
{
  /// <summary>
  /// Логика взаимодействия для Item.xaml
  /// </summary>
  public partial class ItemDetailsPage : Page
  {
    public ObservableCollection<Comment> Comments { get; set; } = new ObservableCollection<Comment>();
    public ItemDetailsPage(ClothingItem item)
    {
      InitializeComponent();
      LoadData(item);
    }

    public ItemDetailsPage() { }


    private void LoadData(ClothingItem item)
    {
      var repository = ServiceLocator.GetService<IClothingRepository>();
      var navigationService = ServiceLocator.NavigationService;
      DataContext = new ItemDetailsPageViewModel(repository,navigationService, item);
    }
  }
}


namespace FashionHub.ViewModels
{
  public class ItemDetailsPageViewModel : NavigationServiceViewModel
  {

    private ClothingItem _currentItem;
    public ClothingItem CurrentItem
    {
      get => _currentItem;
      set
      {
        _currentItem = value;
        OnPropertyChanged(nameof(CurrentItem));
        UpdateAvailableSizes();
        UpdateAvailableColors();
      }
    }

    public ObservableCollection<string> AvailableSizes { get; } = new ObservableCollection<string>();
    public ObservableCollection<string> AvailableColors { get; } = new ObservableCollection<string>();

    public ObservableCollection<Comment> Comments { get; set; }

    private string _selectedSize;
    public string SelectedSize
    {
      get => _selectedSize;
      set
      {
        _selectedSize = value;
        OnPropertyChanged(nameof(SelectedSize));
      }
    }

    private string _selectedColor;
    public string SelectedColor
    {
      get => _selectedColor;
      set
      {
        _selectedColor = value;
        OnPropertyChanged(nameof(SelectedColor));
      }
    }

    public ObservableCollection<ClothingItem> RecentlyViewedItems
    {
      get
      {
        var allItems = RecentlyViewedService.GetRecentlyViewed();

        if (CurrentItem == null)
          return allItems;

        var filteredItems = new ObservableCollection<ClothingItem>(
            allItems.Where(item => item.ProductId != CurrentItem.ProductId)
        );

        return filteredItems;
      }
    }

    public ICommand OpenAddingCommentWindowCommand { get; }
    public ICommand AddToCartCommand { get; }

    public ItemDetailsPageViewModel(IClothingRepository repository, INavigationService navigationService, ClothingItem item) : base(repository, navigationService)
    {
      CurrentItem = item;
      RecentlyViewedService.AddToRecentlyViewed(item);
      Comments = item.Comments != null
          ? new ObservableCollection<Comment>(item.Comments)
          : new ObservableCollection<Comment>();

      OpenAddingCommentWindowCommand = new RelayCommand(OpenAddingCommentWindow);
      AddToCartCommand = new RelayCommand(AddToCart);

    }

    public ItemDetailsPageViewModel() { }


    private void UpdateAvailableSizes()
    {
      AvailableSizes.Clear();

      if (CurrentItem?.Size != null)
      {
        foreach (var size in CurrentItem.Size.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries))
        {
          AvailableSizes.Add(size.Trim());
        }
      }
    }

    private void UpdateAvailableColors()
    {
      AvailableColors.Clear();

      if (CurrentItem?.Color != null)
      {
        foreach (var color in CurrentItem.Color.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries))
        {
          AvailableColors.Add(color.Trim());
        }
      }
    }

    private void AddToCart(object parameter)
    {
      if (CurrentUserService.UserId == null)
      {
        CustomMessageBox.Show("Ошибка", "Для того, чтобы добавить товар в корзину, нужно войти в аккаунт.");
        return;
      }
      if (string.IsNullOrEmpty(SelectedColor) || string.IsNullOrEmpty(SelectedSize))
      {
        CustomMessageBox.Show("Ошибка", "Выберите цвет и размер перед добавлением в корзину.");
        return;
      }

      using (var context = new DataBaseContext())
      {
        var currentUserId = CurrentUserService.UserId;

        var product = context.Products.FirstOrDefault(p => p.ProductId == CurrentItem.ProductId);
        if (product == null)
        {
          CustomMessageBox.Show("Ошибка", "Товар не найден.");
          return;
        }

        if (product.Quantity <= 0)
        {
          CustomMessageBox.Show("Ошибка", "Товара больше нет в наличии.");
          return;
        }

        var cartItem = new CartItem
        {
          ProductId = product.ProductId,
          SelectedColor = SelectedColor,
          SelectedSize = SelectedSize,
          UserId = (int)currentUserId
        };

        context.CartItems.Add(cartItem);

        product.Quantity--;
        _repository.AddOrUpdateClothingItemAsync(product);

        context.SaveChanges();
      }

      CustomMessageBox.Show("Успех!", "Товар добавлен в корзину!");
    }

    private void OpenAddingCommentWindow(object paremeter)
    {
      AddingCommentWindow addingCommentWindow = new AddingCommentWindow(CurrentItem.ProductId);
      addingCommentWindow.ShowDialog();
      LoadComments(CurrentItem.ProductId);
      OnPropertyChanged(nameof(Comments));
    }

    public void LoadComments(int productId)
    {
      Comments.Clear();
      foreach (var comment in CommentService.GetCommentsForItem(productId))
        Comments.Add(comment);
       
    }
  }
}