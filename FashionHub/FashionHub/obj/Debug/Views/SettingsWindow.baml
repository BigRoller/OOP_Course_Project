   M S B A M L   `   `   `  ����E  AFashionHub, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:FashionHub.ComponentsFashionHub.Components  5   	   =#clr-namespace:FashionHub.ViewModelsFashionHub.ViewModels  5   	   %    FashionHub.Views.SettingsWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:FashionHub.Components     5   	   2vm#clr-namespace:FashionHub.ViewModels     5   	     � Title$  SettingsWindow��5	   	   $	��600��6    $	��600��6-    � WindowStyle$
 None=�5
   	    � AllowsTransparency .  � WindowStartupLocation$ CenterScreen=�5   	    � 	Resources 5       �� Source�� 5      $ ../Resources/Styles.xaml?�6   %     � DataContext 5      3   -FashionHub.ViewModels.SettingsWindowViewModel  5      .��5      �� 
���   �$��3q�6    J� InputBindings 5      d� 5       d� MouseAction$ 	LeftClicke�5      	 �� Command	 5      �� *DragMoveCommand+._�5      � ���#252424.+�5      �� $"��../Resources/icons/close.png��5   	   $��40��5   	   $��40��5    	   $��0��5!   	   $��Right=�5"   	   $	��Top=�5#   	   $��
0 16 20 16q�5$   	   
 J� Opacity$

 0.25Q�5%   	    � Cursor$
 Hand��5&   	    5'   
   d� 5(      $ 	LeftClicke�5)      	 5*      �� *CloseWindowCommand+5,   	   �� 5-      $��Center=�6   .+�5/   
   �� $��Настройки��50   	   $��Center=�51   	    	  Title#��[  52   	   $��	0 16 0 16q�53   	   -   'FashionHub.Components.InnerShadowBorder  55   
   $	��300��56      $
��auto��57         
ShadowFill �#502971   ContentBackground �#633788   ContentBorderBrush �#A76AD9   InnerContent 5;      �� 5<       �� Padding$ 36q�6   ._�5=      ��  �� Orientation$ Vertical=�6   $��Center=�62   .+�5?      �� $��	Язык:��6     StandardTextBlock#��[ 6)   $��0 0 0 10q�6T   �� 5@      $ 
Horizontal=�6   $��5 0 0 15q�66   $��Center=�6H   .+�5A      	�  	� 	GroupName$ Language��6      CustomToggleStyle#��[ 65   $
��Left=�6`    d� 	IsChecked$
 TrueY�6{   ��5B   !   �� *SetRussianCommand+�� 5C      $��Русский��6   #��[ 6-   $��14(�6X   $��10 0 0 0q�6f   5D      �� 5F      $ 
Horizontal=�6   $��5 0 0 15q�66   $��Center=�6H   .+�5G      	� $ Language��6    #��[ 65   $
��Left=�6`   ��5H       �� *SetEnglishCommand+�� 5I      $��Английский��6   #��[ 60   $��14(�6[   $��10 0 0 0q�6j   5J      �� 5L      $��	Тема:��6   #��[ 6)   $��	0 10 0 10q�6T   �� 5M      $ 
Horizontal=�6   $��5 0 0 15q�66   $��Center=�6H   .+�5N      	� $ Theme��6    #��[ 62   $
��Left=�6]   ��5O       �� *SetLightThemeCommand+�� 5P      $��Светлая��6   #��[ 6-   $��14(�6X   $��10 0 0 0q�6g   5Q      �� 5T      $ 
Horizontal=�6   $��5 0 0 15q�66   $��Center=�6H   .+�5U      	� $ Theme��6    #��[ 62   $
��Left=�6]   $
 TrueY�6x   ��5V       �� *SetDarkThemeCommand+�� 5W      $��Тёмная��6   #��[ 6,   $��14(�6W   $��10 0 0 0q�6f   5X      5Y      5Z      5\      5]   	   5`      5a      5c      