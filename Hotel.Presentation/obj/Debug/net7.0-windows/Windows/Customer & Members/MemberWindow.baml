   M S B A M L   `   `   `  ����M  IHotel.Presentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:Hotel.Presentation.Customer___Members%Hotel.Presentation.Customer___Members  5   	   8    2Hotel.Presentation.Customer___Members.MemberWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Elocal3clr-namespace:Hotel.Presentation.Customer___Members     5   	     � Title$  MemberWindow��5   	   $	��450��6   $	��800��6+   .��5	      � $��50q�6    � ColumnDefinitions 5   
   �� 5      $
��auto �6   $	��150��6,   �� 5       � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   �� $
��Name��6   $��5q�6   $��Right=�6*   �� 5   
   $��	Birthdate��6   $��5q�6$   $��1��6/   $��Right=�6<   �� 5   
   -   5        $��namebox��5   O   $��1��6   $��5q�6"   $��Center=�6-   6O   �� 5   
   -   5        $��birthdaybox��5   \   $��1��6   $��1��6"   $��5q�6/   $��Center=�6:   $��2 0 0 0q�6�   �� 5    
   -   5        $��dateformatlabel��5    �   $��
dd/mm/yyyy��6   
������  J� Opacity$	 0.5Q�6>   $��1��6L   $��1��6Y   $��Center=�6i   $��5 0 0 0q�6�    J� IsHitTestVisible$ False��6�   6�   �� 5"   
   $��2��6   $��2��6"    �� Orientation$ 
Horizontal=�66   $��Center=�6O   $��20q�6l   .+�5$      �� -   5        $��	AddMember��5$   B   $��
Add member��6   $��5q�6*   $��10q�65   6S   �� 5%      -   5        $��SaveMembers��5%   <   $
��Save��6   $��5q�6$   $��10q�6/   6O   �� 5&      -   5        $��UpdateConfirmation��5&   >   $��Update��6   $��5q�6&   $��10q�61   6X   5(      �� 5*   
   $��Current members��6   $��3��6*   $��Center=�67   $��2��6T   &   System.Windows.Controls.DataGrid  5,   
   -   5        $��MembersGrid��5,   F   $��4��6   $��2��6       
IsReadOnly$
 True��64   6F   5.      5/      