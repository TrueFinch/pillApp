using System;
using System.Collections.Generic;
using System.ComponentModel;
using pillApp.Models;
using pillApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace pillApp.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            //BindingContext = new NewItemViewModel();
        }
    }
}