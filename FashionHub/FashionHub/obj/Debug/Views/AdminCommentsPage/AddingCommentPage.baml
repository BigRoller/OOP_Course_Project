   M S B A M L   `   `   `  ����E  AFashionHub, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:FashionHub.ComponentsFashionHub.Components  5      =#clr-namespace:FashionHub.ViewModelsFashionHub.ViewModels  5      'clr-namespace:FashionHub
FashionHub  5      (    "FashionHub.Views.AddingCommentPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :
components#clr-namespace:FashionHub.Components     5      2vm#clr-namespace:FashionHub.ViewModels     5      +modelsclr-namespace:FashionHub     5        � 	Resources  5       �� Source�� 5      $! ../../Resources/Styles.xaml?�6   %    .��5      � ���#252424 � RowDefinitions 5      �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��* �6   .+�5      )   #FashionHub.Components.NavigationBar  $	��Top=�6   $��0��67   �� 5      $��Center=�6+    	  Title#��[  6H   $��1��6g   $��2��6t   $��0 20 0 0q�6�   ��6   �� *	TitleText+6�   �� 5      $��2��6   $��Visible=�6    .��5      � .+�5   
   ��  �� Padding$ 50q�6   $��0��6   $��1��6,   ._�5      �� $	��500��6   $
��auto��6&    �� Orientation$ Vertical=�63   $��Stretch=�6J   .+�5      �� $ Vertical=�6   $��0 0 0 32q�60   $��Stretch=�6B   .+�5       �� $��Текст:��6     StandardTextBlock#��[ 6(   $��0 0 0 10q�6S   -   'FashionHub.Components.InnerShadowBorder  5!         
ShadowFill
 �����   ContentBackground �#252424   ContentBorderBrush
 �   �$
��auto��5%      $��30��5&      $��Stretch=�5'         InnerContent 5(      �� 5)        StandardTextBox#��[ 6:   
������ $��0q�6}   ��6   �� *Comment.Text+6}   5+      �� 5,      $
��Wrap=�5-      ���#FF6E2F$��12(�5/      $��5 2 0 0q�50      ��5,      �� *CommentErrors[Text]+50      51      �� 53      $ Vertical=�6   $��0 0 0 32q�60   $��Stretch=�6B   .+�54      �� $��Рейтинг:��6   #��[ 6*   $��0 0 0 10q�6U     55      
 ����� �#252424
 �   �$
��auto��59      $��30��5:      $��Stretch=�5;       5<      �� 5=      #��[ 6:   
������ $��0q�6}   ��6   �� *Comment.Rate+6}   5?      �� 5@      $
��Wrap=�5A      ���#FF6E2F$��12(�5C      $��5 2 0 0q�5D      ��5@      �� *CommentErrors[Rate]+5D      5E      �� 5G      $ Vertical=�6   $��0 0 0 32q�60   $��Stretch=�6B   .+�5H      �� $��Пользователь:��6   #��[ 6/   $��0 0 0 10q�6Z   *   $FashionHub.Components.CustomComboBox  5I      	   SelectedValuePath$	 UserId��5K      
   DisplayMemberPath$
 Login��5M      
������ $��0q�5O         ItemsSource 5J      �� *Users+   SelectedValue 5L      �� *Comment.UserId+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5Q      �� 5R      $
��Wrap=�5S      ���#FF6E2F$��12(�5U      $��5 2 0 0q�5V      ��5R      �� *CommentErrors[UserId]+5V      5W      �� 5Y        CustomButton#��[ 64   $��85��6Z   $
��auto��6f   $��0 40 0 0q�6s    � Cursor$
 Hand��6�   ��6   �� *
ButtonText+��5Z      �� *FinishCommand+5\      5]      5_   	   5`      5b      5c      