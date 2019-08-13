using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DeepLibClient.Contols
{
    public class CommandListBox : ListBox
    {
        public CommandListBox()
        {
            base.SelectionChanged += OnSelectionChanged;
        }

        public static readonly DependencyProperty CommandProperty = 
            DependencyProperty.Register("Command", typeof(ICommand), typeof(CommandListBox));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        private static void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CommandListBox control = (CommandListBox)sender;

            if (control != null && control.Command != null)
            {
                ICommand command = control.Command;
                if (command.CanExecute(control.CommandParameter)) { command.Execute(control.CommandParameter); }
            }
        }

        public static readonly DependencyProperty CommandParameterProperty =
                DependencyProperty.Register("CommandParameter", typeof(object),typeof(CommandListBox));

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
    }
}
