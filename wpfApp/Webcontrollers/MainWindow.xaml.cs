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
        private IList<Data> dataList;
        private int latest = 0;
        private string choice;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void getMaxValue()
        {
            if (dataList.Count == 0)
            {
                MessageBox.Show("Empty Datalist");
            }
            else
            {
                Data lastOne = dataList.Last();
                latest = lastOne.Id;
            }
        }

        private void showData(IDataRepository data)
        {
            dataRepository = data;    
            try
            {                
                int index = latest;
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

        private void aButton_Click(object sender, RoutedEventArgs e)
        {
            choice = "A";
            isWin();
        }

        private void bButton_Click(object sender, RoutedEventArgs e)
        {
            choice = "B";
            isWin();
        }

        private void isWin()
        {
            dataRepository = new DataRepository();
            dataList = dataRepository.GetAll();
            getMaxValue();
            int index = latest;
            selectedData = selectedData = dataRepository.GetOne(index);
            showData(dataRepository);
            
            if(selectedData.A > selectedData.B)
            {
                if (choice == "A")
                {
                    resultLabel.Content = "Congratulations, you're right";
                }
                else
                {
                    resultLabel.Content = "Unfortunately, you're wrong";
                }
            }
            else if(selectedData.A < selectedData.B)
            {
                if (choice == "B")
                {
                    resultLabel.Content = "Congratulations, you're right";
                }
                else
                {
                    resultLabel.Content = "Unfortunately, you're wrong";
                }
            }
            else
            {
                resultLabel.Content = "You're unlucky, they were equal";

            }
        }

        private void showDataButton_Click(object sender, RoutedEventArgs e)
        {
            dataRepository = new DataRepository();
            dataList = dataRepository.GetAll();

            foreach (Data data in dataList)
            {
                dataListBox.Items.Add(Convert.ToString(data.Id).PadRight(20) + Convert.ToString(data.A).PadRight(20) + Convert.ToString(data.B));
            }
        }

        private void emptyButton_Click(object sender, RoutedEventArgs e)
        {
            dataListBox.Items.Clear();
        }
    }
}
