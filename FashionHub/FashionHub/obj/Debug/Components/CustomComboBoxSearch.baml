   M S B A M L   `   `   `  ����E  AFashionHub, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:FashionHub.ComponentsFashionHub.Components  5      0    *FashionHub.Components.CustomComboBoxSearch   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:FashionHub.Components     5        � 	Resources  5
       �� Source�� 5      $ ../Resources/Styles.xaml?�6   %    .��5      � .+�5      �� -   5         ��Name$ 
MyComboBox��5   *   $
��Auto��6   $��50��6   $��Center=�5       �� 
IsEditable .  �� IsTextSearchEnabled .  $��Inter)�5   !   $��16(�5   !   $��Regular"�5   !   ��5      �� *ItemsSource+ �� RelativeSource ��  �� AncestorType$ UserControlL� �� SelectedItem 5      �� *SelectedItem+ �� Mode$ TwoWay=� �� $ UserControlL�	 �� SelectedValuePath	 5      �� *SelectedValuePath+ �� $ UserControlL�
 �� SelectedValue
 5      �� *SelectedValue+ �� $ UserControlL�$ TwoWay=� �� DisplayMemberPath 5      �� *DisplayMemberPath+ �� $ UserControlL�  5   
      ComboBoxToggleButton �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� CornerRadius F�Name �� Triggers �� Value �� Binding �� 
TargetName��%�  &	        �� 5      " d�6/   .�5       ��  � �� 5!      �� 5"      " d�6"   .R�5#      � .+�5$      �� $ 
MainBorder��6   $ 20��5%   )   ���#83AFFF$��0q�5'   )   ._�5(      �� $
��Left=�6)   $��Center=�5)   7   $��10,0,0,0q�5*   7   5+      �� 5-      $��Right=�6   $��0 0 20 0q�69   ._�5.      F� $ Arrow��6   
������ 
���   �$��2��51      "����        � z  z � $�     	$��Center=�53      $��Center=�54      55      56       57      �� 58      $
 True��6|    6"   �� *IsDropDownOpen+ �� $ ComboBoxL�.a�59      �� $ 
MainBorder��6    �@��CornerRadius$ 	20,20,0,0��6O   5:      �� 5;      $ False��6|    6"   �� *IsDropDownOpen+ �� $ ComboBoxL�.a�5<      �� $ 
MainBorder��6    �@��CornerRadius$ 20��6O   5=      5?      5A      5B      5C      ��5E   
   �� 5F      " ��6   .�5G      ��  � ��$	 8 4q�6(   �� 5H       � �� 5I      �� 5J      " ��6"   .R�5K      �� $ Border��6   ���#83AFFF$ 0��5N   %   ��5M   %   �� *ActualWidth+ �� $ ComboBoxL�._�5O      �� #��z��6'   $��Center=�5P   1   $	��8,4q�5Q   1   5R       5S      P� 5T       P� Property �@��IsHighlighted P� Value . . �5U      �� $ Border��6    � �� �#528EFD6I   5V      5X      5Z      5[      ��5^   
   �� 5_      " ��6   .R�5`      � .+�5a      d�  d�Name$ ToggleButton��6   #��[  5b      |�.   �� 	ClickMode$ Press=�5e       d� 	IsChecked 5d      �� *IsDropDownOpen+$ TwoWay=� �� *TemplatedParent+5f      �� 5h       ��Name$ PART_EditableTextBox��6   
������ $��0q�5k      $��	10,0,50,0q�5l      $��Center=�5m      $
��Left=�5n      |�.  �� 
IsReadOnly �� 
IsReadOnly# z 5q      ��5i      Z� y�5p      �� *
IsEditable+ �� *TemplatedParent+  BoolToVisibilityConverter �� 	Converter# [ 5q      �� 5t        ��Name$  ContentSite��6!   ! �� SelectionBoxItem#��z! 5u      " �� SelectionBoxItemTemplate#��z" 5v      #��z��5w      $��	10,0,25,0q�5x      $��Center=�5y      $
��Left=�5z      ��. y�5|      �� *
IsEditable+ �� *TemplatedParent+# [ # �� ConverterParameter$# False��� 5      $ �Name$$ Popup��6   $��Bottom=�5�      % �� IsDropDownOpen#��z% 5�      & � AllowsTransparency& . |�.  $��Slide=�5�      .��5�      �� $��0q�6   $ 	0,0,10,10��6-   ._�5�      �� ��. .��5�      �� 5�      5�      5�      5�      5�      5�   	   5�      5�      