   M S B A M L   `   `   `  ����E  AFashionHub, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:FashionHub.ViewModelsFashionHub.ViewModels  5      =#clr-namespace:FashionHub.ComponentsFashionHub.Components  5      (    "FashionHub.Views.AdminProductsPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2vm#clr-namespace:FashionHub.ViewModels     5      :
components#clr-namespace:FashionHub.Components     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      -   5          N�Name$  AdminPageRoot��5       N� Title$ 	AdminPage��5
       � 	Resources 5       �� Source�� 5      $! ../../Resources/Styles.xaml?�6   %    .��5      ]� ���#252424.+�5      )   #FashionHub.Components.NavigationBar  $	��Top=�6   .   (FashionHub.Components.AdminNavigationBar  5      $	��Top=�6$   �� 5      $��Товары��6    	  Title#��[  6   $��	0,10,0,10q�6=   $��Center=�6P   $	��Top=�6m   � 5      $��10q�6    � RowDefinitions 5      �� 5   
   $
��Auto �6   �� 5   
   $��* �6   .+�5      �� $
��Left=�6    �� Orientation$ 
Horizontal=�6/   .+�5   
   �� $!��Добавить товар��6   $
��10,5q�6*   $��0 0 20 0q�69   $��0��5    1     AdminButtons#��[ 6>   $	��150��6d   $��50��6p   ��6   �� *AddProductCommand+6p   �� 5"   
   $
��undo��6   #��[ 5$      $	��100��5%      $��50��6   $��0 0 20 0q�6#   $��0��5&      $
��Left=�6   ��5#      �� *UndoCommand+5&      �� 5(   
   $
��redo��6   #��[ 5*      $	��100��5+      $��50��6   $��0 0 20 0q�6#   $��0��5,      $
��Left=�6   ��5)      �� *RedoCommand+5,      5-   	   &   System.Windows.Controls.DataGrid  5/      -   5          Name$ ClothingItemsGrid��5/      $��1 ��6      AutoGenerateColumns .     CanUserAddRows .  	   
IsReadOnly	 . 
   SelectionMode$
 Single=�53        DataGridHeaderStyle   ColumnHeaderStyle# [ 56        DataGridCellStyle   	CellStyle# [ 57      ���#252424   AlternatingRowBackground �#2D2D2D��54      �� *ClothingItems+ �� SelectedItem 55      �� *SelectedItem+ �� Mode$ TwoWay=�   Columns 5:   
   0  *System.Windows.Controls.DataGridTextColumn  5;      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6      Width5  /System.Windows.Controls.DataGridLengthConverter$ 40 6I   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6+   �� *	ProductId+6I     5<      $ Короткое имя��6   $ * 6S    65   �� *	ShortName+6S     5=      $ Полное имя��6   $ * 6P    63   �� *
FullName+6P     5>      $ Категория��6   $
 auto 6O    62   �� *
Category+6O     5?      $ Цвет��6   $
 auto 6G    6-   �� *Color+6G     5@      $ Размер��6   $
 auto 6H    6/   �� *Size+6H     5A      $ Цена��6   $
 auto 6G    6-   �� *Price+6G     5B      $ Рейтинг��6   $
 auto 6K    60   �� *Rating+6K     5C      $ Состав��6   $
 auto 6L    6/   �� *
Compound+6L     5D      $# Страна доставки��6   $
 auto 6\    68   �� *DeliveryCountry+6\     5E      $8 2Количество проданных вещей��6   $	 100 6b    6C   �� *
SalesCount+6b     5F      $7 1Количество вещей на складе��6   $	 100 6`    6C   �� *
Quantity+6`   4  .System.Windows.Controls.DataGridTemplateColumn  5G      $ Действия��6#   $	 400 65      MinWidth$	 400Q�6A      CellTemplate 5H      �� 5I      .R�5J      �� $ 
Horizontal=�6   $��Center=�66   $��0 5 0 0q�6S   .+�5K      �� $ ��Редактировать��6   $	��5,0q�63   $	��5,2q�6@   #��[ 5L      ��6   �� *DataContext.EditCommand+ �� ElementName$ AdminPageRoot����6b   �� 6   �� 5M      $��Удалить��6   $	��5,0q�6-   $	��5,2q�6:   #��[ 5N   �   ��6   �� *DataContext.DeleteCommand+$ AdminPageRoot����6d   �� 6�   �� 5O      $-��'Добавить комментарий��6   $	��5,0q�6:   $	��5,2q�6G   #��[ 5P   �   ��6   �� *DataContext.AddCommentCommand+$ AdminPageRoot����6h   �� 6�   5Q      5R      5T      5V   	   5W      5X      5Y      