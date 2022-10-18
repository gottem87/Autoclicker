//this will hold the location where to click
Point clickLocation = new Point(0,0);

private void btnSetPoint_Click(object sender, EventArgs e)
{
   timerPoint.Interval = 5000;
   timerPoint.Start();
}

private void timerPoint_Tick(object sender, EventArgs e)
{
    clickLocation = Cursor.Position;
    //show the location on window title
    this.Text = "autoclicker " + clickLocation.ToString();
    timerPoint.Stop();
}

[DllImport("User32.dll", SetLastError = true)]
public static extern int SendInput(int nInputs, ref INPUT pInputs, 
                                   int cbSize);
                                   
                                   //mouse event constants
const int MOUSEEVENTF_LEFTDOWN = 2;
const int MOUSEEVENTF_LEFTUP = 4;
//input type constant
const int INPUT_MOUSE = 0;

public struct MOUSEINPUT
{
    public int dx;
    public int dy;
    public int mouseData;
    public int dwFlags;
    public int time;
    public IntPtr dwExtraInfo;
}

public struct INPUT
{
    public uint type;
    public MOUSEINPUT mi;
};

private void timer1_Tick(object sender, EventArgs e)
{
    //set cursor position to memorized location
    Cursor.Position = clickLocation;
    //set up the INPUT struct and fill it for the mouse down
    INPUT i = new INPUT();
    i.type = INPUT_MOUSE;
    i.mi.dx = 0;
    i.mi.dy = 0;
    i.mi.dwFlags = MOUSEEVENTF_LEFTDOWN;
    i.mi.dwExtraInfo = IntPtr.Zero;
    i.mi.mouseData = 0;
    i.mi.time = 0;
    //send the input 
    SendInput(1, ref i, Marshal.SizeOf(i));
    //set the INPUT for mouse up and send it
    i.mi.dwFlags = MOUSEEVENTF_LEFTUP;
    SendInput(1, ref i, Marshal.SizeOf(i));
}

private void btnStart_Click(object sender, EventArgs e)
{
    timer1.Interval = (int)numericUpDown1.Value;
    if (!timer1.Enabled)
    {
        timer1.Start();
        this.Text = "autoclicker - started";
    }
    else
    {
        timer1.Stop();
        this.Text = "autoclicker - stopped";
    }
}
