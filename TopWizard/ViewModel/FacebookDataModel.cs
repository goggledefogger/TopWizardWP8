using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopWizard.ViewModel
{
    public class Friend
    {
        public string id { get; set; }

        public string Name { get; set; }

        public Uri PictureUri { get; set; }
    }

    public class Photo
    {
        public Uri PictureUri { get; set; }
    }

    public class FacebookData
    {
        private static ObservableCollection<Friend> friends = new ObservableCollection<Friend>();
        private static ObservableCollection<Photo> winPhotos = new ObservableCollection<Photo>();
        private static ObservableCollection<Photo> losePhotos = new ObservableCollection<Photo>();

        public static ObservableCollection<Friend> Friends
        {
            get
            {
                return friends;
            }
        }

        private static ObservableCollection<Friend> selectedFriends = new ObservableCollection<Friend>();

        public static ObservableCollection<Friend> SelectedFriends
        {
            get
            {
                return selectedFriends;
            }
        }

        private static ObservableCollection<Photo> selectedWinPhotos = new ObservableCollection<Photo>();

        public static ObservableCollection<Photo> SelectedWinPhotos
        {
            get
            {
                return selectedWinPhotos;
            }
        }

        private static ObservableCollection<Photo> selectedLosePhotos = new ObservableCollection<Photo>();

        public static ObservableCollection<Photo> SelectedLosePhotos
        {
            get
            {
                return selectedLosePhotos;
            }
        }

        public static ObservableCollection<Photo> WinPhotos
        {
            get
            {
                return winPhotos;
            }
        }

        public static ObservableCollection<Photo> LosePhotos
        {
            get
            {
                return losePhotos;
            }
        }
    }
}
