   M S B A M L   `   `   `  ����E  AFashionHub, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:FashionHub.ComponentsFashionHub.Components  5      =#clr-namespace:FashionHub.ViewModelsFashionHub.ViewModels  5      c _GMap.NET.WindowsPresentation, Version=2.1.7.0, Culture=neutral, PublicKeyToken=b85b9027b614afefqPclr-namespace:GMap.NET.WindowsPresentation;assembly=GMap.NET.WindowsPresentationGMap.NET.WindowsPresentation 5	      '    !FashionHub.Views.OrderCourierPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :
components#clr-namespace:FashionHub.Components     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a?interactivity+http://schemas.microsoft.com/xaml/behaviors  5      ,vm#clr-namespace:FashionHub.ViewModels  5      [gmapPclr-namespace:GMap.NET.WindowsPresentation;assembly=GMap.NET.WindowsPresentation  5	        N� Title$  OrderPointPage��5       � 	Resources 5       �� Source�� 5      $ ../Resources/Styles.xaml?�6   %    .��5      �  � ColumnDefinitions 5      �� 5      $��1* �6   �� 5      $��2* �6    � RowDefinitions 5      �� 5      $	��100 �6   �� 5      $��* �6   .+�5      )   #FashionHub.Components.NavigationBar  $��0��6   $��2��6,   �� 5      $��0��6   $��1��6    �� Padding$ 
80 40 40 0q�6*   ���#252424._�5       �� $��Hidden=�6   .��5!   
   ��  �� Orientation$ Vertical=�6   .+�5"   
   �� $#��Способ доставки��6    	  Title#��[  6+   �� 5$   
   $ 
Horizontal=�6   $��	0 18 0 30q�6.   .+�5%      �� $��Пункт выдачи��6   #��[  6*   $��20(�6I   ���#83AFFF$��0 0 20 0q�6l   *  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5&      +  %Microsoft.Xaml.Behaviors.EventTrigger  5'         	EventName$ MouseLeftButtonUp��6)   *  $Microsoft.Xaml.Behaviors.TriggerBase	   Actions.	 5(      2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction  
   Command
 62   �� *GoToPointPageCommand+5)      5+      �� 5.      $ Vertical=�6   $��0 0 20 0q�60   .+�5/      �� -   5         ��Name$ TitleTextBlock��5/   l   $��Курьером��6   #��[  6*   $��20(�6I   ���#83AFFF 50        51      $ MouseLeftButtonUp��6)   .	 52        
 63   �� *GoToCourierPageCommand+53      55      �� 56       �� RadiusX$ 3��6    �� RadiusY$ 3��6&   $��3��63   ���#83AFFF��6N   �� *ActualWidth+ �� ElementName$ TitleTextBlock��57      58      �� 5:   
   $%��Населённый пункт��6     StandardTextBlock#��[ 6,   $��0 0 0 10q�6W   -   'FashionHub.Components.InnerShadowBorder  5;   
      
ShadowFill
 �����   ContentBackground �#252424   ContentBorderBrush
 �   �$��Stretch=�5?      $��	 0 0 10 0q�5@         InnerContent 5A      �� 5B        StandardTextBox#��[ 6Q   
������ $��0q�6�    �� 
CaretBrush
 �����$��5 0 0 2q�6�   ��6   �� *City+ �� UpdateSourceTrigger$ PropertyChanged=�6�   5D      �� 5E      ���#FF6E2F$��12(�6U   $��5 2 0 15q�6c   ��6   �� *OrderAddressErrors[City]+6c   �� 5G   
   $��
Улица��6   #��[ 6!   $��0 0 0 10q�6L     5H   
   
 ����� �#252424
 �   �$��Stretch=�5L      $��	 0 0 10 0q�5M       5N      �� 5O      #��[ 6S   
������ $��0q�6�   
 �����$��5 0 0 2q�6�   ��6   �� *Street+$ PropertyChanged=�6�   5Q      �� 5R   
   ���#FF6E2F$��12(�6U   $��5 2 0 15q�6c   ��6   �� *OrderAddressErrors[Street]+6c   �� 5T   
   $��Дом��6   #��[ 6   $��0 0 0 10q�6J     5U   
   
 ����� �#252424
 �   �$��Stretch=�5Y      $��	 0 0 10 0q�5Z       5[      �� 5\      #��[ 6R   
������ $��0q�6�   
 �����$��5 0 0 2q�6�   ��6   �� *House+$ PropertyChanged=�6�   5^      �� 5_      ���#FF6E2F$��12(�6V   $��5 2 0 15q�6d   ��6   �� *OrderAddressErrors[House]+6d   �� 5a   
   $��Квартира��6   #��[ 6$   $��0 0 0 10q�6O     5b   
   
 ����� �#252424
 �   �$��Stretch=�5f      $��	 0 0 10 0q�5g       5h      �� 5i      #��[ 6V   
������ $��0q�6�   
 �����$��5 0 0 2q�6�   ��6   �� *	Apartment+$ PropertyChanged=�6�   5k      �� 5l      ���#FF6E2F$��12(�6Z   $��5 2 0 15q�6h   ��6   �� *OrderAddressErrors[Apartment]+6h   �� 5n   
   $+��%Комментарий курьеру��6   #��[ 6/   $��0 0 0 10q�6Z     5o   
   
 ����� �#252424
 �   �$��Stretch=�5s      $��0 0 0 5q�5t      $��	 0 0 10 0q�5u       5v      �� 5w      #��[ 6[   
������ $��0q�6�   
 �����$��5 0 0 2q�6�   ��6   �� *CourierComment+$ PropertyChanged=�6�   5y      �� 5z   
   $���}Подъезд, домофон, этаж. Мы сохраним эти данные для следующих заказов.��6   #��[ 6a   $��0 0 0 30q�5{      �� 5}   
   $��Привезти сюда��6     CustomButton#��[ 6)   $��85��6O   ��6[   �� *PlaceCourierOrderCommand+5~   	   5   	   5�      .  (GMap.NET.WindowsPresentation.GMapControl  5�      -   5          Name$ 
MapControl��5�      $��1��6+   $��1��6<   5�      5�      