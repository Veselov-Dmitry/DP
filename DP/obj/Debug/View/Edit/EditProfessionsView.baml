   M S B A M L   `   `   `  ����=  9DP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:DP.View.EditDP.View.Edit  5   	   &     DP.View.Edit.EditProfessionsView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:DP.View.Edit     5   	     � Title$3  -Редактировать профессию��5   	   $
��auto��6)      WindowC#��[  5	   	     WSL � WindowStartupLocation# [ 5
   	   .��5      � 
������� � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6     StackPanelC#��[ 6"   .+�5      ��   StackPanelCV#��[ 6   .+�5      �� $)��#Название профессии��6   68   �� 5      $	��150��6A   $
��Left=�6M   ��6   ��  �� Path$ Name_Profession�6M   � 5      .+�5      �� $	��ID ��6    �� 5      ��6    �� $ Profession_ID�5      5      5      �� 5   
   $��2��6    �� Orientation$ 
Horizontal=�6"   $��Right=�6;   .+�5      �� $��Сохранить��6     ButtonC#��[ 6I   ��6)   �� *SaveCommand+6I   �� 5      $��
Выйти��6   #��[ 6F   ��6%   �� *ExitCommand+6F   5      5      5       