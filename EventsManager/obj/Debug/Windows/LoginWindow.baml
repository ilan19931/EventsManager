   M S B A M L   `   `   `  ����H  DEventsManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:EventsManager.WindowsEventsManager.Windows  5   	   '    !EventsManager.Windows.LoginWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:EventsManager.Windows     5   	   b ^MaterialDesignThemes.Wpf, Version=4.1.0.2556, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	      MaterialDesignBody#���   5	   	   $��Regular"�5
   	   $��13(�5   	   &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	    � Title$ LoginWindow��5   	   $	��350��6   $	��300��6*    � WindowStyle$ 
ToolWindow=�66    � 
ResizeMode$ NoResize=�6O    � WindowStartupLocation$ CenterScreen=�6e   .��5      � .+�5   
   �� $��Login��6   $��95,24,82.6,0q�6    $	��Top=�66   $
��Bold"�6N   $��36(�6`   �� 5   
   $��
Username: ��6   $
��Left=�6%   $��
52,108,0,0q�6@   $	��Top=�6T   �� 5   
   $��
Password: ��6   $
��Left=�6%   $��
52,139,0,0q�6@   $	��Top=�6T   �� 5   
   -   5         ��Name$ buttonLogin��5      $��Login��6&   $��Center=�66   $��	0,216,0,0q�6S   $	��Top=�6f   $	��102��6~   $��28��6�   $
��Bold"�6�   $��15(�6�   6�   �� 5   
   -   5         ��Name$ textBoxUsername��5      $
��Left=�6+   $��23��6F   $��126,112,0,0q�6R    �� TextWrapping$
 Wrap=�6g   $	��Top=�6{   $	��120��6�   G� 5   
   -   5        	 G�Name$	 textBoxPassword��5      $
��Left=�6/   $��126,142,0,0q�6J   $	��Top=�6_   $	��120��6w   $��23��6�   �� 5   
   -   5        $ buttonRegister��5      $��Create New Account��6)   $
��Left=�6F   $��
52,271,0,0q�6a   $	��Top=�6u   $	��194��6�   $��28��6�   $
��Bold"�6�   6�   �� 5   
   -   5        
 ��Name$
 checkBoxRememberMe��5      $��Remember Me��6/   $
��Left=�6E   $��186,183,0,0q�6`   $	��Top=�6u   5      5      