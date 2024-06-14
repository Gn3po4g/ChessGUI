using ChessGUI.Common;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ChessGUI.View {
    /// <summary>
    /// Chess.xaml 的交互逻辑
    /// </summary>
    public partial class Chess : UserControl {
        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type", typeof(Model.Type), typeof(Chess), new PropertyMetadata(Model.Type.none));
        public static readonly DependencyProperty RowProperty = DependencyProperty.Register("Row", typeof(int), typeof(Chess), new PropertyMetadata(0));
        public static readonly DependencyProperty ColomnProperty = DependencyProperty.Register("Colomn", typeof(int), typeof(Chess), new PropertyMetadata(0));
        public Model.Type Type {
            get { return (Model.Type)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }
        public int Row {
            get { return (int)GetValue(RowProperty); }
            set { SetValue(RowProperty, value); }
        }
        public int Colomn {
            get { return (int)GetValue(ColomnProperty); }
            set { SetValue(ColomnProperty, value); }
        }

        public Chess() {
            InitializeComponent();
            Mask.Source = (BitmapImage)Application.Current.Resources["mark"];
        }

        public event EventHandler<ClickEventArgs>? ButtonClicked;

        private void Click(object sender, RoutedEventArgs e) {
            ButtonClicked?.Invoke(this, new ClickEventArgs() {
                Row = Row, Column = Colomn
            });
            //if (LastSelectedChess == null) {
            //    if (Type == Type.none) return;
            //    Mask.Visibility = Visibility.Visible;
            //} else if (IsAlly(LastSelectedChess)) {
            //    LastSelectedChess.Mask.Visibility = Visibility.Hidden;
            //    Mask.Visibility = Visibility.Visible;
            //}else {
            //    return;
            //}
            //LastSelectedChess = this;
        }

        //private bool IsAlly(Chess other) {
        //    return Type < Type.none && other.Type < Type.none
        //        || Type > Type.none && other.Type > Type.none;
        //}
    }
}
