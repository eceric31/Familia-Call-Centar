   M S B A M L   `   `   `  ����N  JFamilia Call Centar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Familia_Call_Centar.ViewFamilia_Call_Centar.View  5      %    Familia_Call_Centar.View.Vozilo   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:Familia_Call_Centar.View     5        N� Title$  Vozilo��5	      .��5      � 
������� � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   $	��640��6,   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   $	��290��6*   �� 5      $��* �6   $��40��6'   .+�5   
   �� -   5         ��Name$ idLabel��5      $$��Identifikacijska oznaka vozila��6!   $
��Left=�6J   $��	87,10,0,0q�6e   $��1��6x   $��1��6�   $	��Top=�6�   �� 5   
   -   5         ��Name$ 	idTextBox��5      $
��Left=�6%   $��26��6@   $��	87,36,0,0q�6O   $��1��6b   $��1��6o    �� TextWrapping$
 Wrap=�6   $	��Top=�6�   $	��120��6�   $��Center=�6�   �� 5   
   -   5        $ tipLabel��5      $��
Tip vozila��6"   $
��Left=�67   $��	87,67,0,0q�6R   $��1��6e   $��1��6r   $	��Top=�6�   �� 5   
   -   5        $ 
tipTextBox��5      $
��Left=�6&   $��26��6A   $��	87,93,0,0q�6P   $��1��6c   $��1��6p   $
 Wrap=�6�   $	��Top=�6�   $	��120��6�   $��Center=�6�   �� 5   
   -   5        $ nosivostLabel��5      $0��*Nosivost vozila (broj porcija po isporuci)��6'   $
��Left=�6\   $��
87,124,0,0q�6w   $��1��6�   $��1��6�   $	��Top=�6�   �� 5   
   -   5        $ nosivostTextBox��5      $
��Left=�6+   $��26��6F   $��
87,150,0,0q�6U   $��1��6i   $��1��6v   $
 Wrap=�6�   $	��Top=�6�   $	��120��6�   $��Center=�6�   �� 5   
   -   5         ��Name$ image��5      $
��Left=�6   $	��109��6:   $��
332,10,0,0q�6G   $��1��6[   $��1��6h   $	��Top=�6x   $	��145��6�   �� 5    
   -   5         ��Name$ dodajButton��5       $��Dodaj vozilo��6&   $
��Left=�6=   $��	528,4,0,0q�6X   $��2��6k   $��1��6x   $	��Top=�6�   $��75��6�   $��26��6�   ���#0844A6
�������6�   �� 5!   
   -	   5        $ obrisiButton��5!      $��Obriši��6'   $
��Left=�68   $��	432,4,0,0q�6S   $��2��6f   $��1��6t   $	��Top=�6�   $��75��6�   $��26��6�   
����ح�6�   �� 5"   
   -
   5        $ nazadButton��5"      $��Nazad��6&   $
��Left=�66   $��	332,4,0,0q�6Q   $��2��6d   $��1��6q   $	��Top=�6�   $��75��6�   $��26��6�   6�   �� 5#   
   -   5        $ addImage��5#      $��Dodaj sliku vozila��6#   $
��Left=�6@   $��
87,207,0,0q�6[   $��1��6o   $��1��6|   $	��Top=�6�   $��75��6�   $��26��6�   
����ح�$	��120��6�   6�   5$      5%      