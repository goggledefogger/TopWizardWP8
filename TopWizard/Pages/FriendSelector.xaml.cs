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
using Facebook;

namespace TopWizard.Pages
{
    public partial class FriendSelector : PhoneApplicationPage
    {
        public FriendSelector()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // this runs in the UI thread, so it is ok to modify the 
            // viewmodel directly here
            FacebookData.SelectedFriends.Clear();
            var selectedFriends = this.friendList.SelectedItems;
            foreach (Friend oneFriend in selectedFriends)
            {
                FacebookData.SelectedFriends.Add(oneFriend);
            }

            photoSelector();

            base.OnNavigatedFrom(e);
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/WinPhotoSelector.xaml", UriKind.Relative));
        }

        private void photoSelector()
        {
            App.CurrentFriend = FacebookData.SelectedFriends[0];

            FacebookClient fb = new FacebookClient(App.AccessToken);

            fb.GetCompleted += (o, e) =>
            {
                if (e.Error != null)
                {
                    Dispatcher.BeginInvoke(() => MessageBox.Show(e.Error.Message));
                    return;
                }

                var result = (IDictionary<string, object>)e.GetResultData();

                var data = (IEnumerable<object>)result["data"]; ;


                Dispatcher.BeginInvoke(() =>
                {
                    // The observable collection can only be updated from within the UI thread. See 
                    // http://10rem.net/blog/2012/01/10/threading-considerations-for-binding-and-change-notification-in-silverlight-5
                    // If you try to update the bound data structure from a different thread, you are going to get a cross
                    // thread exception.
                    foreach (var photoObject in data)
                    {
                        FacebookData.WinPhotos.Add(
                            new Photo 
                            {
                                PictureUri = new Uri(
                                    string.Format("https://graph.facebook.com/{0}/picture?access_token={1}", (string)((JsonObject)photoObject)["id"], App.AccessToken)
                                )
                            }
                        );
                    }
                });

            };

            fb.GetTaskAsync(string.Format("https://graph.facebook.com/{0}/photos", App.CurrentFriend.id));
        }


    }
}