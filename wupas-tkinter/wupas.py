from wasmtime import Func, FuncType, ValType
from tkinter import Tk, ttk

def define_all(store):
    return [
        Func(store, FuncType([], [ValType.i32()]), def_window, True),
        Func(store, FuncType([ValType.i32()], [ValType.i32()]), add_button, True),
        Func(store, FuncType([ValType.i32(), ValType.i32(), ValType.i32()], []), set_text, True),
        Func(store, FuncType([ValType.i32(), ValType.i32(), ValType.i32(), ValType.i32(), ValType.i32()], []), set_location, True),
        Func(store, FuncType([ValType.i32()], []), run, True),
    ]

handles = []

def append_handle(h):
    global handles
    handles.append(h)
    return len(handles) - 1

def def_window(caller):
    window = Tk()
    handle = append_handle(window)
    return handle

def add_button(caller, hparent):
    parent = handles[hparent]
    button = ttk.Button(parent)
    return append_handle(button)

def set_text(caller, hctrl, ptr, len):
    mem = caller.get("memory")
    text = read_mem_string(mem, ptr, len)
    ctrl = handles[hctrl]
    ctrl.configure(text=text)

def set_location(caller, hctrl, x, y, w, h):
    ctrl = handles[hctrl]
    ctrl.place(x=x, y=y, width=w, height=h)

def run(caller, hwindow):
    window = handles[hwindow]
    window.mainloop()

def read_mem_string(mem, ptr, len):
    b = []
    for p in range(ptr, ptr+len):
        b.append(mem.data_ptr[p])
    return bytes(b) # b''.join(b).decode('utf-8')
