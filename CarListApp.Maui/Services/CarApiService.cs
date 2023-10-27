using CarListApp.Maui.Models;
using CarListApp.Maui.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CarListApp.Maui.Services
{
    public class CarApiService
    {
        private HttpClient _httpClient;
        public static string BaseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:88" : "http://localhost:88";
        public string StatusMessage { get; private set; }

        public CarApiService()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(BaseAddress)
            };
        }

        public async Task<List<Car>> GetCarsAsync()
        {
            try
            {
                await SetAuthToken();
                var response = await _httpClient.GetStringAsync("/api/cars");
                //response.EnsureSuccessStatusCode();

                var result = JsonConvert.DeserializeObject<List<Car>>(response);
                return result;
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to retrieve data";
            }

            return null;
        }

        public async Task<Car> GetCarAsync(int id)
        {
            try
            {
                await SetAuthToken();
                var response = await _httpClient.GetStringAsync($"/api/cars/{id}");
                //response.EnsureSuccessStatusCode();

                var result = JsonConvert.DeserializeObject<Car>(response);
                return result;
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to delete car";
            }

            return null;
        }

        public async Task AddCarAsync(Car car)
        {
            try
            {
                await SetAuthToken();
                var response = await _httpClient.PostAsJsonAsync("/api/cars", car);
                response.EnsureSuccessStatusCode();
                StatusMessage = "Insert successful";
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to add car";
            }
        }

        public async Task UpdateCarAsync(int id, Car car)
        {
            try
            {
                await SetAuthToken();
                var response = await _httpClient.PutAsJsonAsync($"/api/cars/{id}", car);
                response.EnsureSuccessStatusCode();
                StatusMessage = "Update successful";
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to update car";
            }
        }

        public async Task<bool> DeleteCarAsync(int id)
        {
            try
            {
                await SetAuthToken();
                var response = await _httpClient.DeleteAsync($"/api/cars/{id}");
                response.EnsureSuccessStatusCode();
                StatusMessage = "Update successful";
                return true;
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to delete car";
                return false;
            }
        }

        public async Task<AuthResponseModel> Login(LoginModel loginModel)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/login", loginModel);
                response.EnsureSuccessStatusCode();
                StatusMessage = "Login successful";

                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AuthResponseModel>(data);
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to login successfully";
                return default;
            }
        }

        public async Task SetAuthToken()
        {
            // this will add the token to all of the endpoints (other than login) because they require tokens
            var token = await SecureStorage.GetAsync("Token");

            // add a header with the word Bearer and the token to our http client
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
    }
}
