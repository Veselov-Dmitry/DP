   M S B A M L   `   `   `  ����=  9DP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DP.View.AddDP.View.Add  5   	   "    DP.View.Add.AddDocumentsView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DP.View.Add     5   	     � Title$%  Создать документ��5   	   $
��auto��6"      WindowC#��[  5	   	     WSL � WindowStartupLocation# [ 5
   	   .��5      � 
������� � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6     StackPanelC#��[ 6"   .+�5      ��   StackPanelCV#��[ 6   $	��170��6?   .+�5      �� $)��#Название программы��6   68   �� 5       �� DisplayMemberPath$ PS_Name��5      $
��Left=�5      $	��150��5      ��5      ��  �� Path$ Programs� �� SelectedIndex 5      �� $ SelectedProgram�5      5      �� 5      #��[ 6   .+�5      �� $��Тип документа��6   63   �� 5      $ Name_document_type��5      $	��150��5       ��5      �� $ DocumentTypes� 5      �� $ SelectedDocumentType�5    )   5!      5"      �� 5#   
   $��1��6   #��[ 6"   .+�5$      �� #��[ 6   .+�5%      �� $#��Номер документа��6   �� 5&      $	��150��6[    �� 	MaxLength$ 15��6g   $
��Left=�6v   $��22��6�   ��6   �� $ Document_Number� �� Mode$ TwoWay=� �� IsAsync . 6�   �� 5'      .+�5(      �� $��Файл��6    � 5)      $	��170��6    .+�5*      �� $	��130��6I   	 �� 
IsReadOnly	 . 
�������$��22��6}   ��6"   �� $ DocumentFilePath�6}   �� 5+      $	��...��6!   $�� ��6Q   $��20��6e   ��6/   �� *BrowseCommand+6e   �� 5,      $��X��6!   $�� ��6N   $��12��6b   $��13(�6m   
���  ����6-   �� *ClearCommand+6{   5-      �� 5.      
��� � ���6    �� $ Length�6=   5/      50      �� 51      #��[ 6   .+�52      �� $!��Дата документа��6   (  "System.Windows.Controls.DatePicker  53      $	��150��6d   
   SelectedDate
 6   �� $ Document_Date�$ TwoWay=� . 6d   54      55      �� 56   
   $��2��6    �� Orientation$ 
Horizontal=�6"   $��Right=�6;   .+�57      �� $��Сохранить��6     ButtonC#��[ 6I   ��6)   �� *SaveCommand+6I   �� 58      $��
Выйти��6   #��[ 6F   ��6%   �� *ExitCommand+6F   59      5:      5;      