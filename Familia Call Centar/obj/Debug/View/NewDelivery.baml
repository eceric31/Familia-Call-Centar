   M S B A M L   `   `   `  ����N  JFamilia Call Centar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Familia_Call_Centar.ViewFamilia_Call_Centar.View  5      E'clr-namespace:Familia_Call_Centar.ModelFamilia_Call_Centar.Model  5      *    $Familia_Call_Centar.View.NewDelivery   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:Familia_Call_Centar.View     5      9Model'clr-namespace:Familia_Call_Centar.Model     5        N� Title$  NewDelivery��5	      $	��360��6    � 	Resources 5         narudzbaViewSource��%   &	        �� 5   
   6   5      .��5      � 
������� � DataContext# [  6    � ShowGridLines .   J� RenderTransformOrigin$ 	0.5,0.511&�6f    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   $��0��6,   �� 5      �� 5      $
��Auto �6   $��0��6,    � RowDefinitions 5   
   �� 5      $��* �6   $��0��6'   �� 5      $��* �6   $	��153��6'   �� 5      $��* �6   $	��153��6'   �� 5      $��* �6   $��60��6'   �� 5      $��* �6   $��0��6'   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ narudzbaDataGrid��5         RowDetailsVisibilityMode$ VisibleWhenSelected=�6-   	   CanUserAddRows	 .  $��31,33,30,10q�6t   
   EnableRowVirtualization
 .    AutoGenerateColumns . $��1��6�   $��1��6�   ��6�   �� 6�   �� 5    
   -   5         ��Name$ daljeButton��5       $��Dalje��6&   $��Right=�66   $��	0,18,30,0q�6R   $��3��6e   $��1��6r   $	��Top=�6�   $��75��6�   $��26��6�   
����������#0844A66�   �� 5!   
   -   5        $ clearButton��5!      $��Očisti��6&   $��Right=�67   $��
0,18,225,0q�6S   $��3��6g   $��1��6t   $	��Top=�6�   $��75��6�   $��26��6�   
����ح�6�   �� 5"   
   -   5        $ copyAll��5"      $��Prebaci Sve��6"   $��Right=�68   
����ح�$��
0,18,127,0q�6k   $��3��6   $��1��6�   $	��Top=�6�   $��75��6�   $��26��6�   6�   �� 5#   
   -   5        $ nazadButton��5#      $��Nazad��6&   $��1��66   $��Right=�6F   $��26��6b   $��
0,18,324,0q�6q   $��3��6�   $	��Top=�6�   $��75��6�   6�   �� 5%   
   -   5         ��Name$ label��5%      $��Primljene narudžbe��6   $
��Left=�6<   $��31,2,0,0q�6W   $	��Top=�6i   $��1��6�   $��1��6�   �� 5&   
   -   5        $ label1��5&      $��Narudžbe za dostavu��6    $
��Left=�6>   $��31,7,0,0q�6Y   $	��Top=�6k   $��2��6�   $��1��6�     5(   
   -   5        $ isporukaDataGrid��5(      $��Stretch=�6.   	 .  $	��Top=�6|   $��
31,38,30,0q�6�   $��2��6�   $	��105��6�   $	��579��6�    . $��1��6�   ��6d   �� 6�   5)      5*      