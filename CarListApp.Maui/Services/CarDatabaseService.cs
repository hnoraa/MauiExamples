using CarListApp.Maui.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarListApp.Maui.Services
{
    public class CarDatabaseService
    {
        private SQLiteConnection _connection;
        private string _databasePath;

        public string StatusMessage { get; private set; }

        public CarDatabaseService(string databasePath)
        {
            _databasePath = databasePath;
        }

        private void Init()
        {
            if (_connection != null) return;

            _connection = new SQLiteConnection(_databasePath);
            _connection.CreateTable<Car>();
        }

        public List<Car> GetCars()
        {
            try
            {
                Init();

                List<Car> result = _connection.Table<Car>().ToList();
                StatusMessage = $"Found {result.Count} Cars";

                return result;
            }
            catch(Exception ex)
            {
                StatusMessage = "Failed to retrieve data";
            }

            return new List<Car>();
        }

        public Car GetCar(int id)
        {
            try
            {
                Init();

                Car result = _connection.Table<Car>().FirstOrDefault(x => x.Id == id);

                if (result == null) throw new Exception("No car found matching id");

                return result;
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to delete car";
            }

            return new Car();
        }

        public bool AddCar(Car car)
        {
            try
            {
                Init();

                if(car == null)
                {
                    throw new Exception("Invalid car record");
                }
            
                int result = _connection.Insert(car);

                StatusMessage = result == 0 ? "Insert failed" : "Added car";
                return true;
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to add car";
                return false;
            }
        }

        public bool DeleteCar(int id)
        {
            try
            {
                Init();

                _connection.Table<Car>().Delete(x => x.Id == id);
                StatusMessage = "Deleted car";

                return true;
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to delete car";
                return false;
            }
        }

        public bool UpdateCar(Car car)
        {
            try
            {
                Car result = _connection.Table<Car>().FirstOrDefault(x => x.Id == car.Id);
                if (result == null) throw new Exception();

                result.Make = car.Make;
                result.Model = car.Model;

                _connection.Update(result);

                StatusMessage = "Updated car";

                return true;
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to update car";
                return false;
            }
        }
    }
}
