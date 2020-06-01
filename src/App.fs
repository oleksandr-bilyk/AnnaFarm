module App

open Browser.Dom
open Fable.Core.JsInterop
open Fable.Import
open System

// Mutable variable to count the number of times we clicked the button
let mutable count = 0

// Get a reference to our button and cast the Element to an HTMLButtonElement
let myButton = document.querySelector(".my-button") :?> Browser.Types.HTMLButtonElement

let window = Browser.Dom.window

// Get our canvas context 
// As we'll see later, myCanvas is mutable hence the use of the mutable keyword
// the unbox keyword allows to make an unsafe cast. Here we assume that getElementById will return an HTMLCanvasElement 
let mutable myCanvas : Browser.Types.HTMLCanvasElement = unbox window.document.getElementById "myCanvas"  // myCanvas is defined in public/index.html

// Get the context
let ctx = myCanvas.getContext_2d()

// All these are immutables values
let w = myCanvas.width
let h = myCanvas.height
let steps = 20
let squareSize = 20

// gridWidth needs a float wo we cast tour int operation to a float using the float keyword
let gridWidth = float (steps * squareSize) 

// resize our canvas to the size of our grid
// the arrow <- indicates we're mutating a value. It's a special operator in F#.
myCanvas.width <- gridWidth
myCanvas.height <- gridWidth

// print the grid size to our debugger console
printfn "%i" steps

// prepare our canvas operations
for x in [0..steps] do
    let v = float ((x) * squareSize) 
    ctx.moveTo(v, 0.)
    ctx.lineTo(v, gridWidth)
    ctx.moveTo(0., v)
    ctx.lineTo(gridWidth, v)

ctx.strokeStyle <- !^"#ddd" // color

// draw our grid
ctx.stroke() 

let mutable circleX = 100.

let paintCircle () =
    ctx.strokeStyle <- !^"#F22"
    ctx.beginPath();
    ctx.arc(circleX,75.,50., 0., 1.9 * Math.PI);
    ctx.stroke();

paintCircle ()

// write Fable
ctx.textAlign <- "center"
ctx.fillText("Fable on Canvas", gridWidth * 0.5, gridWidth * 0.5)

window.onkeypress <- fun _ ->
    circleX <- circleX + 10.
    paintCircle ()

printfn "done!"

// Register our listener
myButton.onclick <- fun _ ->
    console.log("Hello")
    count <- count + 1
    myButton.innerText <- sprintf "You clicked: %i time(s)" count
