using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FashionHub.Components
{
  public partial class Star : UserControl
  {

    public static readonly DependencyProperty StarIndexProperty =
        DependencyProperty.Register("StarIndex", typeof(int), typeof(Star), new PropertyMetadata(0));

    public int StarIndex
    {
      get => (int)GetValue(StarIndexProperty);
      set => SetValue(StarIndexProperty, value);
    }

    public static readonly DependencyProperty CommandProperty =
     DependencyProperty.Register("Command", typeof(ICommand), typeof(Star), new PropertyMetadata(null));

    public ICommand Command
    {
      get => (ICommand)GetValue(CommandProperty);
      set => SetValue(CommandProperty, value);
    }

    public static readonly DependencyProperty CommandParameterProperty =
        DependencyProperty.Register("CommandParameter", typeof(object), typeof(Star), new PropertyMetadata(null));

    public object CommandParameter
    {
      get => GetValue(CommandParameterProperty);
      set => SetValue(CommandParameterProperty, value);
    }

    public static readonly DependencyProperty CurrentRatingProperty =
       DependencyProperty.Register("CurrentRating", typeof(int), typeof(Star),
           new PropertyMetadata(0, OnCurrentRatingChanged));

    public int CurrentRating
    {
      get => (int)GetValue(CurrentRatingProperty);
      set => SetValue(CurrentRatingProperty, value);
    }


    public Star()
    {
      InitializeComponent();
      StarPath.MouseLeftButtonDown += OnStarClicked;
    }

    private void OnStarClicked(object sender, MouseButtonEventArgs e)
    {
      if (Command != null && Command.CanExecute(StarIndex))
      {
        Command.Execute(StarIndex);
      }
    }

    private static void OnCurrentRatingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var star = (Star)d;
      star.UpdateStarColor();
    }

    private void UpdateStarColor()
    {
        bool shouldBeSelected = StarIndex <= CurrentRating;
        StarPath.Fill = shouldBeSelected ? (SolidColorBrush)new BrushConverter().ConvertFrom("#83AFFF") : (SolidColorBrush)new BrushConverter().ConvertFrom("#633788");
    }
  }
}