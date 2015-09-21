using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Windows.Forms.Tools.Navigation;
using Syncfusion.Windows.Shared;

namespace CPMCAppointmentSystem.Helpers
{
    public class TreeViewModel:NotifyPropertyChanged
    {
        #region Fields
     
        private String _content ;
        private ObservableCollection<TreeViewModel> _treeViewModelCollection;            
        #endregion
        #region properties
        public String Content
        {
            get
            {
                return _content;
            }

            set
            {
                if (_content == value)
                {
                    return;
                }

                _content = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<TreeViewModel> TreeViewModelCollection
        {
            get
            {
                return _treeViewModelCollection;
            }
            set
            {
                if (_treeViewModelCollection == value)
                {
                    return;
                }

                _treeViewModelCollection = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Ctors and Methods        
        #endregion
    }
}
