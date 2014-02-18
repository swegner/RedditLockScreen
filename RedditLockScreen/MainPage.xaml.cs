using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System.UserProfile;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace RedditLockScreen
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private string _subreddit;

        private string _sort;

        private string _time;

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            
            string url = string.Format("http://redditfeed.azurewebsites.net/{0}Porn/{1}/{2}", this._subreddit, this._sort, this._time);
            var result = await LockScreen.RequestSetImageFeedAsync(new Uri(url));
            Debug.WriteLine(result);
        }

        private void SubredditCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._subreddit = e.AddedItems
                .Cast<ComboBoxItem>()
                .Select(c => c.Content.ToString())
                .First();
        }

        private void SortCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string sortValue = e.AddedItems
                .Cast<ComboBoxItem>()
                .Select(c => c.Content.ToString())
                .First();

            string sort;
            string time;
            switch (sortValue)
            {
                case "hot":
                case "new":
                case "rising":
                case "controversial":
                case "guilded":
                    sort = sortValue;
                    time = null;
                    break;

                case "top - hour":
                    sort = "top";
                    time = "hour";
                    break;

                case "top - day":
                    sort = "top";
                    time = "day";
                    break;

                case "top - week":
                    sort = "top";
                    time = "week";
                    break;

                case "top - month":
                    sort = "top";
                    time = "month";
                    break;

                case "top - year":
                    sort = "top";
                    time = "year";
                    break;

                case "top - all time":
                    sort = "top";
                    time = "all";
                    break;

                default:
                    throw new InvalidOperationException("Unexpected sort value");
            }

            this._sort = sort;
            this._time = time;
        }
    }
}
