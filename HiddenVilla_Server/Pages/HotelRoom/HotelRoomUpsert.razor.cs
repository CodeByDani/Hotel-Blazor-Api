using Blazored.TextEditor;
using HiddenVilla_Server.Helper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HiddenVilla_Server.Pages.HotelRoom
{
    public partial class HotelRoomUpsert
    {
        [Parameter] public int? Id { get; set; }
        [Required(ErrorMessage = "حتما یک شهر انتخاب کنید")]
        public int? SelectedCity { get; set; }
        private HotelRoomDTO HotelRoomModel { get; set; } = new HotelRoomDTO();
        private string Title { get; set; } = "ایجاد";

        private HotelRoomImageDTO RoomImage { get; set; } = new HotelRoomImageDTO();
        private List<string> DeletedImageNames { get; set; } = new List<string>();
        public BlazoredTextEditor QuillHtml { get; set; } = new BlazoredTextEditor();
        private bool IsImageUploadProcessStarted { get; set; } = false;

        public List<string> Type = new List<string>
        {
            "هتل",
            "آپارتمان",
            "ویلا",
            "باغ"
        };

        [Required]
        private string SelectedType { get; set; }

        private void SelectType(string type)
        {
            SelectedType = type;
        }

        private string GetImageForType(string type)
        {
            return type switch
            {
                "هتل" => "/RoomImages/Hotel.jpg",
                "آپارتمان" => "/RoomImages/Apart.jpg",
                "ویلا" => "/RoomImages/Villa.jpg",
                "باغ" => "/RoomImages/Garden.jpg",
            };
        }

        private IEnumerable<CityDto> cities;
        private IEnumerable<HotelAmenityDTO> amenities;
        private IdentityUser user;

        [CascadingParameter] public Task<AuthenticationState> AuthenticationState { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var userClaims = authState.User;
            if (userClaims.Identity.IsAuthenticated)
            {
                user = new IdentityUser
                {
                    Id = userClaims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value
                };
            }
            cities = await CityService.GetCities();
            amenities = await AmenityRepository.GetAllHotelAmenity();
            var authenticationState = await AuthenticationState;
            if (!authenticationState.User.IsInRole(Common.SD.Role_Admin))
            {
                var uri = new Uri(NavigationManager.Uri);
                NavigationManager.NavigateTo($"/identity/account/login?returnUrl={uri.LocalPath}");
            }

            if (Id != null)
            {
                //updating
                Title = "به‌روزرسانی";
                HotelRoomModel = await HotelRoomRepository.GetHotelRoom(Id.Value);
                if (HotelRoomModel?.HotelRoomImages != null)
                {
                    HotelRoomModel.ImageUrls = HotelRoomModel.HotelRoomImages.Select(u => u.RoomImageUrl).ToList();
                }
            }
            else
            {
                //create
                HotelRoomModel = new HotelRoomDTO();
            }
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
                return;
            bool loading = true;
            while (loading)
            {
                try
                {
                    if (!string.IsNullOrEmpty(HotelRoomModel.Details))
                    {
                        await this.QuillHtml.LoadHTMLContent(HotelRoomModel.Details);
                    }

                    loading = false;
                }
                catch
                {
                    await Task.Delay(10);
                    loading = true;
                }
            }
        }

        private async Task HandleHotelRoomUpsert()
        {
            try
            {
                var roomDetailsByName = await HotelRoomRepository.IsRoomUnique(HotelRoomModel.Name, HotelRoomModel.Id);
                if (roomDetailsByName != null)
                {
                    await JsRuntime.ToastrError("نام اتاق قبلاً وجود دارد.");
                    return;
                }

                if (HotelRoomModel.Id != 0 && Title == "به‌روزرسانی")
                {
                    HotelRoomModel.Details = await QuillHtml.GetHTML();
                    HotelRoomModel.CityHotelId = SelectedCity.HasValue ? Convert.ToInt32(SelectedType)
                        : HotelRoomModel.CityHotelId;
                    HotelRoomModel.PlaceType = string.IsNullOrEmpty(SelectedType) ? HotelRoomModel.PlaceType : SelectedType;
                    HotelRoomModel.UserId = user.Id;
                    HotelRoomModel.Ideas = amenities.Where(x => x.IsSelected).Select(p => p.Id).ToList();
                    //update
                    if (HotelRoomModel.ImageUrls.Count == 0)
                    {
                        await JsRuntime.ToastrError("عکسی انتخاب نشده");
                        return;
                    }
                    var updateRoomResult = await HotelRoomRepository.UpdateHotelRoom(HotelRoomModel.Id, HotelRoomModel);
                    if ((HotelRoomModel.ImageUrls != null && HotelRoomModel.ImageUrls.Any()) ||
                        (DeletedImageNames != null && DeletedImageNames.Any()))
                    {
                        if (DeletedImageNames != null && DeletedImageNames.Any())
                        {
                            foreach (var deletedImageName in DeletedImageNames)
                            {
                                var imageName = deletedImageName.Replace($"{NavigationManager.BaseUri}RoomImages/", "");

                                var result = FileUpload.DeleteFile(imageName);
                                await HotelImagesRepository.DeleteHotelImageByImageUrl(deletedImageName);
                            }
                        }

                        await AddHotelRoomImage(updateRoomResult);
                    }

                    await JsRuntime.ToastrSuccess("اتاق هتل با موفقیت به‌روزرسانی شد.");
                }
                else
                {
                    HotelRoomModel.Ideas = amenities.Where(x => x.IsSelected).Select(p => p.Id).ToList();
                    //create
                    if (!await HandleValidation())
                    {
                        return;
                        //NavigationManager.NavigateTo("hotel-room/create");
                    }

                    HotelRoomModel.PlaceType = SelectedType;
                    HotelRoomModel.CityHotelId = SelectedCity.Value;
                    HotelRoomModel.Details = await QuillHtml.GetHTML();
                    HotelRoomModel.UserId = user.Id;
                    var createdResult = await HotelRoomRepository.CreateHotelRoom(HotelRoomModel);
                    if (HotelRoomModel.ImageUrls.Count == 0)
                    {
                        await JsRuntime.ToastrError("عکسی انتخاب نشده");
                        return;
                    }
                    await AddHotelRoomImage(createdResult);
                    await JsRuntime.ToastrSuccess("اتاق هتل با موفقیت ایجاد شد.");
                }
            }
            catch (Exception ex)
            {
                //log exceptions
            }

            NavigationManager.NavigateTo("hotel-room");
        }

        private async Task HandleImageUpload(InputFileChangeEventArgs e)
        {
            IsImageUploadProcessStarted = true;
            try
            {
                var images = new List<string>();
                if (e.GetMultipleFiles().Count > 0)
                {
                    foreach (var file in e.GetMultipleFiles())
                    {
                        System.IO.FileInfo fileInfo = new System.IO.FileInfo(file.Name);
                        if (fileInfo.Extension.ToLower() == ".jpg" ||
                            fileInfo.Extension.ToLower() == ".png" ||
                            fileInfo.Extension.ToLower() == ".jpeg")
                        {
                            var uploadedImagePath = await FileUpload.UploadFile(file);
                            images.Add(uploadedImagePath);
                        }
                        else
                        {
                            await JsRuntime.ToastrError("لطفاً فقط فایل‌های .jpg/.jpeg/.png انتخاب کنید");
                            return;
                        }
                    }

                    if (images.Any())
                    {
                        if (HotelRoomModel.ImageUrls != null && HotelRoomModel.ImageUrls.Any())
                        {
                            HotelRoomModel.ImageUrls.AddRange(images);
                        }
                        else
                        {
                            HotelRoomModel.ImageUrls = new List<string>();
                            HotelRoomModel.ImageUrls.AddRange(images);
                        }
                    }
                    else
                    {
                        await JsRuntime.ToastrError("بارگذاری تصویر ناموفق بود");
                        return;
                    }
                }

                IsImageUploadProcessStarted = false;
            }
            catch (Exception ex)
            {
                await JsRuntime.ToastrError(ex.Message);
            }
        }

        private async Task AddHotelRoomImage(HotelRoomDTO roomDetails)
        {
            foreach (var imageUrl in HotelRoomModel.ImageUrls)
            {
                if (HotelRoomModel.HotelRoomImages == null ||
                    HotelRoomModel.HotelRoomImages.Where(x => x.RoomImageUrl == imageUrl).Count() == 0)
                {

                    RoomImage = new HotelRoomImageDTO()
                    {
                        RoomId = roomDetails.Id,
                        RoomImageUrl = imageUrl
                    };
                    await HotelImagesRepository.CreateHotelRoomImage(RoomImage);
                }
            }
        }

        internal async Task DeletePhoto(string imageUrl)
        {
            try
            {
                var imageIndex = HotelRoomModel.ImageUrls.FindIndex(x => x == imageUrl);
                var imageName = imageUrl.Replace($"{NavigationManager.BaseUri}RoomImages/", "");
                if (HotelRoomModel.Id == 0 && Title == "ایجاد")
                {
                    var result = FileUpload.DeleteFile(imageName);
                }
                else
                {
                    //update
                    DeletedImageNames ??= new List<string>();
                    DeletedImageNames.Add(imageUrl);
                }

                HotelRoomModel.ImageUrls.RemoveAt(imageIndex);
            }
            catch (Exception ex)
            {
                await JsRuntime.ToastrError(ex.Message);
            }

        }
    }
}