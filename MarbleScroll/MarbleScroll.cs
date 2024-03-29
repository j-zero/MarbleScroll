﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarbleScroll
{
    public class MarbleScroll
    {
        public bool FocusWindow { get; set; }
        private IntPtr hookID = IntPtr.Zero;
        private LowLevelMouseProc hookCallback;

		// horizontal scroll
        private const int SENSITIVITY_X = 200;	// mouse move required for one scroll
        private const int DISTANCE_X = 200;		// one scroll distance

		// vertical scroll
        private const int SENSITIVITY_Y = 50;	// mouse move required for one scroll
        private const int DISTANCE_Y = 200;		// one scroll distance
        
		// are we scrolling or just pressing the button?
        private bool isScroll = false;
		// if we are scrolling, intercept back button action
        private bool supressButtonClick = false;
        // when we are simulating middle click
        private bool simulatingClick = false;

        // some coordinates for detecting scroll
        private int startX;
        private int startY;
        private int dx;
        private int dy;

        private const uint backButton = 1;
        private const uint forwardButton = 2;

        private uint scrollXButton = backButton;
        private bool convertXToMiddleClick = false;

        public bool ConvertXToMiddleClick { get; set; }
        public bool EnableMiddleButtonScroll { get; set; }
        public bool EnableXButtonScroll { get; set; }
        public uint ScrollXButton { get { return scrollXButton;  } set { this.scrollXButton = value; } }
        public MarbleScroll()
        {
            // we need this, else gc will collect it
            hookCallback = HookCallback;
        }

        public void Start()
        {
            hookID = SetHook(hookCallback);
        }

        public void Stop()
        {
            UnhookWindowsHookEx(hookID);
        }

        private static IntPtr SetHook(LowLevelMouseProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        protected uint GetXButton(uint hookStruct)
        {
            return (hookStruct & 0xFFFF0000) >> 16; // see https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-msllhookstruct?redirectedfrom=MSDN
        }

        // main code for scrolling
        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            MouseMessages type = (MouseMessages)wParam;
            MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));

            uint xButton = GetXButton(hookStruct.mouseData); //0x01 == Back, 0x02 == Forward
            //isScroll = Control.IsKeyLocked(Keys.CapsLock);

            if (((EnableXButtonScroll && type == MouseMessages.WM_XBUTTONDOWN && xButton == scrollXButton || (EnableMiddleButtonScroll && type == MouseMessages.WM_MBUTTONDOWN) && !simulatingClick) ))
            {

                isScroll = true;
                supressButtonClick = false;
                startX = hookStruct.pt.x;
                startY = hookStruct.pt.y;
                dx = 0;
                dy = 0;

				// for scrolling in window under mouse pointer
                POINT p = new POINT();
                p.x = startX;
                p.y = startY;
                IntPtr focusWindow = WindowFromPoint(p);
                IntPtr foregroundWindow = GetForegroundWindow();
				
				// only focus is window is not already focused
                /*
                if (this.FocusWindow && (GetAncestor(foregroundWindow, 3) != GetAncestor(focusWindow, 3)))
                    SetForegroundWindow(focusWindow);
					*/
                return new IntPtr(1);
            }
            /*
            else if ((type == MouseMessages.WM_XBUTTONDOWN && xButton == 2))
            {
                simulatingClick = true;
                Task.Factory.StartNew(() =>
                {
                    mouse_event((uint)MouseEvents.XDOWN, (uint)hookStruct.pt.x, (uint)hookStruct.pt.y, 1, UIntPtr.Zero);
                });
                return new IntPtr(1);
            }
            else if ((type == MouseMessages.WM_XBUTTONUP && xButton == 2))
            {
                Task.Factory.StartNew(() =>
                {
                    mouse_event((uint)MouseEvents.XUP, (uint)hookStruct.pt.x, (uint)hookStruct.pt.y, 1, UIntPtr.Zero);
                });

                simulatingClick = false;
                return new IntPtr(1);
            }
            */
            else if (((EnableXButtonScroll && type == MouseMessages.WM_XBUTTONUP && xButton == scrollXButton || EnableMiddleButtonScroll && type == MouseMessages.WM_MBUTTONUP)) && !simulatingClick)
            {
                isScroll = false;
                if (supressButtonClick)
                {
                    return new IntPtr(1);
                }
                else
                {
                    if ((EnableXButtonScroll && type == MouseMessages.WM_XBUTTONUP && xButton == scrollXButton && convertXToMiddleClick))
                    {
                        simulatingClick = true;
                        Task.Factory.StartNew(() =>
                        {
                            mouse_event((uint)MouseEvents.MIDDLEDOWN, (uint)hookStruct.pt.x, (uint)hookStruct.pt.y, 0, UIntPtr.Zero);
                            mouse_event((uint)MouseEvents.MIDDLEUP, (uint)hookStruct.pt.x, (uint)hookStruct.pt.y, 0, UIntPtr.Zero);
                        });
                        return new IntPtr(1);
                    }
                    else if (EnableMiddleButtonScroll && type == MouseMessages.WM_MBUTTONUP && !simulatingClick)
                    {
                        simulatingClick = true;
                        Task.Factory.StartNew(() =>
                        {
                            mouse_event((uint)MouseEvents.MIDDLEDOWN, (uint)hookStruct.pt.x, (uint)hookStruct.pt.y, 0, UIntPtr.Zero);
                            mouse_event((uint)MouseEvents.MIDDLEUP, (uint)hookStruct.pt.x, (uint)hookStruct.pt.y, 0, UIntPtr.Zero);
                        });
                        return new IntPtr(1);
                    }
                }
            }
            else if (simulatingClick && (EnableXButtonScroll && type == MouseMessages.WM_XBUTTONUP && xButton == scrollXButton) || EnableMiddleButtonScroll && type == MouseMessages.WM_MBUTTONUP)
            {
                simulatingClick = false;
            }
            else if ((isScroll) && type == MouseMessages.WM_MOUSEMOVE)
            {


                dx += hookStruct.pt.x - startX;
                dy += hookStruct.pt.y - startY;

                // horizontal
                if (Math.Abs(dx) > SENSITIVITY_X)
                {
                    int d = DISTANCE_X;
                    if (dx < 0)
                    {
                        d *= -1;
                        dx += SENSITIVITY_X;
                    }
                    else
                        dx -= SENSITIVITY_X;

                    // reset vertical scroll (because vertical is more sensitive)
                    dy = 0;

                    // scroll me (:
                    // need to run in different thread, as it takes to long to execute mouse_event and windows doesn't like it
                    Task.Factory.StartNew(() =>
                    {
                        mouse_event((uint)MouseEvents.HWHEEL, 0U, 0U, d, UIntPtr.Zero);
                    });
                    supressButtonClick = true;
                }

                // vertical
                if (Math.Abs(dy) > SENSITIVITY_Y)
                {
                    int d = DISTANCE_Y;
                    if (dy > 0)
                    {
                        d *= -1;
                        dy -= SENSITIVITY_Y;
                    }
                    else
                        dy += SENSITIVITY_Y;


#if DEBUG
                    //Debug.WriteLine(d);
#endif
                    // scroll me (:
                    // need to run in different thread, as it takes to long to execute mouse_event and windows doesn't like it
                    Task.Factory.StartNew(() =>
                    {
                        mouse_event((uint)MouseEvents.WHEEL, 0U, 0U, d, UIntPtr.Zero);
                    });
                    supressButtonClick = true;
                }

                return new IntPtr(1);
            }
			
			// nothing for me? pass it to next hook
            return CallNextHookEx(hookID, nCode, wParam, lParam);
        }


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, int dwData, UIntPtr dwExtraInfo);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr WindowFromPoint(POINT Point);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetAncestor(IntPtr hWnd, uint gaFlags);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);
        private const int WH_MOUSE_LL = 14;

        private enum MouseMessages
        {
            WM_LBUTTONDOWN      = 0x0201,
            WM_LBUTTONUP        = 0x0202,
            WM_MOUSEMOVE        = 0x0200,
            WM_MOUSEWHEEL       = 0x020A,
            WM_RBUTTONDOWN      = 0x0204,
            WM_RBUTTONUP        = 0x0205,
            WM_XBUTTONDOWN      = 0x020B,
            WM_XBUTTONUP        = 0x020C,
            WM_XBUTTONDBLCLK    = 0x020D,
            WM_MBUTTONDOWN      = 0x0207,
            WM_MBUTTONUP        = 0x0208
        }

        private enum MouseEvents
        {
            ABSOLUTE    = 0x8000,
            LEFTDOWN    = 0x0002,
            LEFTUP      = 0x0004,
            MIDDLEDOWN  = 0x0020,
            MIDDLEUP    = 0x0040,
            MOVE        = 0x0001,
            RIGHTDOWN   = 0x0008,
            RIGHTUP     = 0x0010,
            XDOWN       = 0x0080,
            XUP         = 0x0100,
            WHEEL       = 0x0800,
            HWHEEL      = 0x1000,
            ABSMOVE     = 0x8001 /// ???
            
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

    }

}
