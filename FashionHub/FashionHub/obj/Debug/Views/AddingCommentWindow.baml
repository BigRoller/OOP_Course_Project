   M S B A M L   `   `   `  ����E  AFashionHub, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:FashionHub.ComponentsFashionHub.Components  5   	   =#clr-namespace:FashionHub.ViewModelsFashionHub.ViewModels  5   	   *    $FashionHub.Views.AddingCommentWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :
components#clr-namespace:FashionHub.Components     5   	   2vm#clr-namespace:FashionHub.ViewModels     5   	     � Title$  SettingsWindow��5	   	   $	��600��6    $	��600��6-    � WindowStyle$
 None=�5
   	    � AllowsTransparency .  � WindowStartupLocation$ CenterScreen=�5   	    � 	Resources 5       �� Source�� 5      $ ../Resources/Styles.xaml?�6   %    .��5      �� 
���   �$��3q�6    J� InputBindings 5      d� 5       d� MouseAction$ 	LeftClicke�5       �� Command 5      �� *DragMoveCommand+._�5      � ���#252424	 � RowDefinitions	 5      �� 5   
   $	��53* �6   �� 5   
   $	��32* �6   .+�5      �� $"��../Resources/icons/close.png��5   	   $��40��5   	   $��40��5    	   $��0��5!   	   $��Right=�5"   	   $	��Top=�5#   	   $��	0,16,20,0q�5$   	   
 J� Opacity$

 0.25Q�5%   	    � Cursor$
 Hand��5&   	    5'   
   d� 5(      $ 	LeftClicke�5)       5*      �� *CloseWindowCommand+5,   	   �� 5-      $	��Top=�6   $��0,42,0,0q�6+   $��2��6=   .+�5/   
   �� $)��#Отправление отзыва��50   	   $��Center=�51   	    	  Title#��[  52   	   $��	0 16 0 16q�53   	   -   'FashionHub.Components.InnerShadowBorder  55   
   $	��450��56      $	��450��57         
ShadowFill �#502971   ContentBackground �#633788   ContentBorderBrush �#A76AD9   InnerContent 5;      �� 5<       �� Padding$ 40q�6   ._�5=      ��  �� Orientation$ Vertical=�6   .+�5>      �� $��Рейтинг:��6     StandardTextBlock#��[ 6,   $��0 0 0 10q�6W   �� 5@      $ 
Horizontal=�6   $��0 0 0 10q�66   .+�5A          FashionHub.Components.Star  $��40��6$   $��40��6/      	StarIndex$ 1��5C         Command 5B      �� *SetRatingCommand+   CurrentRating 5D      �� *CurrentRating+  5E      $��40��6$   $��40��6/   $ 2��5G       5F      �� *SetRatingCommand+ 5H      �� *CurrentRating+  5I      $��40��6$   $��40��6/   $ 3��5K       5J      �� *SetRatingCommand+ 5L      �� *CurrentRating+  5M      $��40��6$   $��40��6/   $ 4��5O       5N      �� *SetRatingCommand+ 5P      �� *CurrentRating+  5Q      $��40��6$   $��40��6/   $ 5��5S       5R      �� *SetRatingCommand+ 5T      �� *CurrentRating+5U      �� 5V      $��Комментарий:��6   #��[ 60   $��0 0 0 10q�6[     5X      $
��auto��5Y      $	��120��5Z       �#A76AD9 �#633788 �#502971$��0 0 0 40q�5^       5_      �� 5`      
������ $��0q�67   
������� �� 
CaretBrush
 �����$��10q�6q   ��5a   "   �� *CommentText+ �� UpdateSourceTrigger$ PropertyChanged=�5c      �� 5e      $-��'Оставить комментарий��6     CustomButton#��[ 68   $
��auto��6^   $��85��6k   $
 Hand��6w   ��5f      �� *SendCommentCommand+5g      5h      5j      5k   	   5n      5o      5q      