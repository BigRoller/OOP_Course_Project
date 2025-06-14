using FashionHub.Commands;
using FashionHub.Models;
using FashionHub.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Data.Entity;
using FashionHub.Views;
using FashionHub.ViewModels;
using NavigationService = FashionHub.Services.NavigationService;

namespace FashionHub.Views
{
  /// <summary>
  /// Логика взаимодействия для AdminCommentsPage.xaml
  /// </summary>
  public partial class AdminCommentsPage : Page
  {
    public AdminCommentsPage()
    {
      InitializeComponent();
      LoadData();
    }


    private void LoadData()
    {
      var navigationService = ServiceLocator.NavigationService;
      DataContext = new AdminCommentsPageViewModel(navigationService);
    }
  }
}

namespace FashionHub.ViewModels
{

  public class AdminCommentsPageViewModel : NavigationBarViewModel
  {

    public ObservableCollection<Comment> Comments { get; set; }

    private Comment _selectedComment;
    public Comment SelectedComment
    {
      get => _selectedComment;
      set
      {
        _selectedComment = value;
        OnPropertyChanged(nameof(SelectedComment));
      }
    }

    public ICommand DeleteCommentCommand { get; }
    public ICommand EditCommentCommand { get; }

    public AdminCommentsPageViewModel(INavigationService navigationService) : base(navigationService) 
    {
      Comments = new ObservableCollection<Comment>();
      DeleteCommentCommand = new RelayCommand(DeleteComment);
      EditCommentCommand = new RelayCommand(EditCommand);
      LoadComments();
    }

    public AdminCommentsPageViewModel() : base() { }


    private void LoadComments()
    {
      try
      {
        using (var context = new DataBaseContext())
        {
          var comments = context.Comments
              .Include(c => c.User)
              .Include(c => c.ClothingItem)
              .ToList();

          Comments.Clear();
          foreach (var comment in comments)
          {
            Comments.Add(comment);
          }
        }
      }
      catch (Exception ex)
      {
        CustomMessageBox.Show("Ошибка", $"Не удалось загрузить комментарии: {ex.Message}");
      }
    }

    private void DeleteComment(object parameter)
    {
      if (parameter is Comment comment)
      {
        var result = CustomMessageBox.Show("Подтверждение", "Вы уверены, что хотите удалить комментарий?", true);
        if (result == true)
        {
          try
          {
            using (var context = new DataBaseContext())
            {
              var commentToDelete = context.Comments.FirstOrDefault(c => c.CommentId == comment.CommentId);
              if (commentToDelete != null)
              {
                context.Comments.Remove(commentToDelete);
                context.SaveChanges();
                Comments.Remove(comment);
              }
            }
          }
          catch
          {
            CustomMessageBox.Show("Ошибка", "Ошибка при удалении комментария.");
          }
        }
      }
    }

    private void EditCommand(object parameter)
    {
      if (parameter is Comment)
      {
        IsCatalogPage = false;
        _navigationService.NavigateTo(typeof(AddingCommentPage), parameter);
      }
    }

  }

}
