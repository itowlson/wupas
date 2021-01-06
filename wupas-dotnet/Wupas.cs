using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Wasmtime;

namespace wupas_dotnet {
    public static class Wupas {
        public static void DefineAll(Host host) {
            host.DefineFunction("wupas", "def_window", (Caller caller) => DefWindow(caller));
            host.DefineFunction("wupas", "add_button", (Caller caller, int parent) => AddButton(caller, parent));
            host.DefineFunction("wupas", "set_text", (Caller caller, int ctrl, int ptr, int len) => SetText(caller, ctrl, ptr, len));
            host.DefineFunction("wupas", "set_location", (Caller caller, int ctrl, int x, int y, int w, int h) => SetLocation(caller, ctrl, x, y, w, h));
            host.DefineFunction("wupas", "add_child", (Caller caller, int parent, int child) => AddChild(caller, parent, child));
            host.DefineFunction("wupas", "run", (Caller caller, int window) => Run(caller, window));
        }

        private static List<object> _handles = new List<object>();

        public static int DefWindow(Caller caller) {
            var form = new Form();
            _handles.Add(form);
            return _handles.Count - 1;
        }

        // This model looks like it won't work in Tk where the button needs
        // to know its parent at creation time
        public static int AddButton(Caller caller, int parent) {
            var parentCtrl = (Control)(_handles[parent]);
            var button = new Button();
            parentCtrl.Controls.Add(button);
            _handles.Add(button);
            return _handles.Count - 1;
        }

        public static void SetText(Caller caller, int ctrl, int ptr, int len) {
            var text = caller.GetMemory("memory").ReadString(ptr, len);
            var control = (Control)(_handles[ctrl]);
            control.Text = text;
        }

        public static void SetLocation(Caller caller, int ctrl, int x, int y, int w, int h) {
            var control = (Control)(_handles[ctrl]);
            control.Location = new Point(x, y);
            control.Size = new Size(w, h);
        }

        public static void AddChild(Caller caller, int parent, int child) {
            var parentCtrl = (Control)(_handles[parent]);
            var childCtrl = (Control)(_handles[child]);
            parentCtrl.Controls.Add(childCtrl);
        }

        public static void Run(Caller caller, int window) {
            var form = (Form)(_handles[window]);
            Application.Run(form);
        }
    }
}
