   M S B A M L   `   `   `  ����J  FPDF-Rotate-Tool, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:PDF_Rotate_ToolPDF_Rotate_Tool  5   	        PDF_Rotate_Tool.HelpWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:PDF_Rotate_Tool     5   	   -   5        $	��400��5   	   $	��450��6     � Icon$  Icons/help.ico��6"    � Title$ 帮助��68   
������ $��0q�5	   "   
������  � WindowStyle$
 None=�5
   	    J� 	AllowDrop$
 True��6    � AllowsTransparency$
 True��6-    � 
ResizeMode$ NoResize=�5   *    � WindowStartupLocation$ CenterScreen=�5   	    � ShowInTaskbar$
 True��6.   .��5      �� 
������� �� CornerRadius$ 15��6%   $	��0.5q�5      
�������._�5   
   � 
������ 	 � RowDefinitions	 5      �� 5      $��30 �6    �� 5      .+�5      � $��0��6   .+�5      �� $��Icons/help.ico��6   $��20��60   $��20��6;   $
��Left=�6G   $	��Top=�6b   $��10,4,0,0q�6z   �� 5      $��帮助��6   
������ $��12(�6>   $��30,2,0,0q�6L   $	��Top=�6^   $
��Left=�6v   �� 5      -   5        
 ��Name$
 
Bt_Minimum��5      $	��—��6-   $��12(�69   
������ $��0q�6`   $	��Top=�6t   $��Right=�6�   $��0,0,40,0q�6�   $��30��6�   $��30��6�   6�   �� 5      -   5        $
 Bt_Close��5      $	��✕��6+   $��12(�67   
������ $��0q�6^   $	��Top=�6r   $��Right=�6�   $��0,0,10,0q�6�   $��30��6�   $��30��6�   6�   5      �� 5      
������ 
�������$��	0,0,0,0.5q�6G   ${�False��6c   $��Bottom=�6u   $��Stretch=�6�   � 5      $��1��6   .+�5      �� $��Icons/logo.ico��6   $��30��60   $��30��6;   $	��Top=�6G   $
��Left=�5      $��	10,10,0,0q�63    �� BitmapScalingMode$ HighQuality=�6F   �� 5      $��PDF-Rotate-Tool©️��6   
������ $��	35,10,0,0q�6M   ���#7228FF$	��160��5       $��30��6$   $��15(�60   $��
ExtraBlack"�6>   $��Italic$�6V   $
��Left=�5!      $	��Top=�63   $��Center=�5"      $��Center=�6<   �� 5#      -   5         ��Name$ HelpInfoTbx��5#      
�������
���   �$	��0.5q�5$      
�������$	��430��5%      $	��300��6&    �� AcceptsReturn$
 True��63   $	��Top=�6H   $
��Left=�5&      $
��Left=�65   $	��Top=�5'       �� TextWrapping$
 Wrap=�69    �� 
IsReadOnly$
 True��6M   " �� VerticalScrollBarVisibility$ Visible=�5(      $��	10,50,0,0q�6@   $
��Auto=�5)      5*      5+      5,      5-      