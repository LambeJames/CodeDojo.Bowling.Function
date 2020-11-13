namespace BowlingCalculator

module BowlingCalculator =

    let rec calculateScore rolls totalScore frame =
        match rolls with
        // Frame 10 strike
        | 10 :: second :: third :: tail when frame = 10 -> totalScore + 10 + second + third
        // Frame 10 spare
        | first :: second :: third :: tail when first + second = 10 && frame = 10 -> totalScore + 10 + third
        // Strike
        | 10 :: second :: third :: tail -> calculateScore (second :: third :: tail) (totalScore + 10 + second + third) (frame + 1)
        // Spare
        | first :: second :: third :: tail when first + second = 10 -> calculateScore (third :: tail) (totalScore + 10 + third) (frame + 1)
        // None spare strike
        | first :: second :: tail -> calculateScore tail (totalScore + first + second) (frame + 1)
        //Empty array
        | [] -> totalScore

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

