   M S B A M L   `   `   `  ����=  9DP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:DP.ViewDP.View  5          DP.View.PrgSystemsView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:DP.View     5        N� Title$  PrgSystemsView��5       	  PageC#��[  5	       J� InputBindings 5
      �� 5   
    �� 	Modifiers$
 Ctrlf�6   
 �� Key$ F��6&    �� CommandParameter$  ��6N    �� Command 6.   �� *FindCommand+6N   �� 5   
   $
 Ctrlf�6   $ P��6&    6.   �� *PrintCommand+ 6O   ��  �� ElementName$ DG���� 5   
   $
 Ctrlf�6   $ S��6&    6.   �� *
PDFCommand+ 6M   �� $ DG���� 5   
   $ F5��6   $  ��6C    6   �� *RefreshDGCommand+6C   .��5      � 
������� � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $
��auto �6   .+�5   
   ]� $��0��6   .+�5      �� $)��#Програмые средства��6   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ DG��5   7     	DataGridC#��[ 6   	   Columns	 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 30 6?      Header$ ID��6J   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *PS_ID+6J     5      $) #Название программы��6A   $
 * 6]    6%   �� *	PS_Name+6]     5      $/ )Язык программирования��6B   $
 * 6a    6%   �� *
Prg_Lang+6a   5       � 5!   
   $��2��6   .+�5"      �� $,��&Добавить программу...��6     ButtonC#��[ 6^   ��65   �� *AddPrgSystemsCommand+6^   �� 5#      $6��0Редактировать программу...��6   #��[ 6d   ��6:   �� *EditPrgSystemsCommand+6d   �� 5$      $*��$Удалить программу...��6   #��[ 6`   ��64   �� *DeletePrgSystemsCommand+6`   5&      �� 5'   
   $��1��6   ���	#90000000 �� CornerRadius$ 10��65   $��0,0,0,0q�6G   $	��100��5(   4   $��20��6C   $��Bottom=�6R   $��Right=�6m   y�6   �� *VisibleFind+._�5)      �  � Orientation$ Vertical=�6   $��Right=�6/   .+�5*      � $��Right=�6   .+�5+      �� $��20��6l   $��20��6y   $��
10,10,0,10q�6�   #��[ 6�   ��6   �� *FindMinimizeMark+��6B   �� *MinimizeFilterCommand+6�   �� 5,      $��X��6   $��20��6O   $��20��6\   #��[ 6k   ��6)   �� *ExitFilterCommand+6k   5-      � 5.      $ Vertical=�6   y�63   �� *VisibleFindMinimize+.+�5/      � .+�50      �� $,��&ID ( 0 - не учитывается )��6%   $	��150��6F   ���	#90000000$��10,0,0,0q�6i   
�������6{   �� 51      
�������$	��150��6}   $��0,0,10,0q�6�   ��6=   ��  �� Path$ FindRule.PS_ID� �� Mode$ TwoWay=� �� IsAsync . 6�   52      � 53      .+�54      �� $)��#Название программы��6%   $	��150��6?   ���	#90000000$��10,0,0,0q�6b   
�������6t   �� 55      
�������$	��150��6   $��0,0,10,0q�6�   ��6=   �� $ FindRule.PS_Name�$ TwoWay=� . 6�   56      � 57      .+�58      �� $/��)Язык программирования��6%   $	��150��6B   ���	#90000000$��10,0,0,0q�6e   
�������6w   �� 59      
�������$	��150��6�   $��0,0,10,0q�6�   ��6=   �� $ FindRule.Prg_Lang�$ TwoWay=� . 6�   5:      � 5;      $��Right=�6    .+�5<      �� $��
Поиск��6!   #��[ 6S   ��61   �� *FilterCommand+6S   5=      5>      5?      5@      5A      5B      