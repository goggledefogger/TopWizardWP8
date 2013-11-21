using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TopWizard.ViewModel;

namespace TopWizard.Pages
{
    public partial class WinPhotoSelector : PhoneApplicationPage
    {
        public WinPhotoSelector()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // this runs in the UI thread, so it is ok to modify the 
            // viewmodel directly here
            FacebookData.SelectedWinPhotos.Clear();
            var selectedPhotos = this.photoList.SelectedItems;
            foreach (Photo photo in selectedPhotos)
            {
                FacebookData.SelectedWinPhotos.Add(photo);
            }

            base.OnNavigatedFrom(e);
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {

        }

    }
}