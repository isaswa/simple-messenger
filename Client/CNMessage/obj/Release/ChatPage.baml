   M S B A M L   `   `   `  ���� D  @CNMessage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:CNMessage	CNMessage      CNMessage.ChatPage   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     )localclr-namespace:CNMessage     .���   � ColumnDefinitions  �� $��3* ��� $��2* �.+��  � RowDefinitions �� $��* ��� $��* ��� $
��Auto ��� $��6* �.+�� $��0��  �� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0��$��To Username: ��$��20(�$��8q�$��Center=�$��Center=��� -   $��
TargetUser��$��1��$��8q� �� TextWrapping$ NoWrap=� �� AcceptsReturn .  �� -   $��2��$��Logout��$��20(�$��8q��� $��2���� $��3��$��My Messages��$��20(�.���� -   $��ChatBox��$��10q� �� 
IsReadOnly . � $��1��  �� $
��Auto ��� $��* ��� $
��Auto ��� $
��Auto �.+��� $��0��$��	Message: ��$��20(�$��8q�$��Center=�$��Center=��� -   $��PreparetoSend��$��1��$��8q�$ NoWrap=� .  �� -   $��2��$
��Send��$��20(�$��8q�{��� *IsChatNotEmpty+�� -   $��3��$
��File��$��20(�$��8q��� $��1��$��Received Files��$��20(�.��&   System.Windows.Controls.DataGrid  -   $��	FilesGrid��$��10q�   HeadersVisibility$ Column=�
�������   AutoGenerateColumns .     SelectionMode$ Single=�   CanUserResizeRows .  	   GridLinesVisibility$
	 None=�
   Columns
 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Sender��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 4*    
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Sender+  $ Filename��$ 7*  .  �� *
Filename+  $
 Size��$ 3*  .  �� *Size+