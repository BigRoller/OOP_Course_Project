   M S B A M L   `   `   `  ����E  AFashionHub, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:FashionHub.ComponentsFashionHub.Components  5      "    FashionHub.Views.CatalogPage   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      :
components#clr-namespace:FashionHub.Components     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a?interactivity+http://schemas.microsoft.com/xaml/behaviors  5        N� Title$  Fashion Hub��5       � 	Resources 5       �� Source�� 5	      $ ../Resources/Styles.xaml?�6   %    .��5      � ���#252424 � RowDefinitions 5      �� 5   
   $	��100 �6   �� 5   
   $��* �6    � ColumnDefinitions 5      �� 5   
   $	��250 �6   �� 5   
   $��* �6   .+�5      )   #FashionHub.Components.NavigationBar  $��0��6!   $��2��6.    K� ZIndex$ 1��6B   �� 5       �� Padding$ 52 20 52 20q�6   $��0q�6%   ���#633788$��0��6O   $��1��6_   $ 0��6l   ._�5   
   ��  �� Orientation$ Vertical=�6   .+�5      �� $��Фильтры��6      StandardTextBlock#��[  6%   $��22(�6P   $
��Bold"�6^   $��0 0 0 10q�6p   �� 5      $��Диапазон цены��6   #��[  6+   $��Medium"�6V   $��14(�6j   $��0 0 0 10q�6x   � 5       5       �� 5!      $
��Auto �6!   �� 5"      $��* �6!   .+�5%      �� $
��От��6   #��[  6"   $��0 0 10 0q�6M   $��14(�6_   $��0��6m   $��Center=�6}   �� 5'      -   5         ��Name$ 	PriceFrom��5(      $��0��5'       	 Input#��[ 6   $	��110��6<   $��25��6H   	 �� 
CaretBrush
	 �����$��1��6g   $��Center=�6w   5(      5)      �� 5*   
   ���#FF6E2F$
��Wrap=�6)   $��0 0 0 20q�6>   ��5+      �� *FilterErrors[PriceFrom]+y�5,      �� *FilterErrors[PriceFrom]+  StringToVisibilityConverter
 �� 	Converter#
 [ � 5.   
    5/      �� 50      $
��Auto �6!   �� 51      $��* �6!   .+�54      �� $
��До��6   #��[  6"   $��0 0 10 0q�6M   $��14(�6_   $��0��6m   $��Center=�6}   �� 56      -   5        $ PriceTo��57      $
��1000��56      #��[ 6    $	��110��6?   $��25��6K   
	 �����$��1��6j   $��Center=�6z   57      58      �� 59   
   ���#FF6E2F$��0 0 0 20q�6)   $
��Wrap=�6;   ��5:      �� *FilterErrors[PriceTo]+y�5;      �� *FilterErrors[PriceTo]+#
 [ �� 5=   
   $��Категории��6   #��[  6%   $��Medium"�6P   $��0 0 0 0q�6e   � 5>   
   $
��0,10q�6    5?      �� 5@      $��30 �6   �� 5A      $��30 �6   �� 5B      $��30 �6   �� 5C      $	��30  �6    5E      �� 5F      $��* �6   �� 5G      $
��Auto �6   .+�5J      �� $��0��6   $��0��6#   $��Все��5K      #��[  5L      $��14(�5M      $��Center=�5N      $��0,0,10,5q�5O      	� 5P      -   5        $��CategoryFilter_All��5Q      $��0��5P      $��1��6%    	� 	GroupName$ Filters��5R        AllClothing#���  5S        CustomToggleStyle#��[ 5T       d� 	IsChecked$
 TrueY�5U      $��Center=�5V      $��0,0,0,5q�5W      �� 5Y      $��1��6   $��0��6#   $��Мужчинам��5Z      #��[  5[      $��14(�5\      $��Center=�5]      $��0,0,10,5q�5^      	� 5_      -   5        $��CategoryFilter_Men��5`      $��1��5_      $��1��6%   $ Filters��5a        MenClothing#���  5b      #��[ 5c      $��Center=�5d      $��0,0,0,5q�5e      �� 5g      $��2��6   $��0��6#   $��Женщинам��5h      #��[  5i      $��14(�5j      $��Center=�5k      $��0,0,10,5q�5l      	� 5m      -   5        $��CategoryFilter_Women��5n      $��2��5m      $��1��6%   $ Filters��5o        WomenClothing#���  5p      #��[ 5q      $��Center=�5r      $��0,0,0,5q�5s      �� 5u      $��3��6   $��0��6#   $��
Детям��5v      #��[  5w      $��14(�5x      $��Center=�5y      $��0,0,10,5q�5z      	� 5{      -   5        $��CategoryFilter_Kids��5|      $��3��5{      $��1��6%   $ Filters��5}        KidsClothing#���  5~      #��[ 5      $��Center=�5�      $��0,0,0,5q�5�      5�      �� 5�   
   $'��!Применить фильтры��6     CustomButton#��[ 6-   $��12(�6S   $
��auto��6a   $��40��6n    � Cursor$
 Hand��6z   ��5�      �� *ApplyFiltersCommand+��69   ��  �� RelativeSource ��  �� AncestorType$
 PageL�5�   	   5�   	   �� 5�      $��1��6   $��1��6"   $��Visible=�63   $��Disabled=�6Y   .��5�   
   �� .+�5�      ��  		 Title#��[	 6   $��Каталог��65   $��	50 40 0 0q�6D   �� 5�      ��6   �� *ClothingItems+��5�      �� 5�      .R�5�      �  � 	ItemWidth$	 300��6    � Orientation$ 
Horizontal=�6,   $��Center=�6E   5�      ��5�      �� 5�      .R�5�      '   !FashionHub.Components.ProductCard  $��20q�6)   5�      5�      5�      5�   	   5�      5�      