   M S B A M L   `   `   `  ����N  JFamilia Call Centar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Familia_Call_Centar.ViewFamilia_Call_Centar.View  5      '    !Familia_Call_Centar.View.AddVozac   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:Familia_Call_Centar.View     5        N� Title$  AddVozac��5	      .��5      � 
������� � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   $	��640��6,   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   $	��290��6*   �� 5      $��* �6   $��40��6'   .+�5   
   �� -   5         ��Name$ imeLabel��5      $��Ime vozača��6"   $
��Left=�67   $��	87,10,0,0q�6R   $��1��6e   $��1��6r   $	��Top=�6�   �� 5   
   -   5         ��Name$ 
imeTextBox��5      $
��Left=�6&   $��26��6A   $��	87,36,0,0q�6P   $��1��6c   $��1��6p    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��120��6�   $��Center=�6�   �� 5   
   -   5        $ prezimeLabel��5      $��Prezime vozača��6&   $
��Left=�6?   $��	87,67,0,0q�6Z   $��1��6m   $��1��6z   $	��Top=�6�   �� 5   
   -   5        $ prezimeTextBox��5      $
��Left=�6*   $��26��6E   $��	87,93,0,0q�6T   $��1��6g   $��1��6t   $
 Wrap=�6�   $	��Top=�6�   $	��120��6�   $��Center=�6�   �� 5   
   -   5        $ idLabel��5      $��Identifikacijski broj��6!   $
��Left=�6A   $��
87,124,0,0q�6\   $��1��6p   $��1��6}   $	��Top=�6�   �� 5   
   -   5        $ 	idTextBox��5      $
��Left=�6%   $��26��6@   $��
87,150,0,0q�6O   $��1��6c   $��1��6p   $
 Wrap=�6�   $	��Top=�6�   $	��120��6�   $��Center=�6�   �� 5   
   -   5        $ passwordLabel��5      $��Lozinka��6'   $
��Left=�69   $��
87,181,0,0q�6T   $��1��6h   $��1��6u   $	��Top=�6�   G� 5   
   -   5         G�Name$ passwordTextBox��5      $
��Left=�6/   $��26��6J   $��
87,207,0,0q�6Y   $��1��6m   $��1��6z   $	��Top=�6�   $	��120��6�   $��Center=�6�   �� 5    
   -	   5        $ passwordLabel_Copy��5       $��Potvrdite lozinku��6,   $
��Left=�6H   $��
87,238,0,0q�6c   $��1��6w   $��1��6�   $	��Top=�6�   G� 5!   
   -
   5        $ passwordTextBox_Copy��5!      $
��Left=�64   $��26��6O   $��
87,264,0,0q�6^   $��1��6r   $��1��6   $	��Top=�6�   $	��120��6�   $��Center=�6�   �� 5#   
   -   5         ��Name$ dodajButton��5#      $��Dodaj vozača��6&   $
��Left=�6=   $��	528,4,0,0q�6X   $��2��6k   $��1��6x   $	��Top=�6�   $��75��6�   $��26��6�   ���#0844A6
�������6�   �� 5$   
   -   5        $ obrisiButton��5$      $��Obriši��6'   $
��Left=�68   $��	432,4,0,0q�6S   $��2��6f   $��1��6t   $	��Top=�6�   $��75��6�   $��26��6�   
����ح�6�   �� 5%   
   -   5        $ nazadButton��5%      $��Nazad��6&   $
��Left=�66   $��	332,4,0,0q�6Q   $��2��6d   $��1��6q   $	��Top=�6�   $��75��6�   $��26��6�   6�   5&      5'      