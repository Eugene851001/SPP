   M S B A M L   `   `   `  ����J  FAssemblyBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:AssemblyBrowserAssemblyBrowser  5      "    AssemblyBrowser.TreeViewPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:AssemblyBrowser     5        N� Title$  TreeViewPage��5	       � 	Resources 5
      &    AssemblyBrowser.MembersConverter   MembersConverter��%   &	          5   
   6!   5      .��5      �  � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      .+�5   
   o� $	��Top=�6   $��20��6'   .@�5      m� $
��File��6   .@�5      m� $
��Open��6    m� Command 6)   �� *OpenCommand+6K   5      5      R� 5   
   -   5         R�Name$ assemblyTree��5       $��1��6   ��66   �� *AssembliesInfo+��5      �� 5       �� ItemsSource 6+   �� *
Namespaces+.R�5      �� ��6    �� *Name+ �� ItemTemplate 5      �� 5       63   �� *	DataTypes+.R�5      �� ��6(   �� *Name+ 5       �� 5!   "    6;   �� *MembersInfo+.R�5"   &   �� ��5#   *   `� 5$   .    `� 	Converter# [  6;   .1�5%   2   ��  �� Path$
 Name�6:   �� 5&   2   $
 Type�6:   �� 5'   2   $ 
ReturnType�6:   �� 5(   2   $ 
Parameters�6:   �� 5)   2   $ IsExtension�6:   5*   /   5,   '   5-   #   5/      51      53      54      55      