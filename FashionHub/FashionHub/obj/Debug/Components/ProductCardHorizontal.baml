   M S B A M L   `   `   `  ����E  AFashionHub, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:FashionHub.ComponentsFashionHub.Components  5      1    +FashionHub.Components.ProductCardHorizontal   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:FashionHub.Components     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a?interactivity+http://schemas.microsoft.com/xaml/behaviors  5      $��Stretch=�5
        � 	Resources  5       �� Source�� 5      $ ../Resources/Styles.xaml?�6   %    .��5      �  K� ZIndex$ 1��6	   $��Stretch=�6   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��Stretch=�6*   .+�5      �� ���#502971 �� RadiusX$ 10��6!    �� RadiusY$ 10��6.   $��Stretch=�6;    J� OpacityMask 5   
   !� 5       !� Visual 5      �� 5       �� CornerRadius$ 10��5      
�������$��2q�5      ��5      �� *ActualWidth+	 �� ElementName$	 
mainBorder����5      �� *ActualHeight+$	 
mainBorder��5      5      ��5   
   u� 5      
 u� Left$
 2Q�6   
 u� Top$ 2Q�6    u� Right$ -2Q�6'    u� Bottom$ -2Q�62   5!   	   �� 5#      -   5         ��Name$ 
mainBorder��5#      $ 2��6#   $ 10��5$      ���#633788���#A76AD9$��2 2 0 0q�5'      $��Stretch=�5(       �� Padding$ 25q�5)      *  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5*   
   +  %Microsoft.Xaml.Behaviors.EventTrigger  5+         	EventName$ MouseUp��6'   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5,      2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command 60   �� *OnItemClickedCommand+   CommandParameter 6Y   �� *	Product+5-      ._�5/   
   �  � ColumnDefinitions 50      �� 51      $
��auto �6   �� 52      $��* �6   .+�54      �� $	��200��6/   $	��200��6;   $
��0 10q�6H   $��Center=�6V   ��6   �� *	ImagePath+6V   � 55      $��1��6    � RowDefinitions 56      �� 57      $��5* �6   �� 58      $��1* �6    5:      �� 5;      $��1* �6!   �� 5<      $
��1.5* �6!   .+�5>      �� $ Vertical=�6   $��0��60   $��0��6=   .+�5?      ��    StandardTextBlock#��[  6=   $��	0,10,0,12q�6h   ��6   �� *Product.ShortName+6h   �� 5@      $ 
Horizontal=�6   .+�5A      �� #��[  6:   $��0 0 32 0q�6e   ��6   �� *SelectedSize+6e   �� 5B      #��[  6;   ��6   �� *SelectedColor+6;   5C      5D      �� 5F      $ 
Horizontal=�6   $��0��62   $��1��6?   $��Right=�6O     5G       �� 
TargetType �� BasedOn��%0   '��       7   �� 5H      " ��6   8   6/   5I      .+�5J      �� $��22(�69   $
��Bold"�6G   ��6   �� *Product.Price+6G   �� 5K      $�� $��6   $��22(�6$   $
��Bold"�62   5L      �� 5N      $ 
Horizontal=�6   $��1��62   $��1��6?   $��Right=�6O   $��Bottom=�6k   .+�5O      �� $ 
Horizontal=�6   y�64   �� *IsUserLoggedIn+  BoolToVisibilityConverter �� 	Converter# [ .+�5P      d�  � Cursor$
 Hand��5Q      
������ $��0q�5T       d� 	IsChecked 5R      �� *
IsFavorite+ �� Mode$ TwoWay=���5U      �� *ToggleFavoriteCommand+��5V      �� 5W       �� 
TargetType" d�6&   .R�5X      �� $��30��5Y      $��30��5Z      ��5X      �� *
IsFavorite+  FavoriteIconConverter# [ 5Z      5[      5]      �� 5^      $��В избранное��6   #��[  6/   ���#83AFFF$��Center=�6o   $��6 0 20 0q�6�   5_      �� 5a       5b        5c      $ MouseUp��6/   . 5d         68   �� *DeleteCommand+5e      ._�5g      �� $ 
Horizontal=�6   $
 Hand��66   .+�5h      �� $"��../Resources/icons/trash.png��6   $��30��6@   $��30��6K   �� 5i      $��Удалить��6   #��[  6-   ���#83AFFF$��Center=�6n   $	��6 0q�6�   5j      5k      5l      5m      5n      5o   	   5p      5q      5r      