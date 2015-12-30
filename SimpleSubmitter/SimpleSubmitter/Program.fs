open Suave
open Suave.Types
open Suave.Http
open Suave.Http.Applicatives
open Suave.Http.Successful
open Suave.Web

//Two pieces:
//1) someone submits a list which kicks off the contribution machinery
//2) A pull request is merged which kicks off the "pull down master file, create a zip file, and push it back to the repo"

let SleepFoo millis message : WebPart =
    fun (x : HttpContext) -> async {
        do! Async.Sleep millis
        return! OK message x
    }

//If there's nothing in the collection, do nothing
//Get the master list into a set of some kind
//Add the new collection to the set
//Sort the set / emit the set into some kind of sequence, and sort it
//
let SubmitCollection collection


let app =
  choose
    [ GET >>= choose
        [ path "/hello" >>= OK "Hello GET" ]
      POST >>= choose
        [ path "/hello" >>= OK "Hello POST"
          path "/submit" >>= OK "Good bye POST" ] ]

startWebServer defaultConfig app