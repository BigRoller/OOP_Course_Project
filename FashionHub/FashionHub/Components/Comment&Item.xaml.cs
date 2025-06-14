using System;
using System.Collections.Generic;
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
using FashionHub.Models;
using FashionHub.Services;
using FashionHub.Views;

namespace FashionHub.Components
{
  /// <summary>
  /// Логика взаимодействия для Comment_Item.xaml
  /// </summary>
  public partial class Comment_Item : UserControl
  {
    public Comment_Item()
    {
      InitializeComponent();
    }

    public void OnLoaded(object sender, RoutedEventArgs e)
    {
      if (DataContext is Comment context)
      {
        DataContext = new Comment_ItemViewModel(context);
      }
    }


    public class Comment_ItemViewModel : BaseViewModel
    {
      public Comment Comment { get; set; }
      public ClothingItem Product { get; set; }

      public string ImagePath => Product.ImagePaths.First() ?? "C:\\Users\\glora\\Desktop\\ООП курсовой\\FashionHub\\FashionHub\\Resources\\icons\\no-image.png";

      private bool _isSelected;
      public bool IsSelected
      {
        get => _isSelected;
        set
        {
          _isSelected = value;
          OnPropertyChanged(nameof(IsSelected));
        }
      }

      private bool _isFavorite;
      public bool IsFavorite
      {
        get => _isFavorite;
        set
        {
          _isFavorite = value;
          OnPropertyChanged(nameof(IsFavorite));
        }
      }

      private FavoriteService favoriteService { get; set; }

      public ICommand ToggleFavoriteCommand { get; }


      public Comment_ItemViewModel(Comment comment)
      {
        this.Comment = comment;
        this.Product = comment.ClothingItem;

        favoriteService = new FavoriteService(new DataBaseContext());
        if (favoriteService.IsFavorite(CurrentUserService.UserId ?? 0, Product.ProductId))
        {
          IsFavorite = true;
        }
        ToggleFavoriteCommand = new RelayCommand(ToggleFavorite);
      }

      private void ToggleFavorite(object obj)
      {
        if (Product != null && CurrentUserService.UserId != null && Product.ProductId != 0)
        {
          favoriteService.ToggleFavorite(CurrentUserService.UserId.Value, Product.ProductId);
        }
      }

    }
  }
}
