   M S B A M L   `   `   `  ����  
FashionHub'    !FashionHub.Views.CustomMessageBox   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � WindowStyle$
  None=�5   	    � AllowsTransparency . 
������  � SizeToContent$ WidthAndHeight=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � 	Resources 5
       �� Source�� 5      $ ../Resources/Styles.xaml?�6   %    .��5      �  � ColumnDefinitions 5      �� 5      $	��300 �6   .+�5      �� ���#252424
���   �$��2q�5       �� CornerRadius$ 10��5       �� Padding$ 20q�5      	 J� InputBindings	 5      d� 5   
   
 d� MouseAction$
 	LeftClicke�6    �� Command 5      �� *DragMoveCommand+._�5      �� $��Center=�6   .+�5   
   ��    StandardTextBlock#��[  5      $
��Bold"�5      $��18(�5      $��Center=�5       ��5      �� *Title+5!      �� 5#   
   #��[  5$      $
��Wrap=�5%      $	��300��5&      $��0 20 0 0q�6%   ��5#      �� *	Message+5&   %   �� 5(   
    �� Orientation$ 
Horizontal=�6   $��Right=�5)      $��0 20 0 0q�5*      .+�5+      �� $��OK��6     AdminButtons#��[ 5-      $��80��5.      $��0 0 10 0q�6   ��5,      �� *CloseCommand+5.      �� 50      $��Отмена��6   #��[ 52      $��80��53      ��51      �� *CancelCommand+y�54      �� 55       �� Path$ ShowCancelButton�6     BoolToVisibilityConverter �� 	Converter# [ 60   57      58      59   	   5:      5;      5<      