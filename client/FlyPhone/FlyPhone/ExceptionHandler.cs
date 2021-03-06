﻿using Xamarin.Forms;

namespace FlyPhone
{
    public static class ExceptionHandler
    {
        public static void NetworkError()
        {
            Device.BeginInvokeOnMainThread(() => Application.Current.MainPage.DisplayAlert("Network error", "Please check your network connection and do action again.", "Ok"));
        }

        public static void DatabaseError()
        {
            Device.BeginInvokeOnMainThread(() => Application.Current.MainPage.DisplayAlert("Database error", "Problem with database connection or connection query.", "Ok"));
        }
    }
}