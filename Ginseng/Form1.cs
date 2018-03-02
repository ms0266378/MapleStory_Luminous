using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Media;
using AllHotkey;
using System.IO;
using GS;
using System.Runtime.InteropServices;

namespace Luminous
{
    public partial class Form1 : Form
    {
        //---------------------------------------------
        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int Left;
            public int Right;
            public int Top;
            public int Bottom;
        }
        //DLL申明
        [DllImport("dwmapi.dll", PreserveSig = false)]
        static extern void DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS
        margins);
        //DLL申明
        [DllImport("dwmapi.dll", PreserveSig = false)]
        static extern bool DwmIsCompositionEnabled();
        //直接添加代碼
        protected override void OnLoad(EventArgs e)
        {
            if (DwmIsCompositionEnabled())
            {
                MARGINS margins = new MARGINS();
                margins.Right = margins.Left = margins.Top = margins.Bottom =
                this.Width + this.Height;
                DwmExtendFrameIntoClientArea(this.Handle, ref margins);
            }
            base.OnLoad(e);
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            if (DwmIsCompositionEnabled())
            {
                e.Graphics.Clear(Color.Black);
            }
        }
        //--------------------------------------------------------------
        //--------------Varibiles-------------
        public static int Skill, PressSkill, SuperSkill;
        public int Delay_Skill;
        public static Boolean Start = false;
        Boolean Direction = false;
        public Boolean test = false;
        //public Rectangle Screen = new Rectangle(0, 0, 300, 300);
        public Rectangle Screen = new Rectangle(0, 0, 200, 200);
        public Point CoordResult = new Point(-1, -1);
        public Point CoordResultRed = new Point(-1, -1);
        public Point CoordResultGreen = new Point(-1, -1);
        public Point CoordResultBule = new Point(-1, -1);
        public static Point initial = new Point(-1, -1);
        public Point[] MulitSerchResult= new Point[10] { initial, initial, initial, initial, initial, initial, initial, initial, initial, initial };
        public int ExitCount=0;
        public int intRedFind, intGreenFind;
        public int X = 0,RedX,GreenX;
        public int Y = 0,RedY,GreenY;
        public int ColorYellow=0xFFDD44;
        public int ColorRed = 0xDD0000;
        public int ColorGreen = 0x00DD00;
        public int ColorBule = 0x00CCEE;
        public int Total_RunTime;
        public int Skill_1,Skill_2,Skill_3,Skill_4,Skill_5,Skill_6,Skill_7;
        public byte AttackModeCount;
        public byte AttackTrans = 30;
        public bool AttackMode;
        public string job = "";
        public int DFX, DFY;
        public Rectangle DFS = new Rectangle(0, 0, 185, 125);
        public Point CoordResult1 = new Point(-1, -1);
        public string DefaultAudioPath, AudioPath;
        public string[] Time = { "00", "00", "00" };
        Galaxy.Properties.Settings MySetting = new Galaxy.Properties.Settings();
        WMPLib.WindowsMediaPlayer player1 = new WMPLib.WindowsMediaPlayer();
        //------------------------------------
        public Boolean NowRing = false;
        public int Ring=0;
        System.Media.SoundPlayer player = new System.Media.SoundPlayer();

        //--------------Varibiles-------------
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //--------Develope option------------------
            WatchDog.Enabled = true;
            label5.Visible = true;
            label5.Text = "";
            //----------------------------Background Setting-----------------------
            Form1.CheckForIllegalCrossThreadCalls = false;//Cross Thread
            Thread WorkScript = new Thread(TestFunction);
            WorkScript.IsBackground = true;
            WorkScript.Start();
            //-----------------------------Test Setting----------------------------------------
            
            Skill = 190;
            PressSkill = 190;
            Skill_1= Skill_2= Skill_3= Skill_4= Skill_5= Skill_6= Skill_7 = 1000;
            //---------------------------------------------------------------------------------
            label1.Text = "State:False";
            label2.Text = "X:__ Y:__";
            button1.Text = "AudioPath";
            groupBox1.Text = "Map";
            radioButton1.Text = "深處";
            radioButton2.Text = "下路";
            label4.Text = "";
            label3.Text = "";
            timer1.Interval = 200;
            timer2.Interval = 1000;
            timer3.Interval = 100;
            //Counter
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 1030;
            //Skill_1
            progressBar2.Minimum = 0;
            progressBar3.Minimum = 0;
            progressBar4.Minimum = 0;
            progressBar5.Minimum = 0;
            progressBar6.Minimum = 0;
            progressBar7.Minimum = 0;

            progressBar2.Maximum = 30;
            progressBar3.Maximum = 180;
            progressBar4.Maximum = 180;
            progressBar5.Maximum = 180;
            progressBar6.Maximum = 180;
            progressBar7.Maximum = 150;
            //
            checkBox1.Text = "渾沌";
            checkBox2.Text = "格檔";
            checkBox3.Text = "祈禱";
            checkBox4.Text = "會心";
            checkBox5.Text = "極速";
            checkBox6.Text = "平衡";
            checkBox7.Text = "輪迴";
            checkBox8.Text = "Teleport";
            checkBox9.Text = "Top";

            //------------------------Register Hotkey--------------
            AllHotkey.Hotkey hokey1 = new AllHotkey.Hotkey(this.Handle);
            AllHotkey.Hotkey.Hotkey1 = hokey1.RegisterHotkey(System.Windows.Forms.Keys.F2, AllHotkey.Hotkey.KeyFlags.MOD_None);
            hokey1.OnHotkey += new AllHotkey.HotkeyEventHandler(OnHotkey);

            AllHotkey.Hotkey hotkey2 = new AllHotkey.Hotkey(this.Handle);
            AllHotkey.Hotkey.Hotkey2 = hokey1.RegisterHotkey(System.Windows.Forms.Keys.F6, AllHotkey.Hotkey.KeyFlags.MOD_CONTROL);
            hotkey2.OnHotkey += new AllHotkey.HotkeyEventHandler(OnHotkey);

            AllHotkey.Hotkey hotkey3 = new AllHotkey.Hotkey(this.Handle);
            AllHotkey.Hotkey.Hotkey3 = hokey1.RegisterHotkey(System.Windows.Forms.Keys.F12, AllHotkey.Hotkey.KeyFlags.MOD_CONTROL);
            hotkey3.OnHotkey += new AllHotkey.HotkeyEventHandler(OnHotkey);

            AllHotkey.Hotkey hotkey4 = new AllHotkey.Hotkey(this.Handle);
            AllHotkey.Hotkey.Hotkey4 = hokey1.RegisterHotkey(System.Windows.Forms.Keys.F7, AllHotkey.Hotkey.KeyFlags.MOD_None);
            hotkey4.OnHotkey += new AllHotkey.HotkeyEventHandler(OnHotkey);

            AllHotkey.Hotkey hotkey5 = new AllHotkey.Hotkey(this.Handle);
            AllHotkey.Hotkey.Hotkey5 = hokey1.RegisterHotkey(System.Windows.Forms.Keys.F8, AllHotkey.Hotkey.KeyFlags.MOD_None);
            hotkey5.OnHotkey += new AllHotkey.HotkeyEventHandler(OnHotkey);

            AllHotkey.Hotkey hotkey6 = new AllHotkey.Hotkey(this.Handle);
            AllHotkey.Hotkey.Hotkey6 = hokey1.RegisterHotkey(System.Windows.Forms.Keys.F2, AllHotkey.Hotkey.KeyFlags.MOD_CONTROL);
            hotkey6.OnHotkey += new AllHotkey.HotkeyEventHandler(OnHotkey);


            //------------------------Register Hotkey---------------------------------------

            //------------Read Setting----------------------
            Boolean Evisble = false;
            job = MySetting.job;
            this.Text = job;
            this.Size = new Size(1440, 120);

            radioButton1.Checked = MySetting.radiobutton1;
            radioButton2.Checked = MySetting.radiobutton2;
            checkBox8.Checked = MySetting.Teleport;
            //Audio Setting
            AudioPath = MySetting.SettingAudioPath;
            if (AudioPath != null)
            {
                if (!File.Exists(AudioPath))
                {
                    AudioPath = null;
                }
            }
            //------------Read Setting----------------------
            label3.Text = "00-00-00";
        }
        public void OnHotkey(int HotkeyID)
        {
            if (HotkeyID == Hotkey.Hotkey1)
            {
                SystemSounds.Beep.Play();
                Start = !Start;
                if (Start == true)
                {
                    timer2.Enabled = true;
                    timer1.Enabled = true;
                }
                else
                {
                    timer1.Enabled = false;
                }
            }

            if (HotkeyID == Hotkey.Hotkey2)
            {
                SystemSounds.Beep.Play();
                Application.Restart();   
            }

            if(HotkeyID == Hotkey.Hotkey3)
            {
                SystemSounds.Beep.Play();
                this.Close();
                Application.Exit();
            }

            if(HotkeyID==Hotkey.Hotkey4)
            {
                if (!NowRing)
                {
                    SystemSounds.Beep.Play();

                    if (!RingCounter.Enabled)
                    {
                        progressBar1.Value = 0;
                        for (int i=1;i<progressBar1.Maximum;i++)
                        {
                            progressBar1.Value++;
                        }
                    }

                    RingCounter.Enabled = !RingCounter.Enabled;
                }

                if ((!RingCounter.Enabled) && NowRing)
                {
                    player1.controls.stop();
                    RingCounter.Enabled = false;
                    timer3.Enabled = false;
                    NowRing = false;
                    Ring = 0;
                }
            }

            if(HotkeyID==Hotkey.Hotkey5)
            {
                SystemSounds.Beep.Play();
                RingCounter.Enabled = false;
                timer3.Enabled = false;
                NowRing = false;
                Ring = 0;
                label4.Text = "Counter:" + Ring;
                progressBar1.Value = Ring;
            }

            if(HotkeyID==Hotkey.Hotkey6)
            {
                for(byte x=0;x<7;x++)
                {
                    sendInput.Mouse.MouseLeftClick();
                    Thread.Sleep(10);
                }
                    
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            Start = !Start;
            if(Start==true)
            {
                this.WindowState = FormWindowState.Minimized;
                timer2.Enabled = true;
                timer1.Enabled = true;
                label1.Text = "State:Start";
            }
            else
            {
                timer1.Enabled = false;
                label1.Text = "State:Pause";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            FocusMainWindow.SetMainWindows.MapleStoryKeepTop();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            
            Total_RunTime++;
            ConvertToStandatdFormat();
            
            Skill_1++;//渾沌共鳴
            Skill_2++;//格檔
            Skill_3++;//祈禱
            Skill_4++;//會心
            Skill_5++;//輪迴
            Skill_6++;//平衡
            Skill_7++;


            if (checkBox1.Checked)
            {
                Skill_1 = 1000;
                checkBox1.Checked = false;
            }
            if (checkBox2.Checked)
            {
                Skill_2 = 1000;
                checkBox2.Checked = false;
            }
            if (checkBox3.Checked)
            {
                Skill_3 = 1000;
                checkBox3.Checked = false;
            }
            if (checkBox4.Checked)
            {
                Skill_4 = 1000;
                checkBox4.Checked = false;
            }
            if (checkBox5.Checked)
            {
                Skill_5 = 1000;
                checkBox5.Checked = false;
            }
            if (checkBox6.Checked)
            {
                Skill_6 = 1000;
                checkBox6.Checked = false;
            }
            if(checkBox7.Checked)
            {
                Skill_7 = 1000;
                checkBox7.Checked = false;
            }
            try
            {
                if(Skill_1>=progressBar2.Maximum)
                {
                    progressBar2.Value = progressBar2.Maximum;
                }
                else
                {
                    progressBar2.Value = Skill_1;
                }

                if (Skill_2 >= progressBar3.Maximum)
                {
                    progressBar3.Value = progressBar3.Maximum;
                }
                else
                {
                    progressBar3.Value = Skill_2;
                }

                if (Skill_3 >= progressBar4.Maximum)
                {
                    progressBar4.Value = progressBar4.Maximum;
                }
                else
                {
                    progressBar4.Value = Skill_3;
                }

                if (Skill_4 >= progressBar5.Maximum)
                {
                    progressBar5.Value = progressBar5.Maximum;
                }
                else
                {
                    progressBar5.Value = Skill_4;
                }

                if (Skill_5 >= progressBar6.Maximum)
                {
                    progressBar6.Value = progressBar6.Maximum;
                }
                else
                {
                    progressBar6.Value = Skill_5;
                }

                if (Skill_6 >= progressBar7.Maximum)
                {
                    progressBar7.Value = progressBar7.Maximum;
                }
                else
                {
                    progressBar7.Value = Skill_6;
                }
            }
            catch
            {

            }
                
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            MySetting.radiobutton1 = radioButton1.Checked;
            MySetting.radiobutton2 = radioButton2.Checked;
            MySetting.Teleport = checkBox8.Checked;

            MySetting.SettingAudioPath = AudioPath;
            MySetting.Save();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (("已停止" == player1.status))
            {
                RingCounter.Enabled = true;
                timer3.Enabled = false;
            }
        }


        private void RingCounter_Tick(object sender, EventArgs e)
        {
            if (Ring >=1030)
            {
                PlayMp3();
                timer3.Enabled = true;
                RingCounter.Enabled = false;
                NowRing = true;
            }
            else
            {
                Ring++;
            }
            label4.Text = "Counter: " + Ring.ToString();
            progressBar1.Value = Ring;
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
            SystemSounds.Beep.Play();
        }

        private void Warming_Tick(object sender, EventArgs e)
        {
            Warming.Enabled = false;
            for (byte i = 0; i < 3; i++)
            {
                SystemSounds.Beep.Play();
                Thread.Sleep(100);
            }
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox9.Checked)
            {
                if (!radioButton1.Checked)
                { checkBox9.Checked = false; }
                checkBox8.Checked = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox8.Checked = false;
            if (radioButton2.Checked)
            {
                checkBox9.Checked = false;
                checkBox9.Enabled = false;
            }
            else if (radioButton1.Checked)
            {
                checkBox9.Enabled = true;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            RE:
            OpenFileDialog file = new OpenFileDialog();
            file.ShowDialog();
            AudioPath = file.FileName;
            try
            {
                Path.GetDirectoryName(AudioPath);
            }
            catch
            {
                string Mess = "音樂尚未設定，請問是否重新設定?";
                DialogResult result;
                result = MessageBox.Show(Mess, "Error", MessageBoxButtons.OKCancel);
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    goto RE;
                }
                else if (result == System.Windows.Forms.DialogResult.Cancel)
                {
                    //MessageBox.Show("Fuck", "Fuck you!!!!", MessageBoxButtons.OK);
                }
            }
        }

        

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            MySetting.initial = true;
            MySetting.Save();
            Application.Restart();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void WatchDog_Tick(object sender, EventArgs e)
        {
            //WatchDog.Enabled = false;
            CoordResult = UFO202.ColorSearch.PixelSearch(Screen, ColorYellow, 1);
            CoordResultRed= UFO202.ColorSearch.PixelSearch(Screen, ColorRed, 1);
            CoordResultGreen= UFO202.ColorSearch.PixelSearch(Screen, ColorGreen, 1);
            X = Convert.ToInt32(CoordResult.X.ToString());
            Y = Convert.ToInt32(CoordResult.Y.ToString());

            if(Convert.ToInt16(CoordResultRed.X.ToString())>0 && Convert.ToInt16(CoordResultRed.Y.ToString())>0)//RED
            {
                intRedFind++;
                if(intRedFind>=10)
                {
                    if(Start && intRedFind>=10)
                    {
                        Warming.Enabled = true;
                    }
                    intRedFind = 0;
                }
            }

            if (Convert.ToInt16(CoordResultGreen.X.ToString()) > 0 && Convert.ToInt16(CoordResultGreen.Y.ToString()) > 0 && !radioButton2.Checked)//Green
            {
                intGreenFind++;
                if (intGreenFind >=5)
                {
                    if (Start && intGreenFind >=5)
                    {
                        Warming.Enabled = true;
                    }
                    intGreenFind = 0;
                    Start = false;
                }
            }
            label1.Text = "State:" + Start.ToString();
            label5.Text = CoordResult.ToString() + "\n" + CoordResultRed.ToString() + "\n" + CoordResultGreen.ToString();
            //WatchDog.Enabled = true;
        }

        private void PlayMp3()
        {
            if (AudioPath != null)
            {
                player1.URL = @"" + Path.GetFullPath(AudioPath);
            }
            else
            {

                DefaultAudioPath = System.Environment.CurrentDirectory;
                DefaultAudioPath += "/fireflower.mp3";
                player1.URL = @"" + DefaultAudioPath;
            }
            player1.controls.play();
        }
        private void WorkOn()
        {
            #region Direction Judge      
            label2.Text = "X:" + X + " " + "Y:" + Y;
            if (radioButton1.Checked && !checkBox8.Checked && !checkBox9.Checked)
            {
                if (X <= 60)
                {
                    Direction = true;
                }
                else if (X >= 130)
                {
                    Direction = false;
                }
            }
            else if (radioButton1.Checked && checkBox8.Checked && !checkBox9.Checked)
            {
                if (X <= 65)
                {
                    Direction = true;
                }
                else if (X >= 130)
                {
                    Direction = false;
                }
            }
            else if (radioButton2.Checked && !checkBox8.Checked && !checkBox9.Checked)
            {
                if (X <= 70)
                {
                    Direction = true;
                }
                else if (X >= 85)
                {
                    Direction = false;
                }
            }
            else if (radioButton1.Checked && checkBox9.Checked)
            {   
                
                if (X <= 65)
                {
                    Direction = true;
                }
                else if (X >= 120)
                {
                    Direction = false;
                }
                
            }
            #endregion

            #region Action
            SupportSkill();
            
            if (radioButton1.Checked)
            {
                #region Attack_Mode_Map
                if (!checkBox8.Checked && !checkBox9.Checked)
                {
                    #region Jump_BlackHug
                    if (!Direction)//false
                    {
                        MapleStory.MapleStoryActions.Left_Down();
                        MapleStory.MapleStoryActions.X_Down();
                        MapleStory.MapleStoryActions.Delay(200);
                        MapleStory.MapleStoryActions.X_Up();
                        MapleStory.MapleStoryActions.Left_Up();
                    }
                    else
                    {
                        MapleStory.MapleStoryActions.Right_Down();
                        MapleStory.MapleStoryActions.X_Down();
                        MapleStory.MapleStoryActions.Delay(200);
                        MapleStory.MapleStoryActions.X_Up();
                        MapleStory.MapleStoryActions.Right_Up();
                    }
                    #endregion
                }
                else if(checkBox8.Checked && !checkBox9.Checked)
                {
                    #region Teleport_Jump_BlackHug
                    if (!Direction)//false
                    {
                        MapleStory.MapleStoryActions.Left_Down();
                        MapleStory.MapleStoryActions.V_Down();
                        MapleStory.MapleStoryActions.Delay(50);
                        MapleStory.MapleStoryActions.V_Up();
                        MapleStory.MapleStoryActions.Delay(250);
                        MapleStory.MapleStoryActions.X_Down();
                        MapleStory.MapleStoryActions.Delay(250);
                        MapleStory.MapleStoryActions.X_Up();
                        MapleStory.MapleStoryActions.Left_Up();
                    }
                    else
                    {
                        MapleStory.MapleStoryActions.Right_Down();
                        MapleStory.MapleStoryActions.V_Down();
                        MapleStory.MapleStoryActions.Delay(50);
                        MapleStory.MapleStoryActions.V_Up();
                        MapleStory.MapleStoryActions.Delay(250);
                        MapleStory.MapleStoryActions.X_Down();
                        MapleStory.MapleStoryActions.Delay(250);
                        MapleStory.MapleStoryActions.X_Up();
                        MapleStory.MapleStoryActions.Right_Up();
                    }
                    #endregion
                }
                else if(!checkBox8.Checked && checkBox9.Checked)
                {
                    #region Top_Walk_BlackHug
                    if (!Direction)//false
                    {
                        MapleStory.MapleStoryActions.Left_Down();
                        Thread.Sleep(700);
                        MapleStory.MapleStoryActions.Left_Up();
                    }
                    else
                    {
                        MapleStory.MapleStoryActions.Right_Down();
                        Thread.Sleep(700);
                        MapleStory.MapleStoryActions.Right_Up();
                    }
                    #endregion
                }
                #endregion

                MapleStory.MapleStoryActions.BlackHug();
                MainAttackPress();
            }
            else if(radioButton2.Checked)
            {
                #region 下路
                if (!Direction)//false
                {
                    MapleStory.MapleStoryActions.Left_Down();
                    Thread.Sleep(200);
                    MapleStory.MapleStoryActions.Left_Up();
                }
                else
                {
                    MapleStory.MapleStoryActions.Right_Down();
                    Thread.Sleep(200);
                    MapleStory.MapleStoryActions.Right_Up();
                }
                MapleStory.MapleStoryActions.BlackHug();

                if (AttackMode)
                {
                    LightMagic();
                }
                else
                {
                    BlackMagic();
                }
                #endregion
            }
#endregion
        }

        public void SupportSkill()
        {
            //OEM1 ;
            //OEM_period ,
            //OEM2_COMMA .
            Thread.Sleep(200);
            if(Skill_1>=30)
            {
                MapleStory.MapleStoryActions.G_Down();
                MapleStory.MapleStoryActions.Delay(50);
                MapleStory.MapleStoryActions.G_Up();
                Skill_1 = 0;
                MapleStory.MapleStoryActions.Delay(1400);
                
            }
           if(Skill_2>=180)
            {
                sendInput.Keyboard.KeyPress(sendInput.win32.ScanCodeShort.OEM_1);
                Skill_2 = 0;
                MapleStory.MapleStoryActions.Delay(2000);

            }
            if(Skill_3>=180)
            {
                sendInput.Keyboard.KeyPress(sendInput.win32.ScanCodeShort.KEY_L);
                Skill_3 = 0;
                MapleStory.MapleStoryActions.Delay(2000);
            }
            if(Skill_4>=180)
            {
                sendInput.Keyboard.KeyPress(sendInput.win32.ScanCodeShort.OEM_PERIOD);
                Skill_4 = 0;
                MapleStory.MapleStoryActions.Delay(2000);
            }
           if(Skill_5>=180)
            {
                sendInput.Keyboard.KeyPress(sendInput.win32.ScanCodeShort.OEM_COMMA);
                Skill_5 = 0;
                MapleStory.MapleStoryActions.Delay(2000);
            }
           if(Skill_6>=150)
            {
                MapleStory.MapleStoryActions.End_Down();
                MapleStory.MapleStoryActions.Delay(50);
                MapleStory.MapleStoryActions.End_Up();
                Skill_6 = 0;
                MapleStory.MapleStoryActions.Delay(1500);
                sendInput.Keyboard.KeyPress(sendInput.win32.ScanCodeShort.KEY_T);
                Thread.Sleep(1500);
            }
             if(Skill_7>=180)
            {
                sendInput.Keyboard.KeyPress(sendInput.win32.ScanCodeShort.KEY_J);
                Thread.Sleep(500);
                Skill_7 = 0;
            }
            
        }
        //******************************************************************************************
        public void MainAttackPress()
        {
            for (byte a = 0; a < 2; a++)
            {
                MapleStory.MapleStoryActions.Space_Down();
                MapleStory.MapleStoryActions.Delay(50);
                MapleStory.MapleStoryActions.Space_Up();
                Thread.Sleep(1100);
            }            
        }
        public void BlackMagic()
        {
            for (byte a = 0; a < 2; a++)
            {
                MapleStory.MapleStoryActions.Space_Down();
                MapleStory.MapleStoryActions.Delay(50);
                MapleStory.MapleStoryActions.Space_Up();
                Thread.Sleep(1100);
            }
            AttackModeCount++;
            if (AttackModeCount >= AttackTrans)
            {
                AttackMode = !AttackMode;
                AttackModeCount=0;
                OpenDoor();
            }
        }
        public void LightMagic()
        {
            for (byte a = 0; a < 2; a++)
            {
                MapleStory.MapleStoryActions.C_Down();
                MapleStory.MapleStoryActions.Delay(50);
                MapleStory.MapleStoryActions.C_Up();
                Thread.Sleep(800);
            }

            AttackModeCount++;
            if(AttackModeCount>=AttackTrans)
            {
                AttackModeCount = 0;
                AttackMode = !AttackMode;
                OpenDoor();
            }
        }

        public void OpenDoor()
        {
            sendInput.Keyboard.KeyPress(sendInput.win32.ScanCodeShort.KEY_T);
            Thread.Sleep(1500);
        }
        public void ConvertToStandatdFormat()
        {
            if (Total_RunTime / 3600 < 10)
            {
                Time[0] = "0" + (Total_RunTime / 3600).ToString();
            }
            else
            {
                Time[0] = (Total_RunTime / 3600).ToString();
            }

            if (((Total_RunTime % 3600) / 60) < 10)
            {

                Time[1] = "0" + ((Total_RunTime % 3600) / 60).ToString();
            }
            else
            {
                Time[1] = ((Total_RunTime % 3600) / 60).ToString(); ;
            }

            if ((Total_RunTime % 3600) % 60 < 10)
            {
                Time[2] = "0" + ((Total_RunTime % 3600) % 60).ToString(); ;
            }
            else
            {
                Time[2] = ((Total_RunTime % 3600) % 60).ToString(); ;
            }
            label3.Text = Time[0] + "-" + Time[1] + "-" + Time[2];
        }

        public void JobMode()
        {
            if(job== "Luminous")
            {
                this.Size = new Size(1440, 120);
            }
            else
            {
                MessageBox.Show("Mode?");
            }

        }

      
         public void TestFunction()
        {
        while(true)
            {
                while (Start)
                {
                        WorkOn();
                }
            }
        }

    }
}