   M S B A M L   `   `   `  ����M  IHotel.Presentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Hotel.PresentationHotel.Presentation  5   	   '    !Hotel.Presentation.CustomerWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:Hotel.Presentation     5   	     � Title$  CustomerWindow��5   	   $	��450��6    $	��800��6-   .��5	      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��2* �6   �� 5      $��* �6   �� 5      $��2* �6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $
��auto �6   .+�5   
   �� $��0��6   $��0��6   $��Id��6-   $��5q�6:   �� 5   
   $��0��6   $��2��6   $
��City��6-   $��5q�6<   �� 5   
   $��1��6   $��0��6   $
��Name��6-   $��5q�6<   �� 5   
   $��1��6   $��2��6   $	��Zip��6-   $��5q�6;   �� 5   
   $��2��6   $��0��6   $��Email��6-   $��5q�6=   �� 5    
   $��2��6   $��2��6   $��Street��6-   $��5q�6>   �� 5!   
   $��3��6   $��0��6   $��Phone��6-   $��5q�6=   �� 5"   
   $��3��6   $��2��6   $��House number��6-   $��5q�6D   �� 5$   
   -   5        $��	Idtextbox��5$   /   $��0��6   $��1��6   $��5q�6@   $��Center=�6K    �� 
IsReadOnly$
 True��6f   �� 5%   
   -   5        $��Citytextbox��5%   /   $��0��6   $��3��6   $��5q�6B   $��Center=�6M   �� 5&   
   -   5        $��Nametextbox��5&   /   $��1��6   $��1��6   $��5q�6B   $��Center=�6M   �� 5'   
   -   5        $��
Ziptextbox��5'   /   $��1��6   $��3��6   $��5q�6A   $��Center=�6L   �� 5(   
   -   5        $��Emailtextbox��5(   /   $��2��6   $��1��6   $��5q�6C   $��Center=�6N   �� 5)   
   -   5        $��Streettextbox��5)   /   $��2��6   $��3��6   $��5q�6D   $��Center=�6O   �� 5*   
   -   5        $��Phonetextbox��5*   /   $��3��6   $��1��6   $��5q�6C   $��Center=�6N   �� 5+   
   -   5        $��Housenumbertextbox��5+   /   $��3��6   $��3��6   $��5q�6I   $��Center=�6T   &   System.Windows.Controls.DataGrid  5-   
   -	   5        $��MemberDataGrid��5-   O   $��4��6   $��0��6    $��4��60   $��5q�6D    � ContextMenu 5.      �� 5/      .@�50      m� -
   5        $��Add a member��50      65   m� 51      -   5        $��Delete a member��51      68   m� 52      -   5        $��Update��52      6/   53      55      �� 56   
   -   5        $��	AddButton��56   M   $��5��6   $��0��6   $��2��6.   $��5q�6B   $	��Add��6v   �� 57   
   -   5        $��CancelButton��57   M   $��5��6   $��2��6   $��2��6.   $��5q�6B   $��Cancel��6|   59      5:      