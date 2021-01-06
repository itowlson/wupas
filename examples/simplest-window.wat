(module
    (memory 1)
    (export "memory" (memory 0))
    (data (i32.const 50) "hello from wasm")
    (func $def_window (import "wupas" "def_window") (result i32))
    (func $add_button (import "wupas" "add_button") (param i32) (result i32))
    (func $set_text (import "wupas" "set_text") (param i32 i32 i32))
    (func $set_location (import "wupas" "set_location") (param i32 i32 i32 i32 i32))
    (func $run (import "wupas" "run") (param i32))
    (func (export "main") (local $w i32) (local $b i32)
        call $def_window
        call $run
    )
)
