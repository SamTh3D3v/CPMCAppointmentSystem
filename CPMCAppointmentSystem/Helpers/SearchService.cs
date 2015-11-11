using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataLayer.Attributes;
using Syncfusion.Windows.Forms;
using ICommand = System.Windows.Input.ICommand;

namespace CPMCAppointmentSystem.Helpers
{
    //To be reimplemented usieng ReactiveUI
    public class SearchService<T> :  INotifyPropertyChanged
    {
        #region Fields
        private ObservableCollection<T> _dataSource;        
        private String _searchTerm;
        #endregion
        #region Properties
        public ObservableCollection<T> DataSource
        {
            get
            {
                return _dataSource;
            }

            set
            {
                if (_dataSource == value)
                {
                    return;
                }

                _dataSource = value;
                OnPropertyChanged();
            }
        }
        public String SearchTerm
        {
            get
            {
                return _searchTerm;
            }

            set
            {
                if (_searchTerm == value)
                {
                    return;
                }

                _searchTerm = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Commands              
        
        #endregion
        #region Ctors and Methods
        public SearchService(ObservableCollection<T> source)
        {
            DataSource = source;                     
        }
        public SearchService()
        {
        }
        public async Task<SearchResult> SearchAsync(string term=null)
        {
            var searchResult = await Task.Run(() =>
            {
                var query =
                    DataSource.Where(
                        element =>
                            element.GetType()
                                .GetProperties().Where(prop=>Attribute.IsDefined(prop,typeof(SearchAttribute)))
                                .Any(property => (property.GetValue(element) != null) && (property.GetValue(element).ToString().ToLower().Contains((term ?? SearchTerm).ToLower()))))
                        .Select(element => element);

                return query;
            });
            return new SearchResult(new ObservableCollection<T>(searchResult));
        }
        #endregion       
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public class SearchResult
        {            
            public ObservableCollection<T> Matches { get; private set; }
            public SearchResult(ObservableCollection<T> matches)
            {
                Matches = matches;
            }
           
        }
    }
}
