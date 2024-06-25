
using BusinessObjects;
using Repository;
using System;
using System.Collections.Generic;
using System.Data;
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
using MessageBox = System.Windows.MessageBox;

namespace HotelManagementWPFApp
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {

        private int selectedCustomerId = -1;
        private int selectedRoomId = -1;

        public Admin()
        {
            InitializeComponent();
            LoadCustomer();
            LoadRoom();
        }

        public void LoadCustomer()
        {
            try
            {
                List<Customer> customers = CustomerRepository.GetAllCustomers();
                dgCustomer.ItemsSource = null;
                dgCustomer.ItemsSource = customers;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fail to load customer");
            }

        }

        public void LoadRoom()
        {
            try
            {

                List<RoomInformation> rooms = RoomRepository.GetAllRooms();
                dgRoom.ItemsSource = null;
                dgRoom.ItemsSource = rooms;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fail to load Room");
            }
        }

        private void btnDeleteCustomer_Clicked(object sender, RoutedEventArgs e)
        {
            if (selectedCustomerId == -1)   // show warning
            {
                MessageBox.Show("Please choose a customer!");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Do You want to delete?", "Confirmation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)  // don't delete
            {
                return;
            }

            CustomerRepository.DeleteCustomer(selectedCustomerId);
            selectedCustomerId = -1;
            LoadCustomer();

        }

        private void dgCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgCustomer.ItemsSource == null)
            {
                selectedCustomerId = -1;
                return;
            }
            if (dgCustomer.SelectedCells.Count == 0)
            {
                selectedCustomerId = -1;
                return;
            }

            // get dataGrid
            DataGrid dataGrid = sender as DataGrid;
            // get selected row
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dgCustomer.SelectedIndex);
            // get the cell contain the index
            DataGridCell cell = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
            string id = ((TextBlock)cell.Content).Text.ToString();
            selectedCustomerId = int.Parse(id);
        }

        private void btnAddCustomer_Clicked(object sender, RoutedEventArgs e)
        {
            AddCustomerForm form = new AddCustomerForm();
            form.SetAdminForm(this);
            form.Show();
        }

        private void btnUpdateCustomer_Clicked(object sender, RoutedEventArgs e)
        {
            if (selectedCustomerId == -1)   // show warning
            {
                MessageBox.Show("Please choose a customer!");
                return;
            }
            // get the customer
            Customer customer = CustomerRepository.GetCustomerById(selectedCustomerId);
            // display update form
            UpdateCustomer updateCustomer = new UpdateCustomer();
            updateCustomer.SetAdmin(this);
            updateCustomer.SetCustomer(customer);
            updateCustomer.Show();

        }

        private void btnSearchCustomer_Clicked(object sender, RoutedEventArgs e)
        {
            // validate the text field
            string input = txtSearchCustomer.Text;
            if (string.IsNullOrEmpty(input.Trim()))
            {
                MessageBox.Show("Please input something");
                return;
            }

            // search for a list of customer
            List<Customer> customers = CustomerRepository.GetCustomersByName(input);
            if (customers.Count == 0)
            {
                MessageBox.Show("No Customer with name = " + input);
                return;
            }

            // update customer data grid
            dgCustomer.ItemsSource = null;
            dgCustomer.ItemsSource = customers;

        }

        private void btnLoadCustomer_Clicked(object sender, RoutedEventArgs e)
        {
            LoadCustomer();
        }

        private void btnLoadRoom_Clicked(object sender, RoutedEventArgs e)
        {
            LoadRoom();
        }

        private void btnAddRoom_Clicked(object sender, RoutedEventArgs e)
        {
            // create add form
            AddRoom form = new AddRoom();
            form.SetAdmin(this);
            form.Show();
        }

        private void btnUpdateRoom_Clicked(object sender, RoutedEventArgs e)
        {
            if (selectedRoomId == -1)
            {
                MessageBox.Show("Please choose a room");
                return;
            }

            // get update Room
            RoomInformation room = RoomRepository.GetRoomById(selectedRoomId);

            // display update field
            UpdateRoom form = new UpdateRoom();
            form.SetAdmin(this);
            form.SetRoom(room);
            form.Show();

        }

        private void btnDeleteRoom_Clicked(object sender, RoutedEventArgs e)
        {
            if (selectedRoomId == -1)
            {
                MessageBox.Show("Please select a room");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Do you want to delete?", "Confirmation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
            {
                return;
            }

            // delete room
            RoomRepository.RemoveRoom(selectedRoomId);
            selectedRoomId = -1;
            LoadRoom();

        }

        private void btnSearchRoom_Clicked(object sender, RoutedEventArgs e)
        {
            // validate the text field
            string input = txtSearchRoom.Text;
            if (string.IsNullOrEmpty(input))
            {
                MessageBox.Show("Please input something");
                return;
            }
            // get rooms
            List<RoomInformation> roomInformation = RoomRepository.GetRoomsByRoomName(input);

            if (roomInformation.Count == 0)
            {
                MessageBox.Show("Found no room with number = " + input);
                return;
            }

            // add rooms to data grid room
            dgRoom.ItemsSource = null;
            dgRoom.ItemsSource = roomInformation;

        }

        private void dgRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgRoom.ItemsSource == null) // do nothing
            {
                selectedRoomId = -1;
                return;
            }

            if (dgRoom.SelectedItems.Count == 0) // do nothing
            {
                selectedRoomId = -1;
                return;
            }

            DataGrid dataGrid = (DataGrid)sender;
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            DataGridCell cell = (DataGridCell)dataGrid.Columns[0].GetCellContent(row).Parent;
            string id = ((TextBlock)cell.Content).Text.ToString();
            selectedRoomId = int.Parse(id);
        }

        private void btnLogout_Clicked(object sender, RoutedEventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }
    }
}
