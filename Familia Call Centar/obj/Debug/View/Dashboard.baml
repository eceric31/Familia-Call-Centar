   M S B A M L   `   `   `  ����N  JFamilia Call Centar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Familia_Call_Centar.ViewFamilia_Call_Centar.View  5      (    "Familia_Call_Centar.View.Dashboard   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:Familia_Call_Centar.View     5        N� Title$  	Dashboard��5	      .��5      � 
������� � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   $	��640��6,   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   $	��300��6*   �� 5      $��* �6   .+�5   
   �� $
��Left=�6   $	��340��60   $��1��6@   $��1��6P   $��Center=�6]   $	��100��6x   
�������$��0,0,0,59q�6�   .+�5      �� -   5         ��Name$ pocetnaButton��5      $��Početna     >��6,   $��57��6D   �� 5      -   5        $ jelovnikButton��5      $��Jelovnik     >��6-   $��56��6F   6R   �� 5      -   5        $ narudzbeButton��5      $��Narudžbe   >��6-   $��57��6D   �� 5      -   5        $ dostaveButton��5      $��Dostave     >��6,   $��57��6D   �� 5      -   5        $ vozilaButton��5      $��Vozila       >��6+   $��57��6D   6P   �� 5      -   5        $ vozaciButton��5      $��Vozači       >��6+   $��57��6D   6P   5      �� 5    
   -   5        $ zaprimiNaruzbuButton��5       $��Zaprimi novu narudžbu��6/   $��1��6O   $��1��6_   ���#0844A6
�������$
��Left=�6�   $��105,289,0,0q�6�   $	��Top=�6�   $	��151��6�   $��41��6�   6�   �� 5!   
   -   5        $ pokreniDostavu��5!      $��Pokreni novu dostavu��6)   $��1��6H   $��1��6X   ���#0844A6
�������$
��Left=�6�   $��275,289,0,0q�6�   $	��Top=�6�   $	��151��6�   $��41��6�   6�   � 5#   
   -	   5         �Name$ InputBox��5#      $y�	Collapsed=�6!   $��1��68   $��1��6H   .+�5$      � 
���   � J� Opacity$	 0.5Q�6&   �� 5%      $	��250��5&      
����ح�
���   �$��1q�5)       �� CornerRadius$ 	0,55,0,55��5*      $��Center=�5+      $��Center=�5,      ._�5-      �� .+�5.      �� $��5q�6    $��Administratorska lozinka:��6+   $��Cambria)�6L   G� 5/      -
   5         G�Name$ InputTextBox��5/   i   $	��150��6"   $��Center=�61   $��Center=�6N   6i   �� 50       �� Orientation$ 
Horizontal=�6!   $��Center=�6:   .+�51      �� -   5        $ Confirm��51   !   $��5q�62   $��Potvrdi��6=   ��6O   Z� 6e   �� 52      -   5        $ Cancel��52   !   $��5q�61   $��Poništi��6<   ��6N   Z� 6d   53      54      55      56      �� 57   
   -   5        $ napuniSkladiste��57      $��Napuni skladište��6*   $��1��6E   $��1��6U   ���#0844A6
�������$
��Left=�6�   $��444,289,0,0q�6�   $	��Top=�6�   $	��151��6�   $��41��6�   6�   59      5:      