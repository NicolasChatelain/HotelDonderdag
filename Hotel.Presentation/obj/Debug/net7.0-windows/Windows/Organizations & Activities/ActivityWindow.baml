   M S B A M L   `   `   `  ����M  IHotel.Presentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullm;clr-namespace:Hotel.Presentation.Organizations___Activities-Hotel.Presentation.Organizations___Activities  5   	   S.clr-namespace:Hotel.Presentation.BoolConverter Hotel.Presentation.BoolConverter  5   	   '    !Hotel.Presentation.ActivityWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Mlocal;clr-namespace:Hotel.Presentation.Organizations___Activities     5   	   Alocal1.clr-namespace:Hotel.Presentation.BoolConverter     5   	     � Title$  ActivityWindow��5	   	   $	��720��6    $
��1600��6-    � 	Resources 5
      >  8Hotel.Presentation.BoolConverter.BooleanToBrushConverter   BooleanToBrush��%   &	          5   
   6)   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6     � RowDefinitions 5   
   �� 5      $
��0.3* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��activitiesgrid��5   @   $	��750��6   $��3��6"   $��1��63      AutoGenerateColumns$ False��6V      
IsReadOnly$
 True��6r      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 63   �� *Name+  5      $ Location��6%    67   �� *
Location+  5       $ Duration��6%    67   �� *
Duration+  5!      $
 Date��6%    63   �� *	Fixture+	 �� StringFormat$	 {0:dd-MM-yyyy HH:mm}��  5"      $ Discount��6%    67   �� *DiscountPercentage+4  .System.Windows.Controls.DataGridTemplateColumn  5#      $ Status��6)   
   CellTemplate
 5$      �� 5%      .R�5&      ��  ��Name$ 
StatusText��6(   ��6<   �� *
IsUpcoming+ �� 	Converter# [  ��5'   "   �� 5(   &    �� 
TargetType" ��6,   .�5)   *   ��  �� Property � �� �� Value$ Upcoming��6A    �� Triggers 5*   *   �� 5+   .    �� Binding 6:   �� *
IsUpcoming+ �� Value	 False6Y   .a�5,   2   ��  � ��$ Elapsed��6I   5-   /   5/   '   51      52      54       � ContextMenu 58      �� 59      .@�5:      m� -   5        $��SetDiscount��5:   7   $��apply discount��6   6J   m� 5;      -   5        $��UpdateActivity��5;   8   $��update activity��6   6N   m� 5<      -   5        $��RemoveActivity��5<   8   $��remove activity��6   6N   5=      5?      � 5C   
   $��1��6   $��1��6   $��3��6,   .+�5D       � -   5          �Name$ frame��5D        � NavigationUIVisibility$ Hidden=�6#   5E      �� 5G   
   $��1��6    �� Orientation$ 
Horizontal=�6%   $��Center=�6>   .+�5H      �� -   5        $��New_Activity��5H   e   $	��150��6   $��30��6!   $��10q�6-    � Cursor$
 Hand��69   $��create new activity��6G   6y   �� 5I      -   5        $��Plan_Activity��5I   h   $	��150��6   $��30��6!   $��10q�6-   $
 Hand��69   $��Plan existing activity��6G   6}   5J      � 5N   
    5O      �� 5P      $��* �6#   �� 5Q      $��* �6#   �� 5R      $��* �6#   .+�5U      �� -   5         ��Name$ SearchTextBox��5U      $	��200��5V      $��30��5W      
���<��$��Center=�5Y      $
��10 0q�5Z      $��3q�5[      $��1��5]      �� 5_      -	   5        $��searchLabel��5c      $��
Search....��5_      ���#212121$��30��5a      $��	10 7 10 0q�5b       J� IsHitTestVisible$ False��5d      $��1��5e      �� 5g      -
   5        $��search��5k      $��75��5g      $��30��5h      $��Search��5i      $��0��5j      $
 Hand��5m      �� 5o      -   5        $��	activebox��5o   Y   $��2��6   $��show elapsed��6'   $��Center=�6>   $
 Hand��6j   5q      5u      5v      