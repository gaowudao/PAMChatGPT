using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PAMChatGPT
{
    
    /// <summary>
    /// Interaction logic for BookmarkDockpaneView.xaml
    /// </summary>
    public partial class ChatGPTDockpaneView : UserControl
    {
        public ChatGPTDockpaneView()
        {
            InitializeComponent();
        }

        private ChatGPTDockpaneViewModel ViewModel => (ChatGPTDockpaneViewModel)DataContext;
        

        private void TextBoxInput_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (ViewModel.SendMessageCommand.CanExecute(null))
                {
                    ViewModel.SendMessageCommand.Execute(null);
                }
            }
        }

        //private void Window_Closed(object sender, EventArgs e)
        //{
        //    ViewModel.Closed();
        //}

    }
}
