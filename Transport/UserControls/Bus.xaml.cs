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
    /// Логика взаимодействия для Bus.xaml
    /// </summary>
    public partial class Bus : UserControl
    {
        private List<Route> RouteList = new List<Route>();
        public Bus()
        {
            InitializeComponent();
            using (AppDbContext db = new AppDbContext())
            {
                //db.Stations.Load();
                //db.Buses.Load();
                //foreach (var item in db.Stations)
                //{
                //    MessageBox.Show(item.Name);
                //}
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
            //Bus bus = new Bus() { Number};
        }

        public object Response { get; private set; }

        private void BusInit()
        {
            try {
                using (AppDbContext db = new AppDbContext())
                {
                    Autobus bus = new Autobus()
                    {
                        Number = 1
                    };
                    db.Buses.Add(bus);
                    Trolleybus trolleybus = new Trolleybus()
                    {
                        Number = 13
                    };

                    Station station = new Station()
                    {
                        Name = "БГТУ",
                    };

                    station.Buses.Add(bus);
                    station.Trolleys.Add(trolleybus);

                    Route route = new Route()
                    {
                        Name = "Вокзал - ДС Автозаводская"
                    };
                    route.Stations.Add(station);
                    bus.Route = route; //у автобуса есть машрут
                    db.Routes.Add(route);
                    
                    db.SaveChanges();
                }
            }

            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                {
                    MessageBox.Show("Object: " + validationError.Entry.Entity.ToString() );

                    foreach (DbValidationError err in validationError.ValidationErrors)
                    {
                        MessageBox.Show(err.ErrorMessage + " ");
                    }
                }
            }
        }

        private void ShowByNumber(object sender, RoutedEventArgs e)
        {
            //BusInit();
            using (AppDbContext db = new AppDbContext())
            {
                //db.Routes.Where(p=>p.Stations.Where(c=>c.Buses.))
                List<Route> routes = new List<Route>();
                int num =  db.Buses.Where(p => p.Number.ToString() == NumberTextBox.Text).Count();
                if (num > 0)
                {
                    Autobus bus = db.Buses.Where(p => p.Number.ToString() == NumberTextBox.Text).First();
                    Route route = db.Routes.Where(p => p.Id == bus.RouteId).FirstOrDefault();
                    routes.Add(route);
                    List<Station> stations = new List<Station>();
                    foreach (var station in route.Stations)
                    {
                        stations.Add(station);
                    }
                    StationsDataGrid.Visibility = Visibility.Visible;
                    RoutesListView.ItemsSource = routes;
                    StationsDataGrid.ItemsSource = stations;
                }
                else
                {
                    MessageBox.Show("Такого автобуса не существует");
                }

            }
        }

        private void ShowByRouteName(object sender, RoutedEventArgs e)
        {
            StationsDataGrid.Visibility = Visibility.Visible;
            using (AppDbContext db = new AppDbContext())
            {
                List<Route> routes = new List<Route>();
                Route route = db.Routes.Where(p => p.Name == ComboBoxRoutes.SelectedItem.ToString()).First();
                routes.Add(route);

                List<Station> stations = new List<Station>();
                foreach (var station in route.Stations)
                {
                    stations.Add(station);
                }
                RoutesListView.ItemsSource = routes;
                StationsDataGrid.ItemsSource = stations;
            }
        }
    }
}
