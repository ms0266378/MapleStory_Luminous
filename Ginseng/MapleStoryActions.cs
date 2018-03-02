using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MapleStory
{
    public static class MapleStoryActions
    {
        public static void BlackHug()
        {
            sendInput.Keyboard.KeyDown(sendInput.win32.ScanCodeShort.LSHIFT);
            Delay(150);
            sendInput.Keyboard.KeyUp(sendInput.win32.ScanCodeShort.LSHIFT);
            Delay(1000);
        }
        public static void Delay(int DelayTime)
        {
            Thread.Sleep(DelayTime);
        }
        public static void MainAttack(int times, int SkillDelay)
        {
            for (int i = 0; i < times; i++)
            {
                sendInput.Keyboard.KeyDown(sendInput.win32.ScanCodeShort.KEY_C);
                Delay(70);
                sendInput.Keyboard.KeyUp(sendInput.win32.ScanCodeShort.KEY_C);
                Delay(SkillDelay);
            }
            Delay(200);
        }
        //Direction Clear
        public static void Left_Down()
        {
            sendInput.Keyboard.KeyDown(sendInput.win32.ScanCodeShort.LEFT);

        }
        public static void Left_Up()
        {
            sendInput.Keyboard.KeyUp(sendInput.win32.ScanCodeShort.LEFT);
        }

        public static void Right_Down()
        {
            sendInput.Keyboard.KeyDown(sendInput.win32.ScanCodeShort.RIGHT);
        }

        public static void Right_Up()
        {
            sendInput.Keyboard.KeyUp(sendInput.win32.ScanCodeShort.RIGHT);
        }

        public static void Up_Down()
        {
            sendInput.Keyboard.KeyDown(sendInput.win32.ScanCodeShort.UP);
        }
        public static void Up_Up()
        {
            sendInput.Keyboard.KeyUp(sendInput.win32.ScanCodeShort.UP);
        }
        public static void Down_Down()
        {
            sendInput.Keyboard.KeyDown(sendInput.win32.ScanCodeShort.DOWN);
        }
        public static void Down_Up()
        {
            sendInput.Keyboard.KeyUp(sendInput.win32.ScanCodeShort.DOWN);
        }
        public static void DirectionRelease()//Release all direction key
        {
            Left_Up();
            Delay(10);
            Right_Up();
            Delay(10);
        }
        //Direction Clear
        public static void L_Ctrl_Down()
        {
            sendInput.Keyboard.KeyDown(sendInput.win32.ScanCodeShort.LCONTROL);
        }
        public static void L_Ctrl_Up()
        {
            sendInput.Keyboard.KeyUp(sendInput.win32.ScanCodeShort.LCONTROL);
        }
        public static void End_Down()
        {
            sendInput.Keyboard.KeyDown(sendInput.win32.ScanCodeShort.END);
        }
        public static void End_Up()
        {
            sendInput.Keyboard.KeyUp(sendInput.win32.ScanCodeShort.END);
        }

        public static void Del_Down()
        {
            sendInput.Keyboard.KeyDown(sendInput.win32.ScanCodeShort.DELETE);
        }
        public static void Del_Up()
        {
            sendInput.Keyboard.KeyUp(sendInput.win32.ScanCodeShort.DELETE);
        }

        public static void PgDn_Down()
        {
            sendInput.Keyboard.KeyDown(sendInput.win32.ScanCodeShort.NEXT);
        }
        public static void PgDn_Up()
        {
            sendInput.Keyboard.KeyUp(sendInput.win32.ScanCodeShort.NEXT);
        }
        public static void PgUp_Down()
        {
            sendInput.Keyboard.KeyDown(sendInput.win32.ScanCodeShort.PRIOR);
        }
        public static void PgUp_Up()
        {
            sendInput.Keyboard.KeyUp(sendInput.win32.ScanCodeShort.PRIOR);
        }
        public static void Home_Down()
        {
            sendInput.Keyboard.KeyDown(sendInput.win32.ScanCodeShort.HOME);
        }
        public static void Howe_Up()
        {
            sendInput.Keyboard.KeyUp(sendInput.win32.ScanCodeShort.HOME);
        }
        public static void Insert_Down()
        {
            sendInput.Keyboard.KeyDown(sendInput.win32.ScanCodeShort.INSERT);
        }
        public static void Insert_Up()
        {
            sendInput.Keyboard.KeyUp(sendInput.win32.ScanCodeShort.INSERT);
        }
        public static void Space_Down()
        {
            sendInput.Keyboard.KeyDown(sendInput.win32.ScanCodeShort.SPACE);
        }
        public static void Space_Up()
        {
            sendInput.Keyboard.KeyUp(sendInput.win32.ScanCodeShort.SPACE);
        }

        public static void X_Down()
        {
            sendInput.Keyboard.KeyDown(sendInput.win32.ScanCodeShort.KEY_X);
        }

        public static void X_Up()
        {
            sendInput.Keyboard.KeyUp(sendInput.win32.ScanCodeShort.KEY_X);
        }

        public static void C_Down()
        {
            sendInput.Keyboard.KeyDown(sendInput.win32.ScanCodeShort.KEY_C);
        }
        public static void C_Up()
        {
            sendInput.Keyboard.KeyUp(sendInput.win32.ScanCodeShort.KEY_C);
        }
        public static void F_Down()
        {
            sendInput.Keyboard.KeyDown(sendInput.win32.ScanCodeShort.KEY_F);
        }
        public static void F_Up()
        {
            sendInput.Keyboard.KeyUp(sendInput.win32.ScanCodeShort.KEY_F);
        }
        public static void D_Down()
        {
            sendInput.Keyboard.KeyDown(sendInput.win32.ScanCodeShort.KEY_D);
        }
        public static void D_Up()
        {
            sendInput.Keyboard.KeyUp(sendInput.win32.ScanCodeShort.KEY_D);
        }

        public static void G_Down()
        {
            sendInput.Keyboard.KeyDown(sendInput.win32.ScanCodeShort.KEY_G);
        }

        public static void G_Up()
        {
            sendInput.Keyboard.KeyUp(sendInput.win32.ScanCodeShort.KEY_G);
        }
        public static void V_Down()
        {
            sendInput.Keyboard.KeyDown(sendInput.win32.ScanCodeShort.KEY_V);
        }
        public static void V_Up()
        {
            sendInput.Keyboard.KeyUp(sendInput.win32.ScanCodeShort.KEY_V);
        }

        public static void MainAttackPress(int time)
        {
            C_Down();
            Delay(time);
            C_Up();
        }
        public static void Demon_Killer_CommonAttack()
        {
            for(int i=0;i<=4;i++)
            {
                Space_Down();
                Delay(50);
                Space_Up();
            }
        }



    }
}
