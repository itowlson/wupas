from wasmtime import Store, Module, Instance
from wupas import define_all

store = Store()
module = Module.from_file(store.engine, '../examples/simple-window.wat')
instance = Instance(store, module, define_all(store))

main = instance.exports["main"]
main()
