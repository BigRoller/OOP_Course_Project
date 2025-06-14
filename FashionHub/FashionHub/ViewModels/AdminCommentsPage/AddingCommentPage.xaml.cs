using FashionHub.Commands;
using FashionHub.Models;
using FashionHub.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
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
using FashionHub.Views;
using FashionHub.ViewModels;
using NavigationService = FashionHub.Services.NavigationService;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace FashionHub.Views
{
  /// <summary>
  /// Логика взаимодействия для AddingCommentPage.xaml
  /// </summary>
  public partial class AddingCommentPage : Page
  {
    public AddingCommentPage(ClothingItem item)
    {
      InitializeComponent();
      LoadData(item);
    }

    public AddingCommentPage(Comment item)
    {
      InitializeComponent();
      LoadData(item);
    }

    public AddingCommentPage()
    {
      InitializeComponent();
      LoadData<object>(null);
    }


    private void LoadData<T>(T item)
    {
      var navigationService = ServiceLocator.NavigationService;
      if (item is ClothingItem clothingItem)
      {
        DataContext = new AddingCommentPageViewModel(navigationService, clothingItem);
      }
      else if (item is Comment comment)
      {
        DataContext = new AddingCommentPageViewModel(navigationService, comment);
      }
      else if (item == null)
      {
        DataContext = new AddingCommentPageViewModel(navigationService);
      }
    }
  }
  }


namespace FashionHub.ViewModels
{
  public class AddingCommentPageViewModel : NavigationBarViewModel
  {
    private readonly PageState pageState;

    public Comment Comment { get; set; } = new Comment();

    public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();

    public Dictionary<string, string> CommentErrors { get; } = new Dictionary<string, string>();

    public ICommand FinishCommand { get; }
    
    public string ButtonText { get; set; }

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

    public AddingCommentPageViewModel(INavigationService navigationService, ClothingItem clothingItem) : base(navigationService)
    {
      Comment.ProductId = clothingItem.ProductId;
      LoadUsers();
      TitleText = "Добавление комментария";
      ButtonText = "Добавить комментарий";

      FinishCommand = new RelayCommand(FinishComment);

      pageState = PageState.ADDITION;
    }

    public AddingCommentPageViewModel(INavigationService navigationService, Comment comment) : base(navigationService)
    {
      Comment = comment;
      LoadUsers();
      TitleText = "Изменение комментария";
      ButtonText = "Изменить комментарий";

      FinishCommand = new RelayCommand(FinishComment);

      pageState = PageState.EDITING;
    }

    public AddingCommentPageViewModel(INavigationService navigationService) : base(navigationService)
    {
      LoadUsers();

      FinishCommand = new RelayCommand(FinishComment);
    }

    public AddingCommentPageViewModel() : base() {}


    private enum PageState
    {
      EDITING,
      ADDITION,
    }



    private void LoadUsers()
    {
      using (var db = new DataBaseContext())
      {
        var users = db.Users.ToList();
        Users = new ObservableCollection<User>(users);
        OnPropertyChanged(nameof(Users));
      }
    }


    private void FinishComment(object parameter)
    {
      if (pageState == PageState.EDITING)
      {
        EditComment();
      }
      else if (pageState == PageState.ADDITION)
      {
        AddComment();
      }
    }

    private void ClearValidationErrors()
    {
      CommentErrors.Clear();
      OnPropertyChanged(nameof(CommentErrors));
    }

    private void SetValidationErrors(List<ValidationResult> results)
    {
      foreach (var error in results)
      {
        foreach (var member in error.MemberNames)
        {
          CommentErrors[member] = error.ErrorMessage;
        }
      }
      OnPropertyChanged(nameof(CommentErrors));
    }

    private void AddComment()
    {
      ClearValidationErrors();

      var context = new ValidationContext(Comment);
      var results = new List<ValidationResult>();

      if (!Validator.TryValidateObject(Comment, context, results, true))
      {
        SetValidationErrors(results);
        return;
      }

      try
      {
        using (var db = new DataBaseContext())
        {
          db.Comments.Add(Comment);
          db.SaveChanges();
        }

        CustomMessageBox.Show("Успех", "Комментарий успешно добавлен.");
        _navigationService.NavigateTo(typeof(AdminCommentsPage), null);
      }
      catch (Exception)
      {
        CustomMessageBox.Show("Ошибка", $"Не удалось добавить комментарий");
      }
    }

    private void EditComment()
    {
      ClearValidationErrors();

      var context = new ValidationContext(Comment);
      var results = new List<ValidationResult>();

      if (!Validator.TryValidateObject(Comment, context, results, true))
      {
        SetValidationErrors(results);
        return;
      }

      try
      {
        using (var db = new DataBaseContext())
        {
          var existingComment = db.Comments.FirstOrDefault(c => c.CommentId == Comment.CommentId);

          if (existingComment != null)
          {
            existingComment.Text = Comment.Text;
            existingComment.Rate = Comment.Rate;
            existingComment.UserId = Comment.UserId;
            existingComment.ProductId = Comment.ProductId;

            db.SaveChanges();

            CustomMessageBox.Show("Успех", "Комментарий успешно обновлён.");
            _navigationService.NavigateTo(typeof(AdminCommentsPage), null);
          }
          else
          {
            CustomMessageBox.Show("Ошибка", "Комментарий не найден.");
          }
        }
      }
      catch (Exception ex)
      {
        CustomMessageBox.Show("Ошибка", $"Не удалось обновить комментарий: {ex.Message}");
      }
    }



  }

}
