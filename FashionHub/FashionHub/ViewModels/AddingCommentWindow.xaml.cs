using FashionHub.Commands;
using FashionHub.Components;
using FashionHub.Models;
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
using FashionHub.Services;
using FashionHub.Views;
using FashionHub.ViewModels;

namespace FashionHub.Views
{
  /// <summary>
  /// Логика взаимодействия для Login.xaml
  /// </summary>
  public partial class AddingCommentWindow : Window
  {
    public AddingCommentWindow(int id)
    {
      InitializeComponent();
      LoadData(id);
    }

    private void LoadData(int id)
    {
      DataContext = new AddingCommentWindowViewModel(id);
    }

  }
}

namespace FashionHub.ViewModels
{ 
  public partial class AddingCommentWindowViewModel : BaseWindowViewModel
  {
    private int itemID;
    public int ItemID
    {
      get => itemID;
      set
      {
        itemID = value;
        OnPropertyChanged(nameof(ItemID));
      }
    }

    private int _rating;
    public int Rating
    {
      get => _rating;
      set
      {
        _rating = value;
        OnPropertyChanged(nameof(Rating));
      }
    }

    private string _commentText;
    public string CommentText
    {
      get => _commentText;
      set
      {
        _commentText = value;
        OnPropertyChanged(nameof(CommentText));
      }
    }

    private int _currentRating;
    public int CurrentRating
    {
      get => _currentRating;
      set
      {
        _currentRating = value;
        OnPropertyChanged(nameof(CurrentRating));
      }
    }


    public ICommand SendCommentCommand { get; }
    public ICommand SetRatingCommand { get; }

    public AddingCommentWindowViewModel(int id) : base()
    {
      ItemID = id;
      SendCommentCommand = new RelayCommand(SendComment);
      SetRatingCommand = new RelayCommand(SetRating);
    }

    public AddingCommentWindowViewModel() : base() { }


    private void SetRating(object parameter)
    {
      if (parameter is int rating)
      {
        CurrentRating = rating;
        Rating = rating;
      }
    }

    private void SendComment(object parameter)
    {
      if (CurrentUserService.UserId == null)
      {
        CustomMessageBox.Show("Ошибка", "Вы должны войти в аккаунт, чтобы оставить отзыв");
        return;
      }

      if (string.IsNullOrWhiteSpace(CommentText))
      {
        CustomMessageBox.Show("Ошибка", "Пожалуйста, введите текст комментария");
        return;
      }


      if (Rating <= 0)
      {
        CustomMessageBox.Show("Ошибка", "Пожалуйста, поставьте оценку товару");
        return;
      }

      try
      {
        var newComment = new Comment
        {
          UserId = (int)CurrentUserService.UserId,
          ProductId = ItemID,
          Rate = Rating,
          Text = CommentText,
        };

        CommentService.AddComment(newComment);

        CustomMessageBox.Show("Успех", "Спасибо за ваш отзыв!");
        CloseWindowCommand.Execute(null);
      }
      catch (Exception ex)
      {
        CustomMessageBox.Show("Ошибка", $"Ошибка при отправке отзыва: {ex.Message}");
      }
    }
  }

}