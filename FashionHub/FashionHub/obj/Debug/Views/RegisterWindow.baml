   M S B A M L   `   `   `  ����E  AFashionHub, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:FashionHub.ComponentsFashionHub.Components  5   	   =#clr-namespace:FashionHub.ViewModelsFashionHub.ViewModels  5   	   %    FashionHub.Views.RegisterWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:FashionHub.Components     5   	   2vm#clr-namespace:FashionHub.ViewModels     5   	     � Title$  RegisterWindow��5	   	   $	��700��6    $	��600��6-    � WindowStyle$
 None=�5
   	    � AllowsTransparency .  � WindowStartupLocation$ CenterScreen=�5   	    � 	Resources 5       �� Source�� 5      $ ../Resources/Styles.xaml?�6   %     � DataContext 5      3   -FashionHub.ViewModels.RegisterWindowViewModel  5      .��5      �� 
���   �$��3q�6    J� InputBindings 5      d� 5       d� MouseAction$ 	LeftClicke�5      	 �� Command	 5      �� *DragMoveCommand+._�5      � ���#252424.+�5      �� .+�5   
   � 
 � ColumnDefinitions
 5      �� 5       $��* �6   .+�5#      �� $��Регистрация��5$   	   $��0��5%   	   $��Center=�5&   	    	  Title#��[  5'   	   $��	0 32 0 50q�5(   	   �� 5*      $"��../Resources/icons/close.png��5+   	   $��40��5,   	   $��40��5-   	   $��0��5.   	   $��Right=�5/   	   $��
0 16 20 16q�50   	    J� Opacity$
 0.25Q�51   	    � Cursor$
 Hand��52   	    53      d� 54      $ 	LeftClicke�55      	 56      �� *CloseWindowCommand+58      59      � 5;   
    � RowDefinitions 5<      �� 5=      $��* �6   .+�5?      -   'FashionHub.Components.InnerShadowBorder  $	��300��5@      $
��auto��5A         
ShadowFill �#502971   ContentBackground �#633788   ContentBorderBrush �#A76AD9$��Center=�5E      $��0��6&      InnerContent 5F      �� 5G       �� Padding$ 24q�6   ._�5H      �� .+�5J      �� $��Логин:��6     StandardTextBlock#��[ 6,   $��	0 12 0 10q�6W     5K      $	��250��5L      $��40��5M       �#A76AD9 �#633788 �#502971 5Q      �� 5R      
������ $��0q�69   $��Center=�6M    �� 
CaretBrush
 �����
���������5S      �� *	LoginName+ �� UpdateSourceTrigger$ PropertyChanged=�5U      �� 5V      ���#FF6E2F$��11(�6O   ��6   �� *
LoginError+y�6]   �� *
LoginError+  StringToVisibilityConverter �� 	Converter# [ �� 5X      $��Email:��6   #��[ 6,   $��	0 12 0 10q�6W     5Y      $	��250��5Z      $��40��5[       �#A76AD9 �#633788 �#502971 5_      �� 5`      
������ $��0q�69   $��Center=�6M   
 �����
���������5a      �� *Email+$ PropertyChanged=�5c      �� 5d      ���#FF6E2F$��11(�6O   ��6   �� *
EmailError+y�6^   �� *
EmailError+# [ �� 5f      $��Пароль:��6   #��[ 6-   $��	0 20 0 10q�6X     5g      $	��250��5h      $��40��5i       �#A76AD9 �#633788 �#502971 5m      G� 5n      -   5        
������ $��0q�5n   =   $��Center=�6Q    G� 
CaretBrush
 �����
�������5o      5q      �� 5r      ���#FF6E2F$��11(�6R   ��6   �� *PasswordError+y�6a   �� *PasswordError+# [ �� 5t      $&�� Повторите пароль:��6   #��[ 67   $��	0 20 0 10q�6b     5u      $	��250��5v      $��40��5w       �#A76AD9 �#633788 �#502971 5{      G� 5|      -   5        
������ $��0q�5|   =   $��Center=�6Q   
 �����
�������5}      5      �� 5�      ���#FF6E2F$��11(�6X   ��6   �� *RepeatPasswordError+y�6g   �� *RepeatPasswordError+# [ �� 5�        AdminButtons#��[ 6   $
��auto��6A   $��50��6N   $��0q�6Z   $��0 20 0 0q�6n   $*��$Зарегистрироваться��5�      $��12(�64   ��6C   �� *RegisterCommand+�� 5�      $��
Войти��6   #��[ 6+   $��12(�6V   ���#83AFFF$��Right=�5�   1   $
 Hand��6M    5�      d� 5�      $ 	LeftClicke�6%   	 5�      �� *OpenLoginCommand+5�      5�      5�      5�      5�      5�   	   5�      5�      5�      