   M S B A M L   `   `   `  ����N  JFamilia Call Centar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Familia_Call_Centar.ViewFamilia_Call_Centar.View  5      '    !Familia_Call_Centar.View.FoodPick   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:Familia_Call_Centar.View     5        N� Title$  FoodPick��5	      .��5      � 
������� � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� -   5        $��scroller��5      $��1��6'   $��1��67   $��Right=�6E   
�������$��1q�6t   $	��180��6�   $��0,22,0,0q�6�   $	��Top=�6�   $	��462��6�   ��. $
��Auto=�6�   .��5      � -   5         �Name$ 
scrollGrid��5      $	��300��6'   $	��462��64    � ShowGridLines .  ��.   5      �� 5      $
��Auto �6'   �� 5      $
��Auto �6'   �� 5      $
��Auto �6'   5      5       �� 5!   
   -   5         ��Name$ opisJela��5!      $
��Left=�6(   $	��100��6C   $��1��6P   $��1��6`   $��151,219,0,0q�6m   $	��Top=�6�   $	��200��6�   . �5"      /� .T�5#      J� .*�6   .*�5$      �� 5%      5&      5'      �� 5(   
   -   5         ��Name$ 
stanjeJela��5(      $
��Left=�6+   $��1��6F   $��1��6S   $	��253��6c   $��	10,56,0,0q�6p   $	��Top=�6�   $	��136��6�   ��. $
��Auto=�6�   .��5)      � -   5        $ stanjeJelaGrid��5)      $	��262��6+   $	��136��68   ��.   .   5*      �� 5+      $
��Auto �6'   �� 5,      $
��Auto �6'   5.      5/      �� 50   
   -   5         ��Name$ button��50      $��Spasi��6!   ���#0844A6
�������$��1��6Y   $��1��6i   $
��Left=�6v   $��538,293,0,0q�6�   $	��Top=�6�   $��75��6�   $��26��6�   6�   �� 51   
   -   5        $ clear��51      $��Očisti��6    $
��Left=�61   $��442,293,0,0q�6L   $��1��6a   $��1��6q   $	��Top=�6~   $��75��6�   $��26��6�   6�   �� 52   
   -   5         ��Name$ jeloL��52      $
��Jelo��6   $��Center=�6.   $��1��6R   $
��Left=�6b   $��	10,25,0,0q�6}   $��1��6�   $	��Top=�6�   $��47��6�   �� 53   
   -	   5        $ 	kolicinaL��53      $��	Količina��6#   $��1��66   $��Center=�6F   $
��Left=�6j   $��	62,25,0,0q�6�   $��1��6�   $	��Top=�6�   	 J� RenderTransformOrigin$	 0.481,0.507&�6�   $��65��6�   55      56      