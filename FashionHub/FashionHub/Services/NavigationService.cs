using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using FashionHub.Views;
using Microsoft.Extensions.DependencyInjection;

namespace FashionHub.Services
{

  public interface INavigationService
  {
    void NavigateTo(Type page, object parameter);
    void RefreshCurrentPage();
    void GoBack();
    void GoForward();
    bool CanGoBack { get; }
    bool CanGoForward { get; }
    void ClearStacks();
  }


  public static class ServiceLocator
  {
    private static ServiceProvider _provider;

    public static INavigationService NavigationService { get; private set; }
    public static UndoRedoManager UndoRedoManager { get; } = new UndoRedoManager();

    public static void Initialize(Frame mainFrame)
    {
      NavigationService = new NavigationService(mainFrame);
    }

    public static void ConfigureServices(ServiceCollection services)
    {
      _provider = services.BuildServiceProvider();
    }

    public static T GetService<T>() => _provider.GetRequiredService<T>();
  }



  public class NavigationService : INavigationService
  {
    private readonly Frame _navigationFrame;
    private double _savedScrollPosition;

    private readonly Stack<(Type PageType, object Parameter)> _backStack = new Stack<(Type PageType, object Parameter)>();
    private readonly Stack<(Type PageType, object Parameter)> _forwardStack = new Stack<(Type PageType, object Parameter)>();
    private (Type PageType, object Parameter)? _currentPage;

    public bool CanGoBack => _backStack.Count > 0;
    public bool CanGoForward => _forwardStack.Count > 0;

    public NavigationService(Frame navigationFrame)
    {
      _navigationFrame = navigationFrame;
    }

    public void NavigateTo(Type pageType, object parameter)
    {
      if (_currentPage != null)
      {
        _backStack.Push(_currentPage.Value);
        _forwardStack.Clear();
      }

      var pageInstance = parameter != null
          ? (Page)Activator.CreateInstance(pageType, parameter)
          : (Page)Activator.CreateInstance(pageType);

      _navigationFrame.Navigate(pageInstance);
      _currentPage = (pageType, parameter);

      var navigation = new NavigationServiceViewModel(ServiceLocator.NavigationService);
      if (_currentPage.Value.PageType == typeof(CatalogPage))
      {
        navigation.IsCatalogPage = true;
      }
      else
      {
        navigation.IsCatalogPage = false;
      }

    }

    public void GoBack()
    {
      if (_backStack.Count > 0)
      {
        _forwardStack.Push(_currentPage.Value);
        var previousPage = _backStack.Pop();
        var pageInstance = previousPage.Parameter != null
            ? (Page)Activator.CreateInstance(previousPage.PageType, previousPage.Parameter)
            : (Page)Activator.CreateInstance(previousPage.PageType);
        _navigationFrame.Navigate(pageInstance);
        _currentPage = previousPage;
      }

      var navigation = new NavigationServiceViewModel(ServiceLocator.NavigationService);
      if (_currentPage.Value.PageType == typeof(CatalogPage))
      {
        navigation.IsCatalogPage = true;
      }
      else
      {
        navigation.IsCatalogPage = false;
      }

    }

    public void GoForward()
    {
      if (_forwardStack.Count > 0)
      {
        _backStack.Push(_currentPage.Value);
        var nextPage = _forwardStack.Pop();
        var pageInstance = nextPage.Parameter != null
            ? (Page)Activator.CreateInstance(nextPage.PageType, nextPage.Parameter)
            : (Page)Activator.CreateInstance(nextPage.PageType);
        _navigationFrame.Navigate(pageInstance);
        _currentPage = nextPage;
      }

      var navigation = new NavigationServiceViewModel(ServiceLocator.NavigationService);
      if (_currentPage.Value.PageType == typeof(CatalogPage))
      {
        navigation.IsCatalogPage = true;
      }
      else
      {
        navigation.IsCatalogPage = false;
      }

    }


    public void RefreshCurrentPage()
    {
      if (_navigationFrame.Content is Page currentPage)
      {
        _savedScrollPosition = GetCurrentScrollPosition();

        var newPage = Activator.CreateInstance(currentPage.GetType());
        _navigationFrame.Navigate(newPage);

        if (newPage is Page page)
        {
          page.Loaded += (s, e) =>
          {
            RestoreScrollPosition(_savedScrollPosition);
          };
        }
      }
    }

    public void ClearStacks()
    {
      _backStack.Clear();
      _forwardStack.Clear();
    }

    private double GetCurrentScrollPosition()
    {
      if (_navigationFrame.Content is Page page &&
          FindVisualChild<ScrollViewer>(page) is ScrollViewer scrollViewer)
      {
        return scrollViewer.VerticalOffset;
      }
      return 0;
    }

    private void RestoreScrollPosition(double position)
    {
      if (_navigationFrame.Content is Page page &&
          FindVisualChild<ScrollViewer>(page) is ScrollViewer scrollViewer)
      {
        scrollViewer.ScrollToVerticalOffset(position);
      }
    }

    private static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
    {
      for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
      {
        var child = VisualTreeHelper.GetChild(obj, i);
        if (child is T result)
          return result;

        var childResult = FindVisualChild<T>(child);
        if (childResult != null)
          return childResult;
      }
      return null;
    }

  }
}
