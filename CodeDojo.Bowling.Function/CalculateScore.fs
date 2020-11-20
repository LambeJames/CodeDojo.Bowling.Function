namespace BowlingCalculator

module BowlingCalculator =

    open Microsoft.AspNetCore.Http
    open System.IO
    open Newtonsoft.Json
    open Microsoft.Extensions.Logging
    open Microsoft.Azure.WebJobs

    let calcScore pins =

        let rec calcScore pins frame =

            match pins with

            // Strike with determined bonus
            | 10 :: y :: z :: rest -> 10 + y + z + calcScore (y :: z :: rest) (frame + 1)

            // Strike -without- determined bonus
            | 10 :: y :: [] -> 0

            // Spare with determined bonus
            | x :: y :: z :: rest when x + y = 10 -> 10 + z + calcScore (z :: rest) (frame + 1)

            // Spare -without- determined bonus
            | x :: y :: [] when x + y = 10 -> 0

            // Open frame
            | x :: y :: rest -> x + y + calcScore (rest) (frame + 1)

            // Special last frame
            | x :: y :: z :: [] when frame = 10 -> x + y + z

            // Otherwise
            | _ -> 0

        calcScore pins 1

    [<FunctionName("CalculateScore")>]
    let Run ([<HttpTrigger(Methods=[|"POST"|])>] req:HttpRequest) (log:ILogger) =
        async {
            "Runnning Function"
            |> log.LogInformation

            let! body = 
                new StreamReader(req.Body) 
                |> (fun stream -> stream.ReadToEndAsync()) 
                |> Async.AwaitTask

            let rolls = JsonConvert.DeserializeObject<int list>(body)
            let totalScore = calcScore rolls

            return Ok(totalScore)
        } |> Async.StartAsTask 

