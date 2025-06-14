﻿using System.Windows.Input;
using System;

namespace FashionHub.Commands
{
  public class RelayCommand : ICommand
  {
    private readonly Action<object> execute;
    private readonly Func<object, bool> canExecute;

    public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
    {
      this.execute = execute;
      this.canExecute = canExecute;
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
      return canExecute == null || canExecute(parameter);
    }

    public void Execute(object parameter)
    {
      execute(parameter);
    }

    public void RaiseCanExecuteChanged()
    {
      CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
  }
}