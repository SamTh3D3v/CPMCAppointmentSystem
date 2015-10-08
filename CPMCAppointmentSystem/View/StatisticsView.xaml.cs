using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;
using Syncfusion.Windows.Shared;

namespace CPMCAppointmentSystem.View
{
    /// <summary>
    /// Interaction logic for StatisticsView.xaml
    /// </summary>
    public partial class StatisticsView : Page
    {
        public StatisticsView()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, (m) =>
            {
                switch (m.Notification)
                {
                    case "RestoreTile":
                        var tileitem = TileViewCtl.ItemContainerGenerator.ContainerFromIndex(TileViewCtl.SelectedIndex) as TileViewItem;
                        if (tileitem != null) tileitem.TileViewItemState = TileViewItemState.Normal;
                        break;

                }
            });
        }

    }
}
