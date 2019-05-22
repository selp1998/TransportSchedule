using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
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
using Transport.Models;

namespace Transport.UserControls
{
    /// <summary>
    /// Логика взаимодействия для Trolley.xaml
    /// </summary>

    public partial class Trolley : UserControl
    {
        private List<Route> RouteList = new List<Route>();

        public Trolley()
        {
            InitializeComponent();
            using (AppDbContext db = new AppDbContext())
            { 
                RouteList = db.Routes.ToList();
            //RoutesDataGrid.ItemsSource = RouteList;

            var routesNamesList = new List<string>();
            foreach (var route in db.Routes)
            {
                routesNamesList.Add(route.Name);
            }
            ComboBoxRoutes.ItemsSource = routesNamesList;
            ComboBoxRoutes.SelectedIndex = 0;
            }
        }
        public object Response { get; private set; }
        private void ShowByNumber(object sender, RoutedEventArgs e)
        {
            //BusInit();
            //using (AppDbContext db = new AppDbContext())
            //{
            //    //db.Routes.Where(p=>p.Stations.Where(c=>c.Buses.))
            //    List<Route> routes = new List<Route>();
            //    List<Route> routesTrolley = new List<Route>();

            //    int num = db.Trolleys.Where(p => p.Number.ToString() == NumberTextBox.Text).Count();
            //    if (num > 0)
            //    {
            //        Trolleybus trolley = db.Trolleys.Where(p => p.Number.ToString() == NumberTextBox.Text).First();
            //        Route route = db.Routes.Where(p => p.Id == trolley.RouteId).FirstOrDefault();
            //        routesTrolley.Add(route);
            //        List<Station> stations = new List<Station>();
            //        foreach (var station in route.Stations)
            //        {
            //            stations.Add(station);
            //        }
            //        StationsDataGrid.Visibility = Visibility.Visible;
            //        RoutesListView.ItemsSource = routesTrolley;
            //        StationsDataGrid.ItemsSource = stations;
            //    }
            //    else
            //    {
            //        MessageBox.Show("Такого троллейбуса не существует");
            //    }

            //}
        }

        private void ShowByRouteName(object sender, RoutedEventArgs e)
        {
            //StationsDataGrid.Visibility = Visibility.Visible;
            //using (AppDbContext db = new AppDbContext())
            //{
            //    List<Route> routesTrolley = new List<Route>();
            //    Route route = db.Routes.Where(p => p.Name == ComboBoxRoutes.SelectedItem.ToString()).First();
            //    routesTrolley.Add(route);

            //    List<Station> stations = new List<Station>();
            //    foreach (var station in route.Stations)
            //    {
            //        stations.Add(station);
            //    }
            //    RoutesListView.ItemsSource = routesTrolley;
            //    StationsDataGrid.ItemsSource = stations;
            //}
        }
    }
}