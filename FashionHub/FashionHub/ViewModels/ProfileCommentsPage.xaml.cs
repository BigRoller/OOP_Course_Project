using FashionHub.Services;
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
using System.Collections.ObjectModel;
using FashionHub.Models;
using System.Data.Entity;
using FashionHub.ViewModels;

using NavigationService = FashionHub.Services.NavigationService;
using FashionHub.Views;
using FashionHub.Commands;
using static FashionHub.Components.Comment_Item;

namespace FashionHub.Views
{
  /// <summary>
  /// Логика взаимодействия для ProfilePage.xaml
  /// </summary>
  public partial class ProfileCommentsPage : Page
  {
    public ProfileCommentsPage()
    {
      InitializeComponent();
      LoadData();
    }


    private void LoadData()
    {
      var navigationService = ServiceLocator.NavigationService;
      DataContext = new ProfileCommentsPageViewModel(navigationService);
    }
  }
}

namespace FashionHub.ViewModels
{
  public class ProfileCommentsPageViewModel : NavigationBarViewModel
  {
    public ObservableCollection<Comment> UserComments { get; set; }

    public ICommand DeleteCommentCommand { get; }

    public ProfileCommentsPageViewModel(INavigationService navigationService) : base(navigationService) 
    {
      DeleteCommentCommand = new RelayCommand(DeleteComment);
      LoadUserComments();
    }

    public ProfileCommentsPageViewModel() : base() { }

    private void LoadUserComments()
    {
      using (var context = new DataBaseContext())
      {
        var userId = CurrentUserService.UserId;

        var comments = context.Comments
            .Where(c => c.UserId == userId)
            .Include(c => c.ClothingItem)
            .ToList();

        UserComments = new ObservableCollection<Comment>(comments);
      }

      OnPropertyChanged(nameof(UserComments));
    }

    private void DeleteComment(object parameter)
    {
      if (parameter is Comment_ItemViewModel vm)
      {
        if (vm == null)
          return;

        bool? flag = CustomMessageBox.Show("Подтверждение", "Вы действительно хотите удалить этот комментарий?", true);
        if (flag == true)
        {
          using (var context = new DataBaseContext())
          {
            var commentToDelete = context.Comments.Find(vm.Comment.CommentId);
            if (commentToDelete != null)
            {
              context.Comments.Remove(commentToDelete);
              context.SaveChanges();
            }
          }

          UserComments.Remove(vm.Comment);
        }
      }
    }
  }
}
