   M S B A M L   `   `   `  ����H  DEventsManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulla5clr-namespace:EventsManager.Windows.Admin_Panel.Pages'EventsManager.Windows.Admin_Panel.Pages  5      7    1EventsManager.Windows.Admin_Panel.Pages.UsersPage   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Glocal5clr-namespace:EventsManager.Windows.Admin_Panel.Pages     5      b ^MaterialDesignThemes.Wpf, Version=4.1.0.2556, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5         MaterialDesignBody#���   5	   	   $��Regular"�5
   	   $��13(�5   	   &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignFont N� 
FontFamily# �  5   	    N� Title$ 	UsersPage��5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��5* �6   .+�5   
   #  MaterialDesignThemes.Wpf.Card  $��0��6   $��5q�6.   
�������.��5      �� .+�5      �� $��Search Users��6    �� TextAlignment$ Center=�60   $
��Bold"�6G   $��3q�6Y   �� 5       -   5         ��Name$ textUsername��5       )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Username��60   6Z   �� 5"      -   5         ��Name$ listBoxUsers��5"      $��Stretch=�60   ��5#      �� 5$      .R�5&      �� 
�������$��1q�6<   	 �� Padding$	 2q�6G   $��1q�6S   
���   �._�5'   "   �� 
 �� Orientation$
 
Horizontal=�6-   $��Stretch=�6F   .+�5(   &   �� $��15(�6I   $
��Bold"�6W   $��0 0 5 0q�6i   ��60   ��  �� Path$ Id�6i   �� 5)   &   $��17(�6O   $ Center=�6]   ��60   �� $ Username�6]   5*   #   5+      5-      5/      51      52        56   
   -   5          Name$ cardMain��56      $��1��60   $��5q�6@   
�������.��58      � -   5         �Name$ gridMain��58      $y�Hidden=�6%    59      �� 5:      $��* �6'   �� 5;      $��* �6'   �� 5<      $��* �6'   .+�5?      �� $��0��6   .+�5@      �� $ Center=�6?   $��26(�6V   $
��Bold"�6d   ��6    �� $ Username�6d   �� 5B      $
 
Horizontal=�6!   .+�5C      �� $
��Id: ��6$   �� 5D      ��6$   �� $ Id�5E      �� 5G      $
 
Horizontal=�6!   .+�5H      �� $��
Is Admin: ��6$   �� 5I      ��6$   �� $ IsAdmin�5J      �� 5L      $
 
Horizontal=�6!   .+�5M      �� $��Email: ��6$   �� 5N      ��6$   �� $ Email�5O      �� 5Q      $
 
Horizontal=�6!   .+�5R      �� $��Date Created: ��6$   �� 5S      ��6$   �� $ DateCreated�5T      �� 5V      $
 
Horizontal=�6!   .+�5W      �� $��
Group Id: ��6$   �� 5X      ��6$   �� $ GroupId�5Y      5[      5^      5_      5b      5c      