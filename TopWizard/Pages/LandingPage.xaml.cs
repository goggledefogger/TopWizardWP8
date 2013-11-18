using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Facebook;
using System.Windows.Media.Imaging;
using TopWizard.ViewModel;

namespace TopWizard.Pages
{
    public partial class LandingPage : PhoneApplicationPage
    {
        public LandingPage()
        {
            InitializeComponent();

            LoadUserInfo();
        }

        private void LoadUserInfo()
        {
            var fb = new FacebookClient(App.AccessToken);

            fb.GetCompleted += (o, e) =>
            {
                if (e.Error != null)
                {
                    Dispatcher.BeginInvoke(() => MessageBox.Show(e.Error.Message));
                    return;
                }

                var result = (IDictionary<string, object>)e.GetResultData();

                Dispatcher.BeginInvoke(() =>
                {
                    var profilePictureUrl = string.Format("https://graph.facebook.com/{0}/picture?type={1}&access_token={2}", App.FacebookId, "square", App.AccessToken);
                    
                    this.MyImage.Source = new BitmapImage(new Uri(profilePictureUrl));
                    this.MyName.Text = String.Format("{0} {1}", (string)result["first_name"], (string)result["last_name"]);
                });
            };

            fb.GetTaskAsync("me");
        }

        private void friendSelectorTextBlockHandler(object sender, System.Windows.Input.GestureEventArgs evtArgs)
        {
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
                    foreach (var item in data)
                    {
                        var friend = (IDictionary<string, object>)item;

                        FacebookData.Friends.Add(new Friend { Name = (string)friend["name"], id = (string)friend["id"], PictureUri = new Uri(string.Format("https://graph.facebook.com/{0}/picture?type={1}&access_token={2}", (string)friend["id"], "square", App.AccessToken)) });
                    }

                    NavigationService.Navigate(new Uri("/Pages/FriendSelector.xaml", UriKind.Relative));
                });

            };

            fb.GetTaskAsync("/me/friends");
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (FacebookData.SelectedFriends.Count > 0)
            {
                if (FacebookData.SelectedFriends.Count > 1)
                {
                    this.WithWhoTextBox.Text = String.Format("with {0} and {1} others", FacebookData.SelectedFriends[0].Name, FacebookData.SelectedFriends.Count - 1);
                }
                else
                {
                    this.WithWhoTextBox.Text = "with " + FacebookData.SelectedFriends[0].Name;
                }
            }
            else
            {
                this.WithWhoTextBox.Text = "Select Friends";
            }
        }
    }
}