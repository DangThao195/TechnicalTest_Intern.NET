   M S B A M L   `   `   `  ����  QuizApp    QuizApp.View.QuizView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$
  Quiz��5   	    � WindowStartupLocation$ CenterScreen=�5   	   $	��640��5   	   $	��360��6   .��5      � ���	#FF271140 � RowDefinitions 5   
   �� 5	      $
��Auto �6   �� 5
      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� -   5         ��Name$ QuestionNumberTextBlock��5      $��0��5      $��20,20,20,10q�5      $��20(�5      $
��Bold"�5      
��� ���$��
Question 1��5      �� 5   
   $��1��6   $��20q�6"   .+�5      �� -   5        $ QuestionTextBlock��5      $��18(�5      
�������$
��Wrap=�5      $��0,0,0,20q�5      	� 5      -   5         	�Name$ Answer1RadioButton��5       	� 	GroupName$ Answers��5      $��16(�5       
�������$	��0,5q�5"      5#      	� 5$      -   5        $ Answer2RadioButton��5$      $ Answers��5%      $��16(�5&      
�������$	��0,5q�5(      5)      	� 5*      -   5        $ Answer3RadioButton��5*      $ Answers��5+      $��16(�5,      
�������$	��0,5q�5.      5/      	� 50      -   5        $ Answer4RadioButton��50      $ Answers��51      $��16(�52      
�������$	��0,5q�54      55      �� 58      -   5        $ FeedbackTextBlock��58      $��16(�59      $
��Bold"�5:      $��0,10,0,0q�5;      $y�Hidden=�5<      5=      �� 5@   
   -   5         ��Name$ 
NextButton��5@      $��2��5A      $
��NEXT��5B      $	��150��5C      $��40��5D      $��18(�5E      $
��Bold"�5F      ���	#FFB30821
����������	#FF676F9D$��2q�5J      $��	0,0,20,20q�5K      $��Right=�5L      ${�False��5M       � 	Resources 5O       �� 
TargetType	 �� Property
 �� Value��%_   '��       �� 5P      " ��6   .�5Q      �� 	 �@��CornerRadius$
 10��65   5R      5S      5T      5U      5V      