   M S B A M L   `   `   `  ����=  9DP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DP.View.DelDP.View.Del  5   	   !    DP.View.Del.DelPrgLangsView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DP.View.Del     5   	     � Title$>  8Удалить язык программирования��5   	      WindowC#��[  5	   	     WSL � WindowStartupLocation# [ 5
   	   .��5      � 
������� � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6     StackPanelC#��[ 6"   $
��Left=�6G   .+�5      ��   StackPanelCV#��[ 6   .+�5      �� $C��=Название языка программирования ��6   6F   �� 5       �� 
IsReadOnly . $	��150��6L   ��6   ��  �� Path$ Prg_Lang�6Z   � 5      .+�5      �� $	��ID ��6    �� 5      $	��150��6C   ��6!   �� $ Prg_Lang_ID�6C   5      5      5      �� 5   
   $��2��6    �� Orientation$ 
Horizontal=�6"   $��Right=�6;   .+�5      �� $��Удалить��6     ButtonC#��[ 6F   ��6'   �� *
DelCommand+6F   �� 5      $��
Выйти��6   #��[ 6F   ��6%   �� *ExitCommand+6F   5      5      5       