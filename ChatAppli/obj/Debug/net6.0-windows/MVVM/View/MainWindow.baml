   M S B A M L   `   `   `  ����  	ChatAppli    ChatAppli.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Chat Server��5   	   $	��450��6   $	��800��6*   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��200 �6   �� 5	      .+�5   
   ]� $��0��6   .+�5      �� -   5         ��Name$ ConnectionStatus��5      $��25��5      $	��Top=�5       �� 
IsReadOnly$
 True��5      
�������$��Not Connected��5      $��Stretch=�5      �� 5      $��Connected Clients:��6   $��5,5,0,0q�5      $	��Top=�5      $
��Bold"�5      �� 5      -   5         ��Name$ 
ClientList��5      $	��Top=�5      $	��150��5      $��5,0,5,0q�5      �� 5      -   5         ��Name$ ClientCount��5      $��Total Clients: 0��5      $��5,5,0,0q�5      $	��Top=�5       �� 5!      -   5        $��25��5!      $	��Top=�5"      $��Start Server��5#      5$      5%      �� 5(   
   $��1��6   .+�5*      �� -   5         ��Name$ ServerChatHistory��5*      $	��290��5+      $��5q�5,      ��5-      �� 5.      .R�5/      �� $
��Wrap=�50   $   ��5/   $   �� *	Message+��51   $   �� *Color+52      54      � 57      $��5q�6   .+�58      �� -   5        $ ServerMessageInput��58      $��55��59      $��Center=�5:      $��0,0,80,0q�5;      
���   �$�� ��5=      5>      �� 5@       J� IsHitTestVisible$ False��6   $��Type a message...��5A      $��Center=�5B      $
��Left=�5C      $��20,0,0,0q�5D      
���������5F      �� 5G       �� 
TargetType" ��6    .�5H      �� 	 �� Property	 � y�
 �� Value$
 	Collapsed=�6;    �� Triggers 5I      �� 5J   "    �� Binding 6.   �� *Text+ �� ElementName$ ServerMessageInput�� �� Value  6g   .a�5K   &   �� 	 � y�$
 Visible=�6C   5L   #   5N      5P      �� 5R      -   5        $
��Send��5R      $��	515,0,0,0q�6E   5S      �� 5T      -   5         ��Name$ ServerColorPicker��5T      $	��130��62   $��30��6>    J� RenderTransformOrigin$ 0.504,0.588&�6J   .@�5U      �� $��Black��6    �� 
IsSelected$
 True��6/   �� 5V      $	��Red��6   �� 5W      $��Green��6   �� 5X      $
��Blue��6   �� 5Y      $��Yellow��6   5Z      5[      5\      5]      