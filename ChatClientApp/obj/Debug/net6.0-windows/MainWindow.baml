   M S B A M L   `   `   `  ����  ChatClientApp    ChatClientApp.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  Chat Client��5   	   $	��500��6   $	��800��6*   .��5      �  � RowDefinitions 5	   
   �� 5
      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��10q�6.   $��0��6:   .+�5      �� -   5         ��Name$ ColorPicker��5      $	��100��6,   $��30��68   $��0,0,10,0q�6D   .@�5      �� $��Black��6    �� 
IsSelected$
 True��6/   �� 5      $	��Red��6   �� 5      $��Green��6   �� 5      $
��Blue��6   �� 5      $��Yellow��6   5      � 5      .+�5      �� -   5         ��Name$ ClientNameInput��5      $	��200��63   $��30��6?   $��Center=�6K   $��0,0,10,0q�6m   �� 5       J� IsHitTestVisible$ False��6   $��Enter your name...��65   $��Center=�6O   $
��Left=�6j   $��5,0,0,0q�6�   
���������5      �� 5       �� 
TargetType" ��6    .�5      ��  �� Property � y�	 �� Value$	 	Collapsed=�6;   
 �� Triggers
 5      �� 5   "    �� Binding 6.   �� *Text+ �� ElementName$ ClientNameInput�� �� Value  6d   .a�5    &   ��  � y�$	 Visible=�6C   5!   #   5#      5%      5&      �� 5'      -   5         ��Name$ ConnectButton��5'      $��Connect��6,   $	��100��6>   $��30��6J   6V   �� 5(      -   5        $ DisconnectButton��5(      $��
Disconnect��6/   $	��100��6D   $��30��6P   ${�False��6{   5)      �� 5,   
   -   5         ��Name$ ChatLog��5,      $��1��6$   $��10q�61   ��5-      �� 5.      .R�5/      �� $
��Wrap=�69   ��6    �� *	Message+��6M   �� *Color+50      52      �� 55   
   $��2��6   $ 
Horizontal=�6"   $��10q�6;   .+�56      � .+�57      �� -   5        $ MessageInput��57      $	��600��60   $��30��6<   $��Center=�6H   $��0,0,10,0q�6j   6}   �� 58      $ False��6   $��Type your message here...��65   $��Center=�6V   $
��Left=�6q   $��5,0,0,0q�6�   
���������59      �� 5:      " ��6    .�5;      ��  � y�$	 	Collapsed=�6;   
 5<      �� 5=   "    6.   �� *Text+$ MessageInput��  6a   .a�5>   &   ��  � y�$	 Visible=�6C   5?   #   5A      5C      5D      �� 5E      -   5        $ 
SendButton��5E      $	��100��6)   $��30��65   $
��Send��6A   ${�False��6i   5F      5G      5H      