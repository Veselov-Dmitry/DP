   M S B A M L   `   `   `  ����  DP    DP.View.DepartmentsView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  DepartmentsView��5       	  PageC#��[  5       J� InputBindings 5	      �� 5
   
    �� 	Modifiers$
 Ctrlf�6   
 �� Key$ F��6&    �� CommandParameter$  ��6N    �� Command 6.   �� *FindCommand+6N   �� 5   
   $
 Ctrlf�6   $ P��6&    6.   �� *PrintCommand+ 6O   ��  �� ElementName$ DG���� 5   
   $
 Ctrlf�6   $ S��6&    6.   �� *
PDFCommand+ 6M   �� $ DG���� 5   
   $ F5��6   $  ��6C    6   �� *RefreshDGCommand+6C   .��5      � 
������� � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $
��auto �6   .+�5   
   ]� $��0��6   .+�5      �� $��Отделы��6   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ DG��5   6     	DataGridC#��[ 6   	   Columns	 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 30 6G      Header$ ID��6R   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *Department_ID+6R     5      $# Название отдела��6I   $
 * 6b    6%   �� *Name_Department+6b   5      � 5   
   $��2��6   .+�5       �� $$��Добавить отдел...��6     ButtonC#��[ 6[   ��61   �� *AddDepartmentsCommand+6[   �� 5!      $.��(Редактировать отдел...��6   #��[ 6c   ��67   �� *EditDepartmentsCommand+6c   �� 5"      $"��Удалить отдел...��6   #��[ 6_   ��61   �� *DeleteDepartmentsCommand+6_   5#      �� 5$   
   $��1��6   ���	#90000000 �� CornerRadius$ 10��65   $��0,0,0,50q�6G   $	��100��5%   4   $��20��6C   $��Bottom=�6R   $��Right=�6m   y�6   �� *VisibleFind+._�5&      �  � Orientation$ Vertical=�6   $��Right=�6/   .+�5'      � $��Right=�6   .+�5(      �� $��20��6l   $��20��6y   $��
10,10,0,10q�6�   #��[ 6�   ��6   �� *FindMinimizeMark+��6B   �� *MinimizeFilterCommand+6�   �� 5)      $��X��6   $��20��6O   $��20��6\   #��[ 6k   ��6)   �� *ExitFilterCommand+6k   5*      � 5+      $ Vertical=�6   y�63   �� *VisibleFindMinimize+.+�5,      � .+�5-      �� $,��&ID ( 0 - не учитывается )��6%   $	��150��6F   ���	#90000000$��10,0,0,0q�6i   
�������6{   �� 5.      
�������$	��150��6�   $��0,0,10,0q�6�   ��6=   ��  �� Path$ FindRule.Department_ID� �� Mode$ TwoWay=� �� IsAsync . 6�   5/      � 50      .+�51      �� $#��Название отдела��6%   $	��150��6<   ���	#90000000$��10,0,0,0q�6_   
�������6q   �� 52      
�������$	��150��6�   $��0,0,10,0q�6�   ��6=   �� $ FindRule.Name_Department�$ TwoWay=� . 6�   53      � 54      $��Right=�6    .+�55      �� $��
Поиск��6!   #��[ 6S   ��61   �� *FilterCommand+6S   56      57      58      59      5:      5;      