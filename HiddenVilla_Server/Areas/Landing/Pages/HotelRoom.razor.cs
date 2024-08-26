using HiddenVilla_Server.Model;
using Microsoft.AspNetCore.Components;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HiddenVilla_Server.Areas.Landing.Pages
{
    public partial class HotelRoom
    {
        private HomeModel HomeModel { get; set; } = new HomeModel();
        public IEnumerable<HotelRoomDTO> Rooms { get; set; } = new List<HotelRoomDTO>();
        private bool IsProcessing { get; set; } = false;

        [Required(ErrorMessage = "حتما وارد شود")]
        public int SelectedCity { get; set; }

        private IEnumerable<CityDto> cities;

        protected override async Task OnInitializedAsync()
        {
            IsProcessing = true;
            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
            var queryParams = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(uri.Query);

            if (queryParams.TryGetValue("startDate", out var startDateStr))
            {
                HomeModel.StartDate = DateTime.Parse(startDateStr);
            }

            if (queryParams.TryGetValue("endDate", out var endDateStr))
            {
                HomeModel.EndDate = DateTime.Parse(endDateStr);
            }

            if (queryParams.TryGetValue("noOfNights", out var noOfNightsStr))
            {
                HomeModel.NoOfNights = int.Parse(noOfNightsStr);
            }

            if (queryParams.TryGetValue("location", out var locationStr))
            {
                HomeModel.Location = Convert.ToInt32(locationStr);
            }
            if (queryParams.TryGetValue("type", out var typeStr))
            {
                HomeModel.AccommodationType = typeStr;
            }
            cities = await CityService.GetCities();
            await LoadRooms();
            IsProcessing = false;

        }

        private async Task LoadRooms()
        {
            Rooms = await HotelRoomRepository.
                GetAllHotelRoomsMain(
                    HomeModel.AccommodationType,
                    HomeModel.Location,
                    HomeModel.StartDate.ToString("MM/dd/yyyy"),
                    HomeModel.EndDate.ToString("MM/dd/yyyy"));
            if (Rooms is not null)
            {
                foreach (var room in Rooms)
                {
                    room.TotalAmount = room.RegularRate * HomeModel.NoOfNights;
                    room.TotalDays = HomeModel.NoOfNights;
                }
            }
        }

        public async Task SetDetailsURl(int id)
        {
            var queryParams = new Dictionary<string, object>
            {
                { "endDate", HomeModel.EndDate.ToString("yyyy-MM-dd") },
                { "startDate", HomeModel.StartDate.ToString("yyyy-MM-dd") },
                { "noOfNights", HomeModel.NoOfNights.ToString() },
            };
            var queryString = string.Join("&", queryParams.Select(kvp => $"{kvp.Key}={kvp.Value}"));
            NavigationManager.NavigateTo($"landing/hotel/room-details/{id}?{queryString}", true);
        }
        private async Task SaveBookingInfo()
        {
            HomeModel.EndDate = HomeModel.StartDate.AddDays(HomeModel.NoOfNights);
            HomeModel.Location = SelectedCity;
            var queryParams = new Dictionary<string, object>
            {
                { "endDate", HomeModel.EndDate.ToString("yyyy-MM-dd") },
                { "startDate", HomeModel.StartDate.ToString("yyyy-MM-dd") },
                { "noOfNights", HomeModel.NoOfNights.ToString() },
                { "location", HomeModel.Location },
                { "type", HomeModel.AccommodationType }
            };
            if (HomeModel.AccommodationType is null) queryParams.Remove("type");
            var queryString = string.Join("&", queryParams.Select(kvp => $"{kvp.Key}={kvp.Value}"));

            // Navigate to the same page with updated query parameters
            NavigationManager.NavigateTo($"landing/hotel/rooms?{queryString}", true);

        }
    }
}