   M S B A M L   `   `   `  ����=  9DP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:DP.ViewDP.View  5   	   #    DP.View.AccesRightToUsersView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:DP.View     5   	     � Title$,  &Пользователь и права��5   	   $	��500��6&   $	��500��66   $	��500��6F      WindowC#��[  5	   	     WSL � WindowStartupLocation# [ 5
   	   .��5      � 
������� � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $
��auto �6    � ColumnDefinitions 5   
   �� 5      $	��500 �6   �� 5      $��* �6   .+�5   
   &   System.Windows.Controls.DataGrid     SelectionMode$ Single=�5         
IsReadOnly .    AutoGenerateColumns .  ��5      ��  �� Path$ AccesRightToUsers� �� SelectedItem 5      �� *SelectedARTU+	 �� Mode$	 TwoWay=�
 �� IsAsync
 .    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$! ID пользователя��6A      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 6Z   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *	User_ID+6Z     5      $) #Логин пользователя��6?   $	 125 6[    6%   �� *Login+6[     5      $* $Таб. № пользователя��6J   $	 130 6g    6%   �� *Personnel_Number+6g     5      $ Название ПС��6A   $ 85 6V    6%   �� *	PS_Name+6V     5       $ 
Права��6D   $ 50 6S    6%   �� *
Sub_System+6S   5#      � 5$   
   $��2��6   $��Right=�6!   .+�5%      �� $��Добавить��6     ButtonC#��[ 6P   ��6(   �� *AddWrkToCompCommand+6P   �� 5&      $��Удалить��6   #��[ 6O   ��6'   �� *DelWrkToCompCommand+6O   �� 5'      $��
Выйти��6   #��[ 6F   ��6%   �� *CloseCommand+6F   5(      � 5)   
   $��0��6   $��1��6"   $��3��62   $	��500��5*      $	��500��5+      $��
-500,0,0,0q�5,      .+�5-      �� ���	#90000000$	��500��6K   $	��500��6X   y�6,   �� *	Visible+._�5.      �� $��99,150,70,0q�6   
���   �$��1q�6B   
������� �� CornerRadius$ 5��5/      $	��Top=�50      $��Center=�51      $
��auto��6#   $
��auto��61    J� BitmapEffect 52      F� 53       F� Color$ Black��61    F� Opacity$	 0.5Q�6?    F� 	Direction$	 270Q�6M    F� ShadowDepth$	 0.7Q�6]   ._�55      ��   StackPanelC#��[ 6!   $
��auto��6F   .+�56      ��   StackPanelCV#��[ 6%   .+�57      �� $��Список��6(   $��20��66   �� 58      $	��190��6'   $��20��63   $��0,0,0,20q�6?   $
��Left=�6Q   ��59   '   �� $ SourceComobox� 5:   '   �� *SelectedComobox+��5;   "   �� 5<   &   .R�5=   *   � .+�5>   .   �� ��68   �� *AccesRightID+�� 5?   .   $�� ��68   �� 5@   .   ��68   �� *	PS_Name+�� 5A   .   $�� ��68   �� 5B   .   ��68   �� *
Sub_System+5C   +   5D   '   5F      � 5G      .+�5H   "   �� ��6,   �� *SelectedComobox.PS_Name+�� 5I   "   $	��   ��6,   �� 5J   "   ��6,   �� *SelectedComobox.Sub_System+5K      � 5L      .+�5M   "   �� $��OK��6)   #��[ 6\   ��66   �� $ AddOKCommand�6\   �� 5N   "   $��Закрыть��6)   #��[ 6d   ��6;   �� $ AddCloseCommand�6d   5O      5P      5Q      5R      5S      5T      5U      5V      