   M S B A M L   `   `   `  ����  QuizApp    QuizApp.View.ResultView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Quiz Result��5   	    � WindowStartupLocation$ CenterScreen=�5   	   $	��640��5   	   $	��360��6   .��5      � ���	#FF271140 � RowDefinitions 5   
   �� 5	      $
��Auto �6   �� 5
      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��20,20,20,10q�6"   $��Center=�67   .+�5      �� -   5         ��Name$ ResultMessageTextBlock��5      $��24(�5      $
��Bold"�5      
��� ��� �� TextAlignment$ Center=�5      �� 5      -   5         ��Name$ 
ResultIcon��5      $	��100��5      $	��100��5      $��0,10,0,0q�5      $��Center=�5      5      �� 5   
   $��1��6   $��20q�6"   $��Center=�6.   .+�5      �� -   5        $ CorrectAnswersTextBlock��5      $��18(�5      
�������$ Center=�5      �� 5       -   5        $ TotalTimeTextBlock��5       $��18(�5!      
�������$ Center=�5#      $��0,10,0,0q�5$      5%      �� 5'   
   $��2��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   $��	0,20,0,20q�6X   .+�5(      �� -   5         ��Name$ PlayAgainButton��5(      $��
Play Again��5)      $	��100��5*      $��40��5+      $��16(�5,      $
��Bold"�5-      
��� � �
�������$��5q�50      51      �� 52      -   5        $ ReviewButton��52      $��Review��53      $	��100��54      $��40��55      $��16(�56      $
��Bold"�57      
���   �
�������$��5q�5:      5;      �� 5<      -   5        $ 
ExitButton��5<      $
��Exit��5=      $	��100��5>      $��40��5?      $��16(�5@      $
��Bold"�5A      
���  ��
�������$��5q�5D      5E      5F      5G      5H      