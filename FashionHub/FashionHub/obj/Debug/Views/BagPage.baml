   M S B A M L   `   `   `  ����E  AFashionHub, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:FashionHub.ComponentsFashionHub.Components  5   	   =#clr-namespace:FashionHub.ViewModelsFashionHub.ViewModels  5   	       FashionHub.Views.BagPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   :
components#clr-namespace:FashionHub.Components     5   	   2vm#clr-namespace:FashionHub.ViewModels     5   	   _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a?interactivity+http://schemas.microsoft.com/xaml/behaviors  5   	     N� Title$  Bag Page��5   	    � 	Resources 5	       �� Source�� 5
      $ ../Resources/Styles.xaml?�6   %     � DataContext 5      ,   &FashionHub.ViewModels.BagPageViewModel  5      .��5      � ���#252424 � RowDefinitions 5      �� 5      $	��100 �6   �� 5      $��* �6   �� 5      $
��auto �6   .+�5      )   #FashionHub.Components.NavigationBar  $��0��6   �� 5      $��Visible=�6   $��Disabled=�6:   $��1��6c   .��5      �  � ColumnDefinitions 5   
   �� 5      $��2* �6   �� 5      $��1* �6    5    
   �� 5!      $
��auto �6   �� 5"      $
��auto �6   .+�5%   
   � $��0��6   $��0��6   .+�5&      �� $��Корзина пуста��6      StandardTextBlock#��[  5'      $��24(�5(      
�������$��Center=�5*      $	��Top=�5+      $��	0 200 0 0q�5,      y�5-      �� 5.       �� Path$ CartItems.Count�6      EmptyCartVisibilityConverter �� 	Converter# [ 6/   50      �� 53       �� Padding$ 45q�6   y�54      `� 55         CartVisibilityMultiConverter	 `� 	Converter#	 [ 6   .1�56      �� $ CartItems.Count�6   �� 57      $ IsUserLoggedIn�6   58      ._�5:      �� 
 �� Orientation$
 Vertical=�6   $��0 0 0 40q�60   .+�5;      �� $��Корзина��6    	 Title#��[ 6)   �� 5=      $
 
Horizontal=�6   $��0 20 0 0q�64   .+�5>      �� $#��../Resources/icons/select.png��6   $��40��6?   $��40��6J   $��0 0 10 0q�6V   �� 5?      $��Выбрать всё��6   #��[  6/   ���#83AFFF$��Center=�5@      $��0 0 30 0q�6'    J� InputBindings 5A      d� 5B       d� Gesture$ 	LeftClicka�6#    �� Command 67   �� *SelectAllCommand+5D      �� 5F      $"��../Resources/icons/trash.png��6   $��40��6>   $��40��6I   $��0 0 10 0q�6U   �� 5G      $'��!Удалить выбранные��6   #��[  65   ���#83AFFF$��Center=�5H       5I      d� 5J      $ 	LeftClicka�6#    67   �� *DeleteSelectedCommand+5L      5M      �� 5O      ��6   �� *	CartItems+ �� UpdateSourceTrigger$ PropertyChanged=���5P      �� 5Q      .R�5R      �  � Orientation$ Vertical=�6    $��Stretch=�67   5S      ��5U      �� 5V      .R�5W      �� $
 
Horizontal=�6!   $
��0 10q�6:   .+�5X      �� $��40��6B   $��40��6M   $	��Top=�6Y   $��	0 30 10 0q�6q    d� 	IsChecked 6!   �� *
IsSelected+��5Y      �� 5Z       �� 
TargetType" ��6,   .R�5[      ��  ��Name$
 Icon��6$   $��40��62   $��40��6=    �� Triggers 5\      P� 5]        P� Property �@d�	IsChecked P� Value$
 TrueY�6=   . �5^   "   ��  �� 
TargetName$
 Icon��6)    �� Property � �� �� Value$- '../Resources/icons/check-mark_green.png��6M   5_   !   P� 5`        �@d�	IsChecked$ FalseY�6=   . �5a   "   �� $
 Icon��6)    � ��$# ../Resources/icons/cancel.png��6M   5b   !   5d      5f      1   +FashionHub.Components.ProductCardHorizontal  5h      $��	0 25 0 25q�69   5i      5j      5l      5n      5o      5p      -   'FashionHub.Components.InnerShadowBorder  5r   
   $��1��6'   $��0��67   $
��auto��5s      $
��auto��5t         
ShadowFill �#502971   ContentBackground �#633788   ContentBorderBrush �#A76AD9$	��Top=�5x      $��
0 160 45 0q�5y         InnerContent 5z      �� 5{      $ 45q�6   ._�5|      �� $
 Vertical=�6   .+�5}      �  5~      �� 5      $
��auto �6$   �� 5�      $
��auto �6$   �� 5�      $
��auto �6$   �� 5�      $
��auto �6$    5�      �� 5�      $��* �6'   �� 5�      $
��auto �6'   .+�5�      �� $��0��6   $��2��6+   $��Сумма заказа��6?   #��[  6S   $��20(�6~   $��0 0 0 30q�6�   �� 5�      $��1��6   $��0��6+   #��[  6u   $��0 0 0 15q�6�   ��6;   �� *
TotalCount+ �� StringFormat$ {0} товар(ов)��6�   �� 5�      $��3��6   $��0��6+   $��
Итого��6;   #��[  6H   $��20(�6s   �� 5�      $��3��6   $��1��6+   #��[  6m   $��20(�6�   ��6;   �� *
TotalPrice+$ {0:C}��6�   5�      �� 5�      $��К оформлению��6     CustomButton#��[ 6]   $
��auto��6�   $��85��6�   $��0 20 0 0q�6�    � Cursor$
 Hand��6�   ��60   �� *ProceedToCheckoutCommand+6�   5�      5�      5�      �� 5�   
   $
 Vertical=�6   $��1��6,   $��2��69   .+�5�      �� #��[ 6   $��Рекомендации��65   $��
50 -50 0 0q�6I   �� 5�      ��6   �� *RecommendationsItems+$ PropertyChanged=���5�      �� 5�      .R�5�      �  � 	ItemWidth$	 300��6   $ 
Horizontal=�6,   $��Center=�6E   5�      ��5�      �� 5�      .R�5�      '   !FashionHub.Components.ProductCard  $��20q�6)   5�      5�      5�      5�   	   5�      5�      5�      