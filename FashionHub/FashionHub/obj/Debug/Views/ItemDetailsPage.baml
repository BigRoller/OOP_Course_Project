   M S B A M L   `   `   `  ����E  AFashionHub, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:FashionHub.ComponentsFashionHub.Components  5      =#clr-namespace:FashionHub.ViewModelsFashionHub.ViewModels  5      'clr-namespace:FashionHub
FashionHub  5      &     FashionHub.Views.ItemDetailsPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :
components#clr-namespace:FashionHub.Components     5      2vm#clr-namespace:FashionHub.ViewModels     5      +modelsclr-namespace:FashionHub     5        N� Title$  ItemDetailsPage��5       � 	Resources 5       �� Source�� 5      $ ../Resources/Styles.xaml?�6   %     � DataContext 5      4   .FashionHub.ViewModels.ItemDetailsPageViewModel  5      .��5      � ���#252424 � RowDefinitions 5      �� 5      $
��auto �6   �� 5      $��* �6   .+�5      )   #FashionHub.Components.NavigationBar  $	��Top=�6   $��0��67   �� 5      $��1��6   $��Visible=�6    .��5      �  5      �� 5    
   $
��auto �6   �� 5!   
   $
��auto �6   �� 5"   
   $
��auto �6    � ColumnDefinitions 5$      �� 5%   
   $��2* �6   �� 5&   
   $��1* �6   .+�5)   
   ��  �� Padding$ 25q�6   $��0��6   $��0��6.   $��Center=�6;   ._�5*      �� $��0��6H   $��0��6X   ��6   �� *CurrentItem.ImagePaths+��5+      �� 5,      .R�5-      �  � Orientation$ 
Horizontal=�6   5.      ��50      �� 51      .R�52      �� 
������ $	��400��62   $	��400��6>   $��25q�6K   ._�53      �� $��UniformToFill=�6-   ��6   �� 6-   54      55      57      59      �� 5;   
   $ 50q�6   $��0��6   $��1��6,   ._�5<      �� $
��auto��6    �� Orientation$ Vertical=�6$   .+�5>      ��    StandardTextBlock#��[  6?   $��0 0 0 15q�6j   ��6   �� *CurrentItem.ShortName+6j   �� 5@      #��[  6R   $��20(�6}   $
��Bold"�6�   $��0 0 0 15q�6�   ��6   �� *CurrentItem.Price+	 �� StringFormat$	 {0} $��6�   �� 5B      $ 
Horizontal=�6   .+�5C      F� ���#FDF3FF���#FDF3FF$��1��65   a������� @B � � ��j р���j �@]� �ا ���� @fр�� @]� �@KL @f���j �ا р� ��j � z ��j 	z�5E      �� 5F      
 �� ScaleX$

 0.75Q�6#    �� ScaleY$
 0.75Q�61   5H      �� 5I      #��[  6>   $��0 0 0 15q�6i   ��6   �� *CurrentItem.Rating+6i   5J      *   $FashionHub.Components.CustomComboBox  5L         Placeholder$ Выбрать цвет��5O      $��0 0 0 20q�62      MaxItems$ 10��5P         ItemsSource 5M      �� *AvailableColors+   SelectedItem 5N      �� *SelectedColor+ �� Mode$ TwoWay=�5P        5R      $! Выбрать размер��5U      $ 10��5V       5S      �� *AvailableSizes+ 5T      �� *SelectedSize+$ TwoWay=�5V      �� 5X      $(��"Добавить в корзину��6     CustomButton#��[ 62   $
��auto��6X   $��85��6e   $��	0 40 0 40q�6q   ��5Y      �� *AddToCartCommand+�� 5[      #��[  6A   ��6   �� *CurrentItem.Description+6A   �� 5]      #��[  6j   $��0 10 0 0q�6�   ��6   �� *CurrentItem.DeliveryCountry+$(	 "Страна доставки: {0}��6�   �� 5_      #��[  6Z   $��0 10 0 0q�6�   ��6   �� *CurrentItem.Compound+$	 Состав: {0}��6�   5`      5a      �� 5c   
   $ 50 0 0 0q�6   $��1��6$   $��2��61   ._�5d      �� $ Vertical=�6   $
��Left=�6.   .+�5e      �� $��Отзывы��6    	 Title#��[ 6&   $��	0 40 0 20q�6E   �� 5g      ��6   �� *
Comments+$ OneWay=���5h      �� 5i      .R�5j      -   'FashionHub.Components.InnerShadowBorder  $	��700��61   $
��auto��6=   $��0 0 0 20q�6K      
ShadowFill �#502971   ContentBackground �#633788   ContentBorderBrush �#A76AD9   InnerContent 5n      �� 5o      $ 25q�6   ._�5p      �  5q      �� 5r      $
��auto �6/   �� 5s      $
��auto �6/    5u      �� 5v      $
��auto �6,   �� 5w      $
��auto �6,   .+�5z      �� $	��100��6M   $	��100��6Y   $��0��6f   $��0��6v   ��6"   �� *User.Profile.AvatarPath+6v   �� 5|      #��[  6<   $��40 0 0 0q�6g   $��1��6y   $��0��6�   ��6&   �� *Text+6�   �� 5~      $ 
Horizontal=�6'   $��1��6A   $��2��6Q   $��Bottom=�6b   $��40 0 0 0q�6}   .+�5      �� #��[  6[   ��6(   �� *Rate+$	 Оценка: {0}��6[   F� 5�      ���#FDF3FF���#FDF3FF$��1��6C   a������� @B � � ��j р���j �@]� �ا ���� @fр�� @]� �@KL @f���j �ا р� ��j � z ��j 	z�5�       �� 5�   "   $	
 0.9Q�61   $	 0.9Q�6>   5�      5�      5�      5�      5�      5�      5�      �� 5�      $-��'Оставить комментарий��6   #��[ 64   $	��400��6[   $��85��6g   $��	0 20 0 40q�6s   $
��Left=�6�    � Cursor$
 Hand��6�   ��5�      �� * OpenAddingCommentWindowCommand+5�      5�      �� 5�   
   $��2��6   $��2��6   $ 50 0 0 0q�62   ._�5�   
   �� $ Vertical=�6   .+�5�      �� $+��%Недавно просмотрено��6   $��0 0 0 20q�63   ��5�      �� 5�       �� 
TargetType" ��6    �� BasedOn# [ 6/   .�5�      ��  �� Property � y� �� Value$ Visible=�61    �� Triggers 5�      �� 5�       �� Binding 6"   �� *RecentlyViewedItems.Count+ �� Value 06P   .a�5�      ��  � y�$ 	Collapsed=�65   5�      5�      5�      �� 5�      ��6   �� *RecentlyViewedItems+��5�      �� 5�      .R�5�      � $ 
Horizontal=�6   5�      ��5�      �� 5�      .R�5�      '   !FashionHub.Components.ProductCard  $��	0 0 50 50q�6+   $	��250��6>   5�      5�      5�      5�      5�   	   5�      5�      5�      