   M S B A M L   `   `   `  ����D  @CNMessage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:CNMessage	CNMessage  5          CNMessage.ChatPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:CNMessage     5      .��5	      �   � ColumnDefinitions  5
   
   �� 5      $��3* �6   �� 5      $��2* �6   .+�5   
   �  � RowDefinitions 5      �� 5      $��* �6    �� 5      $��* �6    �� 5      $
��Auto �6    �� 5      $��6* �6    .+�5      � $��0��6     5      �� 5      $
��Auto �6'   �� 5      $��* �6'   �� 5      $
��Auto �6'   .+�5      �� $��0��6   $��To Username: ��6,   $��20(�6A   $��8q�6O   $��Center=�6Z   $��Center=�6w   �� 5      -   5        $��
TargetUser��5      $��1��6,   $��8q�6<    �� TextWrapping$ NoWrap=�6G    �� AcceptsReturn .  6]   �� 5      -   5        $��2��5      $��Logout��6)   $��20(�6:   $��8q�6H   6S   5      �� 5      $��2��6   �� 5       $��3��6   $��My Messages��6$   $��20(�69   .��5!      �� -   5        $��ChatBox��5!      $��10q�6)    �� 
IsReadOnly . 65   5"      � 5#      $��1��6     5$      �� 5%      $
��Auto �6'   �� 5&      $��* �6'   �� 5'      $
��Auto �6'   �� 5(      $
��Auto �6'   .+�5*      �� $��0��6   $��	Message: ��6,   $��20(�6=   $��8q�6K   $��Center=�6V   $��Center=�6s   �� 5+      -   5        $��PreparetoSend��5+      $��1��6/   $��8q�6?   $ NoWrap=�6J    .  6`   �� 5,      -   5        $��2��5,      $
��Send��6)   $��20(�68   $��8q�6F   {�6Q   �� *IsChatNotEmpty+6v   �� 5-      -   5        $��3��5-      $
��File��6)   $��20(�68   $��8q�6F   6Q   5.      5/      �� 50   
   $��1��6   $��Received Files��6#   $��20(�6;   .��51      &   System.Windows.Controls.DataGrid  -   5        $��	FilesGrid��51      $��10q�6(      HeadersVisibility$ Column=�64   
�������   AutoGenerateColumns .     SelectionMode$ Single=�52   3      CanUserResizeRows .  	   GridLinesVisibility$
	 None=�6d   
   Columns
 53      0  *System.Windows.Controls.DataGridTextColumn  54      ,  &System.Windows.Controls.DataGridColumn   Header$ Sender��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ 4* 69      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6D   �� *Sender+6_     55      $ Filename��6)   $ 7* 6;    .  6F   �� *
Filename+6c     56      $
 Size��6)   $ 3* 67    .  6B   �� *Size+6[   58      59      5:      5;      