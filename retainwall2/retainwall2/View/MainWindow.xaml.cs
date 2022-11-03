using System.Windows;

namespace retainwall2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Trong view khi khởi tạo ta truyền vm vào.
        //Lưu nó lại dạng readonly.
        private readonly Viewmodel viewmodel;

        public MainWindow(Viewmodel _viewmodel)
        {
            InitializeComponent();
            //gán 2 dòng này để nhận context và lưu giá trị.
            viewmodel = _viewmodel;
            DataContext= _viewmodel;
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Với sự kiện click từ view. gọi hàm tương ứng.
            viewmodel.DrawLine();

            
           
        }
    }
}
