using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;

namespace sendInput
{
    public static class Keyboard
    {
        // KeyDown Events
        public static void KeyDown(win32.ScanCodeShort scancode)
        {
            win32.INPUT[] InputData = new win32.INPUT[1];

            InputData[0].type = win32.INPUTTYPE.KEYBOARD;
            InputData[0].U.ki.wScan = scancode;
            InputData[0].U.ki.dwFlags = win32.KEYEVENTF.KEYDOWN | win32.KEYEVENTF.SCANCODE;
            InputData[0].U.ki.time = 1;
            InputData[0].U.ki.dwExtraInfo = UIntPtr.Zero;

            win32.SendInput(1, InputData, Marshal.SizeOf(typeof(win32.INPUT)));
        }

        public static void KeyUp(win32.ScanCodeShort scancode)
        {
            win32.INPUT[] InputData = new win32.INPUT[1];

            InputData[0].type = win32.INPUTTYPE.KEYBOARD;
            InputData[0].U.ki.wScan = scancode;
            InputData[0].U.ki.dwFlags = win32.KEYEVENTF.KEYUP | win32.KEYEVENTF.SCANCODE;
            InputData[0].U.ki.time = 1;
            InputData[0].U.ki.dwExtraInfo = UIntPtr.Zero;

            win32.SendInput(1, InputData, Marshal.SizeOf(typeof(win32.INPUT)));
        }

        public static void KeyPress(win32.ScanCodeShort scancode)
        {
            KeyDown(scancode);
            Thread.Sleep(10);
            KeyUp(scancode);
        }

        public static void KeyPress(win32.ScanCodeShort scancode, bool Shift, bool Ctrl, bool Alt, bool Win)
        {
            if (Shift)
                KeyDown(win32.ScanCodeShort.SHIFT);
            if (Ctrl)
                KeyDown(win32.ScanCodeShort.CONTROL);
            if (Alt)
                KeyDown(win32.ScanCodeShort.MENU);
            if (Win)
                KeyDown(win32.ScanCodeShort.LWIN);

            KeyDown(scancode);

            KeyUp(scancode);

            if (Shift)
                KeyDown(win32.ScanCodeShort.SHIFT);
            if (Ctrl)
                KeyDown(win32.ScanCodeShort.CONTROL);
            if (Alt)
                KeyDown(win32.ScanCodeShort.MENU);
            if (Win)
                KeyDown(win32.ScanCodeShort.LWIN);

        }
    }
	
	 public static class Mouse
    {
        /// <summary>
        /// Moves the position of the mouse cursor relative to the current position.
        /// </summary>
        /// <param name="x">The amount of pixels the mouse cursor needs to be moved in the horizontal direction.</param>
        /// <param name="y">The amount of pixels the mouse cursor needs to be moved in the vertical direction.</param>
        public static void MouseMoveRelative(int x, int y)
        {
            win32.INPUT[] InputData = new win32.INPUT[1];
            InputData[0].type = win32.INPUTTYPE.MOUSE;
            InputData[0].U.mi.dwFlags = win32.MOUSEEVENTF.MOVE;
            InputData[0].U.mi.dx = x;
            InputData[0].U.mi.dy = y;
            InputData[0].U.mi.time = 1;
            InputData[0].U.mi.dwExtraInfo = UIntPtr.Zero;
            win32.SendInput(1, InputData, Marshal.SizeOf(typeof(win32.INPUT)));
        }

        /// <summary>
        /// Moves the position of the mouse cursor relative to the top left of the screen.
        /// </summary>
        /// <param name="x">The horizontal position of the mouse cursor in pixels.</param>
        /// <param name="y">The vertical position of the mouse cursor in pixels.</param>
        public static void MouseMoveAbsolute(int x, int y)
        {
            win32.SetCursorPos(x, y);
        }
        public static void MouseMove(int x, int y)
        {
            win32.SetCursorPos(x, y);
        }

        public enum MouseButton
        {
            Left,
            Middle,
            Right
        }

        public static void MouseDown(MouseButton Button)
        {
            win32.INPUT[] InputData = new win32.INPUT[1];
            InputData[0].type = win32.INPUTTYPE.MOUSE;

            if (Button == MouseButton.Left)
                InputData[0].U.mi.dwFlags = win32.MOUSEEVENTF.LEFTDOWN;
            if (Button == MouseButton.Middle)
                InputData[0].U.mi.dwFlags = win32.MOUSEEVENTF.MIDDLEDOWN;
            if (Button == MouseButton.Right)
                InputData[0].U.mi.dwFlags = win32.MOUSEEVENTF.RIGHTDOWN;

            InputData[0].U.mi.time = 1;
            InputData[0].U.mi.dwExtraInfo = UIntPtr.Zero;

            win32.SendInput(1, InputData, Marshal.SizeOf(typeof(win32.INPUT)));
        }

        public static void MouseUp(MouseButton Button)
        {
            win32.INPUT[] InputData = new win32.INPUT[1];
            InputData[0].type = win32.INPUTTYPE.MOUSE;

            if (Button == MouseButton.Left)
                InputData[0].U.mi.dwFlags = win32.MOUSEEVENTF.LEFTUP;
            if (Button == MouseButton.Middle)
                InputData[0].U.mi.dwFlags = win32.MOUSEEVENTF.MIDDLEUP;
            if (Button == MouseButton.Right)
                InputData[0].U.mi.dwFlags = win32.MOUSEEVENTF.RIGHTUP;

            InputData[0].U.mi.time = 1;
            InputData[0].U.mi.dwExtraInfo = UIntPtr.Zero;

            win32.SendInput(1, InputData, Marshal.SizeOf(typeof(win32.INPUT)));
        }

        public static void MouseClick(MouseButton Button)
        {
            MouseDown(Button);
            Thread.Sleep(10);
            MouseUp(Button);
        }

        public static void MouseLeftClick()
        {
            MouseClick(MouseButton.Left);
        }

        public static void MouseRightClick()
        {
            MouseClick(MouseButton.Right);
        }

        public static void MouseLeftClick(int x,int y)
        {
            win32.SetCursorPos(x, y);
            MouseLeftClick();
        }
    }
	
}
