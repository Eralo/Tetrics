   M S B A M L   `   `   `  ����B  >Tetrics, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:TetricsTetrics  5   	       Tetrics.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Tetrics     5   	   -   5          � Title$  	Tetrics !��5   	   $	��600��6   $	��800��6(   $	��600��5	   	   $	��600��6   
�������$��
Montserrat)�5   	   $��30(�6!   .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��auto �6   �� 5      $��* �6   ��5   
   �� 5       �� ImageSource$ Assets\Background.png��6    j� Stretch$ UniformToFill=�5      .+�5   
   (� $��1��5      $��1��5       $��0,0,0,20q�5!      .��5#      �� -   5         ��Name$ 
GameCanvas��5#      ���#101010$	��250��5%      $	��510��5&      $
}�True��5'      5(      5*      �� 5,   
   -   5         ��Name$ 	ScoreText��5,      $��0��5-      $��1��5.      $��Score: ��5/      $�� 0, 10q�50       �� TextAlignment$ Center=�51      �� 53   
   -   5        $ 	LevelText��53      $��0��54      $��0��55      $��Level: ��56      $�� 0, 10q�57      $ Center=�58      �� 5:   
   -   5        $ 	LinesText��5:      $��0��5;      $��2��5<      $��Lines: ��5=      $�� 0, 10q�5>      $ Center=�5?      �� 5A   
   $��1��6   $��0��5B      $��Center=�5C      $��Right=�5D      .+�5F      �� $
��Hold��6   $ Center=�5G      �� 5I      -   5         ��Name$ 	HoldImage��5I      $��20q�5J      $	��125��5K      5L      �� 5N   
   $��1��6   $��2��5O      $��Center=�5P      $
��Left=�5Q      .+�5S      �� $
��Next��6   $ Center=�5T      �� 5V      -   5        $ 	NextImage��5V      $��20q�5W      $	��125��5X      5Y      � 5[   
   -   5        	 �Name$	 GameOverMenu��5[      ���	#CC000000$��2��5]      $��3��5^      $y�Hidden=�5_      .+�5a      �� $��Center=�6   $��Center=�5b      .+�5d      �� $��Game Over !��6   $��48(�5e      $ Center=�5f      �� 5h      -	   5        $ FinalScoreText��5h      $��Score :��5i      $��36(�5j      $ Center=�5k      �� 5l      -
   5        $��Try again ?��5l      
���� ��$��0,20,0,0q�5n      $��5q�5o      5p      5q      5r      5s      5t      