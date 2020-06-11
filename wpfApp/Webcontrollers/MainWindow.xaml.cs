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
using Webcontrollers.Models;
using Webcontrollers.Repositories;

namespace Webcontrollers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IDataRepository dataRepository;
        private Data selectedData;


        public MainWindow()
        {
            InitializeComponent();

            dataRepository = new DataRepository();
        }

        private void showButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(indexTextBox.Text);
                selectedData = dataRepository.GetOne(index);

                this.DataContext = selectedData;
            }
            catch (Exception)
            {
                int index = 1;
                selectedData = dataRepository.GetOne(index);

                this.DataContext = selectedData;
            }
            
            
        }
    }
}
