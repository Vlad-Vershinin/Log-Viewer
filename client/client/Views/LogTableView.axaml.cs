using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using client.ViewModels;
using ReactiveUI;
using System;

namespace client.Views;

public partial class LogTableView : UserControl
{
    public LogTableView()
    {
        InitializeComponent();
        DataContext = new LogTableViewModel();
		
        //LogGridGUI.ItemsSource = 
    }

    
}
=======
}

/*
 <DataGridCheckBoxColumn Header="Скрыть" Binding="{Binding IsHidden}" Width="0.15*"/>
					<DataGridTextColumn Header="Время" Binding="{Binding Timestamp}" Width="0.4*"/>
					<DataGridTextColumn Header="Вид лога" Binding="{Binding Level}" Width="0.2*"/>
					<DataGridTemplateColumn Header="Содержание" Binding="{Binding Message}" Width="0.5*"/>
 */


/*
 <DataGrid Background="#000a2c"  ColumnHeaderTheme="" Name="LogGridGUI" ColumnHeaderHeight="30" AutoGenerateColumns="False" HorizontalAlignment="Stretch" VerticalAlignment="Stretch" CanUserResizeColumns="True" IsEnabled="True">
				<DataGrid.Columns>
					<DataGridCheckBoxColumn Header="Скрыть" Width="0.15*"/>
					<DataGridTextColumn Header="Время" Width="0.4*"/>
					<DataGridTextColumn Header="Вид лога" Width="0.2*"/>
					<DataGridTemplateColumn Header="Содержание" Width="0.5*"/>
				</DataGrid.Columns>
			</DataGrid>
*/