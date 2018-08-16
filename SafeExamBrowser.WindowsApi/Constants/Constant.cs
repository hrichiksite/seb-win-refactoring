﻿/*
 * Copyright (c) 2018 ETH Zürich, Educational Development and Technology (LET)
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

namespace SafeExamBrowser.WindowsApi.Constants
{
	internal static class Constant
	{
		/// <summary>
		/// A window has received mouse capture. This event is sent by the system, never by servers.
		/// 
		/// See https://msdn.microsoft.com/en-us/library/windows/desktop/dd318066(v=vs.85).aspx.
		/// </summary>
		internal const uint EVENT_SYSTEM_CAPTURESTART = 0x8;

		/// <summary>
		/// The foreground window has changed. The system sends this event even if the foreground window has changed to another window in
		/// the same thread. Server applications never send this event.
		/// For this event, the WinEventProc callback function's hwnd parameter is the handle to the window that is in the foreground, the
		/// idObject parameter is OBJID_WINDOW, and the idChild parameter is CHILDID_SELF.
		/// 
		/// See https://msdn.microsoft.com/en-us/library/windows/desktop/dd318066(v=vs.85).aspx.
		/// </summary>
		internal const uint EVENT_SYSTEM_FOREGROUND = 0x3;

		/// <summary>
		/// Minimize all open windows.
		/// </summary>
		internal const int MIN_ALL = 419;

		/// <summary>
		/// Specifies the default priority class for processes, i.e. a process with no special scheduling needs.
		/// 
		/// See https://msdn.microsoft.com/en-us/library/windows/desktop/ms686219(v=vs.85).aspx.
		/// </summary>
		internal const int NORMAL_PRIORITY_CLASS = 0x20;

		/// <summary>
		/// Standard access rights required for a desktop.
		/// 
		/// See https://docs.microsoft.com/de-de/windows/desktop/SecAuthZ/standard-access-rights.
		/// </summary>
		internal const int STANDARD_RIGHTS_REQUIRED = 0xF0000;

		/// <summary>
		/// The constant for the name of a user object.
		/// 
		/// See https://msdn.microsoft.com/en-us/library/windows/desktop/ms683238(v=vs.85).aspx.
		/// </summary>
		internal const int UOI_NAME = 2;

		/// <summary>
		/// The callback function is not mapped into the address space of the process that generates the event. Because the hook function
		/// is called across process boundaries, the system must queue events. Although this method is asynchronous, events are guaranteed
		/// to be in sequential order.
		/// 
		/// See https://msdn.microsoft.com/en-us/library/windows/desktop/dd373640(v=vs.85).aspx.
		/// </summary>
		internal const uint WINEVENT_OUTOFCONTEXT = 0x0;

		/// <summary>
		/// Sent when the user selects a command item from a menu, when a control sends a notification message to its parent window, or
		/// when an accelerator keystroke is translated.
		/// 
		/// See https://msdn.microsoft.com/en-us/library/windows/desktop/ms647591(v=vs.85).aspx.
		/// </summary>
		internal const int WM_COMMAND = 0x111;

		/// <summary>
		/// Posted to the window with the keyboard focus when a nonsystem key is pressed. A nonsystem key is a key that is pressed when
		/// the ALT key is not pressed.
		/// 
		/// See https://msdn.microsoft.com/en-us/library/windows/desktop/ms646280(v=vs.85).aspx.
		/// </summary>
		internal const int WM_KEYDOWN = 0x100;

		/// <summary>
		/// Posted to the window with the keyboard focus when a nonsystem key is released. A nonsystem key is a key that is pressed when
		/// the ALT key is not pressed, or a keyboard key that is pressed when a window has the keyboard focus.
		/// 
		/// See https://msdn.microsoft.com/en-us/library/windows/desktop/ms646281(v=vs.85).aspx.
		/// </summary>
		internal const int WM_KEYUP = 0x101;

		/// <summary>
		/// Posted when the user presses the left mouse button while the cursor is in the client area of a window. If the mouse is not
		/// captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has
		/// captured the mouse.
		/// 
		/// See https://msdn.microsoft.com/en-us/library/windows/desktop/ms645607(v=vs.85).aspx.
		/// </summary>
		internal const int WM_LBUTTONDOWN = 0x201;

		/// <summary>
		/// Posted when the user releases the left mouse button while the cursor is in the client area of a window. If the mouse is not
		/// captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has
		/// captured the mouse.
		/// 
		/// See https://msdn.microsoft.com/en-us/library/windows/desktop/ms645608(v=vs.85).aspx.
		/// </summary>
		internal const int WM_LBUTTONUP = 0x202;

		/// <summary>
		/// Posted when the user presses the middle mouse button while the cursor is in the client area of a window. If the mouse is not
		/// captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has
		/// captured the mouse.
		/// 
		/// See https://msdn.microsoft.com/en-us/library/windows/desktop/ms645610(v=vs.85).aspx.
		/// </summary>
		internal const int WM_MBUTTONDOWN = 0x207;

		/// <summary>
		/// Posted when the user releases the middle mouse button while the cursor is in the client area of a window. If the mouse is not
		/// captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has
		/// captured the mouse.
		/// 
		/// See https://msdn.microsoft.com/en-us/library/windows/desktop/ms645611(v=vs.85).aspx.
		/// </summary>
		internal const int WM_MBUTTONUP = 0x208;

		/// <summary>
		/// Posted to a window when the cursor moves. If the mouse is not captured, the message is posted to the window that contains the
		/// cursor. Otherwise, the message is posted to the window that has captured the mouse.
		/// 
		/// See https://msdn.microsoft.com/en-us/library/windows/desktop/ms645616(v=vs.85).aspx.
		/// </summary>
		internal const int WM_MOUSEMOVE = 0x200;

		/// <summary>
		/// Sent to the focus window when the mouse wheel is rotated. The DefWindowProc function propagates the message to the window's
		/// parent. There should be no internal forwarding of the message, since DefWindowProc propagates it up the parent chain until i
		/// finds a window that processes it.
		/// 
		/// See https://msdn.microsoft.com/en-us/library/windows/desktop/ms645617(v=vs.85).aspx.
		/// </summary>
		internal const int WM_MOUSEWHEEL = 0x20A;

		/// <summary>
		/// Posted when the user presses the right mouse button while the cursor is in the client area of a window. If the mouse is not
		/// captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has
		/// captured the mouse.
		/// 
		/// See https://msdn.microsoft.com/en-us/library/windows/desktop/ms646242(v=vs.85).aspx.
		/// </summary>
		internal const int WM_RBUTTONDOWN = 0x204;

		/// <summary>
		/// Posted when the user releases the right mouse button while the cursor is in the client area of a window. If the mouse is not
		/// captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has
		/// captured the mouse.
		/// 
		/// See https://msdn.microsoft.com/en-us/library/windows/desktop/ms646243(v=vs.85).aspx.
		/// </summary>
		internal const int WM_RBUTTONUP = 0x205;

		/// <summary>
		/// A window receives this message when the user chooses a command from the Window menu (formerly known as the system or control
		/// menu) or when the user chooses the maximize button, minimize button, restore button, or close button.
		/// 
		/// See https://msdn.microsoft.com/en-us/library/windows/desktop/ms646360(v=vs.85).aspx.
		/// </summary>
		internal const int WM_SYSCOMMAND = 0x112;

		/// <summary>
		/// Posted to the window with the keyboard focus when the user presses the F10 key (which activates the menu bar) or holds down
		/// the ALT key and then presses another key. It also occurs when no window currently has the keyboard focus; in this case, the
		/// WM_SYSKEYDOWN message is sent to the active window. The window that receives the message can distinguish between these two
		/// contexts by checking the context code in the lParam parameter.
		/// 
		/// See https://msdn.microsoft.com/en-us/library/windows/desktop/ms646286(v=vs.85).aspx.
		/// </summary>
		internal const int WM_SYSKEYDOWN = 0x104;

		/// <summary>
		/// Posted to the window with the keyboard focus when the user releases a key that was pressed while the ALT key was held down.
		/// It also occurs when no window currently has the keyboard focus; in this case, the WM_SYSKEYUP message is sent to the active
		/// window. The window that receives the message can distinguish between these two contexts by checking the context code in the
		/// lParam parameter.
		/// 
		/// See https://msdn.microsoft.com/en-us/library/windows/desktop/ms646287(v=vs.85).aspx
		/// </summary>
		internal const int WM_SYSKEYUP = 0x105;
	}
}
