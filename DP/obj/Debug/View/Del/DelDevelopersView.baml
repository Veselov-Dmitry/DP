   M S B A M L   `   `   `  ����=  9DP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DP.View.DelDP.View.Del  5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity 5   	   #    DP.View.Del.DelDevelopersView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DP.View.Del     5   	   ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5   	     � Title$-  'Удалить разработчика��5	   	   $	��260��6&      WindowC#��[  5
   	     WSL � WindowStartupLocation# [ 5   	   .��5      � 
������� � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6     StackPanelC#��[ 6"   .+�5      ��   StackPanelCV#��[ 6   .+�5      �� $-��'ID записи об изменении��6   6<   �� 5       �� 
IsReadOnly . $	��150��5      ��5      ��  �� Path$ 	Record_ID�.��5   *   5      5      �� 5      #��[ 6   .+�5      �� $)��#Таб № Разработчика��6   68   �� 5       . $	��150��5      ��5      �� $ Personnel_Number�5   ,   5      5      �� 5   
   $��1��6   #��[ 6"   .+�5       �� #��[ 6   .+�5!      �� $��Изменение��6   6/   �� 5"       �� TextWrapping$
 Wrap=�6    . $	��150��6v   $��66��6�   ��6.   �� $
 Note� �� Mode$ TwoWay=� �� IsAsync . 6�   � 5#      .+�5$      �� $/��)ID записи разработчика ��6    �� 5%      ��6    �� $ Developer_part_ID�5&      5'      �� 5(      #��[ 6   .+�5)      �� $(��"Дата начала записи��6   68   �� 5*       . $	��150��6,   ��6;   �� $ 
Date_start��� 5+      $��ID программы��6   62   �� 5,      ��6   �� $ PS_Name�$ TwoWay=� . 5-      5.      �� 5/   
   $��2��6    �� Orientation$ 
Horizontal=�6"   $��Right=�6;   .+�50      �� $��Удалить��6     ButtonC#��[ 6F   ��6'   �� *
DelCommand+6F   �� 51      $��
Выйти��6   #��[ 6F   ��6%   �� *ExitCommand+6F   52      53      54      