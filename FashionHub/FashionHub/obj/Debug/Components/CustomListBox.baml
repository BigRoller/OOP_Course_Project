   M S B A M L   `   `   `  ����E  AFashionHub, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:FashionHub.ComponentsFashionHub.Components  5      )    #FashionHub.Components.CustomListBox   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:FashionHub.Components     5        � 	Resources  5
       �� Source�� 5      $ ../Resources/Styles.xaml?�6   %    .��5      � .+�5      �� �� 5      ��5   
   �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � �� �� Value 6%   �� *SelectedSummary+ �� RelativeSource ��  �� AncestorType$ UserControlL� �� Triggers 5      ^� 5       ^� 
Conditions 5      �� 5      	 �� Binding	 6   �� *SelectedItems.Count+ �� $ UserControlL�
 �� Value
 06�   �� 5      	 6   �� *ItemsSource.Count+ �� $ UserControlL�
 06~   .0�5      ��  � ��$2 ,Нет доступных элементов��6)   �� 5       � ��
 �  ��6/   5      5      5       5"      �� 5$      -   5        ���#83AFFF �� CornerRadius$ 20��5$   "    �� Padding$ 10q�64   ._�5%      �� $��Inter)�5&      $��16(�6%   $��Center=�63   ��5%      �� *SelectedSummary+ �� $ UserControlL�5&   3   5'      � 5)      -   5         �Name$ 	ListPopup��5)      $��Bottom=�6    � 	StaysOpen .   � AllowsTransparency . $��Slide=�5*   &   .��5+      �� 
�������$ 	0,0,10,10��6"   $��1q�6;   ���#83AFFF._�5,   
   �� $	��200��6   .��5-      ��  �� SelectionMode$ Multiple=�5.   
   
�������$��0q�50   
   $��0q�51   
   ��5-      �� *ItemsSource+ �� $ UserControlL���52      �� 53      .R�54      �� ��6   ��  d� 	IsChecked 55      �� *DataContext.SelectedItems+ �� $ UserControlL� �� Mode$ TwoWay=�   ListContainsConverter �� 	Converter# [   �� ConverterParameter �� 5:      5<      5>      5?   	   5@      5A      5B      