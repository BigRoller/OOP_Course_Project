   M S B A M L   `   `   `  ����E  AFashionHub, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:FashionHub.ViewModelsFashionHub.ViewModels  5      '    !FashionHub.Components.ProductCard   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      2vm#clr-namespace:FashionHub.ViewModels     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a?interactivity+http://schemas.microsoft.com/xaml/behaviors  5      -   5          � 	Resources  5       �� Source�� 5      $ ../Resources/Styles.xaml?�6   %    .��5      � $��Center=�6	    K� ZIndex$ 1��6&   .+�5      �� ���#502971 �� RadiusX$ 10��6    �� RadiusY$ 10��6,    J� OpacityMask 5      !� 5   
    !� Visual 5      �� 5       �� CornerRadius$ 10��5      
�������$��2q�5      ��5      �� *ActualWidth+ �� ElementName$ 
mainBorder����5      �� *ActualHeight+$ 
mainBorder��5      5      ��5      u� 5   
   	 u� Left$	 2Q�6   

 u� Top$
 2Q�6    u� Right$ -2Q�6%    u� Bottom$ -2Q�60   5      �� 5      -   5         ��Name$ 
mainBorder��5      $ 2��6!   $ 10��5      ���#633788���#A76AD9$��2 2 0 0q�5!       �� Padding$ 30q�5"      *  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5#      +  %Microsoft.Xaml.Behaviors.EventTrigger  5$   
      	EventName$ MouseUp��6%   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5%      2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command 6.   �� *OnItemClickedCommand+   CommandParameter 6W   �� *	Product+5&      ._�5(      �  � RowDefinitions 5)   
   �� 5*      $
��auto �6   �� 5+      $��* �6   �� 5,      $��* �6   .+�5/   
   �� $	��200��6   $	��200��6   $��UniformToFill=�51      $��0,0,0,10q�52      }�. $��0��53      ��50      �� *	ImagePath+53      � 54   
   $��1��6    � ColumnDefinitions 55      �� 56      $��5* �6   �� 57      $��1* �6   .+�5:      ��  �� Orientation$ Vertical=�6   $��0��6.   $	��130��6>   $	��200��6K   .+�5;      �� $ 
Horizontal=�6   $��0, 10, 0, 0q�62     5<       �� 
TargetType   StandardTextBlock �� BasedOn��%0   '��       7   �� 5=      " ��6   8   6/   5>      .+�5?      �� $��22(�69   $
��Bold"�6G   ��6   �� *Product.Price+6G   �� 5@      $�� $��6   $��22(�6$   $
��Bold"�62   5A      �� 5C      #��[  6;   $��0,10,0,0q�6f   ��6   �� *Product.ShortName+6f   �� 5E      $ 
Horizontal=�6   $��Center=�62   $��0,10,0,0q�6M     5F      ��%0   '��       7   �� 5G      " ��6   8   6/   5H      .+�5I      F� ���#FDF3FF���#FDF3FF$��1��65   a������� @B � � ��j р���j �@]� �ا ���� @fр�� @]� �@KL @f���j �ا р� ��j � z ��j 	z�5K      �� 5L       �� ScaleX$
 0.75Q�6#    �� ScaleY$
 0.75Q�61   5N      �� 5O      ��6   �� *Rating+5P      5Q      d� 5R      $��1��6   $��Bottom=�6)   
������ $��0q�5U       d� 	IsChecked 5S      �� *
IsFavorite+ �� Mode$ TwoWay=���5V      �� *ToggleFavoriteCommand+y�5W      �� *IsUserLoggedIn+  BoolToVisibilityConverter �� 	Converter# [ ��5X      �� 5Y       �� 
TargetType" d�6    .R�5Z      �� $��30��5[      $��30��5\      ��5Z      �� *
IsFavorite+  FavoriteIconConverter# [ 5\      5]      5_      5`      �� 5b   
   $��2��6l   $��Bottom=�6y   y�6   �� *IsCatalogPage+# [ ._�5c   
   �� $ Vertical=�6   $��0 10 0 0q�6,   $
��auto��6>   y�5d      �� *	IsAdmin+# [   5e        AdminButtons��%0   '��       7  �� 5f      " ��6   8   6(   5g      .+�5h      ��   EditItem#���  6   ��5i      �� *DataContext.EditCommand+ �� RelativeSource ��   �� AncestorType$
  PageL���5j      �� *	Product+�� 5k        
DeleteItem#���  6   ��5l      �� *DataContext.DeleteCommand+ �� $
  PageL���5m      �� *	Product+5n      5o      5p   	   5q      5r      5s      