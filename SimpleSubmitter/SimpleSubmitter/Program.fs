open Suave                 // always open suave
open Suave.Http.Successful // for OK-result
open Suave.Web             // for config

//[<EntryPoint>]
startWebServer defaultConfig (OK "Hello World!")