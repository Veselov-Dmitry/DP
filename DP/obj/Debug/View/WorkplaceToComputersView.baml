   M S B A M L   `   `   `  ����=  9DP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:DP.ViewDP.View  5   	   &     DP.View.WorkplaceToComputersView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:DP.View     5   	   -   5          �Name$	  WTC��5   	    � Title$7 1Компьютеры и рабочие места��5	   	   $	��500��6,   $	��400��6<      WindowC#��[  5
   	     WSL � WindowStartupLocation# [ 5   	   .��5      � 
������� � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $
��auto �6    � ColumnDefinitions 5   
   �� 5      $	��400 �6   �� 5      $��* �6   .+�5   
   ]� $��0��6   .+�5      �� $��Пользователи��6   5      &   System.Windows.Controls.DataGrid  5   
      SelectionMode$ Single=�5         
IsReadOnly .    AutoGenerateColumns .  ��5      ��  �� Path$ WorkplaceToComputers�	 �� SelectedItem	 5      �� *SelectedWTC+
 �� Mode$
 TwoWay=� �� IsAsync .    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5       ,  &System.Windows.Controls.DataGridColumn   Header$$ ID рабочего места��6F      Width5  /System.Windows.Controls.DataGridLengthConverter$	 120 6a   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *Workplace_ID+6a     5!      $! Имя компьютера��6B   $	 110 6Z    6%   �� *
Net_Name+6Z     5"      $ ID работника��6E   $ 90 6[    6%   �� *Employee_ID+6[     5#      $ ID бюро��6C   $ 60 6T    6%   �� *	Office_ID+6T   5&      � 5'   
   $��2��6   $��Right=�6!   .+�5(      �� $��Добавить��6     ButtonC#��[ 6P   ��6(   �� *AddWrkToCompCommand+6P   �� 5)      $��Удалить��6   #��[ 6O   ��6'   �� *DelWrkToCompCommand+6O   �� 5*      $��
Выйти��6   #��[ 6F   ��6%   �� *CloseCommand+6F   5+      � 5,   
   $��0��6   $��1��6"   $��3��62   $	��500��5-      $	��400��5.      $��
-400,0,0,0q�5/      .+�50      �� ���	#90000000$	��500��6K   $	��400��6X   y�6,   �� *	Visible+._�51      �� $��99,150,70,0q�6   
���   �$��1q�6B   
������� �� CornerRadius$ 5��52      $	��Top=�53      $��Center=�54      $
��auto��6#   $
��auto��61    J� BitmapEffect 55      F� 56       F� Color$ Black��61    F� Opacity$	 0.5Q�6?    F� 	Direction$	 270Q�6M    F� ShadowDepth$	 0.7Q�6]   ._�58      ��   StackPanelC#��[ 6!   $
��auto��6F   .+�59      ��   StackPanelCV#��[ 6%   .+�5:      �� $��Список��6(   $��20��66   �� 5;      $	��190��6'   $��20��63   $��0,0,0,20q�6?   $
��Left=�6Q    �� DisplayMemberPath$ Net_Name��5=   '   ��5<   '   �� $ SourceComobox�	 5>   '   �� *SelectedComobox+� 5?      .+�5@   "   �� $��OK��6)   #��[ 6\   ��66   �� $ AddOKCommand�6\   �� 5A   "   $��Закрыть��6)   #��[ 6d   ��6;   �� $ AddCloseCommand�6d   5B      5C      5D      5E      5F      5G      5H      5I      