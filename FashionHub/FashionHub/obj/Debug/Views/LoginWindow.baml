   M S B A M L   `   `   `  ����E  AFashionHub, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:FashionHub.ComponentsFashionHub.Components  5   	   =#clr-namespace:FashionHub.ViewModelsFashionHub.ViewModels  5   	   "    FashionHub.Views.LoginWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:FashionHub.Components     5   	   2vm#clr-namespace:FashionHub.ViewModels     5   	     � Title$  LoginWindow��5	   	   $	��600��6   $	��600��6*    � WindowStyle$
 None=�5
   	    � AllowsTransparency .  � WindowStartupLocation$ CenterScreen=�5   	    � 	Resources 5       �� Source�� 5      $ ../Resources/Styles.xaml?�6   %     � DataContext 5      0   *FashionHub.ViewModels.LoginWindowViewModel  5      .��5      �� 
���   �$��3q�6    J� InputBindings 5      d� 5       d� MouseAction$ 	LeftClicke�5      	 �� Command	 5      �� *DragMoveCommand+._�5      � ���#252424.+�5      �� .+�5      �� $"��../Resources/icons/close.png��6   $��40��64   $��40��6?   $��Right=�5      $��
20 20 20 0q�6*   
 J� Opacity$

 0.25Q�6>    � Cursor$
 Hand��6M    5    
   d� 5!      $ 	LeftClicke�6   	 5"      �� *CloseWindowCommand+5$   	   �� 5%   
   $��Вход��6   $��Center=�5&       	  Title#��[  5'      $��	0 25 0 25q�5(      -   'FashionHub.Components.InnerShadowBorder  5)      $	��300��5*      $
��auto��5+         
ShadowFill �#502971   ContentBackground �#633788   ContentBorderBrush �#A76AD9   InnerContent 5/   
   �� 50       �� Padding$ 24q�6   ._�51      �� .+�53      �� $��Логин:��6     StandardTextBlock#��[ 6(   $��	0 12 0 10q�6S     54      $	��250��55      $��40��56       �#A76AD9 �#633788 �#502971 5:      �� 5;      
������ $��0q�65   $��Center=�6I    �� 
CaretBrush
 �����
���������5<      �� *	LoginName+ �� UpdateSourceTrigger$ PropertyChanged=�5>      �� 5?      ���#FF6E2F$��11(�6M   ��6   �� *
LoginError+y�5@      �� *
LoginError+  StringToVisibilityConverter �� 	Converter# [ �� 5B      $��Пароль:��6   #��[ 6+   $��	0 20 0 00q�6V     5C      $	��250��5D      $��40��5E       �#A76AD9 �#633788 �#502971 5I      G� 5J      -   5        
������ $��0q�5J   ;   $��Center=�6O    G� 
CaretBrush
 �����
�������5K      5M      �� 5N      ���#FF6E2F$��11(�6P   ��6   �� *PasswordError+y�5O      �� *PasswordError+# [ �� 5Q        AdminButtons#��[ 6   $
��auto��6?   $��50��6L   $��0 20 0 0q�6X   $��
Войти��5R      $��12(�6'   ��65   �� *LoginCommand+�� 5T      $��Регистрация��6   #��[ 6/   $��12(�6Z   ���#83AFFF$��Right=�5U   1   $
 Hand��6M    5V      d� 5W      $ 	LeftClicke�6#   	 5X      �� *OpenRegistrationCommand+5Z      5\      5]      5_   	   5`      5c      5d      5f      