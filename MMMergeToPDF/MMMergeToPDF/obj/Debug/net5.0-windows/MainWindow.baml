   M S B A M L   `   `   `  ????G  CMMMergeToPDF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:MMMergeToPDFMMMergeToPDF  5   	       MMMergeToPDF.MainWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:MMMergeToPDF     5   	     ? Title$  
MainWindow??5   	   $	??450??6   $	??800??6)   .??5	      ? .+?5
   
   ?? -   5         ??Name$ FolderTxtBox??5
      $
??Left=?6(   $??	31,33,0,0q?6C   $??Select Folder to Merge...??6V    ?? TextWrapping$ NoWrap=?6w   $	??Top=?6?   $	??270??6?   6?   ?? 5   
   -   5         ??Name$ RotateComboBox??5      $
??Left=?6+   $??
405,31,0,0q?6F   $	??Top=?6Z   $	??120??6r    ?? SelectedIndex$ 0??6~   .@?5      ?? $??No Rotar??6    ?? 
IsSelected$
 True??6.   ??6@   Z? ?? 5      $??Rotar Pares??6   ?? 5      $??Rotar Impares??6   5      ?? 5   
   -   5         ??Name$ 	BrowseBtn??5      $??	Browse...??6$   $
??Left=?68   $??
315,33,0,0q?6S   $	??Top=?6g   $??70??6   6?   ?? 5   
   -   5        $ StartBtn??5      $??Start??6#   $
??Left=?63   $??	31,56,0,0q?6N   $	??Top=?6a   $??56??6y   6?   ?? 5   
   -   5        $ TipoFicheros??5      $
??Left=?6)   $??
405,60,0,0q?6D   $	??Top=?6X   $	??120??6p   .@?5      ?? $??
Merge PDFs??6   $
 True??60   ?? 5      $??
Merge JPGs??6   5      5      5      