   M S B A M L   `   `   `  ����  ChatClientApp1    ChatClientApp.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  Chat Client��5   	   $	��500��6   $	��800��6*   .��5      �  � RowDefinitions 5	   
   �� 5
      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��10q�6.   $��0��6:   .+�5      � .+�5      �� -   5         ��Name$ ClientNameInput��5      $	��200��63   $��30��6?   $��Center=�6K   $��0,0,10,0q�6m   �� 5       J� IsHitTestVisible$ False��6   $��Enter your name...��65   $��Center=�6O   $
��Left=�6j   $��5,0,0,0q�6�   
���������5      �� 5       �� 
TargetType" ��6    .�5      ��  �� Property � y� �� Value$ 	Collapsed=�6;    �� Triggers 5      �� 5   "   	 �� Binding	 6.   �� *Text+
 �� ElementName$
 ClientNameInput�� �� Value  6d   .a�5   &   ��  � y�$ Visible=�6C   5   #   5      5      5      �� 5       -   5         ��Name$ ConnectButton��5       $��Connect��6,   $	��100��6>   $��30��6J   6V   �� 5!      -   5        $ DisconnectButton��5!      $��
Disconnect��6/   $	��100��6D   $��30��6P   ${�False��6{   5"      �� 5&   
   -   5         ��Name$ ChatLog��5&      $��1��6$   $��10q�61   ��5'      �� 5(      .R�5)      �� $
��Wrap=�61   ��6    �� 61   5*      5,      �� 5/   
   $��2��6   $ 
Horizontal=�6"   $��10q�6;   .+�50      � .+�51      �� -   5        $ MessageInput��51      $	��600��60   $��30��6<   $��Center=�6H   $��0,0,10,0q�6j   �� 52      $ False��6   $��Type your message here...��65   $��Center=�6V   $
��Left=�6q   $��5,0,0,0q�6�   
���������53      �� 54      " ��6    .�55      ��  � y�$ 	Collapsed=�6;    56      �� 57   "   	 6.   �� *Text+$
 MessageInput��  6a   .a�58   &   ��  � y�$ Visible=�6C   59   #   5;      5=      5>      �� 5?      -   5        $ 
SendButton��5?      $	��100��6)   $��30��65   $
��Send��6A   ${�False��6i   5@      5A      5B      