   M S B A M L   `   `   `  ����M  IHotel.Presentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Hotel.PresentationHotel.Presentation  5   	   #    Hotel.Presentation.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:Hotel.Presentation     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��2* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   �� -   5        $��SearchTextBox��5      $��0��6   $��5q�64   �� 5   
   -   5        $��SearchButton��5   .   $��0��6   $��1��6   $��5q�6B   $��Search��6M   6^   &   System.Windows.Controls.DataGrid  5   
   -   5        $��CustomerDataGrid��5   ?   $��1��6   $��2��6    $��5q�64      
IsReadOnly$
 True��6W    � ContextMenu 5      �� 5      .@�5      m� -   5        $��Add a customer��5      67   m� 5      -   5        $��Delete a customer��5      6:   m� 5      -   5        $��Update��5      6/   5      5!      5"      5#      