using System.Windows.Forms;

namespace SMWControlibControls.GraphicsControls
{
    public delegate void WndProcHandler(ref Message m);
    public partial class PictureBoxWithoutMouseEvents : PictureBox
    {
        public event WndProcHandler MouseEventDetected;
        public PictureBoxWithoutMouseEvents()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0xc1da:
                {
                    MouseEventDetected?.Invoke(ref m);
                    return;
                }
                case 0x020://WM_LBUTTONDOWN
                {
                    MouseEventDetected?.Invoke(ref m);
                    return;
                }
                case 0x021://WM_LBUTTONDOWN
                {
                    MouseEventDetected?.Invoke(ref m);
                    return;
                }
                case 0x084://WM_LBUTTONDOWN
                {
                    MouseEventDetected?.Invoke(ref m);
                    return;
                }
                case 0x0200://WM_LBUTTONDOWN
                {
                    MouseEventDetected?.Invoke(ref m);
                    return;
                }
                case 0x0201://WM_LBUTTONDOWN
                {
                    MouseEventDetected?.Invoke(ref m);
                    return;
                }
                case 0x0202://WM_LBUTTONUP
                {
                    MouseEventDetected?.Invoke(ref m);
                    return;
                }
                case 0x0203://WM_LBUTTONDBLCLK
                {
                    MouseEventDetected?.Invoke(ref m);
                    return;
                }
                case 0x0204://WM_RBUTTONDOWN
                {
                    MouseEventDetected?.Invoke(ref m);
                    return;
                }
                case 0x0205://WM_RBUTTONUP
                {
                    MouseEventDetected?.Invoke(ref m);
                    return;
                }
                case 0x0206://WM_RBUTTONDBLCLK
                {
                    MouseEventDetected?.Invoke(ref m);
                    return;
                }
                case 0x0210:
                {
                    MouseEventDetected?.Invoke(ref m);
                    return;
                }
                case 0x02A1://WM_RBUTTONDBLCLK
                {
                    MouseEventDetected?.Invoke(ref m);
                    return;
                }
                case 0x02A2://WM_RBUTTONDBLCLK
                {
                    MouseEventDetected?.Invoke(ref m);
                    return;
                }
                case 0x02A3://WM_RBUTTONDBLCLK
                {
                    MouseEventDetected?.Invoke(ref m);
                    return;
                }
            }
            base.WndProc(ref m);
        }

    protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
