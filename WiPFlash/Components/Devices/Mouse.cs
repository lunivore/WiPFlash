using System;
using System.Runtime.InteropServices;
using System.Windows.Automation;

namespace WiPFlash.Components.Devices
{
    public class Mouse
    {
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern uint SendInput(uint numberOfInputs, Input[] inputs, int sizeOfInputStructure);

        private const int RightDown = 0x0008;
        private const int RightUp = 0x0010;
        private const int InputMouse = 0;

        public void RightClick<T>(T element) where T: AutomationElementWrapper
        {

            var point = element.Element.GetClickablePoint();
            var processId = element.Element.GetCurrentPropertyValue(AutomationElement.ProcessIdProperty);
            var window = AutomationElement.RootElement.FindFirst(
                TreeScope.Children,
                new PropertyCondition(AutomationElement.ProcessIdProperty,
                                      processId));

            window.SetFocus();

             var x = (int)point.X;
             var y = (int)point.Y;

            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(x, y);

            SendInput(2, new[] {InputFor(RightDown, x, y), InputFor(RightUp, x, y)}, Marshal.SizeOf(typeof (Input)));
        }


        private static Input InputFor(uint mouseButtonAction, int x, int y)
        {
            var mouseInput = new MouseInput
                            {
                                Dx = x,
                                Dy = y,
                                MouseData = 0,
                                DwFlags = mouseButtonAction,
                                Time = 0,
                                MouseExtraInfo = new IntPtr()
                            };

            var input = new Input()
                            {
                                u = new InputUnion { mi = mouseInput }
                            };
            return input;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct MouseInput
        {
            public int Dx;
            public int Dy;
            public uint MouseData;
            public uint DwFlags;
            public uint Time;
            public IntPtr MouseExtraInfo;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct InputUnion {
            [FieldOffset(0)] internal MouseInput mi;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct Input {
            int Type;
            internal InputUnion u;
        }
    }
}