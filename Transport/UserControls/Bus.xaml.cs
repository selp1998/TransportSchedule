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
                RouteList = db.Routes.ToList();

                var routesNamesList = new List<string>();
                foreach (var route in db.Routes)
                {
                    routesNamesList.Add(route.Name);
                }
                ComboBoxRoutes.ItemsSource = routesNamesList;
                ComboBoxRoutes.SelectedIndex = 0;
            }
            ShowArriveTime();
            ComboBoxBus.SelectedIndex = 0;
        }
        private void BusInit()
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    Autobus bus = new Autobus()
                    {
                        Number = 1
                    };

                    Station station = new Station()
                    {
                        Name = "Вокзал",
                    };

                    Station station2 = new Station()
                    {
                        Name = "Аранская",
                    };

                    Station station3 = new Station()
                    {
                        Name = "Пролетарская",
                    };

                    Station station4 = new Station()
                    {
                        Name = "Мясниковича",
                    };

                    Station station5 = new Station()
                    {
                        Name = "Автозаводская",
                    };

                    bus.Stations.Add(station);
                    bus.Stations.Add(station2);
                    bus.Stations.Add(station3);
                    bus.Stations.Add(station4);
                    bus.Stations.Add(station5);

                    Route route = new Route()
                    {
                        Name = "Вокзал - ДС Автозаводская"
                    };
                    route.Autobuses.Add(bus);
                    bus.Route = route; //у автобуса есть машрут

                    AutobusStation autobusStation = new AutobusStation()
                    {
                        Autobus = bus,
                        Station = station
                    };

                    AutobusStation autobusStation2 = new AutobusStation()
                    {
                        Autobus = bus,
                        Station = station2
                    };

                    AutobusStation autobusStation3 = new AutobusStation()
                    {
                        Autobus = bus,
                        Station = station3
                    };

                    AutobusStation autobusStation4 = new AutobusStation()
                    {
                        Autobus = bus,
                        Station = station4
                    };

                    AutobusStation autobusStation5 = new AutobusStation()
                    {
                        Autobus = bus,
                        Station = station5
                    };

                    Arrive arrive = new Arrive()
                    {
                        AutobusStation = autobusStation,
                        Time = "9:37 9:55 10:21 11:37 13:12"
                    };
                    Arrive arrive2 = new Arrive()
                    {
                        AutobusStation = autobusStation2,
                        Time = "9:40 9:57 10:25 11:42 13:17"
                    };
                    Arrive arrive3 = new Arrive()
                    {
                        AutobusStation = autobusStation3,
                        Time = "9:45 10:00 10:30 11:47 13:20"
                    };
                    Arrive arrive4 = new Arrive()
                    {
                        AutobusStation = autobusStation4,
                        Time = "9:48 10:05 10:35 11:51 13:27"
                    };
                    Arrive arrive5 = new Arrive()
                    {
                        AutobusStation = autobusStation5,
                        Time = "9:55 10:10 10:40 11:56 13:33"
                    };

                    db.Routes.Add(route);
                    db.Arrives.Add(arrive);
                    db.Arrives.Add(arrive2);
                    db.Arrives.Add(arrive3);
                    db.Arrives.Add(arrive4);
                    db.Arrives.Add(arrive5);
                    db.SaveChanges();
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                {
                    MessageBox.Show("Object: " + validationError.Entry.Entity.ToString());

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
                List<Route> routes = new List<Route>();
                int num = db.Buses.Where(p => p.Number.ToString() == NumberTextBox.Text).Count();
                if (num > 0)
                {
                    Autobus bus = db.Buses.Where(p => p.Number.ToString() == NumberTextBox.Text).First();
                    Route route = db.Routes.Where(p => p.Id == bus.RouteId).FirstOrDefault();
                    routes.Add(route);
                    List<Station> stations = new List<Station>();
                    foreach (var station in bus.Stations)
                    {
                        stations.Add(station);
                    }
                    TextBlockNothing.Visibility = Visibility.Hidden;
                    TextBlockNothing2.Visibility = Visibility.Hidden;
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
                List<Autobus> buses = new List<Autobus>();
                db.Buses.Load();
                foreach (var bus in db.Buses.Local.ToList())
                {
                    if (bus.Route.Id == route.Id)
                    {
                        buses.Add(bus);
                    }
                }

                foreach (var bus in buses)
                {
                    foreach (var station in bus.Stations)
                    {
                        stations.Add(station);
                    }
                }
                TextBlockNothing.Visibility = Visibility.Hidden;
                TextBlockNothing2.Visibility = Visibility.Hidden;
                RoutesListView.ItemsSource = routes;
                StationsDataGrid.ItemsSource = stations;
            }
        }

        private void ShowArriveTime()
        {
            using (AppDbContext db = new AppDbContext())
            {
                List<string> busNumbers = new List<string>();
                List<Autobus> buses = new List<Autobus>();
                foreach (var bus in db.Buses)
                {
                    busNumbers.Add(bus.Number.ToString());
                    buses.Add(bus);
                }
                ComboBoxBus.ItemsSource = busNumbers;
                ComboBoxStation.IsEnabled = true;
            }
        }

        private void ShowArriveTime_Click(object sender, RoutedEventArgs e)
        {
            ShowArriveTime();
            using (AppDbContext db = new AppDbContext())
            {
                Station station = (Station)ComboBoxStation.SelectedItem;
                Station currentStation = db.Stations.Find(station.Id);
                AutobusStation autst = db.AutobusStations.Where(p => p.Station.Id == currentStation.Id).First();
                Arrive arrive = db.Arrives.Where(p => p.AutobusStationId == autst.Id).First();
                MessageBox.Show(arrive.Time);
            }
        }

        private void ComboBoxBus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (AppDbContext db = new AppDbContext())
            {
                List<Station> stations = new List<Station>();
                List<Autobus> buses = new List<Autobus>();
                foreach (var bus in db.Buses)
                {
                    buses.Add(bus);
                }

                Autobus autobus = db.Buses.Where(p => p.Number.ToString() == ComboBoxBus.SelectedItem.ToString()).First();
                foreach (var station in autobus.Stations)
                {
                    stations.Add(station);
                }
                ComboBoxStation.ItemsSource = stations;
                ComboBoxStation.SelectedIndex = 0;
            }
        }
    }
}
