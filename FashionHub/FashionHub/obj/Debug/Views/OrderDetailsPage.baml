   M S B A M L   `   `   `  ����E  AFashionHub, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:FashionHub.ComponentsFashionHub.Components  5      '    !FashionHub.Views.OrderDetailsPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      :
components#clr-namespace:FashionHub.Components     5        N� Title$  Order Details��5       N� 
Background
 ����� � 	Resources 5       �� Source�� 5	      $ ../Resources/Styles.xaml?�6   %    .��5      � ���#252424 � RowDefinitions 5      �� 5      $	��100 �6   �� 5      $
��auto �6   �� 5      $��* �6   .+�5      �� $*��$Информация о заказе��6    	  Title#��[  6+   $��1��6J   $��	30 50 0 0q�6W   )   #FashionHub.Components.NavigationBar  5      $��0��6   � 5      $��2��6   $��30q�6    � ColumnDefinitions 5      �� 5   
   $��2* �6   �� 5   
   $��* �6   .+�5      �� $
��Auto=�6   $��0��68   .��5   
   �� ��6   �� *
OrderItems+��5      �� 5       .R�5!      ��  �� Orientation$ Vertical=�6   5"      ��5$      �� 5%      .R�5&      1   +FashionHub.Components.ProductCardHorizontal  $
��0 10q�61   5'      5)      5*   	   -   'FashionHub.Components.InnerShadowBorder  5,      $��1��6%   $
��Auto��5-   %   $
��Auto��5.   %      
ShadowFill �#502971   ContentBackground �#633788	   ContentBorderBrush	 �#A76AD9$	��Top=�52   %   $��30 0 0 0q�53   %   
   InnerContent
 54   
   �� 55       �� Padding$ 30q�6   ._�56      �� $ Vertical=�6   $��Stretch=�60   .+�58      �� $*��$Информация о заказе��6   #��[  65   $��0 0 0 20q�6T   �� 5:        StandardTextBlock#��[ 6a   $��0 0 0 10q�6�   ��6   �� *	OrderDate+ �� StringFormat$+ %Дата заказа: {0:dd.MM.yyyy}��6�   �� 5;      #��[ 6_   $��0 0 0 10q�6�   ��6   �� *DeliveryMethod+$( "Способ доставки: {0}��6�   �� 5<      #��[ 6N   $��0 0 0 10q�6y   ��6   �� *Status+$ Статус: {0}��6y   �� 5=      #��[ 6N   $��0 0 0 10q�6y   ��6   �� *	Address+$ Адрес: {0}��6y   �� 5>      #��[ 6b   $��0 0 0 30q�6�   ��6   �� *NearestDeliveryDate+$$ Дата доставки: {0}��6�   �� 5@      $
��0 10q�6   �� 5B      #��[ 6Y   $��0 0 0 10q�6�   ��6   �� *
TotalCount+$$ Всего товаров: {0}��6�   �� 5C      #��[ 6S   $��0 0 0 30q�6~   ��6   �� *
TotalPrice+$ Итого: {0:C}��6~   �� 5E      $#��Вернуться Назад��6     CustomButton#��[ 6S   $��60��6y    � Cursor$
 Hand��6�   ��61   �� *GoBackCommand+6�   �� 5F      $!��Отменить заказ��6     SecondaryCustomButton#��[ 6W   $��60��6�   $
 Hand��6�   $��0 20 0 0q�6�   ��60   �� *CancelOrderCommand+6�   5H      5I      5K   	   5L      5M      5N      