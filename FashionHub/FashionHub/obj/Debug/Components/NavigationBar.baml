   M S B A M L   `   `   `  ����E  AFashionHub, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:FashionHub.ViewModelsFashionHub.ViewModels  5      =#clr-namespace:FashionHub.ViewModelsFashionHub.ViewModels  5      3clr-namespace:FashionHub.ViewsFashionHub.Views  5      )    #FashionHub.Components.NavigationBar   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      5local#clr-namespace:FashionHub.ViewModels     5      2vm#clr-namespace:FashionHub.ViewModels     5      0viewsclr-namespace:FashionHub.Views     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a?interactivity+http://schemas.microsoft.com/xaml/behaviors  5      $	��100��5        � 	Resources  5	       �� Source�� 5
      $ ../Resources/Styles.xaml?�6   %     � DataContext 5      0   *FashionHub.ViewModels.CatalogPageViewModel  5      .��5      � $��Stretch=�6	   $
��auto��6%   $	��100��62    K� ZIndex$ 1��6?   .+�5      �� ���#502971 J� OpacityMask 5      !� 5   
    !� Visual 5      �� 5      -   5         ��Name$ 
shadowMask��5      $	��100��5      
�������$��2q�5      ��5      �� *ActualWidth+ �� RelativeSource ��  �� AncestorType$
 GridL�5      5      ��5      u� 5   
   	 u� Left$	 -2Q�6   

 u� Top$
 2Q�6    u� Right$ -2Q�6&    u� Bottom$ -2Q�61   5!      �� 5#      ���#633788���#A76AD9$��2 2 0 0q�68   $ 2��6R   ._�5$      ]� $��Stretch=�6   .+�5%   
   ��  �� Orientation$ 
Horizontal=�6   $
��Left=�6.   $��Center=�6D   
������  � Cursor$
 Hand��6x   *  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5&      +  %Microsoft.Xaml.Behaviors.EventTrigger  5'         	EventName$ MouseLeftButtonUp��6)   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5(      2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     CommandParameter   FashionHub.Views.MainPage"  6V      Command 62   �� *NavigateCommand+6V   5)      .+�5+      �� $��50��6   $��50��6    �� CornerRadius$ 25��6*   
����������#4B0082 J� IsHitTestVisible .  $��2q�5,      $��Center=�6)   $��Center=�6F   $��25 0 0 0q�6a   ._�5-      �� $!��../Resources/icons/logo.png��6   $��45��69   $��45��6D   5.      �� 5/      $��Fashion Hub��6   $��30(�6)   
�������$��10q�6J   $��Center=�6V    .  6q   50      �� 52   
   $ 
Horizontal=�6   $
��Left=�6.   $��30 0 0 0q�6D   .+�53      �� $#��../Resources/icons/return.png��6   $��30��69   $��30��6D   $��0 0 15 0q�6P   $
 Hand��6b    54        55      $ MouseLeftButtonUp��6+   . 56         64   �� *DataContext.GoBackCommand+ �� $
 PageL�57      59      �� 5;      $$��../Resources/icons/forward.png��6   $��30��6:   $��30��6E   $��0 0 50 0q�6Q   $
 Hand��6c    5<        5=      $ MouseLeftButtonUp��6+   . 5>         64   �� *DataContext.GoForwardCommand+ �� $
 PageL�5?      5A      5B      �� 5D   
   $ 
Horizontal=�6   $��Right=�6.   $��Right=�6J   .+�5F      �� $"��../Resources/icons/tools.png��6   $��30��68   $��30��6C   $��0 0 50 0q�6O   $
 Hand��6a   y�5G      �� *	IsManager+   BoolToVisibilityConverter �� 	Converter# [   5H        5I      $ MouseLeftButtonUp��6+   . 5J        &    FashionHub.Views.AdminOrdersPage"  6X    64   �� *NavigateCommand+6X   5K      5M      �� 5O      $ 
Horizontal=�6   y�5P      �� *DataContext.IsCatalogPage+ �� $
 PageL�# [  .+�5R      �� $#��../Resources/icons/search.png��6   $��30��5S      $��30��6#   $��0 0 25 0q�5T      $
 Hand��5U      y�5V      �� *SearchIconVisibility+ 5W        5X      $ MouseLeftButtonUp��6-   . 5Y         66   �� *OpenSearchBlockCommand+5Z      5\      �� 5^      -   5        $ 	SearchBox��5^      $��Center=�6*   ���#633788
�������y�5_   D   �� *SearchBoxVisibility+._�5`      �� $ 
Horizontal=�6   $��Center=�66   .+�5a      �� 
������ 
������ $	��300��5b      $��60��5c      $��0 0 25 0q�5d      $��Center=�5e        SearchInput#��[ 5f       �� 
CaretBrush
 �������5h      �� *
SearchText+ �� UpdateSourceTrigger$ Default=�.��6S    5i        5j      $ KeyDown��61   . 5k         5l      �� *DataContext.SearchCommand+ �� $
 PageL� 5m      �� *Text+ �� $ TextBoxL�5n      .��5o   8   5p      5q      5r      5s      �� 5u      $!��../Resources/icons/user.png��6   $��30��67   $��30��6B   $��0 0 25 0q�6N   $
 Hand��6`   y�5v      �� *IsUserLoggedIn+# [   5w        5x      $ MouseLeftButtonUp��6+   . 5y        (   "FashionHub.Views.ProfileOrdersPage"  6X    64   �� *NavigateCommand+6X   5z      5|      �� 5}      $"��../Resources/icons/heart.png��6   $��30��68   $��30��6C   $��0,0,25,0q�6O   $
 Hand��6a   y�5~      �� *IsUserLoggedIn+# [   5        5�      $ MouseLeftButtonUp��6+   . 5�        $	   FashionHub.Views.FavoritesPage" 	 6X    64   �� *NavigateCommand+6X   5�      z�5�      T� 5�       T� X$ 0Q�6#    T� Y$ 2Q�6)   5�      �� 5�      $)��#../Resources/icons/shopping-bag.png��6   $��30��6?   $��30��6J   $
 Hand��6V   $��0 0 25 0q�6d    5�        5�      $ MouseLeftButtonUp��6+   . 5�        
   FashionHub.Views.BagPage" 
 6X    64   �� *NavigateCommand+6X   5�      5�      �� 5�      $%��../Resources/icons/category.png��6   $��30��6;   $��30��6F   $��0 0 25 0q�6R   $
 Hand��6d    5�        5�      $ MouseLeftButtonUp��6+   . 5�        "   FashionHub.Views.CatalogPage"  6X    64   �� *NavigateCommand+6X   5�      5�      �� 5�      $%��../Resources/icons/settings.png��6   $��30��6;   $��30��6F   $��0 0 50 0q�6R   $
 Hand��6d    5�        5�      $ MouseLeftButtonUp��6+   . 5�      4  .Microsoft.Xaml.Behaviors.Core.CallMethodAction     
MethodName$ OpenSettingsWindow��5�         TargetObject 5�   #   ��  �� $ UserControlL�5�      5�      5�      �� 5�       	 Login#���  6     StandardTextBlock#��[ 66   $��Center=�5�      $��0 0 42 0q�6'   $
 Hand��69    5�        5�      $ MouseLeftButtonUp��6/   . 5�        $ OpenLoginWindow��5�       5�   #   ��  �� $ UserControlL�5�      5�      5�      5�      5�   	   5�      5�      5�      