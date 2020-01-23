﻿using System;
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
using System.Windows.Shapes;
using BE;
namespace PLWPF
{



    /// <summary>
    /// Interaction logic for HostingUnitWindow.xaml
    /// </summary>
    public partial class HostingUnitWindow : Window
    {
        private HostingUnit getHostingUnit()
        {
            return new BE.HostingUnit()
            {
                HostingUnitKey = configurition.GetHostingUnitKey(),
                Owner = new BE.Host()
                {
                    HostKey = 203376655,
                    PrivateName = "shay",
                    FamilyName = "patito",
                    phoneNumber = "0544655345",
                    MailAddress = "shay@gmail.com",
                    BankBranchDetails = new BE.BankAccunt()
                    {
                        BankNumber = 1,
                        BankName = "Leumi",
                        BranchNumber = 747,
                        BranchAddress = "Hayarkot st",
                        BranchCity = "Tel Aviv"
                    },
                    BankAccountNumber = 456789,
                    CollectionClearance = true
                },
                HostingUnitName = "negev",
                Diary = new bool[31, 12]
            };
        }

        HostingUnit hostingUnit;
        BL.IBL bl;
        IEnumerable<Guest> guests;
        IEnumerable<Order> orders;

        public HostingUnitWindow()
        {
            InitializeComponent();
            bl = BL.FactoryBL.GetBL();
            //hostingUnit = new HostingUnit();
            hostingUnit = getHostingUnit();
            bl.addHostingUnit(hostingUnit);

            guests = bl.getAllGuests();
            orders = bl.getAllOrders();



            HostingUnitGrid.DataContext = hostingUnit;
            guestListView.ItemsSource = guests;
            orderListView.ItemsSource = orders;
        }


        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void createOrder_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Guest selectedGuest = guests.ElementAtOrDefault(guestListView.SelectedIndex);
                Order order = bl.guestToOrder(selectedGuest, hostingUnit);
                bl.addOrder(order);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpDateHostingUnit(object sender, RoutedEventArgs e)
        {
            if (HostingUnitGrid.IsEnabled == false)
            {
                HostingUnitGrid.IsEnabled = true;
                upDateHostingUnit.Content = "שלח"
;
            }
            else
            {
                MessageBox.Show("הפעולה בוצעה בהצלחה", "עידכון יחידת אירוח");
                upDateHostingUnit.Content = "ערוך יחידת אירוח";
                HostingUnitGrid.IsEnabled = false;
                if (DeleteHostingUnit.IsEnabled == false)
                {
                    DeleteHostingUnit.IsEnabled = true;
                    orderTabItem.IsEnabled = true;
                    guestTabItem.IsEnabled = true;
                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("?" + "האם ברצונך למחוק יחידת אירוח", "מחיקת יחידת אירוח", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MessageBox.Show("הפעולה בוצעה בהצלחה", "מחיקת יחידת אירוח");
                    this.Close();
                    break;
                case MessageBoxResult.No:
                    MessageBox.Show("!" + "הפעולה לא בוצעה", "מחיקת יחידת אירוח");
                    break;
                case MessageBoxResult.Cancel:
                    MessageBox.Show("!" + "הפעולה לא בוצעה", "מחיקת יחידת אירוח");
                    break;
            }
        }
    }
}
