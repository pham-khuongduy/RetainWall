using Autodesk.AutoCAD.Runtime;

namespace retainwall2
{
    //NÊN TẠO RA 1 CLASS CHƯA CÁC CMD  RIÊNG NTN NÀY NHÉ.
    //KO VIẾT CHUNG VÀO VIEWMODEL NHƯ CODE CŨ CỦA MI.

    public class AutoCadCMD
    {
        [CommandMethod("cmdRetainWall")]
        public void ReatainWall()
        {
            Viewmodel vm = new Viewmodel();
            MainWindow win = new MainWindow(vm);
            win.Show();

        }
        //sau này có bn cái cmd vất hết vào đây
        //view với model cứ tạo tẹt đi.
        //Đấy mi tạo bao nhiêu view thì vất vào View
        // Tạo bao nhiêu vm thì vất vào vm

        [CommandMethod("cmdmove")]
        public void cmd1()
        {
            //ở đây chỉ gọi vm và vm ra để sài thôi.
            //gọi vm trước -> truyền vm cho v.
            //show v.
            Viewmodel vm = new Viewmodel();
           
            vm.Move();

        }

        [CommandMethod("cmdmoveselect")]
        public void cmd2()
        {
            Viewmodel vm = new Viewmodel();

            vm.moveselect();

        }

        [CommandMethod("cmd3")]
        public void cmd3()
        {
            Viewmodel vm = new Viewmodel();
            MainWindow win = new MainWindow(vm);
            win.Show();

        }
    }

    

}
