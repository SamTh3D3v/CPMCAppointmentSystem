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
using CPMCAppointmentSystem.ViewModel;

namespace CPMCAppointmentSystem.UserControls
{

    public partial class DragDrapRichTextBox : UserControl
    {
        public DragDrapRichTextBox()
        {
            InitializeComponent();          
        }        
        private void LvNames_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((sender as Border)!=null)
            {
                var val=(sender as Border).Tag;
                DragDrop.DoDragDrop(LvNames,
                       (sender as Border).Tag,
                       DragDropEffects.Copy);
            }
        }


        private void UIElement_OnDragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
            e.Handled = true;
        }

        private void UIElement_OnDrop(object sender, DragEventArgs e)
        {            
            object text = e.Data.GetData(DataFormats.StringFormat);
        }
    }
}
